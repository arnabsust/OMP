using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMP.API.Models
{
    public class UploadDataModel
    {
        public string testString1 { get; set; }
        public string testString2 { get; set; }
    }
    public class UploadDataModelForProduct
    {
        public string testString1 { get; set; }
        public string testString2 { get; set; }

        public int productID { get; set; }
    }
}