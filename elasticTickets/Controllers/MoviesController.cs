using elasticTickets.Data;
using elasticTickets.Data.Base;
using elasticTickets.Data.Interfaces;
using elasticTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elasticTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IMovieService _elastic;
        private readonly IElasticsearchService _config;

        public MoviesController(IMovieService elastic, IElasticsearchService config, ILogger<MoviesController> logger)
        {
            _elastic = elastic;
            _config = config;
            _logger = logger;


        }
        public IActionResult Get()
        {
            _logger.LogDebug("Debug message");
            _logger.LogTrace("Trace message");
            _logger.LogError("Error message");
            _logger.LogWarning("Warning message");
            _logger.LogCritical("Critical message");
            _logger.LogInformation("Information message");

            return Ok();
        }

        public async Task<IActionResult> Index()
        {
            await _config.ChekIndex("movies-index");
            var model = await _elastic.GetDocumentsMovie("movies-index");
            
            
            return View(model);
        }
        //GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([ModelBinder] Movie movie)
        {
            if (!ModelState.IsValid)
            {

                return View(movie);
            }
            await _elastic.AddMovie("movies-index", movie);
            return RedirectToAction(nameof(Index));
        }
        //GET : Actors/Details/1
        public async Task<IActionResult> Details(string id)
        {
            var movieDetails = await _elastic.GetSingleDocumentMovie("movies-index", id);
            if (movieDetails == null) return View("NotFound");
            return View(movieDetails);
        }

        //GET: Actors/Edit/1
        public async Task<IActionResult> Edit(string id)
        {
            var movieDetails = await _elastic.GetSingleDocumentMovie("movies-index", id);
            if (movieDetails == null) return View("NotFound");
            return View(movieDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, [ModelBinder] Movie newMovie)
        {
            if (!ModelState.IsValid)
            {

                return View(newMovie);
            }
            await _elastic.UpdateMovie("movies-index", newMovie, id);
            return RedirectToAction(nameof(Index));
        }
        //GET: Actors/Delete/1
        public async Task<IActionResult> Delete(string id)
        {
            var movieDetails = await _elastic.GetSingleDocumentMovie("movies-index", id);
            if (movieDetails == null) return View("NotFound");
            return View(movieDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var movieDetails = await _elastic.GetSingleDocumentMovie("movies-index", id);
            if (movieDetails == null) return View("NotFound");

            await _elastic.DeleteMovie("movies-index", id);

            return RedirectToAction(nameof(Index));
        }
    }
}
