using System.Collections.Generic;
using System.Linq;
using ProductRatings.Persistence;

namespace ProductRatings
{
    public class Catalog
    {
        private readonly IPersistenceBackend _persistenceBackend;

        public Catalog(IPersistenceBackend persistenceBackend)
        {
            _persistenceBackend = persistenceBackend;
            _persistenceBackend.Load();
        }

        public IEnumerable<Product> AllProducts => _persistenceBackend.AllProducts;

        public IEnumerable<Product> TopTenRatedProducts => AllProducts.OrderByDescending(p => p.AverageRating).Take(10);

        public Product Get(string productName)
        {
            return _persistenceBackend.Get(productName);
        }

        public Product AddProductCalled(string product)
        {
            return _persistenceBackend.AddProduct(product);
        }

        public void Persist()
        {
            _persistenceBackend.Persist();
        }

        public IEnumerable<int> AllRatingsFor(string product)
        {
            return _persistenceBackend.AllRatingsFor(product);
        }

        public bool Contains(string productName)
        {
            return AllProducts.Any(p => p.Name.Equals(productName));
        }

        public void RemoveAll()
        {
            _persistenceBackend.RemoveAll();
        }
    }
}