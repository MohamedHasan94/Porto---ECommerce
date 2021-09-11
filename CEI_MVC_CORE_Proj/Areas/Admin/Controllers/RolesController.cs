using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CEI_MVC_CORE_Proj.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEI_MVC_CORE_Proj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private RoleManager<AppRole> roleManager;

        private UserManager<AppUser> userManager;

        public RolesController(RoleManager<AppRole> _roleManager,
                                   UserManager<AppUser> _userManager)
        {
            roleManager = _roleManager;
            userManager = _userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {

            var w = roleManager.Roles.ToList();
            var roles = roleManager.Roles.Include(ro => ro.UserRoleRel).ToList();
            return View(roles);
        }
        [HttpGet]
        public IActionResult Details()
        {
            //usersViewModel.UserProductsCount = context.Products.GroupBy(x => x.FK_VendorId).ToDictionary(x => x.Key,x=> x.Count());

            return RedirectToAction("Index", "Users");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return RedirectToAction("Index","Users");
        }
      


        public IActionResult Create() { return View(); }


        //Creating & Deleting Roles 
        //the application have only two roles (Admins, Vendors), we won't need to create or delete from them [they're not used]
        #region Create Or Delete Role 
        [HttpPost]
        public async Task<IActionResult> Create([Required]string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new AppRole { Name = name });
                if (result.Succeeded)
                {
                    TempData["Message"] = "Role Was Created Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            AppRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "No roles with that Id was found");
            }
            var roles = roleManager.Roles.ToList();
            return View("Index", roles);
        }
        #endregion



        //----------------Methods--------------------//
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

    }
}