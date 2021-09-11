using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CEI_MVC_CORE_Proj.Models
{


    //( FirstName , LastName , PhoneNumber , ProfilePicture , Role) 
    //<navigation properties List<Product> "if user is Vendor", List<request>  List<Transaction>  >
    public class AppUser : IdentityUser
    {

        [MaxLength(50),Display(Name ="First Name"), Required]
        public string FirstName { get; set; }

        [MaxLength(50), Display(Name = "Last Name"), Required]  
        public string LastName { get; set; }

        [MaxLength(100), Display(Name = "Profile Picture")] //Not Required
        public string ProfilePictureLink { get; set; }

        public virtual ICollection<UserRoleRel> UserRoleRel { get; set; }



        ///////////////////////////////////////////////////////////////
        #region Navigation Properties

        public virtual List<Product> Products { get; set; }
        
        public virtual List<RequestToAdmin> Requests { get; set; }

        public virtual List<Transaction> Transactions { get; set; }
        #endregion
    }
}
