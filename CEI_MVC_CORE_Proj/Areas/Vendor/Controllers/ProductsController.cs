using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CEI_MVC_CORE_Proj.Models;
using CEI_MVC_CORE_Proj.Models.ViewModels;
using ITI.MVC.Core.Day2.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEI_MVC_CORE_Proj.Areas.Vendor.Controllers
{
    [Area("Vendor")]
    [Authorize(Roles="Vendor")]
    public class ProductsController : Controller
    {
        IUnitOfWork unitOfWork;
        public ProductsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            var vendorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ProductViewModel productViewModel = new ProductViewModel
            {
                Products = PaginatedList<Product>.Create(unitOfWork.Products.GetAll()
                    .Where(p => p.FK_VendorId == vendorId).Include(p => p.Category), 1, 15),
                Categories = unitOfWork.Categories.GetAllBind()
            };
            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult AjaxPage(int pageNo, string order, int[] categoryId, int? tagId, string search)
        {
            var vendorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            PaginatedList<Product> products = unitOfWork.Products.GetPagedVendor(vendorId , search, categoryId , order, pageNo);
            return PartialView("_ProductRowPartial", products);
        }

        public IActionResult Add()
        {
            ProductViewModel productViewModel = new ProductViewModel
            {
                Categories = unitOfWork.Categories.GetAllBind(),
                Tags = unitOfWork.Tags.GetAllBind()
            };
            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Add(Product product, List<IFormFile> images, List<Methods> methods, string[] productTags , string newCategoryName)
        {
            if (ModelState.IsValid)
            {
                product.PaymentMethod = GetPaymentMethods(methods);
                product.ProductTagRels = GetProductTags(productTags);
                product.Images = GetProductImages(images);
                if (unitOfWork.Products.Add(product) != null)
                {
                    TempData["Message"] = "Product added successfully.";
                    CreateRequest(newCategoryName , product.FK_VendorId);
                    return RedirectToAction(nameof(Index));
                }
                return BadRequest();
            }
            return View();
        }

        private void CreateRequest(string newCategoryName , string vendorId)
        {
            if (!string.IsNullOrEmpty(newCategoryName))
            {
                if (unitOfWork.Categories.GetAll().FirstOrDefault(c => c.Name.ToLower() == newCategoryName.ToLower()) == null)
                {
                    if (unitOfWork.Requests.Add(new RequestToAdmin{ Data = newCategoryName,
                        Status = RequestStatus.Pending , Type = RequestType.AddNewCategory,
                        FK_UserId = vendorId }) != null)
                            TempData["Message"] += " A new category request has been submitted to the admin.";
                }
                else
                {
                    TempData["Message"] += " Your request for a new category has been rejected as the name you entered already exists.";
                }
            }
        }
        private List<ProductTagRel> GetProductTags(string[] tags)
        {
            List<ProductTagRel> productTagRels = new List<ProductTagRel>();
            IList<Tag> existingTags = unitOfWork.Tags.GetAllBind();
            Tag foundTag;
            foreach (string tag in tags)
            {
                foundTag = existingTags.FirstOrDefault(t => t.Name.ToUpper() == tag.Trim().ToUpper());
                if (foundTag != null)
                    productTagRels.Add(new ProductTagRel { Tag = foundTag });
                else
                    productTagRels.Add(new ProductTagRel { Tag = new Tag { Name = tag.Trim().ToUpper() } });
            }
            return productTagRels;
        }

        private List<ProductImage> GetProductImages(List<IFormFile> images)
        {
            List<ProductImage> productImages = new List<ProductImage>();
            for (int i = 0; i < images.Count; i++)
            {
                string path = $"/images/ProductImages/{(DateTime.Now).ToString("yyyyMMddHHmmss")}_{i}." + Path.GetFileName(images[i].FileName).Split('.')[1];
                using (var stream = System.IO.File.Create(@"./wwwroot" + path))
                {
                    images[i].CopyTo(stream);
                }
                productImages.Add(new ProductImage { Path = path });
            }            
            return productImages;
        }

        private List<PaymentMethod> GetPaymentMethods(List<Methods> methods)
        {
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>();
            foreach (Methods method in methods)
            {
                paymentMethods.Add(new PaymentMethod { Method = method });
            }
            return paymentMethods;
        }

        public IActionResult Edit(int id)
        {
            Product product = unitOfWork.Products.GetById(id);
            IList<Tag> tags = unitOfWork.Tags.GetAll().Where(t => (t.ProductTagRels.Select(r => r.FK_ProductId == id ? r.Tag : null).FirstOrDefault(l => l != null)) != null).ToList();
            /*t.ProductTagRels.Select(r => r.FK_ProductId == id ? r.Tag : null) return a list of lists of tags that contains nulls 
            so FirstOrDefault(l => l != null) gets the non null tag if the list contains values or null if the list elements are nulls 
            the last (!= null ) removes the nulls*/
            if (product != null)
            {
                ProductViewModel productViewModel = new ProductViewModel
                {
                    Product = product,
                    Categories = unitOfWork.Categories.GetAllBind(),
                    ProductTags = tags
                };
                return View(productViewModel);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Product product, List<IFormFile> images, List<Methods> methods, string[] productTags , List<string> flags)
        {
            if (ModelState.IsValid)
            {
                if(flags[0] == "changeMethods")
                    product.PaymentMethod = unitOfWork.Products.EditPaymentMethods(product.Id, methods);
                
                if (flags[1] == "changeTags")
                {
                    unitOfWork.Products.DeleteProductTags(product.Id);
                    product.ProductTagRels = GetProductTags(productTags);
                }

                product.Images = EditImages(product.Id, images , flags);
                if (unitOfWork.Products.Update(product))
                {
                    TempData["Message"] = "Product edited successfully";
                    return RedirectToAction(nameof(Index));
                }
                return BadRequest();
            }
            return View();
        }

        private List<ProductImage> EditImages(int id , List<IFormFile> newImages , List<string> flags)
        {
            List < ProductImage > productImages = unitOfWork.Products.GetProductImages(id);
            int j = 0;
            for(int i = 0; i< 3; i++)
            {
                if (flags[i+2] == "change")
                {
                    try
                    {
                        System.IO.File.Delete($"./wwwroot{productImages[i].Path}");
                        //Add the extension of the new image
                        productImages[i].Path = $"{productImages[i].Path.Split('.')[0]}.{newImages[i].FileName.Split('.')[1]}";
                        using (var stream = System.IO.File.Create($"./wwwroot{productImages[i].Path}"))
                        {
                            newImages[j].CopyTo(stream);
                        }
                    }
                    // if the user adds image not modifies an existing one it will get out of list index
                    catch (ArgumentOutOfRangeException)
                    {
                        string path = $"{productImages[0].Path.Split('_')[0]}_{i}.{newImages[j].FileName.Split('.')[1]}";
                        using (var stream = System.IO.File.Create($"./wwwroot{path}"))
                        {
                            newImages[j].CopyTo(stream);
                        }
                        productImages.Add(new ProductImage { Path = path });
                    }
                    j++;
                }
                else if( flags[i+2] == "delete")
                {
                    try
                    {
                        System.IO.File.Delete($"./wwwroot{productImages[i].Path}");
                        unitOfWork.Products.DeleteProductImage(id, productImages[i].Path);
                    }
                    //handle the case if the user uploaded an image and deleted it and it was empty before editing 
                    catch (ArgumentOutOfRangeException)
                    { }
                }
            }
            return productImages;
        }


        [HttpPost]
        public bool Delete(int id)
        {
            if (unitOfWork.Products.RemoveById(id))
            {
                return true;
            }
            return false;
        }
    }
}