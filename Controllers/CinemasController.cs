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
    public class CinemasController : Controller
    {
        private readonly ICinemaServices _services;

        public CinemasController(ICinemaServices services)
        {
            _services = services;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var allCinemas = await _services.GetAllAsync();
            return View(allCinemas);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _services.GetByIdAsync(id);
            return View(cinemaDetails);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Description,Logo")]Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }

            await _services.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetails = await _services.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,Name,Description,Logo")]Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }

            await _services.UpdateAsync(id,cinema);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var cinemaDetails = await _services.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");

            return View(cinemaDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmened(int id)
        {
            var cinema = await _services.GetByIdAsync(id);
            if (cinema == null) return View("NotFound");

            await _services.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
