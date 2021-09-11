using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CEI_MVC_CORE_Proj.Models
{
    //you can retrieve vendor data from product
    

    //( Id , PaymentMethod , DateTime  ) 
    //navigation properties < Product   >
    [Table("Transaction")]
    public class Transaction
    {
        public int Id { get; set; }


        public Methods PaymentMethod { get; set; }

        public DateTime DateTime { get; set; }

        [Display(Name ="Product")]
        public int FK_ProductId { get; set; }

        [Display(Name = "Customer")]
        public string FK_CustomerId { get; set; }

        public TransactionStatus Status { get; set; }

        public int Quantity { get; set; }

        [Display(Name = "Checkout Id")]
        public string CheckOutId { get; set; }

        ///////////////////////////////////////////////////////////////
        #region Navigation Properties
        [ForeignKey("FK_ProductId")]
        public Product Product { get; set; }

        [ForeignKey("FK_CustomerId")]
        public AppUser Customer { get; set; }

        #endregion
    }
    public enum TransactionStatus {
        Pending, Shipped, Completed, Cancelled , Rejected
    }
}
