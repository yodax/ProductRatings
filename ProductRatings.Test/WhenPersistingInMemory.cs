using NUnit.Framework;

namespace ProductRatings.Test
{
    [TestFixture]
    internal class WhenPersistingInMemory : WhenPersisting
    {
        private MemoryBackend _memoryBackend;

        [SetUp]
        public override void SetUp()
        {
            _memoryBackend = new MemoryBackend();

            base.SetUp();
        }

        protected override IPersistenceBackend GetBackend()
        {
            return _memoryBackend;
        }
    }
}