using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageSender.Models
{
    public class EventHubInfo
    {
        public string EventHubConnectionString { get; set; }
        public string EventHubPath { get; set; }
        public int NoOfMessages { get; set; }

        public override string ToString()
        {
            return string.Format(@"{0}, {1}, {2}",
                this.EventHubConnectionString, this.EventHubPath, this.NoOfMessages);
        }
    }
}
