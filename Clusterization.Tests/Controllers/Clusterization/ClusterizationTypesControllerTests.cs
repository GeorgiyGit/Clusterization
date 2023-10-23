using AutoMapper;
using Clusterization.Controllers.Clusterization;
using Clusterization.Controllers.Youtube;
using Domain.DTOs.ClusterizationDTOs.TypeDTO;
using Domain.DTOs.YoutubeDTOs.ChannelDTOs;
using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.Entities.Clusterization;
using Domain.Entities.Youtube;
using Domain.Exceptions;
using Domain.Mappers;
using Domain.Mappers.ClusterizationProfiles;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Domain.Services.Clusterization;
using Domain.Services.Youtube;
using FakeItEasy;
using FluentAssertions;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Hangfire.Annotations;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Clusterization.Tests.Controllers.Clusterization
{
    public class ClusterizationTypesControllerTests
    {
        private async Task<ClusterizationDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ClusterizationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ClusterizationDbContext(options);
            databaseContext.Database.EnsureCreated();

            return databaseContext;
        }


        [Fact]
        public async void ClusterizationTypesController_GetAllTypes_ReturnsOkResult()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var typesRepository = new Repository<ClusterizationType>(dbContext);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<ClusterizationTypeMapperProfile>());
            var mapper = new Mapper(mapperConfiguration);

            var channelService = new ClusterizationTypeService(typesRepository, mapper);


            var controller = new ClusterizationTypesController(channelService);

            //Act
            var result = await controller.GetAllTypes() as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            var types = result.Value as List<ClusterizationTypeDTO>;

            types.Should().NotBeNull();
            types.Count().Should().Be(3);
            types.Should().BeOfType<List<ClusterizationTypeDTO>>();
        }
    }
}
