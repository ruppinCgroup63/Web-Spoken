using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Spoken_API.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spoken_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextCorrectorController : ControllerBase
    {
        // GET: api/<TextCorrectorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TextCorrectorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TextCorrectorController>
        [HttpPost]
        public async Task<ActionResult<BlockInTemplate>> Post([FromBody] BlockInTemplate block)
        {
            if (block == null)
            {
                return BadRequest("Invalid block object");
            }

            try
            {
                // תיקון הטקסט של הבלוק ,המתודה היא אסינכרונית ולכן משתמשים ב await
                block = await block.CorrectTextAsync();

                // עדכון הבלוק בבסיס הנתונים
                int updateResult = block.Update();

                if (updateResult > 0)
                {
                    return Ok(block);
                }
                else
                {
                    return StatusCode(500, "Failed to update the block in the database.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // PUT api/<TextCorrectorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TextCorrectorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
