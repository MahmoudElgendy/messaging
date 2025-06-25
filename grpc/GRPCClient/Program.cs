using Grpc.Core;
using Grpc.Net.Client;
using GRPCServer;

var channel = GrpcChannel.ForAddress("http://localhost:5026");
var client = new Greeter.GreeterClient(channel);

Console.Write("Enter your name: ");
var name = Console.ReadLine();

var reply = await client.SayHelloAsync(new HelloRequest { Name = name });
Console.WriteLine($"Server says: {reply.Message}");

Console.WriteLine("------------------------ ");

Console.Write("Enter your name: ");
var name2 = Console.ReadLine();

using var call = client.GreetStream(new GreetStreamRequest { Name = name2 });

await foreach (var message in call.ResponseStream.ReadAllAsync())
{
    Console.WriteLine($"[Server] {message.Message}");
}
/*-------------------------------*/
Console.WriteLine("------------------------ ");
var client2 = new studenter.studenterClient(channel);

Console.Write("Enter The Id: ");
var id = Console.ReadLine();

var reply2 = await client2.SayHelloAsync(new StudentRequest { Id = id });
Console.WriteLine("Student details is "+ reply2.FirstName +"  "+ reply2.LastName);
Console.ReadLine();
