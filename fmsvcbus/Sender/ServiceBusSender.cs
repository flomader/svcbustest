using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmsvcbus.Sender
{
    public class ServiceBusSender : IServiceBusSender
    {
        private string _connectionString;
        private MessageSender _messageSender;

        public ServiceBusSender(string connectionString)
        {
            _connectionString = connectionString;
            _messageSender = new MessageSender(new ServiceBusConnectionStringBuilder(_connectionString));
        }

        public async Task<long> SendValueAsync(string value)
        {
            var timer = Stopwatch.StartNew();

            var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)));

            timer.Start();
            await _messageSender.SendAsync(message);
            timer.Stop();

            return timer.ElapsedMilliseconds;
        }
    }
}
