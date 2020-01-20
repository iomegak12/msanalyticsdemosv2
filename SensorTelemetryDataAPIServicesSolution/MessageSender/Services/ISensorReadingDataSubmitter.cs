using MessageSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageSender.Services
{
    public interface ISensorReadingDataSubmitter
    {
        Task<bool> Submit(EventHubInfo eventHubInfo);
    }
}
