using System.Collections.Generic;
using System.Linq;
using PetaPoco;
using PetaPoco.Providers;

namespace ProductRatings.Persistence
{
    public class DatabaseBackend : IPersistenceBackend
    {
        private readonly Database _db;

        public DatabaseBackend(string fileName)
        {
            _db = new Database($"Data Source={fileName};Version=3", new SQLiteDatabaseProvider());
        }

        public void Persist()
        {
        }

        public void Load()
        {
            InitializeSchema();
        }

        public IEnumerable<Product> AllProducts
        {
            get
            {
                var products = _db.Query<dynamic>("SELECT * FROM Product");

                foreach (var product in products)
                    yield return new Product(this, product.Name);
            }
        }

        public Product Get(string productName)
        {
            var product = _db.Fetch<dynamic>("SELECT * FROM Product");

            return new Product(this, product.First().Name);
        }

        public Product AddProduct(string name)
        {
            _db.Insert("Product", new {Name = name});

            return new Product(this, name);
        }

        public IEnumerable<int> AllRatingsFor(string productName)
        {
            return _db.Query<int>(Sql.Builder
                .Append("SELECT Stars")
                .Append("FROM Rating")
                .Append("WHERE ProductName = @0", productName));
        }

        public void RateProductCalled(string name, int numberOfStars)
        {
            _db.Insert("Rating", new {ProductName = name, Stars = numberOfStars});
        }

        public double AverageRatingFor(string productName)
        {
            var numberOfRatings =
                _db.ExecuteScalar<int>(Sql.Builder
                    .Append("SELECT COUNT(*)")
                    .Append("FROM Rating")
                    .Append("WHERE ProductName = @0", productName));

            if (numberOfRatings == 0)
                return 0;

            return _db.ExecuteScalar<double>(Sql.Builder
                .Append("SELECT AVG(Stars)")
                .Append("FROM Rating")
                .Append("WHERE ProductName = @0", productName));
        }

        public void RemoveAll()
        {
            _db.Execute("DELETE FROM Rating;");
            _db.Execute("DELETE FROM Product;");
        }

        private void InitializeSchema()
        {
            if (TableExists("Product"))
                return;

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
        }

        private bool TableExists(string tableName)
        {
            var result =
                _db.Fetch<dynamic>("SELECT name As Name FROM sqlite_master WHERE type=\'table\' AND name=\'Product\';");
            return result.Count == 1 && result.First().Name.Equals(tableName);
        }
    }
}