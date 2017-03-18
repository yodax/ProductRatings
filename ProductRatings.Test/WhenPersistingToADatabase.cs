namespace ProductRatings.Test
{
    public class WhenPersistingToADatabase : WhenPersitingToAFile
    {
        protected override IPersistenceBackend GetBackend()
        {
            return new DatabaseBackend(FileName);
        }
    }
}