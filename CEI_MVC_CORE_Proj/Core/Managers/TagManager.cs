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
    public class TagManager : Repository<Tag, ApplicationDbContext>
    {
        private ApplicationDbContext context;
        public TagManager(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public override Tag GetById(params object[] Id)
        {
            return context.Tags.Include(p => p.ProductTagRels)
                                   .FirstOrDefault(x => x.Id == (int)Id[0]); //retruns null if not found
        }

        public bool RemoveRange(IList<Tag> tags)
        {
            try
            {
                context.Tags.RemoveRange(tags);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public override PaginatedList<Tag> GetPaged(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var Records = from item in context.Tags
                          select item;
            if (!String.IsNullOrEmpty(searchString))
            {
                Records = Records.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "nameDsc":
                    Records = Records.OrderByDescending(s => s.Name);
                    break;
                default:
                    Records = Records.OrderBy(s => s.Name);
                    break;
            }


            int pageSize = 12;
            return PaginatedList<Tag>.Create(Records.AsNoTracking(), pageNumber ?? 1, pageSize);
        }
    }
}
