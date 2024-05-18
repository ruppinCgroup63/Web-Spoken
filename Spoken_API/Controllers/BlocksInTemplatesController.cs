using Microsoft.AspNetCore.Mvc;
using Spoken_API.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spoken_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlocksInTemplatesController : ControllerBase
    {
        // GET: api/<BlocksController>
        [HttpGet]
        public IEnumerable<BlockInTemplate> Get()
        {
            BlockInTemplate blobk = new BlockInTemplate();
            return blobk.Read();
        }

        [HttpPost("getBlocksByTemplateNo")]
        public IActionResult GetBlocksByTemplateNo(Templates template)
        {
            List<BlockInTemplate> blocks = new List<BlockInTemplate>();

            try
            {
                blocks = BlockInTemplate.getBlocksByTemplateNo(template);
                if (blocks.Count == 0)
                {
                    return NotFound("No blocks found for the specified template number.");
                }
                return Ok(blocks);
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }



        // GET api/<BlocksController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BlocksController>
        [HttpPost]
        public IActionResult Post([FromBody] BlockInTemplate block)
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

        // PUT api/<BlocksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BlocksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
