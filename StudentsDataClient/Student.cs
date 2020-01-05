using Microsoft.Azure.Cosmos.Table;

namespace StudentsDataClient
{
    public class Student : TableEntity
    {
        public string Name { get; set; }
        public int? Score { get; set; }
    }
}
