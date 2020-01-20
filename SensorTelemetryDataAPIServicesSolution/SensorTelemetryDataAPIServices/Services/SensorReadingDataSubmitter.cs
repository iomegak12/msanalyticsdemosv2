using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using SensorTelemetryDataAPIServices.Models;

namespace SensorTelemetryDataAPIServices.Services
{
    public class SensorReadingDataSubmitter : ISensorReadingDataSubmitter
    {
        private static Faker<SensorReading> faker = default(Faker<SensorReading>);

        static SensorReadingDataSubmitter()
        {
            var plants = new string[] { "PLANT-1", "PLANT-2", "PLANT-3", "PLANT-4", "PLANT-5" };

            faker = new Faker<SensorReading>()
                .RuleFor(model => model.Region, generator => generator.PickRandom<Regions>().ToString())
                .RuleFor(model => model.Plant, generator => generator.PickRandom(plants))
                .RuleFor(model => model.RecordedTime, generator => generator.Date.Past(2))
                .RuleFor(model => model.DeviceId, generator => generator.Random.Int(1, 20))
                .RuleFor(model => model.Humidity, generator => generator.Random.Int(20, 80))
                .RuleFor(model => model.Temperature, generator => generator.Random.Int(20, 30));
        }

        public Task<bool> Submit(EventHubInfo eventHubInfo)
        {
            var status = default(bool);

            try
            {
                if (eventHubInfo == default(EventHubInfo))
                    throw new ArgumentNullException(nameof(eventHubInfo));

                var sensorReadings = faker.Generate(eventHubInfo.NoOfMessages);
                var ehConnectionStringBuilder = new EventHubsConnectionStringBuilder(eventHubInfo.EventHubConnectionString)
                {
                    EntityPath = eventHubInfo.EventHubPath
                };

                var eventHubClient = EventHubClient.CreateFromConnectionString(ehConnectionStringBuilder.ToString());

                foreach (var reading in sensorReadings)
                {
                    var jsonString = JsonConvert.SerializeObject(reading);
                    var eventDataBytes = Encoding.ASCII.GetBytes(jsonString);
                    var eventData = new EventData(eventDataBytes);

                    eventHubClient.SendAsync(eventData);
                }

                status = true;
            }
            catch (Exception exceptionObject)
            {
                status = false;
            }

            return Task.FromResult(status);
        }
    }
}
