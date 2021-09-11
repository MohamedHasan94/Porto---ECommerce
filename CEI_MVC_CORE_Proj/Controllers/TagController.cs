using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CEI_MVC_CORE_Proj.Data;
using CEI_MVC_CORE_Proj.Models;
using ITI.MVC.Core.Day2.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEI_MVC_CORE_Proj.Controllers
{
    //[Authorize(Roles = "Vendor,Admin")]
    public class TagController : Controller
    {
        IUnitOfWork unitOfWork;

        public TagController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IList<Tag> tags = unitOfWork.Tags.GetAll().Include(nameof(Tag.ProductTagRels)).ToList(); 
            return View(tags);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Tag tag)
        {
            if(ModelState.IsValid)
            {
                if(unitOfWork.Tags.Add(tag) != null)
                    return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            Tag tag = unitOfWork.Tags.GetById(id);
            if(tag!= null)
                return View(tag);
            
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Tag tag)
        {
            if (ModelState.IsValid)
            {
                if(unitOfWork.Tags.Update(tag))
                    return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (unitOfWork.Tags.RemoveById(id))
                return RedirectToAction(nameof(Index));
            
            return NotFound();
        }
    }
}