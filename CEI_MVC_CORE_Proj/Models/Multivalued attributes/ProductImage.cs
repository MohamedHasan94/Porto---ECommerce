using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CEI_MVC_CORE_Proj.Models
{
    [Table("ProductImage")]
    public class ProductImage
    {
        [Key]
        [Column(Order =0)]
        [Display(Name = "Product")]
        public int FK_ProductId { get; set; }

        [Key]
        [Column(Order =1)]
        [MaxLength(100)]
        public string Path { get; set; }

        ///////////////////////////////////////////////////////////////
        #region Navigation Properties
        [ForeignKey("FK_ProductId")]
        public Product Product { get; set; }
        #endregion
    }
}
