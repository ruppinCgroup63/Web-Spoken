using Microsoft.AspNetCore.Mvc;
using Spoken_API.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spoken_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummaryController : ControllerBase
    {
        // GET: api/<SummaryController>
        [HttpGet]
        public IEnumerable<Summary> Get()
        {
            Summary s = new Summary();
            return s.Read();
        }


        // GET api/<TemplatesController>/5
        [HttpPost("getByUserEmail")]
        public IActionResult GetSummaryByUserEmail([FromBody] string user)
        {

            List<Summary> SummaryList = Summary.ReadByUser(user);
            if (SummaryList.Count == 0)
            {
                return NotFound("sorry, try another email");

            }
            return Ok(SummaryList);
        }

        // GET api/<TemplatesController>/5
        [HttpPost("getBySummaryNo")]
        public IActionResult GetBySummaryNo([FromBody] string SummaryNo)
        {

            List<Summary> SummaryList = Summary.ReadBySummaryNo(SummaryNo);
            if (SummaryList.Count == 0)
            {
                return NotFound("sorry, try another SummaryNo");

            }
            return Ok(SummaryList);
        }

        // POST api/<SummaryController>
        [HttpPost]
        public IActionResult Post([FromBody] Summary summary)
        {

            try
            {
                summary.Insert();
                return Ok("summary inserted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to insert summary: " + ex.Message);
            }

        }

        // PUT api/<SummaryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SummaryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
