using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using ProductRatings.Persistence;

namespace ProductRatings.Test
{
    [TestFixture]
    public class WhenPersitingToAFile : WhenPersisting
    {
        protected string FileName;

        [SetUp]
        public override void SetUp()
        {
            FileName = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".bin");

            base.SetUp();
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(FileName);
        }

        protected override IPersistenceBackend GetBackend()
        {
            return new FileBackend(FileName);
        }

        [Test]
        public void AFileShouldBeCreated()
        {
            File.Exists(FileName).Should().BeTrue();
        }
    }
}