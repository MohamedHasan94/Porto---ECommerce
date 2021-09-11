using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CEI_MVC_CORE_Proj.Core.Managers;
using CEI_MVC_CORE_Proj.Data;
using CEI_MVC_CORE_Proj.Models;

using Microsoft.AspNetCore.Identity;

namespace ITI.MVC.Core.Day2.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<AppUser> userManager;

        public UnitOfWork(UserManager<AppUser> _userManager, ApplicationDbContext _context)
        {
            userManager = _userManager;
            context = _context;
        }
        public UserManager<AppUser> UserManager { get { return userManager; } }



        private ProductManager productManager;
        public ProductManager Products
        {
            get
            {
                if (productManager == null)
                {
                    productManager = new ProductManager(context);
                }
                return productManager;
            }
        }

        private CategoryManager categoryManager;
        public CategoryManager Categories
        {
            get
            {
                if (categoryManager == null)
                {
                    categoryManager = new CategoryManager(context);
                }
                return categoryManager;
            }
        }


        private ExtendedUserManager extendedUserManager;
        public ExtendedUserManager ExtendedUserManager
        {
            get
            {
                if (extendedUserManager == null)
                {
                    extendedUserManager = new ExtendedUserManager(context);
                }
                return extendedUserManager;
            }
        }

        private RequestManager requestManager;
        public RequestManager Requests
        {
            get
            {
                if (requestManager == null)
                {
                    requestManager = new RequestManager(context);
                }
                return requestManager;
            }
        }

        private TagManager tagManager;
        public TagManager Tags
        {
            get
            {
                if (tagManager == null)
                {
                    tagManager = new TagManager(context);
                }
                return tagManager;
            }
        }
        private TransactionManager transactionManager;

        public TransactionManager Transactions
        {
            get
            {
                if (transactionManager == null)
                {
                    transactionManager = new TransactionManager(context);
                }
                return transactionManager;
            }
        }
    }
}
