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
    public class RequestManager : Repository<RequestToAdmin, ApplicationDbContext>
    {
        private ApplicationDbContext context;
        public RequestManager(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public override RequestToAdmin GetById(params object[] Id)
        {
            return context.Requests.Include(p => p.User)
                                   .FirstOrDefault(x => x.Id == (int)Id[0]); //retruns null if not found
        }


        public void SetStatus(RequestToAdmin req, RequestStatus status)
        {
            req.Status = status;
            context.SaveChanges();
        }

        public override PaginatedList<RequestToAdmin> GetPaged(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var Records = from item in context.Requests
                          select item;

            if (!String.IsNullOrEmpty(searchString))

            {
                // Records = Records.Where(p => p.Data.Contains((string)currentFilter));
                switch (searchString)
                {
                    case "Pending":
                        Records = Records.Where(p => p.Status == RequestStatus.Pending);
                        break;
                    case "Accepted":
                        Records = Records.Where(p => p.Status == RequestStatus.Accepted);
                        break;
                    case "Rejected":
                        Records = Records.Where(p => p.Status == RequestStatus.Rejected);
                        break;
                    case "Vendor":
                        Records = Records.Where(p => p.Type == RequestType.RequestVendorRole);
                        break;
                    case "Category":
                        Records = Records.Where(p => p.Type == RequestType.AddNewCategory);
                        break;
                }
            }

            switch (sortOrder)
            {
                case "idDsc":
                    Records = Records.OrderByDescending(s => s.Id);
                    break;
                case "statusAsc":
                    Records = Records.OrderBy(s => s.Status);
                    break;
                case "statusDsc":
                    Records = Records.OrderByDescending(s => s.Status);
                    break;
                case "typeAsc":
                    Records = Records.OrderBy(s => s.Type);
                    break;
                case "typeDsc":
                    Records = Records.OrderByDescending(s => s.Type);
                    break;
                default:
                    Records = Records.OrderBy(s => s.Id);
                    break;
            }

            int pageSize = 12;
            return PaginatedList<RequestToAdmin>.Create(Records.AsNoTracking().Include(x => x.User), pageNumber ?? 1, pageSize);
        }

    }
}
