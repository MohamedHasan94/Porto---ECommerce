using CEI_MVC_CORE_Proj.Data;
using CEI_MVC_CORE_Proj.Models;
using CEI_MVC_CORE_Proj.Models.ViewModels;
using ITI.MVC.Core.Day2.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CEI_MVC_CORE_Proj.Core.Managers
{
    public class TransactionManager : Repository<Transaction, ApplicationDbContext>
    {
        private ApplicationDbContext context;
        public TransactionManager(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public override Transaction GetById(params object[] Id)
        {
            return context.Transactions.Include(p => p.Customer)
                                       .Include(p => p.Product)
                                       .FirstOrDefault(x => x.Id == (int)Id[0]); //retruns null if not found
        }

        public Transaction ChangeStatus(int transactionId, TransactionStatus newStatus)
        {
            Transaction transaction = context.Transactions.Include(t => t.Customer).Include(t => t.Product)
                .FirstOrDefault(t => t.Id == transactionId);

            if (transaction != null)
            {
                if (newStatus == TransactionStatus.Shipped)
                {
                    if (transaction.Quantity <= transaction.Product.Quantity)
                        transaction.Product.Quantity -= (uint)transaction.Quantity;
                    else
                        return null;
                }
                transaction.Status = newStatus;
                context.SaveChanges();
                return transaction;
            }
            else
                return null;
        }
        public PaginatedList<Transaction> GetPagedAdmin(string sortOrder, string searchString, int pageNumber)
        {
            var records = from item in context.Transactions.Include(t => t.Product.Vendor).Include(t => t.Customer)
                          select item;
            return GetPaged(records, sortOrder, searchString, pageNumber);
        }

        public PaginatedList<Transaction> GetPagedVendor(string vendorId, string sortOrder, string searchString, int pageNumber)
        {
            var records = from item in context.Transactions.Include(t => t.Product).Include(t => t.Customer)
                          .Where(t => t.Product.FK_VendorId == vendorId)
                          select item;
            return GetPaged(records, sortOrder, searchString, pageNumber);
        }

        public PaginatedList<Transaction> GetPagedCustomer(string customerId, string sortOrder, string searchString, int pageNumber)
        {
            var records = from item in context.Transactions.Include(t => t.Product.Vendor)
                          .Where(t => t.FK_CustomerId == customerId)
                          select item;
            return GetPaged(records, sortOrder, searchString, pageNumber);
        }

        private PaginatedList<Transaction> GetPaged(IQueryable<Transaction> records, string sortOrder, string searchString, int pageNumber)
        {

            if (!String.IsNullOrEmpty(searchString))
            {
                records = records.Where(s => s.Product.Name.ToLower().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "PriceAsc":
                    records = records.OrderBy(s => s.Quantity * s.Product.OfferPrice);
                    break;

                case "PriceDsc":
                    records = records.OrderByDescending(s => s.Quantity * s.Product.OfferPrice);
                    break;

                case "TimeAsc":
                    records = records.OrderBy(s => s.DateTime);
                    break;

                case "TimeDsc":
                    records = records.OrderByDescending(s => s.DateTime);
                    break;

                default:
                    records = records.OrderBy(s => (int)s.Status);
                    break;
            }

            int pageSize = 12;
            return PaginatedList<Transaction>.Create(records.AsNoTracking(), pageNumber, pageSize);
        }
    }
}