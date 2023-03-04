namespace SimpleDemo1.Interf1Abstract
{
    internal class MyDataProvider : IMyDataProvider
    {
        public IEnumerable<EmailInfo> GetEmailsToBeSent()
        {
            string[] lines = File.ReadAllLines(@"F:\code\DDDDemo\1.txt");
            foreach (var line in lines)
            {
                var segments = line.Split('|');
                var email = segments[0];
                var title = segments[1];
                var body = segments[2];
                yield return new EmailInfo(email, title, body);
            }
        }
    }
}
