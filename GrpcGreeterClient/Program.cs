using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;


namespace GrpcGreeterClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var chanel = GrpcChannel.ForAddress("https://localhost:5001");//使用https
            using var chanelnossl = GrpcChannel.ForAddress("http://localhost:5101");//使用http

            //生成不同服务的客户端
            var client = new Greeter.GreeterClient(chanelnossl);
            var client1 = new Greeter1.Greeter1Client(chanelnossl);

            while (true)
            {
                Console.WriteLine($"即将调用Greeter.SayHello方法，请输入参数Name:");
                var name = Console.ReadLine();
                var response = await client.SayHelloAsync(new HelloRequest { Name = name });
                Console.WriteLine($"Greeting Response:\r\nMessage:{response.Message}，\r\nStatus:{response.Status}");
                Console.ReadLine();

                Console.WriteLine($"即将调用Greeter1.SayHello方法，请输入参数Name和Code:");
                var name1 = Console.ReadLine();
                var code1 = Console.ReadLine();
                var response1 = await client1.SayHello1Async(new HelloRequest1 { Name = name1,Code = code1 });
                Console.WriteLine($"Greeting1 Response:\r\nMessage:{response1.Message}，\r\nStatus：{response1.Status}");
                Console.ReadLine();
            }
        }
    }
}
