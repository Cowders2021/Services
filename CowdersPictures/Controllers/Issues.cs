using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CowdersPictures.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using SignalR.Mvc;
using CowdersPictures.Extentions;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CowdersPictures.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Issues : ControllerBase
    {
        private ChatHub _chatHub;
        private IConfiguration _configuration;
        private IMapper _mapper;

        public Issues(IConfiguration configuration, IMapper mapper )
        {
            _chatHub = new ChatHub();
            _configuration = configuration;
            _mapper = mapper;
        }

        private CloudTable GetIssuesTable()

        {   // use dependency injection to set _config (the configuration) in the controller's constructor or use KeyVault or ManagedIdentity to avoid having to store secrets in settings

            var connectionString = _configuration.GetValue<string>("Cowdersstreamstore:ConnectionString");

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable issueTable = tableClient.GetTableReference("Issues");

            return issueTable;
        }

        // GET: api/<Issues>
        [HttpGet]
        public IEnumerable<Dtos.Issue> Get()
        {
            var issuesTable = GetIssuesTable();

            TableQuery<IssueEntity> rangeQuery = new TableQuery<IssueEntity>()
                  .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, IssueEntity.PartitionKeyConst));

            var entities = issuesTable.ExecuteQuery(rangeQuery).ToList();

            return _mapper.Map<IEnumerable<Dtos.Issue>>(entities);
        }

        // GET api/<Issues>/5
        [HttpGet("{customerId}")]
        public IEnumerable<Dtos.Issue> Get(int customerId)
        {
            var issuesTable = GetIssuesTable();

            TableQuery<IssueEntity> rangeQuery = new TableQuery<IssueEntity>()
                  .Where(TableQuery.GenerateFilterCondition("CustomerId", QueryComparisons.Equal, customerId.ToString()));

            var entities = issuesTable.ExecuteQuery(rangeQuery).ToList();

            return _mapper.Map<IEnumerable<Dtos.Issue>>(entities);
        }

        // POST api/<Issues>
        [HttpPost]
        public async Task Post([FromBody] Dtos.Issue value)
        {
            try
            {
                var table = GetIssuesTable();

                var entity = new IssueEntity(value.Id)
                {
                    CustomerId = value.CustomerId,
                    Desctiption = value.Desctiption,
                    ProductId = value.ProductId,
                    Text = value.Text
                };

                // Create the InsertOrReplace table operation
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

                // Execute the operation.
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
                IssueEntity insertedCustomer = result.Result as IssueEntity;


                //_chatHub.BroadcastMessage(value.Text, value.Desctiption);
            }
            catch (Exception)
            {
            }
            
        }

        //// PUT api/<Issues>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] Dtos.Issue value)
        //{
        //}

        //// DELETE api/<Issues>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
