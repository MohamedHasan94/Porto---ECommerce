using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CEI_MVC_CORE_Proj.Models
{
    public enum RequestType
    {
        AddNewCategory,
        RequestVendorRole,
        ReportProblem
    }

    public enum RequestStatus
    {
        Pending,
        Accepted,
        Rejected
    }


    //( Id , Type , Data , Status) 
    //<navigation properties AppUser  >
    [Table("RequestToAdmin")]
    public class RequestToAdmin
    {
        public int Id { get; set; }
        

        public RequestType Type { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name must be less than 100 characters")]
        public string Data { get; set; }

        public RequestStatus Status { get; set; }
        

        [Display(Name = "User")]
        public string FK_UserId { get; set; }

        ///////////////////////////////////////////////////////////////
        #region Navigation Properties
        [ForeignKey("FK_UserId")]
        public virtual AppUser User { get; set; }
        #endregion
    }
}
