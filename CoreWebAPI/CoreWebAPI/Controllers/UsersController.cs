using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebAPI.Controllers
{
    [Route("api/Users")]
    public class UsersController : Controller
    {
        public UserDataContext DataSet { get; set; }
        public UsersController()
        {
            DataSet = UserDataContext.GetInstance();
        }
        
        // GET: api/Users
        [HttpGet]
        public IActionResult Get([FromQuery] bool activeOnly)
        {
            var users = DataSet.GetUsers();

            if (activeOnly)
            {
                users = users.Where(x => x.Active).ToList();
            }
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var user = DataSet.GetUsers().FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            return Ok(user);
        }
        
        // POST: api/Users
        [HttpPost]
        public IActionResult Post([FromBody]User newUser)
        {
            if (newUser == null || string.IsNullOrWhiteSpace(newUser.Name))
            {
                return BadRequest("A name is required.");
            }

            newUser.Id = DataSet.Users.Count + 1;

            DataSet.Add(newUser);

            return Ok(newUser.Id);
        }
        
        // PUT: api/Users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]User user)
        {
            
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
