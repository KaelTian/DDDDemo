namespace SimpleDemo1.Interf1Abstract
{
    public class MyBizCode1
    {
        private readonly IEmailSender _emailSender;
        private readonly IMyDataProvider _myDataProvider;

        public MyBizCode1(IEmailSender emailSender, IMyDataProvider myDataProvider)
        {
            _emailSender = emailSender;
            _myDataProvider = myDataProvider;
        }

        public async Task DoAsync()
        {
            var items = _myDataProvider.GetEmailsToBeSent();
            foreach (var item in items)
            {
                await _emailSender.SendAsync(
                    email: item.Email,
                    title: item.Title,
                    body: item.Body
                    );
                await Task.Delay(1 * 1000);
            }
        }

    }
}
