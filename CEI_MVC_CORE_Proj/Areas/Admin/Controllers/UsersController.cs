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
using Microsoft.EntityFrameworkCore;

namespace CEI_MVC_CORE_Proj.Areas.Admin.Controllers
{
    public class UsersViewModel
    {
        public PaginatedList<AppUser> Users { get; set; }
        public IEnumerable<AppRole> Roles { get; set; }

    }

    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private RoleManager<AppRole> roleManager;

        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private ApplicationDbContext context;
        private IUnitOfWork unit;

        public UsersViewModel usersViewModel = new UsersViewModel();
        public UsersController(ApplicationDbContext _context, RoleManager<AppRole> _roleManager,
                                   UserManager<AppUser> _userManager, IUnitOfWork _unit)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            unit = _unit;
            context = _context;
        }
        public IActionResult Index(string sortOrder,
                string currentFilter,
                string searchString,
                int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "nameDsc" : "";
            ViewData["CurrentFilter"] = searchString;

            PaginatedList<AppUser> pgUsers = unit.ExtendedUserManager.GetPaged(sortOrder, currentFilter, searchString, pageNumber);

            usersViewModel.Users = pgUsers;
            //usersViewModel.Users = userManager.Users.Include(x => x.UserRoleRel).ToList();
            usersViewModel.Roles = roleManager.Roles.ToList();

            return View(usersViewModel);
        }


        public IActionResult AjaxPage(string sortOrder,
               string currentFilter,
               string searchString,
               int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "nameDsc" : "";
            ViewData["CurrentFilter"] = searchString;

            PaginatedList<AppUser> pgUsers = unit.ExtendedUserManager.GetPaged(sortOrder, currentFilter, searchString, pageNumber);

            usersViewModel.Users = pgUsers;
            //usersViewModel.Users = userManager.Users.Include(x => x.UserRoleRel).ToList();
            usersViewModel.Roles = roleManager.Roles.ToList();

            return PartialView("_PartialUsersList", usersViewModel);
        }


        [HttpGet]
        public IActionResult Details([Required] string Id)
        {
            var user = context.Users.Include(u => u.Products).FirstOrDefault(u => u.Id == Id);
            if (user == null) return NotFound();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Delete([Required] string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user == null) return NotFound();
            var poductsAssociatedWithUser = context.Products.Where(p => p.FK_VendorId == Id);
            if (poductsAssociatedWithUser != null)
            {
                context.Products.RemoveRange(poductsAssociatedWithUser);
            }
            var requestsAssociatedWithUser = context.Requests.Where(p => p.FK_UserId == Id);
            if (requestsAssociatedWithUser != null)
            {
                context.Requests.RemoveRange(requestsAssociatedWithUser);
            }
            var transactionsAssociatedWithUser = context.Transactions.Where(p => p.FK_CustomerId == Id);
            if (transactionsAssociatedWithUser != null)
            {
                context.Transactions.RemoveRange(transactionsAssociatedWithUser);
            }
            context.SaveChanges();
            var result = await userManager.DeleteAsync(user);
            return Json(result);
        }

        /// <summary>
        /// The Updated role will take effect in the next user session, therefore he should logout and re login first
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateRole([Required]string UserId, [Required]string RoleId, [Required]bool IsChecked)
        {

            AppUser user = context.Users.Include(x => x.UserRoleRel).FirstOrDefault(x => x.Id == UserId);
            AppRole role = await roleManager.FindByIdAsync(RoleId);
            if (user == null || role == null) return NotFound();

            var f = user.UserRoleRel; ;
            if (!IsChecked) return Json(await userManager.RemoveFromRoleAsync(user, role.Name));
            if (IsChecked) return Json(await userManager.AddToRoleAsync(user, role.Name));

            return Json(false);
        }
        //Used to test auth It 
        //[HttpGet]
        //[Authorize(Roles = "Vendor")]
        //public IActionResult Vendor()
        //{
        //    var U = User;
        //    var F = User.IsInRole("Vendor");
        //    var Fa = User.IsInRole("Admin");
        //    return Json("IsVendor");
        //}
        //[HttpGet]
        //[Authorize(Roles = "Admin")]
        //public IActionResult Admin()
        //{
        //    var U = User;
        //    var F = User.IsInRole("Vendor");
        //    var Fa = User.IsInRole("Admin");
        //    return Json("IsAdmin");
        //}

    }
}