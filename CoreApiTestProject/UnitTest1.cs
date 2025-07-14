using CoreApiProject.Models;
using CoreApiProject.Models.Repository;
using System.Reflection.Metadata;
using Xunit;

namespace CoreApiTestProject
{
    public class InMemoryDocumentRepositoryTests
    {
        [Fact]
        public void GetAll_InitialDocuments()
        {
            var repo = new InMemoryDocumentRepository();
            var documents = repo.GetAll().ToList();

            Assert.True(documents.Count >= 2);
            Assert.Contains(documents, d => d.Id == 1);
            Assert.Contains(documents, d => d.Id == 2);
        }

        [Fact]
        public void Add_NewDocumentWithUniqueId()
        {
            var repo = new InMemoryDocumentRepository();
            var newDoc = new CoreApiProject.Models.Document
            {
                Id = 0, // Id will be set by repo
                Tags = "new, doc",
                Data = new DocumentData { Data = "New data", Optional = "Extra" }
            };

            var result = repo.Add(newDoc);
            Assert.True(result);

            var allDocs = repo.GetAll().ToList();
            Assert.Contains(allDocs, d => d.Tags == "new, doc");
        }

        [Fact]
        public void Add_FailsForDuplicateId()
        {
            var repo = new InMemoryDocumentRepository();
            var duplicateDoc = new CoreApiProject.Models.Document
            {
                Id = 1,
                Tags = "duplicate",
                Data = new DocumentData { Data = "Dup", Optional = null }
            };

            var result = repo.Add(duplicateDoc);
            Assert.False(result);
        }

        [Fact]
        public void Update_UpdatesExistingDocument()
        {
            var repo = new InMemoryDocumentRepository();
            var doc = repo.GetAll().First();
            var updated = new CoreApiProject.Models.Document
            {
                Id = doc.Id,
                Tags = "updated, tags",
                Data = new DocumentData { Data = "Updated data", Optional = "Updated" }
            };

            var result = repo.Update(updated);
            Assert.True(result);

            var refreshed = repo.GetAll().First(d => d.Id == doc.Id);
            Assert.NotNull(refreshed.Data);
            Assert.Equal("updated, tags", refreshed.Tags);
            Assert.Equal("Updated data", refreshed.Data!.Data);
        }

        [Fact]
        public void Update_FailsForNonexistentDocument()
        {
            var repo = new InMemoryDocumentRepository();
            var nonExistent = new CoreApiProject.Models.Document
            {
                Id = 999,
                Tags = "none",
                Data = new DocumentData { Data = "none", Optional = null }
            };

            var result = repo.Update(nonExistent);
            Assert.False(result);
        }
    }
}