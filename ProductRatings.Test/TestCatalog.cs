namespace ProductRatings.Test
{
    public class TestCatalog
    {
        public static Catalog Create()
        {
            var ratedProduct = new Product {Name = "Product 1"};
            ratedProduct.Rate(3);

            var ratedProduct2 = new Product {Name = "Product 2"};
            ratedProduct2.Rate(4);

            var unRatedProduct3 = new Product {Name = "Product 3"};

            return new Catalog {ratedProduct, ratedProduct2, unRatedProduct3};
        }
    }
}