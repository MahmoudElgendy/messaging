using Grpc.Core;
using GRPCServer;

namespace GRPCServer.Services;

public class CalCulatorService : Calculator.CalculatorBase
{
    private readonly ILogger<CalCulatorService> _logger;
    public CalCulatorService(ILogger<CalCulatorService> logger)
    {
        _logger = logger;
    }

    public override async Task<SumResponse> ComputeSum(IAsyncStreamReader<SumRequest> requestStream, ServerCallContext context)
    {
        int total = 0;
        await foreach (var message in requestStream.ReadAllAsync())
        {
            total += message.Number;
        }
        return new SumResponse { Total = total };
    }
}
