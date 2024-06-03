using Microsoft.AspNetCore.Mvc;
using Spoken_API.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spoken_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFavoritesController : ControllerBase
    {
        // GET: api/<UserFavoritesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserFavoritesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // GET api/<TemplatesController>/5
        [HttpPost("getByUserEmail")]
        public IActionResult GetFavTemplateByUserEmail([FromBody] string user)
        {

            List<UserFavorites> FavtemplatesList = UserFavorites.ReadByUser(user);
            if (FavtemplatesList.Count == 0)
            {
                return NotFound("sorry, try another email");

            }
            return Ok(FavtemplatesList);
        }
        // POST api/<UserFavoritesController>
        [HttpPost]
        public IActionResult Post([FromBody] UserFavorites Favtemplate)
        {
            try
            {
                Favtemplate.Insert();
                return Ok("Template inserted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to insert template: " + ex.Message);
            }
        }

        // PUT api/<UserFavoritesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserFavoritesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
