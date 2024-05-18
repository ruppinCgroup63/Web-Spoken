using Microsoft.AspNetCore.Mvc;
using Spoken_API.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spoken_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LangsController : ControllerBase
    {
        // GET: api/<LengsController>
        [HttpGet]
        public IEnumerable<Langs> Get()
        {
            Langs lang = new Langs();
            return lang.Read();
        }

        // GET api/<LengsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LengsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LengsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LengsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
