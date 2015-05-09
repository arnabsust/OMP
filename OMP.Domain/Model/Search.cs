using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Domain.Model
{
    public class Search
    {
        [Key]
        public int SearchID { get; set; }
        public int ProductID { get; set; }
        public int Hit { get; set; }
        public string UserName { get; set; }
        public DateTime SearchDate { get; set; }
    }
}
