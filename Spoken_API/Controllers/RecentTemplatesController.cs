using Microsoft.AspNetCore.Mvc;
using Spoken_API.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spoken_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecentTemplatesController : ControllerBase
    {
        // GET: api/<RecnetTemplatesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RecnetTemplatesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RecnetTemplatesController>
        [HttpPost]
        public IActionResult Post([FromBody] RecentTemplates rectemplate)
        {

            try
            {
                rectemplate.Insert();
                return Ok("Recent Template inserted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to insert recent template: " + ex.Message);
            }
        }


        // GET api/<TemplatesController>/5
        [HttpPost("getByUserEmail")]
        public IActionResult GetRecentTemplateByUserEmail([FromBody] string user)
        {

            List<RecentTemplates> RectemplatesList = RecentTemplates.ReadByUser(user);
            if (RectemplatesList.Count == 0)
            {
                return NotFound("sorry, try another email");

            }
            return Ok(RectemplatesList);
        }

        // PUT api/<RecnetTemplatesController>/5
        [HttpPut]
        public void Put([FromBody] RecentTemplates recTem)
        {
            //DBservices dbs = new DBservices();
            //return dbs.Update(recTem);
        }

        // DELETE api/<RecnetTemplatesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
