using Akka.Actor;

namespace AkkaNetWebApi.Models
{
    public class AkkaActors
    {
        public AkkaActors(ActorSystem actorSystem, IActorRef userActor)
        {
            ActorSystem = actorSystem;
            UserActor = userActor;
        }

        public ActorSystem ActorSystem { get; }
        public IActorRef UserActor { get; }
    }
}
