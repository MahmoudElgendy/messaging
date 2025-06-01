using Azure.Messaging.ServiceBus;

string connectionString = Environment.GetEnvironmentVariable("AzureServiceBusConnectionString");
const string queueName = "gendiQU";
const string topicName = "genditc";

// ===== Create client =====
await using var client = new ServiceBusClient(connectionString);

// ===== SEND MESSAGE To Queue =====
ServiceBusSender queueSender = client.CreateSender(queueName);
for (int i = 0; i < 3; i++)
{
    var QueueMessage = new ServiceBusMessage($"Hello World {i + 1}!");
    await queueSender.SendMessageAsync(QueueMessage);
    Console.WriteLine($"Message {i + 1} sent.");
}

// ===== SEND MESSAGE TO TOPIC =====
ServiceBusSender topicSender = client.CreateSender(topicName);
ServiceBusMessage topicMessage = new("Hello from Topic2");
await topicSender.SendMessageAsync(topicMessage);
Console.WriteLine("Message sent to topic.");