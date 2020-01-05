using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace StudentsDataClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = "YOUR_PRIMARY_CONNECTION_STRING_HERE";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("students");
            TableQuery<Student> tableQuery = new TableQuery<Student>();
            
            string filter = TableQuery.CombineFilters(
                TableQuery.GenerateFilterConditionForInt("Score", QueryComparisons.GreaterThan, 150),
                TableOperators.And,
                TableQuery.GenerateFilterCondition("Name", QueryComparisons.Equal, "Hassan"));
            
            tableQuery.Where(filter);

            tableQuery.OrderBy("Name");
            tableQuery.Select(new List<string> { "Score" });
            tableQuery.Take(take: 3);

            TableQuerySegment<Student> studentsSegment = 
                await table.ExecuteQuerySegmentedAsync(tableQuery, token: null);
            
            studentsSegment.Results.ForEach(i => 
                Console.WriteLine("{0,5} {1,10} {2,-10}", i.PartitionKey, i.Name, i.Score));
            
            Console.ReadKey();
        }
    }
}
