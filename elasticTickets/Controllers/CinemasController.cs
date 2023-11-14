using elasticTickets.Data;
using elasticTickets.Data.Base;
using elasticTickets.Data.Interfaces;
using elasticTickets.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elasticTickets.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemaService _elastic;
        private readonly IElasticsearchService _config;
        public CinemasController(ICinemaService elastic, IElasticsearchService config)
        {
            _elastic = elastic;
            _config = config;
        }
        public async Task<IActionResult> Index()
        {
            await _config.ChekIndex("cinemas-index");
            var model = await _elastic.GetDocumentsCinema("cinemas-index");
            return View(model);
        }

        //GET: Cinemas/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("id,name,logo,description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {

                return View(cinema);
            }
            await _elastic.AddCinema("cinemas-index", cinema);
            return RedirectToAction(nameof(Index));
        }
        //GET : Cinemas/Details/1
        public async Task<IActionResult> Details(string id)
        {
            var cinemaDetails = await _elastic.GetSingleDocumentCinema("cinemas-index", id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        //GET: Cinemas/Edit/1
        public async Task<IActionResult> Edit(string id)
        {
            var cinemaDetails = await _elastic.GetSingleDocumentCinema("cinemas-index", id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("id,name,logo,description")] Cinema newCinema)
        {
            if (!ModelState.IsValid)
            {

                return View(newCinema);
            }
            await _elastic.UpdateCinema("cinemas-index", newCinema, id);
            return RedirectToAction(nameof(Index));
        }
        //GET: Cinemas/Delete/1
        public async Task<IActionResult> Delete(string id)
        {
            var cinemaDetails = await _elastic.GetSingleDocumentCinema("cinemas-index", id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cinemaDetails = await _elastic.GetSingleDocumentCinema("cinemas-index", id);
            if (cinemaDetails == null) return View("NotFound");

            await _elastic.DeleteCinema("cinemas-index", id);

            return RedirectToAction(nameof(Index));
        }

    }
}
