using RabbitMQ.Client;
using Road.Api.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace Road.Api.Services;
public class MessageProducer : IMessageProducer
{
    public void SendingMessage<T>(T message)
    {
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
        //Sending Data to consumers
        var jsonString = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonString);
        channel.BasicPublish("", "bookings", null, body);
    }
}
