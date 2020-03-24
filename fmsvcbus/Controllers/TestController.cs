using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fmsvcbus.Sender;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;

namespace fmsvcbus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IEventGridSender _eventGridSender;
        private readonly IServiceBusSender _serviceBusSender;

        public TestController(IEventGridSender eventGridSender, IServiceBusSender serviceBusSender)
        {
            _eventGridSender = eventGridSender;
            _serviceBusSender = serviceBusSender;
        }

        // POST: api/Test
        [HttpPost]
        public async Task<string> PostAsync([FromBody] string value)
        {
            var timeEg = await _eventGridSender.SendValueAsync(value);
            var timeSb = await _serviceBusSender.SendValueAsync(value);

            return $"EventGrid: {timeEg} ms, ServiceBus: {timeSb} ms";
        }
    }
}
