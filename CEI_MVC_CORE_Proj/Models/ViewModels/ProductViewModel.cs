using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CEI_MVC_CORE_Proj.Models.ViewModels
{
    public class ProductViewModel
    {
        public PaginatedList<Product> Products { get; set; }

        public Product Product { get; set; }

        public IList<Category> Categories { get; set; }

        public IList<Tag> Tags { get; set; }

        public IList<IFormFile> Image { get; set; }

        public IList<Tag> ProductTags { get; set; }

        [Display(Name ="Request a new category")]
        public string NewCategoryName { get; set; }
    }
}