using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ProductRatings.Persistence
{
    public class FileBackend : MemoryBackend
    {
        private readonly string _fileName;
        private readonly XmlSerializer _serializer;

        public FileBackend(string fileName)
        {
            _fileName = fileName;
            _serializer = new XmlSerializer(typeof(List<InternalProduct>));
        }

        public override void Persist()
        {
            using (var fileStream = File.OpenWrite(_fileName))
            {
                _serializer.Serialize(fileStream, InternalProducts);
            }
        }

        public override void Load()
        {
            if (!File.Exists(_fileName))
                return;

            using (var fileStream = File.OpenRead(_fileName))
            {
                InternalProducts = (List<InternalProduct>) _serializer.Deserialize(fileStream);

                // Wire up the persistence store
                foreach (var internalProduct in InternalProducts)
                    internalProduct.SetBackend(this);
            }
        }
    }
}