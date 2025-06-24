using Grpc.Core;
using GRPCServer;

namespace GRPCServer.Services;

public class StudenterService : studenter.studenterBase
{
    private readonly ILogger<GreeterService> _logger;
    public StudenterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<StudentReply> SayHello(StudentRequest request, ServerCallContext context)
    {
        var students = new List<Student>
        {
           new Student
            {
                Id = "1",
                FirstName = "Mahmoud",
                LastName = "Elgendi"
            },
            new Student
            {
                Id = "2",
                FirstName = "Ahmed",
                LastName = "Ali"
            },
            new Student
            {
                Id = "3",
                FirstName = "Sara",
                LastName = "Mohamed"
            }
        };
        var student = students
            .Where(s => s.Id == request.Id)
            .Select(s => new StudentReply
                  {
                      FirstName = s.FirstName,
                      LastName = s.LastName
                  }).FirstOrDefault();
        return Task.FromResult(student);


    }



}
