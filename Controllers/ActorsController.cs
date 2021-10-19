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
    public class ActorsController : Controller
    {
        private readonly IActorServices _services;

        public ActorsController(IActorServices services)
        {
            _services = services;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var allActors = await _services.GetAllAsync();
            return View(allActors);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName, ProfileImgUrl,Bio")]Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _services.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _services.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _services.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName, ProfileImgUrl,Bio")]Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _services.UpdateAsync(id, actor);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _services.GetByIdAsync(id);

            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmened(int id)
        {
            var actor = await _services.GetByIdAsync(id);
            if (actor == null) return View("NotFound");

            await _services.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
