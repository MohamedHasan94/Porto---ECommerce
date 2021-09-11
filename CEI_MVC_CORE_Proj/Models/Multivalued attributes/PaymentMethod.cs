using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CEI_MVC_CORE_Proj.Models
{
    public enum Methods
    {
        OnDelivery,
        Visa,
        MasterCard,
        Paypal,
        Cheque,
        DirectBankTransfer
    }

    [Table("Product_PaymentMethod")]
    public class PaymentMethod
    {
        [Key]
        [Column(Order =1)]
        [Display(Name ="Product")]
        public int FK_ProductId { get; set; }

        [Key]
        [Column(Order =2)]
        public Methods Method { get; set; }

        ///////////////////////////////////////////////////////////////

        #region Navigation Properties
        [ForeignKey("FK_ProductId")]
        public Product Product { get; set; }
        #endregion
    }
}