using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace MessageReader
{
    public class MessageRecord : TableEntity
    {
        public string TheMessage { get; set; }

        public MessageRecord() { }
        public MessageRecord(string theMessage) : base()
        {
            this.PartitionKey = DateTime.Today.ToString("yyyyMMdd");
            this.RowKey = string.Concat(DateTime.Now.ToString("HHmmss"), " : ", Guid.NewGuid());
            TheMessage = theMessage;

        }

        public async Task SaveRecord()
        {
            string storageConnection = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            string tableName = Environment.GetEnvironmentVariable("TableName");

            CloudStorageAccount thisAcc = CloudStorageAccount.Parse(storageConnection);
            CloudTableClient thisClient = thisAcc.CreateCloudTableClient();
            CloudTable thisTable = thisClient.GetTableReference(tableName);

            await thisTable.CreateIfNotExistsAsync();

            TableOperation thisOperation = TableOperation.InsertOrReplace(this);
            await thisTable.ExecuteAsync(thisOperation);
        }
    }
}
