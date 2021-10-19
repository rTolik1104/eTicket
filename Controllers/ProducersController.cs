using eTicket_Demo.Data.Interfaces;
using eTicket_Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket_Demo.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducerServices _services;

        public ProducersController(IProducerServices services)
        {
            _services = services;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var producers = await _services.GetAllAsync();
            return View(producers);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _services.GetByIdAsync(id);

            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")]Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }

            await _services.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var producerDetails = await _services.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");

            return View(producerDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,FullName,ProfilePictureURL,Bio")]Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }

            await _services.UpdateAsync(id, producer);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var producerDetails = await _services.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");

            return View(producerDetails);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmened(int id)
        {
            var producer = await _services.GetByIdAsync(id);
            if (producer == null) return View("NotFound");

            await _services.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
