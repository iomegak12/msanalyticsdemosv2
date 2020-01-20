using SensorTelemetryDataAPIServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorTelemetryDataAPIServices.Services
{
    public interface ISensorReadingDataSubmitter
    {
        Task<bool> Submit(EventHubInfo eventHubInfo);
    }
}
