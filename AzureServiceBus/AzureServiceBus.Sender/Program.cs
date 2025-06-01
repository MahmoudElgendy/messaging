using Azure.Messaging.ServiceBus;

string connectionString = Environment.GetEnvironmentVariable("AzureServiceBusConnection");
const string queueName = "gendiQU";

// Create client
await using var client = new ServiceBusClient(connectionString);

// ===== SEND MESSAGE =====
ServiceBusSender sender = client.CreateSender(queueName);
for (int i = 0; i < 3; i++)
{
    var message = new ServiceBusMessage($"Hello World {i + 1}!");
    await sender.SendMessageAsync(message);
    Console.WriteLine($"Message {i + 1} sent.");
}
