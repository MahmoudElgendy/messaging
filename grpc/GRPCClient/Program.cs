using Grpc.Core;
using Grpc.Net.Client;
using GRPCServer;

var channel = GrpcChannel.ForAddress("http://localhost:5026");
//var client = new Greeter.GreeterClient(channel);
//var client2 = new studenter.studenterClient(channel);
//var client3 = new Calculator.CalculatorClient(channel);

//Console.Write("Enter your name: ");
//var name = Console.ReadLine();

//var reply = await client.SayHelloAsync(new HelloRequest { Name = name });

//Console.WriteLine($"Server says: {reply.Message}");

//Console.WriteLine("------------------------ ");

//Console.Write("Enter your name: ");
//var name2 = Console.ReadLine();

//using var call = client.GreetStream(new GreetStreamRequest { Name = name2 });

//await foreach (var message in call.ResponseStream.ReadAllAsync())
//{
//    Console.WriteLine($"[Server] {message.Message}");
//}
///*-------------------------------*/
//Console.WriteLine("------------------------ ");

//Console.Write("Enter The Id: ");
//var id = Console.ReadLine();

//var reply2 = await client2.SayHelloAsync(new StudentRequest { Id = id });
//Console.WriteLine("Student details is " + reply2.FirstName + "  " + reply2.LastName);

//Console.WriteLine("------------------------ ");
//using var call3 = client3.ComputeSum();

//for (int i = 1; i <= 5; i++)
//{
//    Console.WriteLine($"Sending number: {i}");
//    await call3.RequestStream.WriteAsync(new SumRequest { Number = i });
//    await Task.Delay(500);
//}

//await call3.RequestStream.CompleteAsync();

//var response = await call3.ResponseAsync;
//Console.WriteLine($"[Server] Sum = {response.Total}");
//Console.WriteLine("------------------------ ");

var client = new ChatService.ChatServiceClient(channel);

using var call = client.Chat();

var sendingTask = Task.Run(async () =>
{
    while (true)
    {
        var input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input)) break;

        await call.RequestStream.WriteAsync(new ChatMessage
        {
            User = "Client",
            Text = input
        });
    }
    await call.RequestStream.CompleteAsync();
});

var receivingTask = Task.Run(async () =>
{
    await foreach (var msg in call.ResponseStream.ReadAllAsync())
    {
        Console.WriteLine($"[Server] {msg.Text}");
    }
});

await Task.WhenAll(sendingTask, receivingTask);

Console.ReadLine();