using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;

        public ProducersController(IProducersService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allProducers = await _service.GetAllAsync();
            return View(allProducers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create ([Bind("ProfilePictureUrl, ProducerName, Bio")]Producer producer)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(producer);
                return RedirectToAction(nameof(Index));
            }

            return View(producer);
        }

        [HttpGet]
        public async Task<IActionResult> Details (int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null)
                return View("Empty");
            return View(producerDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null)
                return View("Not Found");
            return View(producerDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit (int id, [Bind("Id, ProfilePictureUrl, ProducerName, Bio")]Producer producer)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));
            }

            return View(producer);
        }

        [HttpGet]
        public async Task<IActionResult> Delete (int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null)
                return View("Not Found");

            return View(producerDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null)
                return View("Not Found");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
