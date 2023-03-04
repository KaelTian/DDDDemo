namespace SimpleDemo1.Interf1Abstract
{
    public interface IEmailSender
    {
        Task SendAsync(string email,string title,string body);
    }
}
