using NUnit.Framework;
using ProductRatings.Persistence;

namespace ProductRatings.Test
{
    [TestFixture]
    internal class WhenPersistingInMemory : WhenPersisting
    {
        private MemoryBackend _memoryBackend;

        [SetUp]
        public override void SetUp()
        {
            // The in memory backend requires the same instance for the tests
            _memoryBackend = new MemoryBackend();
            base.SetUp();
        }

        protected override IPersistenceBackend GetBackend()
        {
            return _memoryBackend;
        }
    }
}