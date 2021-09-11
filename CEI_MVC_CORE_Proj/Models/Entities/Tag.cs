using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CEI_MVC_CORE_Proj.Models
{
    //( Id , Name ) 
    //navigation properties < List<Product>   >

    [Table("Tag")]
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name must be less than 50 characters")]
        public string Name { get; set; }


        ///////////////////////////////////////////////////////////////
        #region Navigation Properties
        public virtual List<ProductTagRel> ProductTagRels { get; set; }
        #endregion
    }
}
