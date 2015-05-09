using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Domain.Model
{
    public class UserRating
    {
        public int UserRatingID { get; set; }
        public string UserName { get; set; }
        public decimal Rating { get; set; }
        public string RatingBy { get; set; }
    }
}
