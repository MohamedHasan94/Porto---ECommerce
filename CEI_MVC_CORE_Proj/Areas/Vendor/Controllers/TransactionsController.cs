using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CEI_MVC_CORE_Proj.Models;
using CEI_MVC_CORE_Proj.Models.ViewModels;
using ITI.MVC.Core.Day2.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CEI_MVC_CORE_Proj.Areas.Vendor.Controllers
{
    [Area("Vendor")]
    [Authorize(Roles = "Vendor")]
    public class TransactionsController : Controller
    {
        IUnitOfWork unitOfWork;
        public TransactionsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var vendorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            PaginatedList<Transaction> transactions = unitOfWork.Transactions.GetPagedVendor(vendorId, null, null, 1);
            return View(transactions);
        }

        [HttpPost]
        public IActionResult AjaxPage(string order, string searchString, int pageNumber)
        {
            var vendorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            PaginatedList<Transaction> transactions = unitOfWork.Transactions.GetPagedVendor(vendorId, order, searchString, pageNumber);
            return PartialView("_TransactionPartial", transactions);
        }


        [HttpPost]
        public IActionResult ChangeStatus(int transactionId, TransactionStatus newStatus)
        {
            Transaction transaction = unitOfWork.Transactions.ChangeStatus(transactionId, newStatus);
            if (transaction != null)
                return PartialView("_TransactionRowPartial", transaction);
            return new ContentResult {Content = "Process failed : Required quantity is more than available stock" };
        }
    }
}