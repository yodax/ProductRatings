using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ProductRatings.Persistence
{
    public class MemoryBackend : IPersistenceBackend
    {
        protected List<InternalProduct> InternalProducts;

        public MemoryBackend()
        {
            InternalProducts = new List<InternalProduct>();
        }

        public virtual void Load()
        {
        }

        public virtual void Persist()
        {
        }

        public IEnumerable<Product> AllProducts
        {
            get { return InternalProducts.Select(p => p.ToProduct()); }
        }

        public Product Get(string productName)
        {
            return GetInternalProduct(productName).ToProduct();
        }

        public Product AddProduct(string name)
        {
            var internalProduct = new InternalProduct(this, name);
            InternalProducts.Add(internalProduct);
            return internalProduct.ToProduct();
        }

        public IEnumerable<int> AllRatingsFor(string productName)
        {
            return GetInternalProduct(productName).Ratings;
        }

        public void RateProductCalled(string name, int numberOfStars)
        {
            GetInternalProduct(name).Ratings.Add(numberOfStars);
        }

        public double AverageRatingFor(string productName)
        {
            var ratings = GetInternalProduct(productName).Ratings;
            return ratings.Count == 0 ? 0 : ratings.Average();
        }

        public void RemoveAll()
        {
            InternalProducts.Clear();
        }

        private InternalProduct GetInternalProduct(string name)
        {
            return InternalProducts.Single(p => p.Name.Equals(name));
        }

        // Supress message for serialization
        [Serializable]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
        [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
        public class InternalProduct
        {
            private IPersistenceBackend _persistenceBackend;

            public InternalProduct(IPersistenceBackend backend, string name)
            {
                _persistenceBackend = backend;
                Name = name;
                Ratings = new List<int>();
            }

            public InternalProduct()
            {
                Ratings = new List<int>();
            }

            public string Name { get; set; }

            public List<int> Ratings { get; set; }

            public void SetBackend(IPersistenceBackend persistenceBackend)
            {
                _persistenceBackend = persistenceBackend;
            }

            public Product ToProduct()
            {
                return new Product(_persistenceBackend, Name);
            }
        }
    }
}