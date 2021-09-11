using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CEI_MVC_CORE_Proj.Models
{
    [Table("Product_Tag")]
    public class ProductTagRel
    {
        public int FK_ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int FK_TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
