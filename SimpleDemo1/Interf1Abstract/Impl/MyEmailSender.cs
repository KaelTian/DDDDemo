namespace SimpleDemo1.Interf1Abstract
{
    internal class MyEmailSender : IEmailSender
    {
        public async Task SendAsync(string email, string title, string body)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"Send to :{email},title:{title},body:{body}");
            });
        }
    }
}
