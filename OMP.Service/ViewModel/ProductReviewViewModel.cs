using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Service.ViewModel
{
    public class ProductReviewViewModel
    {
        public int ProductReviewID { get; set; }
        public int ProductID { get; set; }
        public string CustomerName { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewedDate { get; set; }
    }
}
