using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ProductRatings.Persistence;

namespace ProductRatings.Test
{
    public class WhenPersistingToADatabase : WhenPersitingToAFile
    {
        protected override IPersistenceBackend GetBackend()
        {
            return new DatabaseBackend(FileName);
        }

        [Test]
        public void OnANonEmptyStorage()
        {
            RestoredCatalog.AddProductCalled("Secondary save product").Rate(1);

            RestoredCatalog.Persist();

            var secondaryCatalog = new Catalog(GetBackend());

            secondaryCatalog.Get("Secondary save product").Should().NotBeNull();
            secondaryCatalog.AllProducts.Count().Should().Be(4);
        }

        [Test]
        public void AllInstancesOfTheStorageShouldReceiveTheUpdates()
        {
            RestoredCatalog.AddProductCalled("Secondary save product").Rate(1);

            RestoredCatalog.Persist();

            var secondaryCatalog = new Catalog(GetBackend());

            secondaryCatalog.AllProducts.ToList().ShouldAllBeEquivalentTo(OriginalCatalog.AllProducts.ToList());
        }
    }
}