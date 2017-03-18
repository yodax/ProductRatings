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
            //_persistence = new Persistence(GetBackend());


            //var ratedProduct = new Product { Name = "Product 1" };
            //ratedProduct.Rate(3);

            //var ratedProduct2 = new Product { Name = "Product 2" };
            //ratedProduct2.Rate(4);

            //var unRatedProduct3 = new Product { Name = "Product 3" };

            //_originalCatalog = new Catalog { ratedProduct, ratedProduct2, unRatedProduct3 };



            //_persistence.Persist(_originalCatalog);

            //_restoredCatalog = _persistence.Load();

            _originalCatalog = new Catalog(GetBackend());

            _originalCatalog.AddProductCalled("Product 1");
            _originalCatalog.Get("Product 1").Rate(5);
            _originalCatalog.Get("Product 1").Rate(1);
            _originalCatalog.AddProductCalled("Product 2").Rate(4);
            _originalCatalog.AddProductCalled("Product 3");

            _originalCatalog.Persist();

            _restoredCatalog = new Catalog(GetBackend());
        }

        protected abstract IPersistenceBackend GetBackend();

        private Catalog _originalCatalog;
        private Catalog _restoredCatalog;

        [Test]
        public void AllPersistedDataShouldBeEquivalent()
        {
            _restoredCatalog.AllProducts.ToList().ShouldAllBeEquivalentTo(_originalCatalog.AllProducts.ToList());
        }

        [Test]
        public void OnANonEmptyStorage()
        {
            _restoredCatalog.AddProductCalled("Secondary save product").Rate(1);

            _restoredCatalog.Persist();

            var secondaryCatalog = new Catalog(GetBackend());

            secondaryCatalog.Get("Secondary save product").Should().NotBeNull();
            secondaryCatalog.AllProducts.Count().Should().Be(4);
        }

        [Test]
        public void AllInstancesOfTheStorageShouldReceiveTheUpdates()
        {
            _restoredCatalog.AddProductCalled("Secondary save product").Rate(1);

            _restoredCatalog.Persist();
    
            var secondaryCatalog = new Catalog(GetBackend());

            secondaryCatalog.AllProducts.ToList().ShouldAllBeEquivalentTo(_originalCatalog.AllProducts.ToList());
    }

        [Test]
        public void AllProductsShouldBePresent()
        {
            _restoredCatalog.AllProducts.Count().Should().Be(_originalCatalog.AllProducts.Count());
        }

        [Test]
        public void RatingsShouldBeStored()
        {
            _restoredCatalog.AllRatingsFor("Product 1").Count().Should().Be(2);
            _restoredCatalog.AllRatingsFor("Product 2").Count().Should().Be(1);
            _restoredCatalog.AllRatingsFor("Product 3").Count().Should().Be(0);
        }

        [Test]
        public void AverageRatingShouldBePresent()
        {
            _restoredCatalog.Get("Product 1").AverageRating.Should().Be(3);
        }

        [Test]
        public void ProductNameShouldBeStore()
        {
            _restoredCatalog.Get("Product 1").Should().NotBeNull();
        }
    }
}