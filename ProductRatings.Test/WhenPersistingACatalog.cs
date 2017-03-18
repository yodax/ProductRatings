using NUnit.Framework;

namespace ProductRatings.Test
{
    [TestFixture]
    internal class WhenPersistingInMemory : WhenPersisting
    {
        protected override IPersistenceBackend GetBackend()
        {
            return new MemoryBackend();
        }
    }

    internal class MemoryBackend : IPersistenceBackend
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