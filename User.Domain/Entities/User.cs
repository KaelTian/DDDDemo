using Zack.Commons;

namespace User.Domain
{
    public record User : IAggregateRoot
    {
        public Guid Id { get; init; }
        public PhoneNumber PhoneNumber { get; private set; }

        private string? passwordHash;

        public UserAccessFail UserAccessFail { get; private set; }

        //EF Core 加载数据
        private User() {
            //UserAccessFail = new UserAccessFail(this); 
        }

        public User(PhoneNumber phoneNumber)
        {
            this.Id = Guid.NewGuid();
            PhoneNumber = phoneNumber;
            this.UserAccessFail = new UserAccessFail(this);
        }

        public bool HasPassword()
        {
            return !string.IsNullOrWhiteSpace(passwordHash);
        }

        public void ChangePassword(string value)
        {
            if (value.Length <= 3) throw new ArgumentException("密码长度不能小于3");
            passwordHash = HashHelper.ComputeMd5Hash(value);
        }

        public bool CheckPassword(string password)
        {
            return passwordHash== HashHelper.ComputeMd5Hash(password);
        }

        public void ChangePhoneNumber(PhoneNumber phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}
