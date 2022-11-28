using Azure.Storage.Blobs.Models;
using Movies.API.Context;
using Movies.API.Entities;
using Movies.API.Enums;
using Movies.API.Models;
using Movies.API.Services;

namespace Movies.API.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly MoviesContext _context;
        private readonly IAzureStorageService _azStorageService;

        public MoviesRepository(MoviesContext context, IAzureStorageService azStorageService)
        {
            _context = context;
            _azStorageService = azStorageService;
        }

        public async Task<Movie> AddAsync(MovieToCreationAndUpdateDto request)
        {
            var movie = new Movie()
            {
                Name = request.Name,
                Valoration = request.Valoration
            };

            if (request.Image != null)
            {
                var path = await _azStorageService.UploadAsync(request.Image, ContainerEnum.IMAGES);
                movie.ImagePath = $"https://moviesfiles.blob.core.windows.net/images/{path}";
            }

            if (request.Pdf != null)
            {
                var path = await _azStorageService.UploadAsync(request.Pdf, ContainerEnum.PDFS);
                movie.PdfPath = $"https://moviesfiles.blob.core.windows.net/pdfs/{path}";

            }

            if (request.Excel != null)
            {
                var path = await _azStorageService.UploadAsync(request.Excel, ContainerEnum.EXCELS);
                movie.ExcelPath = $"https://moviesfiles.blob.core.windows.net/excels/{path}";

            }

            _context.Add(movie);
            _context.SaveChanges();

            return movie;
        }

        public List<Movie> GetAll()
        {
            return _context.Movies.ToList();
        }

        public Movie GetById(int id)
        {
            return _context.Movies.Find(id);
        }

        public async Task RemoveByIdAsync(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie != null)
            {
                if (!string.IsNullOrEmpty(movie.ImagePath))
                {
                    await _azStorageService.DeleteAsync(ContainerEnum.IMAGES, movie.ImagePath.Replace("https://moviesfiles.blob.core.windows.net/images/", ""));
                }

                if (!string.IsNullOrEmpty(movie.PdfPath))
                {
                    await _azStorageService.DeleteAsync(ContainerEnum.PDFS, movie.PdfPath.Replace("https://moviesfiles.blob.core.windows.net/pdfs/", ""));
                }

                if (!string.IsNullOrEmpty(movie.ExcelPath))
                {
                    await _azStorageService.DeleteAsync(ContainerEnum.EXCELS, movie.ExcelPath.Replace("https://moviesfiles.blob.core.windows.net/excels/", ""));
                }

                _context.Remove(movie);
                _context.SaveChanges();
            }
        }

        public async Task<Movie> UpdateAsync(int id, MovieToCreationAndUpdateDto request)
        {
            var movie = _context.Movies.Find(id);
            if (movie != null)
            {
                movie.Name = request.Name;
                movie.Valoration = request.Valoration;

                if (request.Image != null)
                {
                    movie.ImagePath = await _azStorageService.UploadAsync(request.Image, ContainerEnum.IMAGES, movie.ImagePath);
                }

                if (request.Pdf != null)
                {
                    movie.PdfPath = await _azStorageService.UploadAsync(request.Pdf, ContainerEnum.PDFS, movie.PdfPath);
                }

                if (request.Excel != null)
                {
                    movie.ExcelPath = await _azStorageService.UploadAsync(request.Excel, ContainerEnum.EXCELS, movie.ExcelPath);
                }

                _context.Update(movie);
                _context.SaveChanges();
            }




            return movie;
        }



    }
}