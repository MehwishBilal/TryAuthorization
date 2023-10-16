using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TryAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAuthorizationController : ControllerBase
    {
        [Authorize("1")]
        // GET: api/<TestAuthorizationController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TestAuthorizationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TestAuthorizationController>
        [Authorize("3")]
        [HttpPost]
        public string Post()
        {
            return "Successfully Posted";
        }

        // PUT api/<TestAuthorizationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestAuthorizationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
