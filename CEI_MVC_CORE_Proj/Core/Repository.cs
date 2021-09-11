using CEI_MVC_CORE_Proj.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.MVC.Core.Day2.Core
{
    public class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        private readonly TContext context;
        private readonly DbSet<TEntity> entities;

        public Repository(TContext context)
        {
            this.context = context;
            entities = context.Set<TEntity>();
        }
        public TEntity Add(TEntity entity)
        {
            entities.Add(entity);
            return context.SaveChanges() > 0 ? entity : null;
        }
        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entity)
        {
            entities.AddRange(entity);
            return context.SaveChanges() > 0 ? entity : null;
        }

        public virtual PaginatedList<TEntity> GetPaged(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return entities;
        }

        public virtual IList<TEntity> GetAllBind()
        {
            return entities.ToList();
        }

        public virtual TEntity GetById(params object[] id)
        {
            return entities.Find(id); //retruns null if not found
        }

        public bool Remove(TEntity entity)
        {
            if (entities.Contains(entity) == false) return false; // if ni
            entities.Remove(entity);
            context.SaveChanges();
            return true;
        }

        public virtual bool RemoveById(params object[] id)
        {
            TEntity ent = entities.Find(id);
            if (ent == null) return false;

            entities.Remove(ent);
            return context.SaveChanges() > 0;

        }

        public bool Update(TEntity entity)
        {
            if (!entities.Contains(entity)) return false;

            entities.Update(entity);
            return context.SaveChanges() > 0;
        }

     
    }
}
