using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System;
using System.Threading.Tasks;
using ABC.Models;

public class QueueService
{
    private readonly QueueClient _queueClient;

    public QueueService(string connectionString, string queueName)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentNullException(nameof(connectionString));

        if (string.IsNullOrWhiteSpace(queueName))
            throw new ArgumentNullException(nameof(queueName));

        _queueClient = new QueueClient(connectionString, queueName);
    }

    public async Task SendMessageAsync(string message)
    {
        try
        {
            // Ensure the queue exists or create it if it doesn't
            await _queueClient.CreateIfNotExistsAsync();

            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message cannot be null or empty.", nameof(message));

            await _queueClient.SendMessageAsync(message);
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            Console.WriteLine($"Error sending message: {ex.Message}");
            throw;
        }
    }
}
