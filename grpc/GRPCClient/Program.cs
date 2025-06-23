using Grpc.Net.Client;
using GRPCServer;

var channel = GrpcChannel.ForAddress("http://localhost:5026");
var client = new Greeter.GreeterClient(channel);

Console.Write("Enter your name: ");
var name = Console.ReadLine();

var reply = await client.SayHelloAsync(new HelloRequest { Name = name });
Console.WriteLine($"Server says: {reply.Message}");
Console.ReadLine();