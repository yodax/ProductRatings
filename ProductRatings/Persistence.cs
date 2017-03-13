namespace ProductRatings
{
    public class Persistence
    {
        private readonly IPersistenceBackend _backend;

        public Persistence(IPersistenceBackend backend)
        {
            _backend = backend;
        }

        public void Persist(Catalog catalog)
        {
            _backend.Persist(catalog);
        }

        public Catalog Load()
        {
            return _backend.Load();
        }
    }
}