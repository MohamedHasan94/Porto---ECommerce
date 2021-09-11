using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CEI_MVC_CORE_Proj.Models.ViewModels
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }
        public PaginatedList<Category> Categories { get; set; }
    }
}
