using System.IO;
using System.Xml.Serialization;

namespace ProductRatings
{
    public class FilePersistenceBackend : IPersistenceBackend
    {
        private readonly string _fileName;
        private readonly XmlSerializer _xmlSerializer;

        public FilePersistenceBackend(string fileName)
        {
            _fileName = fileName;
            _xmlSerializer = new XmlSerializer(typeof(Catalog));
        }

        public void Persist(Catalog catalog)
        {
            using (var fileStream = File.OpenWrite(_fileName))
            {
                _xmlSerializer.Serialize(fileStream, catalog);
            }
        }

        public Catalog Load()
        {
            using (var fileStream = File.OpenRead(_fileName))
            {
                return (Catalog)_xmlSerializer.Deserialize(fileStream);
            }
        }
    }
}