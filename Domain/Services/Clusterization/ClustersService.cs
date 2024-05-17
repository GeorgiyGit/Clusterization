﻿using AutoMapper;
using Domain.DTOs.ClusterizationDTOs.ClusterDTOs.Requests;
using Domain.DTOs.ClusterizationDTOs.ClusterDTOs.Responses;
using Domain.Entities.Clusterization;
using Domain.Entities.Clusterization.Displaying;
using Domain.Entities.Clusterization.Profiles;
using Domain.Entities.DataObjects;
using Domain.Exceptions;
using Domain.Interfaces.Clusterization;
using Domain.Interfaces.Clusterization.Algorithms;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Embeddings;
using Domain.Interfaces.Other;
using Domain.Interfaces.Quotas;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types.Clusterization;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Clusterization
{
    public class ClustersService : IClustersService
    {
        private readonly IRepository<Cluster> _clustersRepository;
        private readonly IRepository<ClusterizationProfile> _profilesRepository;
        private readonly IRepository<DisplayedPoint> _pointsRepository;

        private readonly IStringLocalizer<ErrorMessages> _localizer;

        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ClustersService(IRepository<Cluster> clustersRepository,
            IRepository<ClusterizationProfile> profilesRepository,
            IRepository<DisplayedPoint> pointsRepository,
            IStringLocalizer<ErrorMessages> localizer,
            IMapper mapper,
            IUserService userService)
        {
            _clustersRepository = clustersRepository;
            _profilesRepository = profilesRepository;
            _pointsRepository = pointsRepository;
            _localizer = localizer;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ICollection<ClusterDTO>> GetClusters(GetClustersRequest request)
        {
            var userId = await _userService.GetCurrentUserId();
            var profile = (await _profilesRepository.GetAsync(e => e.Id == request.ProfileId)).FirstOrDefault();

            if (profile == null || (profile.VisibleType == VisibleTypes.OnlyOwner && profile.OwnerId != userId)) throw new HttpException(_localizer[ErrorMessagePatterns.ProfileNotFound], System.Net.HttpStatusCode.NotFound);

            var clusters = await _clustersRepository.GetAsync(e => e.ProfileId == request.ProfileId,
                                                              orderBy: order => order.OrderByDescending(e => e.ChildElementsCount),
                                                              pageParameters: request.PageParameters);

            return _mapper.Map<ICollection<ClusterDTO>>(clusters);
        }
        public async Task<ICollection<ClusterDataDTO>> GetClusterEntities(GetClusterDataRequest request)
        {
            var clusters = (await _clustersRepository.GetAsync(e => e.ParentClusterId == request.ClusterId,
                                                               orderBy: order => order.OrderByDescending(e => e.ChildElementsCount),
                                                               pageParameters: request.PageParameters));

            IEnumerable<DisplayedPoint> displayedPoints = new List<DisplayedPoint>();
            if (request.PageParameters.PageSize - clusters.Count() > 0)
            {
                request.PageParameters.PageSize -= clusters.Count();
                displayedPoints = await _pointsRepository.GetAsync(e => e.ClusterId == request.ClusterId,
                                                                   includeProperties: $"{nameof(DisplayedPoint.DataObject)}",
                                                                   pageParameters: request.PageParameters);
            }

            var result = new List<ClusterDataDTO>(clusters.Count() + displayedPoints.Count());

            foreach (var cluster in clusters)
            {
                result.Add(new ClusterDataDTO()
                {
                    Cluster = new ClusterDTO()
                    {
                        Id=cluster.Id,
                        ChildElementsCount = cluster.ChildElementsCount,
                        Color = cluster.Color,
                        Name = cluster.Name,
                        ParentClusterId = cluster.ParentClusterId
                    }
                });
            }

            foreach (var point in displayedPoints)
            {
                result.Add(new ClusterDataDTO()
                {
                    DataObject = new ClusterDataObjectDTO()
                    {
                        Id = point.Id,
                        Text = point.DataObject.Text,
                        X = point.X,
                        Y = point.Y
                    }
                });
            }

            return result;
        }
    }
}
