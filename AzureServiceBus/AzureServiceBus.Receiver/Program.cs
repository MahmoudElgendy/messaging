using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Amqp.Framing;

string connectionString = Environment.GetEnvironmentVariable("AzureServiceBusConnection");
const string queueName = "gendiQU";
const string topicName = "genditc";
const string subscriptionName = "gendisubscription";

// Create client
await using var client = new ServiceBusClient(connectionString);

// ===== Receive MESSAGE From Queue =====
ServiceBusReceiver queueReceiver = client.CreateReceiver(queueName);
IReadOnlyList<ServiceBusReceivedMessage> queueMessages = await queueReceiver.ReceiveMessagesAsync(maxMessages: 3);
foreach (ServiceBusReceivedMessage message in queueMessages)
{
    string body = message.Body.ToString();
    Console.WriteLine($"Received: {body}");

    // Complete the message so it's removed from the queue
    await queueReceiver.CompleteMessageAsync(message);
}

// ===== Receive MESSAGE From Topic =====
ServiceBusReceiver receiver = client.CreateReceiver(topicName, subscriptionName);
var received = await receiver.ReceiveMessagesAsync(maxMessages: 5, maxWaitTime: TimeSpan.FromSeconds(10));

foreach (var msg in received)
{
    Console.WriteLine($"Received: {msg.Body}");

    // Complete = remove from subscription
    await receiver.CompleteMessageAsync(msg);
}