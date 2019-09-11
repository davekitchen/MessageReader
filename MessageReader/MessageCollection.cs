using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MessageReader
{
    public static class MessageCollection
    {
        [FunctionName("MessageCollection")]
        public static void Run([QueueTrigger("messagedrop", Connection = "AzureWebJobsStorage")]string myQueueItem, ILogger log)
        {
            MessageRecord thisMessage = new MessageRecord(myQueueItem);

            Task arduousTask = thisMessage.SaveRecord();

            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            arduousTask.Wait();
        }
    }
}
