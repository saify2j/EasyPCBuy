using System;
using System.Collections.Generic;

namespace FinalProjectDemo.Models.Product
{
    public partial class Rating
    {
        public int RateId { get; set; }
        public string ProductName { get; set; }
        public double RatingValue { get; set; }
    }
}
