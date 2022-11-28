using Azure.Storage.Blobs.Models;
using Movies.API.Entities;
using Movies.API.Enums;
using Movies.API.Models;

namespace Movies.API.Repositories
{
    public interface IMoviesRepository
    {
        List<Movie> GetAll();
        Movie GetById(int id);
        Task<Movie> AddAsync(MovieToCreationAndUpdateDto request);
        Task<Movie> UpdateAsync(int id, MovieToCreationAndUpdateDto request);
        Task RemoveByIdAsync(int id);

    }
}
