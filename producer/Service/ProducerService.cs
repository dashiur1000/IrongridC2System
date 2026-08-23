using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace producer.Service
{
    public class ProducerService
    {
        private readonly IProducer<Null, string> _producer;
        public ProducerService()
        {
            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092"
            };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }
        public async Task<DeliveryResult<Null, string>> SendAsync<T>(T message, string name)
        {
            var m = JsonSerializer.Serialize(message);
            var kafkaMessage = new Message<Null, string>
            {
                Value = m
            };
            return await _producer.ProduceAsync(name, kafkaMessage);
        }
        public void Dispose()
        {
            _producer.Flush();
            _producer.Dispose();
        }
    }
}
