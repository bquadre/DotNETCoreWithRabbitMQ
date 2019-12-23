using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer
{
    class Receiver
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                const string QUEUEKEY = "BASICQUEUE";
                channel.QueueDeclare(QUEUEKEY, false, false, false, null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Message received {message}");
                };

                channel.BasicConsume(QUEUEKEY, true, consumer);
                Console.WriteLine("Press any key to exit");
                Console.ReadLine();
            }
        }
    }
}
