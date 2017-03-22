using System.Collections.Generic;

namespace ProductRatings.Web.Models
{
    public class ProductOverview
    {
        public IEnumerable<ProductView> Products { get; set; }

        public class ProductView
        {
            public string Name { get; set; }
            public double AverageRating { get; set; }
        }
    }
}