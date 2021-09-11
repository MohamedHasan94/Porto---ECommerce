using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CEI_MVC_CORE_Proj.Data;
using Microsoft.AspNetCore.Mvc;
using CEI_MVC_CORE_Proj.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using CEI_MVC_CORE_Proj.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Http;
using ITI.MVC.Core.Day2.Core;
using Microsoft.AspNetCore.Identity;

namespace CEI_MVC_CORE_Proj.Controllers
{
    public class ProductsController : Controller
    {
        IUnitOfWork unitOfWork;
        private UserManager<AppUser> userManager;
        public ProductsController(IUnitOfWork unitOfWork, UserManager<AppUser> _userManager)
        {
            this.unitOfWork = unitOfWork;
            userManager = _userManager;
        }

        //Display Products
        public IActionResult Index()
        {
            int noOfPages = unitOfWork.Products.GetAll().Count();
            ProductViewModel productViewModel = new ProductViewModel
            {
                Products = PaginatedList<Product>.Create(unitOfWork.Products.GetAll().Include(p => p.Images).OrderBy(s => s.Name)),
                Categories = unitOfWork.Categories.GetAllBind(),
                Tags = unitOfWork.Tags.GetAllBind(),
            };
            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult AjaxPage(int pageNo, string order, int[] categoryId, int? tagId, string search)
        {
            PaginatedList<Product> products = unitOfWork.Products.GetPagedClient(search, categoryId, tagId, order, pageNo);
            return PartialView("_ProductsPartial", products);
        }

        public IActionResult Details(int id)
        {
            Product product = unitOfWork.Products.GetById(id);
            if (product != null)
            {
                ProductViewModel productViewModel = new ProductViewModel
                {
                    Product = product,
                    Products = PaginatedList<Product>.Create(unitOfWork.Products.GetAll().Where(p => p.FK_CategoryId == product.FK_CategoryId && p.Id != product.Id).Include(p => p.Images), 1, 4)
                };
                return View(productViewModel);
            }
            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel ordersData)
        {

            if (ordersData.ProductIds != null)
            {
                ordersData.Products = unitOfWork.Products.GetByIdList(ordersData.ProductIds);

            }
            return PartialView(ordersData);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PlaceOrders(List<Order> orders)
        {
            //get current user 
            var userId = (await userManager.GetUserAsync(HttpContext.User))?.Id;

            //check for the quantities
            DateTime date = DateTime.Now;
            //Create Unique checkout
            long timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            //Create The Orders
            List<Transaction> transList = new List<Transaction>();
            for (int i = 0; i < orders.Count; i++)
            {
                transList.Add(new Transaction
                {
                    CheckOutId = timestamp.ToString() + "_" + userId.Substring(0, 8),
                    FK_ProductId = orders[i].ProductId,
                    Quantity = orders[i].Qty, //the quantity will be subtracted when it's shipped from the vendor 
                    Status = TransactionStatus.Pending,
                    DateTime = date,
                    PaymentMethod = orders[i].PaymentMethod,
                    FK_CustomerId = userId
                });
            }

               unitOfWork.Transactions.AddRange(transList);

            return Json(true);
        }
    }

}