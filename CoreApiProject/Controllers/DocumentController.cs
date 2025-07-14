using CoreApiProject.Abstractions;
using CoreApiProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreApiProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository _documentsRepository;

        public DocumentController(IDocumentRepository documentRepository)
        {
            _documentsRepository = documentRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_documentsRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Post([FromBody] Document document)
        {
            if (document == null || document.Id <= 0 || string.IsNullOrEmpty(document.Tags))
            {
                return BadRequest("Invalid document data.");
            }

            bool isAdded = _documentsRepository.Add(document);

            if (!isAdded)
            {
                return BadRequest("Document with the same ID already exists.");
            }

            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody] Document document)
        {
            if (document == null || document.Id <= 0 || string.IsNullOrEmpty(document.Tags))
            {
                return BadRequest("Invalid document data.");
            }

            bool isUpdated = _documentsRepository.Update(document);

            if (!isUpdated)
            {
                return NotFound("Document not found or update failed.");
            }

            return Ok();
        }
    }
}
