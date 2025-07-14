using CoreApiProject.Abstractions;

namespace CoreApiProject.Models.Repository
{
    public class InMemoryDocumentRepository : IDocumentRepository
    {
        private static readonly List<Document> _documents = new List<Document>
        {
            new Document
            {
                Id = 1,
                Tags = "example, sample",
                Data = new DocumentData { Data = "Sample data", Optional = "Optional info" }
            },
            new Document
            {
                Id = 2,
                Tags = "test, demo",
                Data = new DocumentData { Data = "Test data", Optional = null }
            }
        };

        public IEnumerable<Document> GetAll()
        {
            return _documents;
        }

        public bool Add(Document document)
        {
            if (document == null || _documents.Any(d => d.Id == document.Id))
            {
                return false; // Invalid document or duplicate ID
            }

            document.Id = _documents.Count > 0 ? _documents.Max(d => d.Id) + 1 : 1;
            _documents.Add(document);

            return true;
        }

        public bool Update(Document document)
        {
            if (document == null || !_documents.Any(d => d.Id == document.Id))
            {
                return false; // Invalid document or ID not found
            }

            var existingDocument = _documents.First(d => d.Id == document.Id);
            existingDocument.Tags = document.Tags;
            existingDocument.Data = document.Data;

            return true;
        }
    }
}
