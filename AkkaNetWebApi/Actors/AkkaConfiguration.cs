using Akka.Actor;
using AkkaNetWebApi.implements;
using AkkaNetWebApi.Interfaces;
using AkkaNetWebApi.Models;

namespace AkkaNetWebApi.Actors
{
    public static class AkkaConfiguration
    {
        public static void AddAkka(this IServiceCollection services)
        {
            //services.AddScoped<IUserRepository, UserRepository>();

            services.AddSingleton(provider =>
            {
                var actorSystem = ActorSystem.Create("MyActorSystem");

                var userRepo = provider.GetRequiredService<IUserRepository>();
                var userActorProps = Props.Create(() => new UserActor(userRepo));
                var userActor = actorSystem.ActorOf(userActorProps, "userActor");

                return new AkkaActors(actorSystem, userActor);
            });
        }
    }
}
