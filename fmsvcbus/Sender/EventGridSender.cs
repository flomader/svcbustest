using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace fmsvcbus.Sender
{
    public class EventGridSender : IEventGridSender
    {
        private string _topicEndpoint;
        private string _topicKey;
        private EventGridClient _client;

        public EventGridSender(string topicEndpoint, string topicKey)
        {
            _topicEndpoint = topicEndpoint;
            _topicKey = topicKey;
            _client = new EventGridClient(new TopicCredentials(_topicKey));
        }


        public async Task<long> SendValueAsync(string value)
        {
            var topicHostname = new Uri(_topicEndpoint).Host;

            var events = new List<EventGridEvent>
            {
                new EventGridEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    Data = value,
                    EventTime = DateTime.Now,
                    EventType = "EventGridSender.Value",
                    Subject = "Test",
                    DataVersion = "1.0"
                }
            };

            var timer = Stopwatch.StartNew();
            timer.Start();
            await _client.PublishEventsAsync(topicHostname, events);
            timer.Stop();

            return timer.ElapsedMilliseconds;
        }
    }
}
