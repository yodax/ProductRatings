using FluentAssertions;
using NUnit.Framework;

namespace ProductRatings.Test
{
    [TestFixture]
    public class WhenPersistingACatalog
    {
        [TestCase]
        public void ACatalogShouldBeSave()
        {
            var catalog = new Catalog {new Product {Name = "Product Name"}};

            var persistence = new Persistence(new InMemoryBackend());
            persistence.Persist(catalog);

            var secondCatalog = persistence.Load();

            secondCatalog.Get("Product Name").Should().NotBeNull();
        }
    }
}