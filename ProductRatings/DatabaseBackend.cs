using System.Linq;
using PetaPoco;

namespace ProductRatings
{
    public class DatabaseBackend : IPersistenceBackend
    {
        private readonly Database _db;

        public DatabaseBackend(string fileName)
        {
            _db = new Database($"Data Source={fileName};Version=3", new SQLiteDatabaseProvider());
        }

        public void Persist(Catalog catalog)
        {
            _db.Execute(Sql.Builder
                .Append("CREATE TABLE Product (")
                .Append("Name VARCHAR(250)")
                .Append(");")   
                );

            _db.Execute(Sql.Builder
                .Append("CREATE TABLE Rating (")
                .Append("ProductName VARCHAR(250),")
                .Append("Stars int")
                .Append(");")
                );

            foreach (var product in catalog)
            {
                _db.Insert(product);
                foreach (var productRating in product.Ratings)
                {
                    _db.Insert("Rating", "ProductName", new { ProductName = product.Name, Stars = productRating});
                }
            }
        }

        public Catalog Load()
        {
            var catalog = new Catalog();
            var products = _db.Fetch<Product>("SELECT * FROM Product");
            var allRatings = _db.Fetch<dynamic>("SELECT * FROM Rating");
            foreach (var rating in allRatings)
            {
                var selectedProduct = products.Single(p => p.Name.Equals(rating.ProductName));
                selectedProduct.Ratings.Add(rating.Stars);
            }
            catalog.AddRange(products);
            return catalog;
        }
    }
}