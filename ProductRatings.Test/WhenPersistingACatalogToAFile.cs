using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace ProductRatings.Test
{
    [TestFixture]
    public class WhenPersistingACatalogToAFile
    {
        private const string ProductNameInTheTest = "Product Name";
        private Catalog _catalog;
        private string _fileName;
        private Persistence _persistence;

        [SetUp]
        public void SetUp()
        {
            var product = new Product {Name = ProductNameInTheTest};
            product.Rate(5);

            _catalog = new Catalog {product};
            _fileName = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".bin");
            _persistence = new Persistence(new FilePersistenceBackend(_fileName));
            _persistence.Persist(_catalog);
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(_fileName);
        }

        [Test]
        public void AFileShouldBeCreated()
        {
            File.Exists(_fileName).Should().BeTrue(" file should exist on disk");
        }

        [Test]
        public void ProductsShouldBeStored()
        {
            var loadedCatalog = _persistence.Load();

            loadedCatalog.Get(ProductNameInTheTest).Should().NotBeNull();
        }

        [Test]
        public void RatingsShouldBeStored()
        {
            var loadedCatalog = _persistence.Load();

            loadedCatalog.Get(ProductNameInTheTest).AverageRating.Should().Be(5);
        }
    }
}