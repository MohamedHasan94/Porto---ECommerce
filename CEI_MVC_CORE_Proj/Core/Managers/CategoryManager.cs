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
    public class CategoryManager: Repository<Category,ApplicationDbContext>
    {
        private ApplicationDbContext context;
        public CategoryManager(ApplicationDbContext context ) :base(context)
        {
            this.context = context;
        }

        public override Category GetById(params object[] Id)
        {
            return context.Categories.Include(x => x.Products)
                                     .FirstOrDefault(x=>x.Id ==(int)Id[0]); //retruns null if not found
        }
        public PaginatedList<Category> GetPaged(string sortOrder, string  searchString, int pageNumber)
        {
            var records = from item in context.Categories.Include(c => c.Products)
                          select item;
            if (!String.IsNullOrEmpty(searchString))
            {
                records = records.Where(s => s.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "productsAsc":
                    records = records.OrderBy(c => c.Products.Count);
                    break;

                case "productsDsc":
                    records = records.OrderByDescending(c => c.Products.Count);
                    break;

                case "nameDsc":
                    records = records.OrderByDescending(s => s.Name);
                    break;

                default:
                    records = records.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 12;
            return PaginatedList<Category>.Create(records.AsNoTracking(), pageNumber , pageSize);
        }

        public override bool RemoveById(params object[] id)
        {
            if ((int)id[0] == 15) //if category is "Others"
                return false;

            //Move category products to "Others" before deleting the category
            Category category = context.Categories.Find(id);
            if (category == null) return false;
            List<Product> products = context.Products.Where(p => p.FK_CategoryId == category.Id).ToList();
            products.ForEach(p => { p.FK_CategoryId = 15; p.Category = null; });
            context.SaveChanges();
            
            context.Categories.Remove(category);
            return context.SaveChanges() > 0;
        }
    }
}
