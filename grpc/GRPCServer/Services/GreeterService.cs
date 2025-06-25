using Grpc.Core;
using GRPCServer;

namespace GRPCServer.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello From Server Mr: " + request.Name
        });
    }

    public override async Task GreetStream(GreetStreamRequest request, IServerStreamWriter<GreetStreamReply> responseStream, ServerCallContext context)
    {
        for (int i = 1; i <= 5; i++)
        {
            await responseStream.WriteAsync(new GreetStreamReply
            {
                Message = $"Hello {request.Name}, message #{i}"
            });
            await Task.Delay(1000); // simulate delay
        }
    }
}
