using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

public class RabbitMqConsumer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqConsumer()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",      // RabbitMQ sunucusunun adresi
            UserName = "guest",          // Kullanıcı adı
            Password = "guest"           // Şifre
        };

        // RabbitMQ bağlantısını oluşturuyoruz
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void Consume()
    {
        // Kuyruğu oluşturuyoruz
        _channel.QueueDeclare(queue: "payment_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

        // Mesajları almak için bir Consumer nesnesi oluşturuyoruz
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            // Alınan mesajın baytlarını alıp, string'e çeviriyoruz
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Alınan Mesaj: {message}");
        };

        // Kuyruğu dinlemeye başlıyoruz
        _channel.BasicConsume(queue: "payment_queue", autoAck: true, consumer: consumer);

        Console.WriteLine("Mesajları dinliyor...");
    }

    public void Close()
    {
        // Bağlantıyı ve kanalı kapatıyoruz
        _channel.Close();
        _connection.Close();
    }
}
