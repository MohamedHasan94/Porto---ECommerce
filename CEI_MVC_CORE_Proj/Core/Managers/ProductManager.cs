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
    public class ProductManager : Repository<Product, ApplicationDbContext>
    {
        private ApplicationDbContext context;
        public ProductManager(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public override Product GetById(params object[] Id)
        {
            return context.Products.Include(p => p.Category)
                                   .Include(p => p.Images)
                                   .Include(p => p.PaymentMethod)
                                   .Include(p => p.ProductTagRels)
                                   .Include(p => p.Vendor)
                                   .FirstOrDefault(x => x.Id == (int)Id[0]); //retruns null if not found
        }
        
        public PaginatedList<Product> GetPagedVendor(string vendorId , string searchString, int[] CategoriesIds, string sortOrder, int pageNumber)
        {
            var records = context.Products.Where(p => p.FK_VendorId == vendorId).Include(p => p.Category);
            return GetPaged(records, searchString, CategoriesIds, sortOrder , pageNumber , 15);
        }

        public PaginatedList<Product> GetPagedClient(string searchString, int[] CategoriesIds, int? tagId, string sortOrder, int pageNumber)
        {
            var records = from item in context.Products select item;
            if(tagId != null)
                records = records.Where(p => (p.ProductTagRels.Select(r => r.FK_TagId == tagId ? r.Product : null).FirstOrDefault(l => l != null)) != null);
            /*p.ProductTagRels.Select(r => r.FK_TagId == tagId ? r.Product : null) returns a list of lists of products that contains nulls 
            so FirstOrDefault(l => l != null) gets the non null product if the inner list contains a value or null if the list elements are nulls 
            the last (!= null ) removes the nulls*/
            return GetPaged(records, searchString, CategoriesIds, sortOrder, pageNumber , 9);
        }
        private PaginatedList<Product> GetPaged(IQueryable<Product> records, string searchString, int[] CategoriesIds ,
            string sortOrder ,  int pageNumber, int pageSize)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                records = records.Where(s => s.Name.ToLower().Contains(searchString.ToLower())
                                       || s.Description.ToLower().Contains(searchString.ToLower()));
            }

            if (CategoriesIds.Length != 0)
                records = records.Where(p => CategoriesIds.Contains(p.FK_CategoryId));
            
            switch (sortOrder)
            {
                case "nameDsc":
                    records = records.OrderByDescending(s => s.Name);
                    break;
                case "priceAsc":
                    records = records.OrderBy(s => s.OfferPrice);
                    break;
                case "priceDsc":
                    records = records.OrderByDescending(s => s.OfferPrice);
                    break;
                default:
                    records = records.OrderBy(s => s.Name);
                    break;
            }

            //Default page size is 9
            return PaginatedList<Product>.Create(records.Include(r => r.Images).AsNoTracking(), pageNumber , pageSize);
        }

        public List<PaymentMethod> EditPaymentMethods(int id, IList<Methods> newMethods)
        {
            context.PaymentMethods.RemoveRange(context.PaymentMethods.Where(m => m.FK_ProductId == id).ToList());
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>();
            foreach (var method in newMethods)
            {
                paymentMethods.Add(new PaymentMethod { Method = method });
            }
            return paymentMethods;
        }

        public List<ProductImage> GetProductImages(int id)
        {
            return context.ProductImages.Where(i => i.FK_ProductId == id).ToList();
        }

        public void DeleteProductTags(int id)
        {
            context.ProductTagRels.RemoveRange(context.ProductTagRels.Where(r => r.FK_ProductId == id).ToList());
        }

        public void DeleteProductImage(int id , string path)
        {
            context.ProductImages.Remove(context.ProductImages.FirstOrDefault(i => i.FK_ProductId == id && i.Path == path));
        }

        public List<Product> GetByIdList(List<int>IdList){

           return context.Products.Where(x => IdList.Contains(x.Id)).Include(x => x.Images).Include(x => x.PaymentMethod).ToList();
        }
    }
}