using AutoMapper;
using Clusterization.Controllers.Youtube;
using Domain.DTOs.YoutubeDTOs.ChannelDTOs;
using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.Entities.Youtube;
using Domain.Exceptions;
using Domain.Mappers;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
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

namespace Clusterization.Tests.Controllers.Youtube
{
    public class ChannelsControllerTests
    {
        private int minSubCount = 100;
        private int minVideoCount = 500;
        private int minViewCount = 200;
        private int minLoadedDateCount = 1001;

        private async Task<ClusterizationDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ClusterizationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ClusterizationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Channels.CountAsync() <= 0)
            {
                for (int i = 0; i < 100; i++)
                {
                    var channel = new Domain.Entities.Youtube.Channel()
                    {
                        Id = i + "",
                        ETag = i + "",
                        ChannelImageUrl = "https://tests/" + i,
                        Country = "UK",
                        CustomUrl = "@customUrl" + i,
                        Title = "Title" + i,
                        Description = "Description" + i,
                        LoadedDate = new DateTime(minLoadedDateCount + i, 1, 1),
                        PublishedAt = new DateTime(1 + i, 1, 1),
                        PublishedAtDateTimeOffset = new DateTimeOffset(1 + i, 1, 1, 0, 0, 0, TimeSpan.Zero),
                        PublishedAtRaw = "01.01." + i + "0:00:00",
                        VideoCount = minVideoCount + i,
                        Videos = new List<Domain.Entities.Youtube.Video>(),
                        Comments = new List<Domain.Entities.Youtube.Comment>(),
                        SubscriberCount = minSubCount + i,
                        ViewCount = minViewCount + i
                    };
                    databaseContext.Channels.Add(channel);
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        #region load_by_id

        #endregion

        #region load_many_by_ids

        #endregion

        #region get_by_id
        [Fact]
        public async void ChannelsController_GetLoadedChannelById_ReturnsChannelById()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var channelRepository = new Repository<Domain.Entities.Youtube.Channel>(dbContext);

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ChannelProfile>());
            var mapper = new Mapper(configuration);

            var channelService = new YoutubeChannelService(channelRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         A.Fake<IConfiguration>(),
                                                         mapper);


            var controller = new ChannelsController(channelService);

            string id = "5";

            //Act
            var result = await controller.GetLoadedChannelById(id) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            result.Value.Should().BeOfType<SimpleChannelDTO>();

            var value = result.Value as SimpleChannelDTO;
            value.Should().NotBeNull();
            value.Id.Should().BeSameAs(id);
        }

