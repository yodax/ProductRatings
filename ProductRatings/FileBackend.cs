using System.IO;
using System.Xml.Serialization;

namespace ProductRatings
{
    public class FileBackend : IPersistenceBackend
    {
        private readonly string _fileName;
        private readonly XmlSerializer _serializer;

        public FileBackend(string fileName)
        {
            _fileName = fileName;
            _serializer = new XmlSerializer(typeof(Catalog));
        }

        public void Persist(Catalog catalog)
        {
            using (var fileStream = File.OpenWrite(_fileName))
            {
                _serializer.Serialize(fileStream, catalog);
            }
        }

        public Catalog Load()
        {
            using (var fileStream = File.OpenRead(_fileName))
            {
                return (Catalog) _serializer.Deserialize(fileStream);
            }
        }
    }
}