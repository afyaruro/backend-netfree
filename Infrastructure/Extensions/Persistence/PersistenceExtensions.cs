
using Application.Service.Actor;
using Application.Service.Country;
using Application.Service.Director;
using Application.Service.Gender;
using Application.Service.Movie;
using Application.Service.MovieActor;
using Application.Service.User;
using Domain.Port;
using Infrastructure.Adapters;
using Infrastructure.Adapters.Actor;
using Infrastructure.Adapters.ActorMovie;
using Infrastructure.Adapters.Country;
using Infrastructure.Adapters.Director;
using Infrastructure.Adapters.Gender;
using Infrastructure.Adapters.Movie;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions.Persistence
{
    public static class PersistenceExtension
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<UserService>();

            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<CountryService>();

            services.AddScoped<IGenderRepository, GenderRepository>();
            services.AddScoped<GenderService>();

            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<ActorService>();

            services.AddScoped<IDirectorRepository, DirectorRepository>();
            services.AddScoped<DirectorService>();

            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<MovieService>();

            services.AddScoped<IMovieActorRepository, ActorMovieRepository>();
            services.AddScoped<MovieActorService>();

            return services;
        }

    }
}