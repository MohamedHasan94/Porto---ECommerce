using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CEI_MVC_CORE_Proj.Models;
using CEI_MVC_CORE_Proj.Models.ViewModels;
using ITI.MVC.Core.Day2.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CEI_MVC_CORE_Proj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class TransactionsController : Controller
    {
        IUnitOfWork unitOfWork;
        public TransactionsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            PaginatedList<Transaction> transactions = unitOfWork.Transactions.GetPagedAdmin(null,  null, 1);
            return View(transactions);
        }

        [HttpPost]
        public IActionResult AjaxPage(string order, string searchString, int pageNumber)
        {
            PaginatedList<Transaction> transactions = unitOfWork.Transactions.GetPagedAdmin(order, searchString, pageNumber);
            return PartialView("_TransactionPartial", transactions);
        }
    }
}