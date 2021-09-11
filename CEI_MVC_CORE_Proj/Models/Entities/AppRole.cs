using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CEI_MVC_CORE_Proj.Models
{   
    public class AppRole : IdentityRole
    {
        public virtual ICollection<UserRoleRel> UserRoleRel { get; set; }
    }
}
