using Microsoft.AspNetCore.Mvc;
using Spoken_API.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spoken_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockInSummaryController : ControllerBase
    {
        // GET: api/<BlockInSummaryController>
        [HttpGet]
        public IEnumerable<BlockInSummary> Get()
        {
            BlockInSummary blobk = new BlockInSummary();
            return blobk.Read();
        }

        [HttpPost("getBlocksBySummaryNo")]
        public IActionResult GetBlocksBySummaryNo(Summary s)
        {
            List<BlockInSummary> blocks = new List<BlockInSummary>();

            try
            {
                blocks = BlockInSummary.getBlocksBySummaryNo(s);
                if (blocks.Count == 0)
                {
                    return NotFound("No blocks found for the specified summary number.");
                }
                return Ok(blocks);
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }


        // GET api/<BlockInSummaryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BlocksController>
        [HttpPost]
        public IActionResult Post([FromBody] BlockInSummary block)
        {
            try
            {
                block.Insert();
                return Ok("Block inserted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to insert block: " + ex.Message);
            }
        }

        // PUT api/<BlockInSummaryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BlockInSummaryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
