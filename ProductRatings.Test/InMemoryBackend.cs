namespace ProductRatings.Test
{
    public class InMemoryBackend : IPersistenceBackend
    {
        private Catalog _catalog;

        public void Persist(Catalog catalog)
        {
            _catalog = catalog;
        }

        public Catalog Load()
        {
            return _catalog;
        }
    }
}