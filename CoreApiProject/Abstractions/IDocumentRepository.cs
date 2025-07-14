using CoreApiProject.Models;

namespace CoreApiProject.Abstractions
{
    public interface IDocumentRepository
    {
        IEnumerable<Document> GetAll();
        bool Add(Document document);
        bool Update(Document document);
    }
}
