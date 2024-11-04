using Akka.Actor;
using AkkaNetWebApi.Interfaces;
using AkkaNetWebApi.Models;

namespace AkkaNetWebApi.Actors
{
    public class UserActor : ReceiveActor
    {
        private readonly IUserRepository _userRepository;

        public UserActor(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            ReceiveAsync<CreateUser>(async message => Sender.Tell(await HandleCreateUser(message.User)));
            ReceiveAsync<GetUser>(async message => Sender.Tell(await _userRepository.GetByIdAsync(message.Id)));
            ReceiveAsync<GetAllUsers>(async _ => Sender.Tell(await _userRepository.GetAllAsync()));
        }

        private async Task<User> HandleCreateUser(User user) => await _userRepository.AddAsync(user);

        public record CreateUser(User User);
        public record GetUser(int Id);
        public record GetAllUsers();
    }
}
