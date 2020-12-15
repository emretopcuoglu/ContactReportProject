using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportAPI.Core;
using ReportAPI.Entity;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ReportAPI.Service
{
    public class RabbitService : IRabbitService
    {
        private readonly IReport _reportService;

        public RabbitService(IReport reportService)
        {
            _reportService = reportService;
        }

        public void ReceiveMessage()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
            };

            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            channel.QueueDeclare(queue: "ReportQuee",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            channel.BasicQos(0, 1, true);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, mq) =>
            {
                var body = mq.Body;
                var message = Encoding.UTF8.GetString(body);

                var rabbitList = JsonConvert.DeserializeObject<Rabbit>(message);

                _reportService.Update(rabbitList.ReportId, rabbitList.Location);

                channel.BasicAck(mq.DeliveryTag, false);
            };

            channel.BasicConsume(queue: "ReportQuee",
                                                 autoAck: false,
                                                 consumer: consumer);
        }

        public Task SendMessage(Guid reportId, string location)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "ReportQuee",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);


                Rabbit list = new Rabbit();
                list.Location = location;
                list.ReportId = reportId;
                var jsonList = JsonConvert.SerializeObject(list);

                var body = Encoding.UTF8.GetBytes(jsonList);

                channel.BasicPublish(exchange: "",
                                     routingKey: "ReportQuee",
                                     basicProperties: null,
                                     body: body);

                return Task.CompletedTask;
            }
        }
    }
}
