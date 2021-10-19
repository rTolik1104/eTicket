using eTicket_Demo.Data.Interfaces;
using eTicket_Demo.Models;
using eTicket_Demo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket_Demo.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IAppUserServices _services;
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(IAppUserServices services,UserManager<AppUser> userManager)
        {
            _services = services;
            _userManager = userManager;
        }
        public IActionResult Details(string id)
        {
            var user = _services.GetById(id);
            var userRoles = _userManager.GetRolesAsync(user).Result;
            var orders = _services.GetOrders(id);

            var model = new ProfileVM
            {
                UserId = user.Id,
                UserName = user.UserName,
                ProfileImgUrl = user.ProfileImgUrl,
                MemberScine = user.MemberScince,
                Orders=_services.GetOrders(user.Id).ToList(),
                isAdmin = userRoles.Contains("Admin")
            };

            return View(model);
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            var users = _services.GetAll().Select(n => new ProfileVM
            {
                Email = n.Email,
                UserName = n.UserName,
                MemberScine = n.MemberScince,
                ProfileImgUrl=n.ProfileImgUrl
            });

            var model = new ProfileListVM
            {
                Profiles = users
            };

            return View(model);
        }

        
    }
}
