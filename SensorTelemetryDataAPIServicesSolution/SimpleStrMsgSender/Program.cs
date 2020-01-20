using Microsoft.Azure.EventHubs;
using System;
using System.Text;

namespace SimpleStrMsgSender
{
    class Program
    {
        static void Main(string[] args)
        {
            var ehConnectionStringBuilder = new EventHubsConnectionStringBuilder(
                connectionString: Environment.GetEnvironmentVariable("EHConnectionString"))
            {
                EntityPath = Environment.GetEnvironmentVariable("EHPath")
            };

            var eventHubClient = EventHubClient.CreateFromConnectionString(ehConnectionStringBuilder.ToString());

            while (true)
            {
                Console.WriteLine("Enter Message to Send to EH ...");

                var message = Console.ReadLine();

                if (message.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;

                var payload = @"
                {
                    ""city"": """", 
                    ""country"": ""United States"", 
                    ""countryCode"": ""US"", 
                    ""isp"": """", 
                    ""lat"": 0.00, ""lon"": 0.00, 
                    ""query"": """", 
                    ""region"": ""CA"", 
                    ""regionName"": ""California"", 
                    ""status"": ""success"", 
                    ""hittime"": ""2017-02-08T17:37:55-05:00"", 
                    ""zip"": ""38917"" 
                }";

                var eventData = new EventData(Encoding.ASCII.GetBytes(payload));

                eventHubClient.SendAsync(eventData).Wait();

                Console.WriteLine("Message Sent Successfully!");
            }

            Console.WriteLine("End of Application!");
            Console.ReadLine();
        }
    }
}
