using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SensorTelemetryDataAPIServices.Models;
using SensorTelemetryDataAPIServices.Services;

namespace SensorTelemetryDataAPIServices.Controllers
{
    [Route("api/eh-telemetry-submission")]
    [ApiController]
    public class EventHubTelemetrySubmissionController : ControllerBase
    {
        private const int MAX_LIMIT = 500;
        private ISensorReadingDataSubmitter sensorReadingDataSubmitter = default(ISensorReadingDataSubmitter);

        public EventHubTelemetrySubmissionController(ISensorReadingDataSubmitter sensorReadingDataSubmitter)
        {
            if (sensorReadingDataSubmitter == default(ISensorReadingDataSubmitter))
                throw new ArgumentNullException(nameof(sensorReadingDataSubmitter));

            this.sensorReadingDataSubmitter = sensorReadingDataSubmitter;
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] EventHubInfo eventHubInfo)
        {
            var validation = eventHubInfo != default(EventHubInfo) &&
                !string.IsNullOrEmpty(eventHubInfo.EventHubConnectionString) &&
                !string.IsNullOrEmpty(eventHubInfo.EventHubPath) &&
                eventHubInfo.NoOfMessages <= MAX_LIMIT;

            if (!validation)
                return BadRequest();

            var status = await this.sensorReadingDataSubmitter.Submit(eventHubInfo);

            if (!status)
                return BadRequest();

            return Ok();
        }
    }
}