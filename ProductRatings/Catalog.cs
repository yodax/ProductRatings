using System.Collections.Generic;
using System.Linq;

namespace ProductRatings
{
    public class Catalog : List<Product>
    {
        public Product Get(string productName)
        {
            return this.Single(p => p.Name.Equals(productName));
        }
    }
}