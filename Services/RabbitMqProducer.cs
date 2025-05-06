using RabbitMQ.Client;
using System;
using System.Text;

public class RabbitMqProducer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqProducer()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost", 
            UserName = "guest",     
            Password = "guest"      
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void SendMessage(string message)
    {
        _channel.QueueDeclare(queue: "payment_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "", routingKey: "payment_queue", basicProperties: null, body: body);

        Console.WriteLine($"Mesaj Gönderildi: {message}");
    }

    public void Close()
    {
        _channel.Close();
        _connection.Close();
    }
}
