using User.Domain;

namespace User.Infrastructure
{
    public class MockSmsCodeSender : ISmsCodeSender
    {
        public  Task SendCodeAsync(PhoneNumber phoneNumber, string code)
        {
            Console.WriteLine($"向{phoneNumber.RegionNumber}_{phoneNumber.Number}发送验证码:{code}");
            return Task.CompletedTask;
        }
    }
}
