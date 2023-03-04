namespace User.Domain
{
    public class UserDomainService
    {

        private readonly IUserRepository _userRepository;
        private readonly ISmsCodeSender _smsCodeSender;

        public UserDomainService(IUserRepository userRepository, ISmsCodeSender smsCodeSender)
        {
            _userRepository = userRepository;
            _smsCodeSender = smsCodeSender;
        }

        public async Task<UserAccessResult> CheckPasswordAsync(PhoneNumber phoneNumber, string password)
        {
            UserAccessResult result;
            var user = await _userRepository.FindOneAsync(phoneNumber);
            if (user is null)
            {
                result = UserAccessResult.PhoneNumberNotFound;
            }
            else if (IsLockout(user))
            {
                result = UserAccessResult.Lockout;
            }
            else if (!user.HasPassword())
            {
                result = UserAccessResult.NoPassword;
            }
            else if (user.CheckPassword(password))
            {
                result = UserAccessResult.Ok;
            }
            else
            {
                result = UserAccessResult.PasswordError;
            }
            if (user is not null)
            {
                if (result == UserAccessResult.Ok)
                {
                    ResetAccessFail(user);
                }
                else
                {
                    AccessFail(user);
                }
            }
            await _userRepository.PublishEventAsync(new UserAccessResultEvent(phoneNumber, result));
            return result;
        }

        public async Task<CheckCodeResult> CheckPhoneNumberCodeAsync(PhoneNumber phoneNumber, string code)
        {
            User? user = await _userRepository.FindOneAsync(phoneNumber);
            if (user is null)
            {
                return CheckCodeResult.PhoneNumberNotFound;
            }
            else if (IsLockout(user))
            {
                return CheckCodeResult.Lockout;
            }
            string? codeInServer = await _userRepository.FindPhoneNumberCodeAsync(phoneNumber);
            if (codeInServer is null)
            {
                AccessFail(user);
                return CheckCodeResult.CodeError;
            }
            if (codeInServer == code)
            {
                await _smsCodeSender.SendCodeAsync(phoneNumber, code);
                return CheckCodeResult.Ok;
            }
            AccessFail(user);
            return CheckCodeResult.CodeError;
        }

        public void ResetAccessFail(User user)
        {
            user.UserAccessFail.Reset();
        }

        public bool IsLockout(User user)
        {
            return user.UserAccessFail.IsLockOut();
        }

        public void AccessFail(User user)
        {
            user.UserAccessFail.Fail();
        }

    }
}
