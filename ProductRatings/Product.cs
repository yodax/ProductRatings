using ProductRatings.Persistence;

namespace ProductRatings
{
    public class Product
    {
        public string Name { get; }
        private readonly IPersistenceBackend _persistenceBackend;

        public Product(IPersistenceBackend persistenceBackend, string name)
        {
            _persistenceBackend = persistenceBackend;
            Name = name;
        }

        public double AverageRating => _persistenceBackend.AverageRatingFor(Name);

        public void Rate(int numberOfStars)
        {
            _persistenceBackend.RateProductCalled(Name, numberOfStars);
        }
    }
}