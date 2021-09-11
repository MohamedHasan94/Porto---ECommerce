using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CEI_MVC_CORE_Proj.Models
{
    //(Id , Name , Description , Price , Quantity )
    //navigation properties < AppUser , |Category| , List<tag> , List<PaymentMethod> , Sale , List<ProductImage> >
    //If vendor requests a new category , will the product category be null until the admin responds?
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50,ErrorMessage ="Name must be less than 50 characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(256 , ErrorMessage ="Description must be less than 256 characters")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0,double.MaxValue,ErrorMessage ="Price can't be negative")]
        public double Price { get; set; }

        [Display(Name = "Price After Sale")]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Price can't be negative")]
        public double OfferPrice { get; set; }

        public uint Quantity { get; set; }

        [Display(Name = "Vendor")]
        public string FK_VendorId { get; set; }

        //Nullable?
        [Display(Name = "Category")]
        public int FK_CategoryId { get; set; }


        // ///////////////////////////////////////
        #region Navigation Properties
        [ForeignKey("FK_VendorId")]
        public virtual AppUser Vendor { get; set; }

        [ForeignKey("FK_CategoryId")]
        public virtual Category Category { get; set; }

        public virtual List<ProductTagRel> ProductTagRels { get; set; }

        [Display(Name = "Payment Methods")]
        public virtual List<PaymentMethod> PaymentMethod { get; set; }

        public virtual List<ProductImage> Images { get; set; }

        #endregion
    }
}
