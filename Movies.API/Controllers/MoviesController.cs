using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Models;
using Movies.API.Repositories;
using Movies.API.Services;
using System.IO.Compression;

namespace Movies.API.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesRepository _moviesRepository;

        public MoviesController(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_moviesRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var movie = _moviesRepository.GetById(id);

            if (movie != null)
            {
                return Ok(movie);
            }

            return NotFound();
        }

        //[HttpGet("download/images")]
        //public IActionResult DownloadImages()
        //{
        //    var zip = ZipFile.Open("test", ZipArchiveMode.Create);


        //    var movies = _moviesRepository.GetAll();
        //    var list = new List<FileContentResult>();
        //    if (movies != null && movies.Count > 0)
        //    {
        //        foreach (var movie in movies)
        //        {
        //            var result = DownloadExt.GetUrlContentImg(movie.ImagePath);
        //            Console.WriteLine(result);

        //            if (result != null)
        //            {
        //                File.WriteAllBytes(path, data);

        //                zip.CreateEntryFromFile(file, Path.GetFileName(movie.Name), CompressionLevel.Optimal);
        //                Console.WriteLine("entro");

        //            }
        //            else
        //            {
        //                return Ok("file is not exist");

        //            }


        //        }
        //        zip.Dispose();
        //        return Ok();


        //    }


        //    return NotFound();
        //}

        [HttpGet("{id}/img/download")]
        public IActionResult DownloadImgById(int id)
        {
            var movie = _moviesRepository.GetById(id);
            if (movie != null)
            {
                var result = DownloadExt.GetUrlContentImg(movie.ImagePath);

                if (result != null)
                {
                    return File(result.Result, "image/png", $"{movie.Name}.jpg");
                }
                return Ok("file is not exist");
            }
            return NotFound();
        }
        [HttpGet("{id}/pdf/download")]
        public IActionResult DownloadPdfById(int id)
        {
            var movie = _moviesRepository.GetById(id);

            if (movie != null)
            {
                var result = DownloadExt.GetUrlContentPdfOrExcel(movie.PdfPath);
                if (result != null)
                {
                    return File(result.Result, "application/pdf", $"{movie.Name}.pdf");
                }
                return Ok("file is not exist");
            }
            return NotFound();
        }
        [HttpGet("{id}/excel/download")]
        public IActionResult DownloadExcelById(int id)
        {
            var movie = _moviesRepository.GetById(id);
            if (movie != null)
            {
                var result = DownloadExt.GetUrlContentPdfOrExcel(movie.ExcelPath);
                if (result != null)
                {
                    return File(result.Result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{movie.Name}.xlsx");
                }
                return Ok("file is not exist");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MovieToCreationAndUpdateDto request)
        {
            return Ok(await _moviesRepository.AddAsync(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] MovieToCreationAndUpdateDto request)
        {
            return Ok(await _moviesRepository.UpdateAsync(id, request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await _moviesRepository.RemoveByIdAsync(id);
            return NoContent();
        }


    }



}
