using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace ProductRatings.Test
{
    public abstract class WhenPersisting
    {
        [SetUp]
        public virtual void SetUp()
        {
            _persistence = new Persistence(GetBackend());

            _originalCatalog = TestCatalog.Create();

            _persistence.Persist(_originalCatalog);

            _restoredCatalog = _persistence.Load();
        }

        protected abstract IPersistenceBackend GetBackend();

        private Persistence _persistence;
        private Catalog _originalCatalog;
        private Catalog _restoredCatalog;

        [Test]
        public void AllPersistedDataShouldBeEquivalent()
        {
            _restoredCatalog.ShouldAllBeEquivalentTo(_originalCatalog);
        }

        [Test]
        public void AllProductsShouldBePresent()
        {
            _restoredCatalog.Count.Should().Be(_originalCatalog.Count);
        }

        [Test]
        public void RatingsShouldBeStored()
        {
            _restoredCatalog.First(p => p.Ratings.Any()).Should().NotBeNull();
        }

        [Test]
        public void AverageRatingShouldBePresent()
        {
            _restoredCatalog.First(p => p.AverageRating > 0).Should().NotBeNull();
        }

        [Test]
        public void ProductNameShouldBeStore()
        {
            _restoredCatalog.Get("Product 1").Should().NotBeNull();
        }
    }
}