
using Domain.Entity.Actor;
using Domain.Entity.Country;
using Domain.Entity.Director;
using Domain.Entity.Gender;
using Domain.Entity.Movie;
using Domain.Entity.MovieActor;
using Domain.Entity.User;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options) { }

        public DbSet<UserEntity> users { get; set; }
        public DbSet<GenderEntity> gender { get; set; }
        public DbSet<CountryEntity> country { get; set; }
        public DbSet<ActorEntity> actor { get; set; }
        public DbSet<DirectorEntity> director { get; set; }
        public DbSet<MovieEntity> movie { get; set; }
        public DbSet<MovieActorEntity> movieActor { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>().ToTable("USERS");
            modelBuilder.Entity<GenderEntity>().ToTable("GENDER");
            modelBuilder.Entity<CountryEntity>().ToTable("COUNTRY");
            modelBuilder.Entity<ActorEntity>().ToTable("ACTOR");
            modelBuilder.Entity<DirectorEntity>().ToTable("DIRECTOR");
            modelBuilder.Entity<MovieEntity>().ToTable("MOVIE");
            modelBuilder.Entity<MovieActorEntity>().ToTable("ACTOR_MOVIE");


        }
    }
}