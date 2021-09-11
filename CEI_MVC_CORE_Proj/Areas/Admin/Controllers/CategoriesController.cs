using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CEI_MVC_CORE_Proj.Models;
using CEI_MVC_CORE_Proj.Models.ViewModels;
using ITI.MVC.Core.Day2.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CEI_MVC_CORE_Proj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class CategoriesController : Controller
    {
        IUnitOfWork unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel
            {
                Categories = unitOfWork.Categories.GetPaged(null,null,1)
            };
            return View(categoryViewModel);
        }

        public IActionResult AjaxPage(int pageNo , string order , string search)
        {
            PaginatedList<Category> categories = unitOfWork.Categories.GetPaged(order , search , pageNo);
            return PartialView("_CategoryPartial", categories);
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                //Check if category already exists
                if(unitOfWork.Categories.GetAll().FirstOrDefault(c => c.Name.ToLower() == category.Name.ToLower()) == null)
                {
                    if(unitOfWork.Categories.Add(category) != null)
                    {
                        category.Products = unitOfWork.Products.GetAll().Where(p => p.FK_CategoryId == category.Id).ToList();
                        return PartialView("_CategoryRowPartial" , category);
                    }
                    else
                        return BadRequest();                    
                }
                else
                    return View("/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = "The name you entered already exists in another category" });
            }
            return View();
        }

        
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                if (unitOfWork.Categories.GetAll().FirstOrDefault(c => c.Name.ToLower() == category.Name.ToLower()) == null)
                {
                    if (unitOfWork.Categories.Update(category))
                    {
                        category.Products = unitOfWork.Products.GetAll().Where(p => p.FK_CategoryId == category.Id).ToList();
                        return PartialView("_CategoryRowPartial", category);
                    }
                    else
                        return NotFound();
                }
                return View("/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = "The name you entered already exists in another category" });
            }
            return View();
        }
        
        [HttpPost]
        public bool Delete(int id)
        {
            if (unitOfWork.Categories.RemoveById(id))
            {
                return true;
            }
            return false;
        }
    }
}