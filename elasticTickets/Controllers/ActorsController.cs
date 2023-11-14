using elasticTickets.Data.Services;
using elasticTickets.Data.Base;
using elasticTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using elasticTickets.Data.Interfaces;

namespace elasticTickets.Controllers
{
    public class ActorsController : Controller
    {

        private readonly IActorService _elastic;
        private readonly IElasticsearchService _config;

        public ActorsController(IActorService elastic, IElasticsearchService config) {
            _elastic = elastic;
            _config = config;
        }
        
        public async Task<IActionResult> Index()
        {
            await _config.ChekIndex("actors-index");
            var model = await _elastic.GetDocumentsActor("actors-index");
            return View(model);
        }

        //GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("id,fullName,profilePictureURL,bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                
                return View(actor);
            }
            await _elastic.AddActor("actors-index", actor);
            return RedirectToAction(nameof(Index));
        }
        //GET : Actors/Details/1
        public async Task<IActionResult> Details (string id)
        {
            var actorDetails = await _elastic.GetSingleDocumentActor("actors-index", id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        //GET: Actors/Edit/1
        public async Task<IActionResult> Edit(string id)
        {
            var actorDetails = await _elastic.GetSingleDocumentActor("actors-index", id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("id,fullName,profilePictureURL,bio")] Actor newActor)
        {
            if (!ModelState.IsValid)
            {

                return View(newActor);
            }
            await _elastic.UpdateActor("actors-index", newActor, id);
            return RedirectToAction(nameof(Index));
        }
        //GET: Actors/Delete/1
        public async Task<IActionResult> Delete(string id)
        {
            var actorDetails = await _elastic.GetSingleDocumentActor("actors-index", id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var actorDetails = await _elastic.GetSingleDocumentActor("actors-index", id);
            if (actorDetails == null) return View("NotFound");
            
            await _elastic.DeleteActor("actors-index", id);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
