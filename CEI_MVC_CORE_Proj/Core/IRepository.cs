
using CEI_MVC_CORE_Proj.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.MVC.Core.Day2.Core
{
    public interface IRepository<TEntity> 
       
    {
        IQueryable<TEntity> GetAll();
        PaginatedList<TEntity> GetPaged(string sortOrder, string currentFilter, string searchString, int? pageNumber);

        IList<TEntity> GetAllBind();
        TEntity GetById(params object[] id);
        TEntity Add(TEntity entity);
        bool Remove(TEntity entity);
        bool RemoveById(params object[] id);
        bool Update(TEntity entity);
    }
}
