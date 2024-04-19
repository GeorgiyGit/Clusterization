using AutoMapper;
using Domain.DTOs.QuotaDTOs.QuotaPackDTOs.Requets;
using Domain.DTOs.QuotaDTOs.QuotaPackDTOs.Responses;
using Domain.Entities.Quotas;
using Domain.Interfaces.Other;
using Domain.Interfaces.Quotas;

namespace Domain.Services.Quotas
{
    public class QuotasPacksService : IQuotasPacksService
    {
        private readonly IRepository<QuotasPack> _packsRepository;
        private readonly IRepository<QuotasPackItem> _packItemsRepository;
        private readonly IMapper _mapper;

        public QuotasPacksService(IRepository<QuotasPack> packsRepository,
            IRepository<QuotasPackItem> packItemsRepository,
            IMapper mapper)
        {
            _packItemsRepository = packItemsRepository;
            _packsRepository = packsRepository;
            _mapper = mapper;
        }
        public async Task Add(AddQuotasPackRequest model)
        {
            var pack = new QuotasPack();

            await _packsRepository.AddAsync(pack);

            foreach(var itemModel in model.Items)
            {
                var item = new QuotasPackItem()
                {
                    Count = itemModel.Count,
                    Pack = pack,
                    TypeId = itemModel.TypeId
                };

                await _packItemsRepository.AddAsync(item);
            }

            await _packsRepository.SaveChangesAsync();
        }

        public async Task<ICollection<QuotasPackDTO>> GetAll()
        {
            var packs = await _packsRepository.GetAsync(includeProperties: $"{nameof(QuotasPack.Items)}");

            return _mapper.Map<ICollection<QuotasPackDTO>>(packs);
        }
    }
}
