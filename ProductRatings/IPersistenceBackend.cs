namespace ProductRatings
{
    public interface IPersistenceBackend
    {
        void Persist(Catalog catalog);
        Catalog Load();
    }
}