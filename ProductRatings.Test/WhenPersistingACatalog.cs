using FluentAssertions;
using NUnit.Framework;

namespace ProductRatings.Test
{
    [TestFixture]
    internal class WhenPersistingACatalog
    {
        [Test]
        public void TheProductsShouldBeStored()
        {
            var catalog = new Catalog {new Product {Name = "Product1"}};

            var persistence = new Persistence(new MemoryBackend());

            persistence.Persist(catalog);

            var restoredCatalog = persistence.Load();

            restoredCatalog.Get("Product1").Should().NotBeNull();
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