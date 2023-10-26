using AutoMapper;
using Clusterization.Controllers.Clusterization;
using Domain.DTOs.ClusterizationDTOs.DimensionTypeDTO;
using Domain.DTOs.ClusterizationDTOs.TypeDTO;
using Domain.Entities.Clusterization;
using Domain.Mappers.ClusterizationProfiles;
using Domain.Services.Clusterization;
using FluentAssertions;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clusterization.Tests.Controllers.Clusterization
{
    public class ClusterizationDimensionTypesControllerTests
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
        public async void ClusterizationDimensionTypesController_GetAllDimensionTypes_ReturnsOkResult()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var typesRepository = new Repository<ClusterizationDimensionType>(dbContext);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<ClusterizationDimensionTypeMapperProfile>());
            var mapper = new Mapper(mapperConfiguration);

            var channelService = new ClusterizationDimensionTypesService(typesRepository, mapper);


            var controller = new ClusterizationDimensionTypesController(channelService);

            //Act
            var result = await controller.GetAllDimensionTypes() as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            var types = result.Value as List<ClusterizationDimensionTypeDTO>;

            types.Should().NotBeNull();
            types.Count().Should().Be(4);
            types.Should().BeOfType<List<ClusterizationDimensionTypeDTO>>();
        }
    }
}
