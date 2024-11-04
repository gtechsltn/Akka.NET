using Akka.Actor;
using AkkaNetWebApi.Actors;
using AkkaNetWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AkkaNetWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IActorRef _userActor;

        public UserController(AkkaActors akkaActors)
        {
            _userActor = akkaActors.UserActor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userActor.Ask<List<User>>(new UserActor.GetAllUsers());
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userActor.Ask<User>(new UserActor.GetUser(id));
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            var createdUser = await _userActor.Ask<User>(new UserActor.CreateUser(user));
            return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, createdUser);
        }
    }
}
