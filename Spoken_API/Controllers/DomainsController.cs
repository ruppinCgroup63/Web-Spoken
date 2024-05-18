using Microsoft.AspNetCore.Mvc;
using Spoken_API.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spoken_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomainsController : ControllerBase
    {
        // GET: api/<DomainsController>
        [HttpGet]
        public IEnumerable<Domains> Get()
        {
            Domains domain = new Domains();
            return domain.Read();
        }

        // GET api/<DomainsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DomainsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DomainsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DomainsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
