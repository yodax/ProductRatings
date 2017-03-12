namespace ProductRatings
{
    public class Persistence
    {
        private readonly IPersistenceBackend _persistenceBackend;

        public Persistence(IPersistenceBackend persistenceBackend)
        {
            _persistenceBackend = persistenceBackend;
        }

        public void Persist(Catalog catalog)
        {
            _persistenceBackend.Persist(catalog);
        }

        public Catalog Load()
        {
            return _persistenceBackend.Load();
        }
    }
}