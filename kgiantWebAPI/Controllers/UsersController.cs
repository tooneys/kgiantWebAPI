using kgiantWebAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace kgiantWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static List<UserDto> listUsers = new List<UserDto>() { 
            new UserDto()
            {
                FirstName = "Bill",
                LastName = "Gates",
                Email = "bill@microsoft.com",
                Phone = "+1 000 1111 2222",
                Address = "New York, USA"
            },
            new UserDto()
            {
                FirstName = "Kyoung Su",
                LastName = "Lee",
                Email = "kslee@microsoft.com",
                Phone = "+82 10 9447 3690",
                Address = "seoul, Korea"
            }
        };

        [HttpGet]
        public IActionResult GetUsers()
        {
            if (listUsers.Count > 0)
            {
                return Ok(listUsers);   
            }
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetUsersById(int id)
        {
            if (id >= 0 && id < listUsers.Count)
            {
                return Ok(listUsers[id]);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddUser(UserDto user)
        {
            if (user.Email.Equals("user@example.com")) {
                ModelState.AddModelError("Email", "승인되지 않는 이메일입니다. 확인바랍니다.");
                return BadRequest();
            }

            listUsers.Add(user);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserDto user)
        {
            if (user.Email.Equals("user@example.com"))
            {
                ModelState.AddModelError("Email", "승인되지 않는 이메일입니다. 확인바랍니다.");
                return BadRequest();
            }

            if (id >= 0 && id < listUsers.Count)
            {
                listUsers[id] = user;
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (id >= 0 && id < listUsers.Count)
            {
                listUsers.RemoveAt(id);
            }

            return NoContent();
        }
    }
}