        [Fact]
        public async void ChannelsController_GetLoadedChannelById_ThrowErrorNotFound()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var channelRepository = new Repository<Domain.Entities.Youtube.Channel>(dbContext);

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ChannelProfile>());
            var mapper = new Mapper(configuration);

            var channelService = new YoutubeChannelService(channelRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         A.Fake<IConfiguration>(),
                                                         mapper);


            var controller = new ChannelsController(channelService);

            string id = "-1";

            //Act
            Func<Task> result = async () => await controller.GetLoadedChannelById(id);

            //Assert
            await result.Should().ThrowAsync<HttpException>();
        }
        #endregion

        #region get_many
        [Fact]
        public async void ChannelsController_GetLoadedChannels_Returns10ChannelsByTimeInc()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var channelRepository = new Repository<Domain.Entities.Youtube.Channel>(dbContext);

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ChannelProfile>());
            var mapper = new Mapper(configuration);

            var channelService = new YoutubeChannelService(channelRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         A.Fake<IConfiguration>(),
                                                         mapper);


            var controller = new ChannelsController(channelService);

            var request = new GetChannelsRequest()
            {
                FilterStr = "",
                FilterType = ChannelFilterTypes.ByTimeInc,
                PageParameters = new Domain.DTOs.PageParametersDTO
                {
                    PageNumber = 1,
                    PageSize = 10
                }
            };

            //Act
            var result = await controller.GetLoadedChannels(request) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            var values = result.Value as List<SimpleChannelDTO>;
            values.Should().BeOfType<List<SimpleChannelDTO>>();
            values.Should().HaveCount(10);

            for(int i = 0; i < 10; i++)
            {
                var value = values[i];

                value.Should().NotBeNull();
                value.LoadedDate.Should().Be(new DateTime(minLoadedDateCount + i, 1, 1));
            }
        }
        [Fact]
        public async void ChannelsController_GetLoadedChannels_Returns10ChannelsByTimeDesc()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var channelRepository = new Repository<Domain.Entities.Youtube.Channel>(dbContext);

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ChannelProfile>());
            var mapper = new Mapper(configuration);

            var channelService = new YoutubeChannelService(channelRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         A.Fake<IConfiguration>(),
                                                         mapper);


            var controller = new ChannelsController(channelService);

            var request = new GetChannelsRequest()
            {
                FilterStr = "",
                FilterType = ChannelFilterTypes.ByTimeDesc,
                PageParameters = new Domain.DTOs.PageParametersDTO
                {
                    PageNumber = 1,
                    PageSize = 10
                }
            };

            //Act
            var result = await controller.GetLoadedChannels(request) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            var values = result.Value as List<SimpleChannelDTO>;
            values.Should().BeOfType<List<SimpleChannelDTO>>();
            values.Should().HaveCount(10);

            for (int i = 0; i < 10; i++)
            {
                var value = values[i];

                value.Should().NotBeNull();
                value.LoadedDate.Should().Be(new DateTime(minLoadedDateCount + 99 - i, 1, 1));
            }
        }

        [Fact]
        public async void ChannelsController_GetLoadedChannels_Returns10ChannelsBySubscribersInc()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var channelRepository = new Repository<Domain.Entities.Youtube.Channel>(dbContext);

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ChannelProfile>());
            var mapper = new Mapper(configuration);

            var channelService = new YoutubeChannelService(channelRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         A.Fake<IConfiguration>(),
                                                         mapper);


            var controller = new ChannelsController(channelService);

            var request = new GetChannelsRequest()
            {
                FilterStr = "",
                FilterType = ChannelFilterTypes.BySubscribersInc,
                PageParameters = new Domain.DTOs.PageParametersDTO
                {
                    PageNumber = 1,
                    PageSize = 10
                }
            };

            //Act
            var result = await controller.GetLoadedChannels(request) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            var values = result.Value as List<SimpleChannelDTO>;
            values.Should().BeOfType<List<SimpleChannelDTO>>();
            values.Should().HaveCount(10);

            for (int i = 0; i < 10; i++)
            {
                var value = values[i];

                value.Should().NotBeNull();
                value.SubscriberCount.Should().Be(minSubCount + i);
            }
        }
        [Fact]
        public async void ChannelsController_GetLoadedChannels_Returns10ChannelsBySubscribersDesc()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var channelRepository = new Repository<Domain.Entities.Youtube.Channel>(dbContext);

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ChannelProfile>());
            var mapper = new Mapper(configuration);

            var channelService = new YoutubeChannelService(channelRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         A.Fake<IConfiguration>(),
                                                         mapper);


            var controller = new ChannelsController(channelService);

            var request = new GetChannelsRequest()
            {
                FilterStr = "",
                FilterType = ChannelFilterTypes.BySubscribersDesc,
                PageParameters = new Domain.DTOs.PageParametersDTO
                {
                    PageNumber = 1,
                    PageSize = 10
                }
            };

            //Act
            var result = await controller.GetLoadedChannels(request) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            var values = result.Value as List<SimpleChannelDTO>;
            values.Should().BeOfType<List<SimpleChannelDTO>>();
            values.Should().HaveCount(10);

            for (int i = 0; i < 10; i++)
            {
                var value = values[i];

                value.Should().NotBeNull();
                value.SubscriberCount.Should().Be(minSubCount + 99 - i);
            }
        }

        [Fact]
        public async void ChannelsController_GetLoadedChannels_Returns10ChannelsByVideoCountInc()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var channelRepository = new Repository<Domain.Entities.Youtube.Channel>(dbContext);

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ChannelProfile>());
            var mapper = new Mapper(configuration);

            var channelService = new YoutubeChannelService(channelRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         A.Fake<IConfiguration>(),
                                                         mapper);


            var controller = new ChannelsController(channelService);

            var request = new GetChannelsRequest()
            {
                FilterStr = "",
                FilterType = ChannelFilterTypes.ByVideoCountInc,
                PageParameters = new Domain.DTOs.PageParametersDTO
                {
                    PageNumber = 1,
                    PageSize = 10
                }
            };

            //Act
            var result = await controller.GetLoadedChannels(request) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            var values = result.Value as List<SimpleChannelDTO>;
            values.Should().BeOfType<List<SimpleChannelDTO>>();
            values.Should().HaveCount(10);

            for (int i = 0; i < 10; i++)
            {
                var value = values[i];

                value.Should().NotBeNull();
                value.VideoCount.Should().Be(minVideoCount + i);
            }
        }
        [Fact]
        public async void ChannelsController_GetLoadedChannels_Returns10ChannelsByVideoCountDesc()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var channelRepository = new Repository<Domain.Entities.Youtube.Channel>(dbContext);

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ChannelProfile>());
            var mapper = new Mapper(configuration);

            var channelService = new YoutubeChannelService(channelRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         A.Fake<IConfiguration>(),
                                                         mapper);


            var controller = new ChannelsController(channelService);

            var request = new GetChannelsRequest()
            {
                FilterStr = "",
                FilterType = ChannelFilterTypes.ByVideoCountDesc,
                PageParameters = new Domain.DTOs.PageParametersDTO
                {
                    PageNumber = 1,
                    PageSize = 10
                }
            };

            //Act
            var result = await controller.GetLoadedChannels(request) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            var values = result.Value as List<SimpleChannelDTO>;
            values.Should().BeOfType<List<SimpleChannelDTO>>();
            values.Should().HaveCount(10);

            for (int i = 0; i < 10; i++)
            {
                var value = values[i];

                value.Should().NotBeNull();
                value.VideoCount.Should().Be(minVideoCount + 99 - i);
            }
        }

        [Fact]
        public async void ChannelsController_GetLoadedChannels_Returns10ChannelsByTitle()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var channelRepository = new Repository<Domain.Entities.Youtube.Channel>(dbContext);

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ChannelProfile>());
            var mapper = new Mapper(configuration);

            var channelService = new YoutubeChannelService(channelRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         A.Fake<IConfiguration>(),
                                                         mapper);


            var controller = new ChannelsController(channelService);

            var request = new GetChannelsRequest()
            {
                FilterStr = "Title5",
                FilterType = ChannelFilterTypes.ByTimeInc,
                PageParameters = new Domain.DTOs.PageParametersDTO
                {
                    PageNumber = 1,
                    PageSize = 10
                }
            };

            //Act
            var result = await controller.GetLoadedChannels(request) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            var values = result.Value as List<SimpleChannelDTO>;
            values.Should().BeOfType<List<SimpleChannelDTO>>();
            values.Should().HaveCount(10);

            for(int i = 0; i < 10; i++)
            {
                values[0].Should().NotBeNull();
                values[0].Title.Should().Contain("Title5");
            }
        }

        [Fact]
        public async void ChannelsController_GetLoadedChannels_ReturnsChannelById()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var channelRepository = new Repository<Domain.Entities.Youtube.Channel>(dbContext);

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ChannelProfile>());
            var mapper = new Mapper(configuration);

            var channelService = new YoutubeChannelService(channelRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         A.Fake<IConfiguration>(),
                                                         mapper);


            var controller = new ChannelsController(channelService);

            var request = new GetChannelsRequest()
            {
                FilterStr = "5",
                FilterType = ChannelFilterTypes.ByTimeInc,
                PageParameters = new Domain.DTOs.PageParametersDTO
                {
                    PageNumber = 1,
                    PageSize = 10
                }
            };

            //Act
            var result = await controller.GetLoadedChannels(request) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            var values = result.Value as List<SimpleChannelDTO>;
            values.Should().BeOfType<List<SimpleChannelDTO>>();
            values.Should().HaveCount(1);

            values[0].Should().NotBeNull();
            values[0].Id.Should().Be("5");
        }

        #endregion

        #region get_without_loading

        #endregion
    }
}
