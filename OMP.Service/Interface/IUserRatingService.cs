using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Service.Interface
{
    public interface IUserRatingService
    {
        void InsertUserRating(string userName, decimal rating, string ratingBy);
        decimal GetAverageUserRating(string userName); 
    }
}
