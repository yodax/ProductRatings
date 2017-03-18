namespace ProductRatings
{
    public class Product
    {
        private readonly string _name;
        private readonly IPersistenceBackend _persistenceBackend;

        public Product(IPersistenceBackend persistenceBackend, string name)
        {
            _persistenceBackend = persistenceBackend;
            _name = name;
        }

        public double AverageRating => _persistenceBackend.AverageRatingFor(_name);

        public void Rate(int numberOfStars)
        {
            _persistenceBackend.RateProductCalled(_name, numberOfStars);
        }
    }
}