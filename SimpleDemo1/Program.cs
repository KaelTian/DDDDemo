//string[] lines = File.ReadAllLines(@"F:\code\DDDDemo\1.txt");
//foreach (var line in lines)
//{
//    var segments = line.Split('|');
//    var email = segments[0];
//    var name = segments[1];
//    var body = segments[2];
//    Console.WriteLine($"发送邮件:{email},name:{name},body:{body}");
//}
//Console.ReadLine();


using Microsoft.Extensions.DependencyInjection;
using SimpleDemo1.Interf1Abstract;

ServiceCollection  services= new ServiceCollection();
services.AddScoped<MyBizCode1>();
services.AddScoped<IEmailSender,MyEmailSender>();
services.AddScoped<IMyDataProvider, MyDataProvider>();

var provider = services.BuildServiceProvider();
var code=provider.GetRequiredService<MyBizCode1>();

await code.DoAsync();

Console.ReadLine();