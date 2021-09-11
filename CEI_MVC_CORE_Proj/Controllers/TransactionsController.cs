using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CEI_MVC_CORE_Proj.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CEI_MVC_CORE_Proj.Models;
using ITI.MVC.Core.Day2.Core;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CEI_MVC_CORE_Proj.Models.ViewModels;

namespace CEI_MVC_CORE_Proj.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        IUnitOfWork unitOfWork;

        public TransactionsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            var customerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            PaginatedList<Transaction> transactions = unitOfWork.Transactions.GetPagedCustomer(customerId, null, null, 1);
            return View(transactions);
        }

               
        [HttpPost]
        public IActionResult ChangeStatus(int transactionId, TransactionStatus newStatus)
        {
            Transaction transaction = unitOfWork.Transactions.ChangeStatus(transactionId, newStatus);
            if (transaction != null)
            {
                transaction.Product.Vendor = unitOfWork.ExtendedUserManager.GetById(transaction.Product.FK_VendorId);
                return PartialView("_TransactionRowPartial", transaction);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult AjaxPage( string order , string searchString , int pageNumber)
        {
            var customerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            PaginatedList<Transaction> transactions = unitOfWork.Transactions.GetPagedCustomer(customerId, order, searchString , pageNumber);
            return PartialView("_TransactionPartial" , transactions);
        }
    }
}