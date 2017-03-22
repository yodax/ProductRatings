using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ProductRatings.Persistence;

namespace ProductRatings.Test
{
    [TestFixture]
    public abstract class WhenPersisting
    {
        [SetUp]
        public virtual void SetUp()
        {
            OriginalCatalog = new Catalog(GetBackend());

            OriginalCatalog.AddProductCalled("Product 1");
            OriginalCatalog.Get("Product 1").Rate(5);
            OriginalCatalog.Get("Product 1").Rate(1);
            OriginalCatalog.AddProductCalled("Product 2").Rate(4);
            OriginalCatalog.AddProductCalled("Product 3");

            OriginalCatalog.Persist();

            RestoredCatalog = new Catalog(GetBackend());
        }

        protected abstract IPersistenceBackend GetBackend();

        protected Catalog OriginalCatalog;
        protected Catalog RestoredCatalog;

        [Test]
        public void ItemsShouldBeRemovable()
        {
            RestoredCatalog.RemoveAll();
            RestoredCatalog.AllProducts.Should().BeEmpty();
            RestoredCatalog.Persist();
        }

        [Test]
        public void AllPersistedDataShouldBeEquivalent()
        {
            RestoredCatalog.AllProducts.ToList().ShouldAllBeEquivalentTo(OriginalCatalog.AllProducts.ToList());
        }

        [Test]
        public void AllProductsShouldBePresent()
        {
            RestoredCatalog.AllProducts.Count().Should().Be(OriginalCatalog.AllProducts.Count());
        }

        [Test]
        public void RatingsShouldBeStored()
        {
            RestoredCatalog.AllRatingsFor("Product 1").Count().Should().Be(2);
            RestoredCatalog.AllRatingsFor("Product 2").Count().Should().Be(1);
            RestoredCatalog.AllRatingsFor("Product 3").Count().Should().Be(0);
        }

        [Test]
        public void AverageRatingShouldBePresent()
        {
            RestoredCatalog.Get("Product 1").AverageRating.Should().Be(3);
        }

        [Test]
        public void ProductNameShouldBeStore()
        {
            RestoredCatalog.Get("Product 1").Should().NotBeNull();
        }
    }
}