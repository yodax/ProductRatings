using System.Collections.Generic;

namespace ProductRatings.Persistence
{
    public interface IPersistenceBackend
    {
        void Persist();
        void Load();
        IEnumerable<Product> AllProducts { get; }
        Product Get(string productName);
        Product AddProduct(string name);
        IEnumerable<int> AllRatingsFor(string productName);
        void RateProductCalled(string name, int numberOfStars);
        double AverageRatingFor(string productName);
    }
}