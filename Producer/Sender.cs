using RabbitMQ.Client;
using System;
using System.Text;

namespace Producer
{
    class Sender
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                const string QUEUEKEY = "BASICQUEUE";
                channel.QueueDeclare(QUEUEKEY, false, false, false, null);

                var message = "Getting Started with .NET Core RabbitMQ";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish("", QUEUEKEY, null, body);
                Console.WriteLine($"Message Sent - {message}");
            }

            Console.WriteLine("Press any key to exist");
            Console.ReadKey();
        }
    }
}
