using Microsoft.EntityFrameworkCore;
using Movies.API.Entities;

namespace Movies.API.Context
{
    public class MoviesContext : DbContext
    {


        public MoviesContext(DbContextOptions<MoviesContext> options) : base(options)
        {

        }

        public DbSet<Movie>? Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}