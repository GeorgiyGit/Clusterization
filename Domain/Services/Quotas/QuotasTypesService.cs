using AutoMapper;
using Domain.DTOs.QuotaDTOs.CustomerQuotaDTOs.Responses;
using Domain.Entities.Quotas;
using Domain.Interfaces;
using Domain.Interfaces.Quotas;

namespace Domain.Services.Quotas
{
    public class QuotasTypesService : IQuotasTypesService
    {
        private readonly IRepository<QuotasType> _repository;
        private readonly IMapper _mapper;

        public QuotasTypesService(IRepository<QuotasType> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<QuotasTypeDTO>> GetAllTypes()
        {
            var types = await _repository.GetAsync();

            return _mapper.Map<ICollection<QuotasTypeDTO>>(types);
        }
    }
}
