using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CowdersPictures.Model
{
    public class IssueEntity : TableEntity
    {
        public const string PartitionKeyConst = "issue";

        public IssueEntity() { }
        public IssueEntity(string id)
        {
            Id = id;
            PartitionKey = PartitionKeyConst;
        }
        public string Id
        {
            get
            {
                return RowKey;
            }
            set
            {
                RowKey = value;
            }
        }
        public string CustomerId { get; set; }
        
        public int ProductId { get; set; }
        public string Text { get; set; }
        public string Desctiption { get; set; }
    }
}
