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
    public class ProducersController : Controller
    {

        private readonly IProducerService _elastic;
        private readonly IElasticsearchService _config;

        public ProducersController(IProducerService elastic, IElasticsearchService config)
        {
            _elastic = elastic;
            _config = config;
        }
        public async Task<IActionResult> Index()
        {
            await _config.ChekIndex("producers-index");
            var model = await _elastic.GetDocumentsProducer("producers-index");
            return View(model);
        }
        //GET: Producer/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("id,fullName,profilePictureURL,bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {

                return View(producer);
            }
            await _elastic.AddProducer("producers-index", producer);
            return RedirectToAction(nameof(Index));
        }
        //GET : Producer/Details/1
        public async Task<IActionResult> Details(string id)
        {
            var producerDetails = await _elastic.GetSingleDocumentProducer("producers-index", id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        //GET: Producer/Edit/1
        public async Task<IActionResult> Edit(string id)
        {
            var producerDetails = await _elastic.GetSingleDocumentProducer("producers-index", id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("id,fullName,profilePictureURL,bio")] Producer newProducer)
        {
            if (!ModelState.IsValid)
            {

                return View(newProducer);
            }
            await _elastic.UpdateProducer("producers-index", newProducer, id);
            return RedirectToAction(nameof(Index));
        }
        //GET: Producer/Delete/1
        public async Task<IActionResult> Delete(string id)
        {
            var producerDetails = await _elastic.GetSingleDocumentProducer("producers-index", id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var producerDetails = await _elastic.GetSingleDocumentProducer("producers-index", id);
            if (producerDetails == null) return View("NotFound");

            await _elastic.DeleteProducer("producers-index", id);

            return RedirectToAction(nameof(Index));
        }
    }
}
