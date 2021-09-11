using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CEI_MVC_CORE_Proj.Models.ViewModels
{
    public class Order
    {
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public Methods PaymentMethod { get; set; }

    }
    public class CheckoutViewModel
    {
        public List<Product> Products { get; set; }
        public List<int> ProductIds { get; set; }
        public List<int> Qtys { get; set; }
    }
}
