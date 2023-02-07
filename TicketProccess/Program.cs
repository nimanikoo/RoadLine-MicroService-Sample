using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace TicketProccess;
internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Ticketing Service!");
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "root",
            Password = "password",
            VirtualHost = "/",
        };
        var connect = factory.CreateConnection();
        using var channel = connect.CreateModel();
        channel.QueueDeclare("bookings", durable: true, exclusive: false, autoDelete: false, arguments: null);
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, eventArgs) =>
        {
            // getting data from producer on bytes[]
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"New Ticket processing started for - {message}");
        };
        channel.BasicConsume("bookings", true, consumer);
        Console.ReadKey();
    }
}