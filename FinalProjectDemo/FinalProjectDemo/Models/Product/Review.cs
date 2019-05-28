using System;
using System.Collections.Generic;

namespace FinalProjectDemo.Models.Product
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public string ProductName { get; set; }
        public string UserName { get; set; }
        public string ReviewText { get; set; }
    }
}
