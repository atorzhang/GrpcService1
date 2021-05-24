using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;


namespace GrpcGreeterClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var chanel = GrpcChannel.ForAddress("https://localhost:5001");
            //生成不同服务的客户端
            var client = new Greeter.GreeterClient(chanel);
            var client1 = new Greeter1.Greeter1Client(chanel);

            while (true)
            {
                //Console.WriteLine($"即将调用Greeter.SayHello方法，请输入参数Name:");
                //var name = Console.ReadLine();
                //var response = await client.SayHelloAsync(new HelloRequest { Name = name });
                //Console.WriteLine($"Greeting Response:\r\nMessage:{response.Message}，\r\nStatus:{response.Status}");
                //Console.ReadLine();

                Console.WriteLine($"即将用grpc方式调用Greeter1.SayHello方法，请输入参数Name和Code:");
                var name1 = Console.ReadLine();
                var code1 = Console.ReadLine();
                //计时
                Stopwatch sw = new Stopwatch();
                sw.Start();
                var response1 = await client1.SayHello1Async(new HelloRequest1 { Name = name1,Code = code1 });
                sw.Stop();
                TimeSpan ts2 = sw.Elapsed;
                Console.WriteLine("使用grpc方式Greeter1.SayHello总共花费{0}ms.", ts2.TotalMilliseconds);
                Console.WriteLine($"Greeting1 Response:\r\nMessage:{response1.Message}，\r\nStatus：{response1.Status}");
                Console.ReadLine();

                Console.WriteLine($"即将用webapi方式调用方法");
                WebClient webClient = new WebClient();
                Stopwatch sw1 = new Stopwatch();
                sw1.Start();
                var apiResult = webClient.DownloadString("http://localhost:5100/WeatherForecast");
                sw1.Stop();
                TimeSpan ts1 = sw1.Elapsed;
                Console.WriteLine("使用webapi方式总共花费{0}ms.", ts1.TotalMilliseconds);
                Console.WriteLine($"webapi Response:{apiResult}");


            }
        }
    }
}
