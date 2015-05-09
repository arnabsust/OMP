using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Domain.Model
{
    public class ProductReview
    {
        [Key]
        public int ProductReviewID { get; set; }
        public int ProductID { get; set; }
        public string CustomerName { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewedDate { get; set; }
    }
}
