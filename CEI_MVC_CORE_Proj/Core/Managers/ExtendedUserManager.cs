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
    public class ExtendedUserManager : Repository<AppUser, ApplicationDbContext>
    {

        private ApplicationDbContext context;
        public ExtendedUserManager (ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public PaginatedList<AppUser> GetPaged(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var Records = context.Users.Include(x => x.UserRoleRel);

            if (!String.IsNullOrEmpty(searchString))
            {
                Records = Records.Where(s => s.FirstName.Contains(searchString) 
                                      || s.LastName.Contains(searchString) 
                                      || s.Email.Contains(searchString) 
                                      || s.UserName.Contains(searchString)).Include(x => x.UserRoleRel);
            }

            switch (sortOrder)
            {
                case "nameDsc":
                    Records = Records.OrderByDescending(s => s.UserName).Include(x => x.UserRoleRel);
                    break;
                default:
                    Records = Records.OrderBy(s => s.UserName).Include(x => x.UserRoleRel);
                    break;
            }
            int pageSize = 12;
            return PaginatedList<AppUser>.Create(Records.AsNoTracking(), pageNumber ?? 1, pageSize);
        }

    }
}
