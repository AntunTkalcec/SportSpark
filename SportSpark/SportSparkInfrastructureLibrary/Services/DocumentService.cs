using AutoMapper;
using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Interfaces.Repositories.Base;
using SportSparkCoreLibrary.Interfaces.Services;
using SportSparkCoreSharedLibrary.DTOs;

namespace SportSparkInfrastructureLibrary.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IBaseRepository<Document> _documentRepository;
        private readonly IMapper _mapper;
        public DocumentService(IBaseRepository<Document> documentRepository, IMapper mapper)
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
        }
        public async Task<int> CreateAsync(DocumentDTO entity)
        {
            Document newDocument = await _documentRepository.AddAsync(_mapper.Map<Document>(entity));
            return newDocument.Id;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DocumentDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DocumentDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, DocumentDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
