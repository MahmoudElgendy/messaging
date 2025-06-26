using Grpc.Core;
using GRPCServer;

namespace GRPCServer.Services;
public class Chater :ChatService.ChatServiceBase
{
    public override async Task Chat(IAsyncStreamReader<ChatMessage> requestStream, IServerStreamWriter<ChatMessage> responseStream, ServerCallContext context)
    {
        await foreach (var incoming in requestStream.ReadAllAsync())
        {
            Console.WriteLine($"[Client] {incoming.User}: {incoming.Text}");

            await responseStream.WriteAsync(new ChatMessage
            {
                User = "Server",
                Text = $"Received: {incoming.Text}"
            });
        }
    }

}

