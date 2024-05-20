using AutoMapper;
using Domain.DTOs.DataObjectDTOs.Responses;
using Domain.Entities.Clusterization.Displaying;
using Domain.Entities.DataObjects;
using Domain.Exceptions;
using Domain.Interfaces.DataObjects;
using Domain.Interfaces.Other;
using Domain.Resources.Localization.Errors;
using Microsoft.Extensions.Localization;

namespace Domain.Services.DataObjects
{
    public class MyDataObjectsService : IMyDataObjectsService
    {
        private readonly IRepository<MyDataObject> _dataObjects;
        private readonly IRepository<DisplayedPoint> _pointsRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;

        private readonly IMapper _mapper;

        public MyDataObjectsService(IRepository<MyDataObject> dataObjects,
            IRepository<DisplayedPoint> pointsRepository,
            IStringLocalizer<ErrorMessages> localizer,
            IMapper mapper)
        {
            _dataObjects = dataObjects;
            _pointsRepository = pointsRepository;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<FullDataObjectDTO> GetFullByDisplayedPointId(int pointId)
        {
            var displayedPoint = await _pointsRepository.FindAsync(pointId);
            if (displayedPoint == null) throw new HttpException(_localizer[ErrorMessagePatterns.DisplayedPointNotFound], System.Net.HttpStatusCode.NotFound);

            var dataObject = (await _dataObjects.GetAsync(e => e.Id == displayedPoint.DataObjectId, includeProperties: $"{nameof(MyDataObject.YoutubeComment)},{nameof(MyDataObject.TelegramMessage)},{nameof(MyDataObject.TelegramReply)},{nameof(MyDataObject.ExternalObject)}")).FirstOrDefault();

            return _mapper.Map<FullDataObjectDTO>(dataObject);
        }
    }
}
