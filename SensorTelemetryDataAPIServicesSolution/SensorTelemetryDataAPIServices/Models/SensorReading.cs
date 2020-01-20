using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorTelemetryDataAPIServices.Models
{
    public class SensorReading
    {
        public string Region { get; set; }
        public string Plant { get; set; }
        public int DeviceId { get; set; }
        public int Temperature { get; set; }
        public int Humidity { get; set; }
        public DateTime RecordedTime { get; set; }

        public override string ToString()
        {
            return string.Format(@"{0}, {1}, {2}, {3}, {4}, {5}",
                this.Region, this.Plant, this.DeviceId, this.Temperature,
                this.Humidity, this.RecordedTime.ToShortDateString());
        }
    }
}
