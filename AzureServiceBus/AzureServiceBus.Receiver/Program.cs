using Azure.Messaging.ServiceBus;
using System.Collections.Generic;

string connectionString = Environment.GetEnvironmentVariable("AzureServiceBusConnection");
const string queueName = "gendiQU";

// Create client
await using var client = new ServiceBusClient(connectionString);

// ===== SEND MESSAGE =====
ServiceBusReceiver receiver = client.CreateReceiver(queueName);
IReadOnlyList<ServiceBusReceivedMessage> messages = await receiver.ReceiveMessagesAsync(maxMessages: 3);
foreach (ServiceBusReceivedMessage message in messages)
{
    string body = message.Body.ToString();
    Console.WriteLine($"Received: {body}");

    // Complete the message so it's removed from the queue
    await receiver.CompleteMessageAsync(message);
}