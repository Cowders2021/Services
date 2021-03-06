﻿using System;
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
    public class Sensors : ControllerBase
    {
        private IConfiguration _configuration;
        private IMapper _mapper;

        public Sensors(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        //// GET api/<SensorsController>/3
        [HttpGet("{minuttes}")]
        public IEnumerable<Dtos.Sensor> Get(int minuttes)
        {
            var table = GetSensorsTable();

            TableQuery<SensorEntity> noQuery = new TableQuery<SensorEntity>()
                  .Where(TableQuery.GenerateFilterCondition("id", QueryComparisons.NotEqual, 0.ToString()));

            var entities = table.ExecuteQuery(noQuery).ToList().OrderByDescending(x => x.EventEnqueuedUtcTime);

            var ent2 = entities.Where(x => x.EventEnqueuedUtcTime > DateTime.UtcNow.AddMinutes(minuttes * -1));

            //TableQuery<SensorEntity> rangeQuery = new TableQuery<SensorEntity>()
            //      .Where(TableQuery.GenerateFilterCondition("EventEnqueuedUtcTime", QueryComparisons.GreaterThan, DateTime.UtcNow.AddMinutes(minuttes * -1).ToString()));

            //var entities = table.ExecuteQuery(rangeQuery).ToList().OrderByDescending(x => x.EventEnqueuedUtcTime);

            return _mapper.Map<IEnumerable<Dtos.Sensor>>(ent2);
        }

        ////// GET api/<SensorsController>
        //[HttpGet]
        //public IEnumerable<Dtos.Sensor> Get()
        //{
        //    var table = GetSensorsTable();

        //    TableQuery<SensorEntity> rangeQuery = new TableQuery<SensorEntity>()
        //          .Where(TableQuery.GenerateFilterCondition("EventEnqueuedUtcTime", QueryComparisons.Equal, id.ToString()));

        //    var entities = table.ExecuteQuery(rangeQuery).ToList().OrderByDescending(x => x.EventEnqueuedUtcTime).Take(3);

        //    return _mapper.Map<IEnumerable<Dtos.Sensor>>(entities);
        //}

        private CloudTable GetSensorsTable()

        {   // use dependency injection to set _config (the configuration) in the controller's constructor or use KeyVault or ManagedIdentity to avoid having to store secrets in settings

            var connectionString = _configuration.GetValue<string>("Cowdersstreamstore:ConnectionString");

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable sensorTable = tableClient.GetTableReference("sensor");

            return sensorTable;
        }

        //// GET api/<SensorsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<SensorsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<SensorsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<SensorsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
