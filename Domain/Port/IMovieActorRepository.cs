
using Domain.Entity.Actor;
using Domain.Entity.MovieActor;

namespace Domain.Port
{
    public interface IMovieActorRepository
    {
        Task<List<ActorEntity>> ActorsByMovieId(int movieId);
        Task<MovieActorEntity> Add(MovieActorEntity entity);
        Task<bool> MovieActorExists(int movieId, int actorId);
        Task<bool> Delete(int id);
    }
}