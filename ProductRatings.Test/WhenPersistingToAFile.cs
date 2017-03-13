using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace ProductRatings.Test
{
    [TestFixture]
    internal class WhenPersistingToAFile
    {
        private string _fileName;
        private Persistence _persistence;
        private Catalog _originalCatalog;


        [SetUp]
        public void SetUp()
        {
            _fileName = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".bin");

            _persistence = new Persistence(new FileBackend(_fileName));

            var product1 = new Product{Name = "Product 1"};
            product1.Rate(5);

            _originalCatalog = new Catalog {product1};
            _persistence.Persist(_originalCatalog);
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(_fileName);
        }

        [Test]
        public void AFileShouldBeCreated()
        {
            File.Exists(_fileName).Should().BeTrue();
        }

        [Test]
        public void PersistedDataShouldBeEquivalent()
        {
            var restoredCatalog = _persistence.Load();

            restoredCatalog.ShouldAllBeEquivalentTo(_originalCatalog);
        }
    }
}