using Microsoft.AspNetCore.Mvc;
using Spoken_API.BL;
using System;
using System.Data.SqlClient;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spoken_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        // GET: api/<TemplatesController>
        [HttpGet]
        public IEnumerable<Templates> Get()
        {
            Templates template = new Templates();
            return template.Read();
        }



        // GET api/<TemplatesController>/5
        [HttpPost("getByUserEmail")]
        public IActionResult GetTemplateByUserEmail([FromBody] string user)
        {

            List<Templates> templatesList = Templates.ReadByUser(user);
            if (templatesList.Count == 0)
            {
                return NotFound("sorry, try another date");

            }
            return Ok(templatesList);
        }



        // POST api/<TemplatesController>
        [HttpPost]
        public IActionResult Post([FromBody] Templates template)
        {

            try
            {
                template.Insert();
                return Ok("Template inserted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to insert template: " + ex.Message);
            }

        }

        // PUT api/<TemplatesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TemplatesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
