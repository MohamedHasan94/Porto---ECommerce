using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CEI_MVC_CORE_Proj.Data;
using CEI_MVC_CORE_Proj.Models;
using CEI_MVC_CORE_Proj.Models.ViewModels;
using ITI.MVC.Core.Day2.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CEI_MVC_CORE_Proj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class RequestsController : Controller
    {

        private RoleManager<AppRole> roleManager;


        private IUnitOfWork unit;
        private UserManager<AppUser> userManager;

        public RequestsController(IUnitOfWork _unit, UserManager<AppUser> _userManager)
        {
            unit = _unit;
            userManager = _userManager;

        }
        public IActionResult Index(string sortOrder,
                    string currentFilter,
                    string searchString,
                    int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "nameDsc" : "";
            ViewData["CurrentFilter"] = searchString;

            PaginatedList<RequestToAdmin> pgRequests = unit.Requests.GetPaged(sortOrder, currentFilter, searchString, pageNumber);

            return View(pgRequests);
        }


        public IActionResult AjaxPage(string sortOrder,
               string currentFilter,
               string searchString,
               int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "nameDsc" : "";
            ViewData["CurrentFilter"] = searchString;

            PaginatedList<RequestToAdmin> pgRequests = unit.Requests.GetPaged(sortOrder, currentFilter, searchString, pageNumber);

            return PartialView("_PartialReuqestsList", pgRequests);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptRequest([Required]int id)
        {

            var req = unit.Requests.GetById(id);
            if (req == null || req.Status != RequestStatus.Pending) return Json("Pending");
            switch (req.Type)
            {
                case RequestType.AddNewCategory:
                    unit.Categories.Add(new Category { Name = req.Data });
                    break;
                case RequestType.RequestVendorRole:
                    await userManager.AddToRoleAsync(req.User, "Vendor");
                    break;
            }
            unit.Requests.SetStatus(req, RequestStatus.Accepted);
            return Json("Accepted");
        }
        public IActionResult DeclineRequest([Required]int id)
        {

            var req = unit.Requests.GetById(id);
            if (req == null || req.Status != RequestStatus.Pending) return Json("Pending");
            unit.Requests.SetStatus(req, RequestStatus.Rejected);
            return Json("Rejected");
        }
    }


}
