using Microsoft.AspNetCore.Mvc;
using Spoken_API.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spoken_API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<Users> Get()
        {
           Users user = new Users();
            return user.Read();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
      
        [HttpPost]
        public IActionResult Post([FromBody] Users user)
        {
            try
            {
                int rowsAffected = user.Insert(); 

                if (rowsAffected > 0)
                {                    
                    return Ok(user);
                }
                else
                {                 
                    return StatusCode(500, "Failed to insert user into the database.");
                }
            }
            catch (Exception ex)
            {          
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("Login")]
        public IActionResult LoginC(Users user)
        {
            try
            {
                DBservices dbs = new DBservices();
                Users loggedInUser = dbs.LoginU(user.Email, user.Password);

                if (loggedInUser.Email != null)
                {
                    return Ok(loggedInUser);
                }
                else
                {
                    return NotFound("Invalid email or password.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to login: " + ex.Message);
            }

        }
    }
}
