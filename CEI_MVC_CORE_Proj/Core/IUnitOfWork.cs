using CEI_MVC_CORE_Proj.Core.Managers;
using CEI_MVC_CORE_Proj.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.MVC.Core.Day2.Core
{
    public interface IUnitOfWork
    {
        ExtendedUserManager ExtendedUserManager { get; }
        ProductManager Products { get; }
        CategoryManager Categories { get; }
        RequestManager Requests { get; }
        TagManager Tags { get; }
        TransactionManager Transactions { get; }
    }
}
