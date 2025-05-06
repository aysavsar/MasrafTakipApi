using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;  
using RabbitMQ.Client.Exceptions;
using MasrafTakipApi.Interfaces.Service;

namespace MasrafTakipApi.Services
{
    public class RabbitMqClient : IMessageBusClient
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqClient(IConfiguration config)
        {
            // RabbitMQ bağlantı fabrikası
            var factory = new ConnectionFactory
            {
                HostName = config["RabbitMQ:Host"] ?? "localhost",
                Port = int.Parse(config["RabbitMQ:Port"] ?? "5672")
            };

            try
            {
                // RabbitMQ bağlantısını oluşturma
                _connection = factory.CreateConnection();
                
                // Kanal oluşturma
                _channel = _connection.CreateModel();
                
                // Exchange tanımlama (fanout tipinde)
                _channel.ExchangeDeclare("expense_exchange", ExchangeType.Fanout);
            }
            catch (BrokerUnreachableException ex)
            {
                Console.WriteLine($"Cannot connect: {ex.Message}");
                throw;
            }
        }

        // Mesaj yayma
        public void PublishMessage(string exchange, string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: exchange, routingKey: "", basicProperties: null, body: body);
            Console.WriteLine($"Published to {exchange}: {message}");
        }
    }
}
