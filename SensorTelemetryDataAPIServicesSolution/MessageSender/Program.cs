using MessageSender.Models;
using MessageSender.Services;
using System;
using System.Threading.Tasks;

namespace MessageSender
{
    class Program
    {
        async static Task MainEx(string[] args)
        {
            var MAX_LIMIT = 200;
            var eventHubInfo = new EventHubInfo
            {
                EventHubConnectionString = Environment.GetEnvironmentVariable("EHConnectionString"),
                EventHubPath = Environment.GetEnvironmentVariable("EHPath"),
                NoOfMessages = MAX_LIMIT
            };

            var submitter = new SensorReadingDataSubmitter();
            var status = await submitter.Submit(eventHubInfo);

            Console.WriteLine("Submission Status : " + status);
            Console.WriteLine("Event Messages have been submitted successfully!");
        }

        public static void Main(string[] args)
        {
            MainEx(args).Wait();

            Console.WriteLine("End of Application!");
            Console.ReadLine();
        }
    }
}
