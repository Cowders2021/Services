using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CowdersPictures.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CowdersPictures.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Issues : ControllerBase
    {

        internal static IEnumerable<Issue> _issuesList = new List<Issue>
            {
                new Issue {Id = 1, CustomerId = 42,  ProductId = 10006, Text = "Ugress",  Desctiption = "Mye ugress i mellomhøyde nær skog som heller i østlig retning"},
                new Issue {Id = 2, CustomerId = 42,  ProductId = 10006, Text = "Ugress her også!!!",  Desctiption = "Mye ugress når jeg myser lett mot sola i fullmåne"}
            };

        // GET: api/<Issues>
        [HttpGet]
        public IEnumerable<Issue> Get()
        {
            return _issuesList;
        }

        // GET api/<Issues>/5
        [HttpGet("{id}")]
        public Issue Get(int id)
        {
            return _issuesList.FirstOrDefault(x => x.Id == id);
        }

        // POST api/<Issues>
        [HttpPost]
        public void Post([FromBody] Issue value)
        {
        }

        // PUT api/<Issues>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Issue value)
        {
        }

        // DELETE api/<Issues>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
