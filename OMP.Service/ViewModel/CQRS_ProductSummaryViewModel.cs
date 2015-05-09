using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Service.ViewModel
{
    public class CQRS_ProductSummaryViewModel
    {
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductTitle { get; set; }
        public string ProductShortDescription { get; set; }
        public string ProductDescription { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public int ProductStatus { get; set; }
        public int Popularity { get; set; }
        public decimal Rating { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }

        public string UserName { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
