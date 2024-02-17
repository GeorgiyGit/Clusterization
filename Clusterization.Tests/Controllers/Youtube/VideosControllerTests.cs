using AutoMapper;
using Clusterization.Controllers.Youtube;
using Domain.DTOs.YoutubeDTOs.Requests;
using Domain.DTOs.YoutubeDTOs.VideoDTOs;
using Domain.Exceptions;
using Domain.Interfaces.Customers;
using Domain.Interfaces.Tasks;
using Domain.Interfaces.Youtube;
using Domain.Mappers;
using Domain.Resources.Localization.Errors;
using Domain.Resources.Types;
using Domain.Services.Youtube;
using FakeItEasy;
using FluentAssertions;
using Hangfire;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clusterization.Tests.Controllers.Youtube
{
    public class VideosControllerTests
    {
        private int channel_minSubCount = 100;
        private int channel_minVideoCount = 500;
        private int channel_minViewCount = 200;
        private int channel_minLoadedDateCount = 1001;

        private int video_minViewCount = 1000;
        private int video_minCommentsCount = 500;
        private int video_minLikeCount = 100;
        private int video_minLoadedDateCount = 1001;

        private async Task<ClusterizationDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ClusterizationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ClusterizationDbContext(options);
            databaseContext.Database.EnsureCreated();

            var channels = new List<Domain.Entities.Youtube.Channel>(10);
            if (await databaseContext.Channels.CountAsync() <= 0)
            {
                for (int i = 0; i < 10; i++)
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
                        LoadedDate = new DateTime(channel_minLoadedDateCount + i, 1, 1),
                        PublishedAt = new DateTime(1 + i, 1, 1),
                        PublishedAtDateTimeOffset = new DateTimeOffset(1 + i, 1, 1, 0, 0, 0, TimeSpan.Zero),
                        PublishedAtRaw = "01.01." + i + "0:00:00",
                        VideoCount = channel_minVideoCount + i,
                        Videos = new List<Domain.Entities.Youtube.Video>(),
                        Comments = new List<Domain.Entities.Youtube.Comment>(),
                        SubscriberCount = channel_minSubCount + i,
                        ViewCount = channel_minViewCount + i
                    };

                    channels.Add(channel);
                    databaseContext.Channels.Add(channel);
                    await databaseContext.SaveChangesAsync();
                }
            }
            if (await databaseContext.Videos.CountAsync() <= 0)
            {
                int j = -1;
                for (int i = 0; i < 100; i++)
                {
                    if (i % 10 == 0) j++;

                    var video = new Domain.Entities.Youtube.Video()
                    {
                        Id = i + "",
                        ETag = i + "",
                        CategoryId = "Category" + i,
                        Title = "Title" + i,
                        Description = "Description" + i,
                        LoadedDate = new DateTime(video_minLoadedDateCount + i, 1, 1),
                        PublishedAt = new DateTime(1 + i, 1, 1),
                        PublishedAtDateTimeOffset = new DateTimeOffset(1 + i, 1, 1, 0, 0, 0, TimeSpan.Zero),
                        PublishedAtRaw = "01.01." + i + "0:00:00",
                        Comments = new List<Domain.Entities.Youtube.Comment>(),
                        ChannelId = channels[j].Id,
                        Channel = channels[j],
                        ChannelTitle = channels[j].Title,
                        CommentCount = video_minCommentsCount + i,
                        ViewCount = video_minViewCount + i,
                        DefaultAudioLanguage = "en",
                        DefaultLanguage = "en",
                        LikeCount = video_minLikeCount + i,
                        LiveBroadcaseContent = "null",
                        VideoImageUrl = "https://tests/" + i
                    };

                    databaseContext.Videos.Add(video);
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }


        #region load_by_id
        [Fact]
        public async void VideosController_LoadVideoById_ReturnsOkResult()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var videoRepository = new Repository<Domain.Entities.Youtube.Video>(dbContext);

            var inMemorySettings = new Dictionary<string, string> {
                    {"YoutubeOptions:ApiKey", StaticData.YoutubeOptions_ApiKey},
                    {"YoutubeOptions:ApplicationName", "SectionValue"},
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var userService = A.Fake<IUserService>();
            var videosService = new YoutubeVideosService(videoRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         configuration,
                                                         A.Fake<IPrivateYoutubeChannelsService>(),
                                                         A.Fake<IYoutubeChannelsService>(),
                                                         A.Fake<IMapper>(),
                                                         A.Fake<IMyTasksService>(),
                                                         A.Fake<IBackgroundJobClient>(),
                                                         userService);


            var controller = new VideosController(videosService);

            string id = "dQw4w9WgXcQ";

            //Act
            var result = await controller.LoadById(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async void VideosController_LoadVideoById_ThrowException()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var videoRepository = new Repository<Domain.Entities.Youtube.Video>(dbContext);

            var inMemorySettings = new Dictionary<string, string> {
                    {"YoutubeOptions:ApiKey", StaticData.YoutubeOptions_ApiKey},
                    {"YoutubeOptions:ApplicationName", "SectionValue"},
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var userService = A.Fake<IUserService>();
            var videosService = new YoutubeVideosService(videoRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         configuration,
                                                         A.Fake<IPrivateYoutubeChannelsService>(),
                                                         A.Fake<IYoutubeChannelsService>(),
                                                         A.Fake<IMapper>(),
                                                         A.Fake<IMyTasksService>(),
                                                         A.Fake<IBackgroundJobClient>(),
                                                         userService);


            var controller = new VideosController(videosService);

            string id = "5";

            //Act
            Func<Task> result = async () => await controller.LoadById(id);

            //Assert
            await result.Should().ThrowAsync<HttpException>();
        }
        #endregion

        #region load_many_by_ids
        [Fact]
        public async void VideosController_LoadManyByIds_ReturnsOkResult()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var videoRepository = new Repository<Domain.Entities.Youtube.Video>(dbContext);

            var inMemorySettings = new Dictionary<string, string> {
                    {"YoutubeOptions:ApiKey", StaticData.YoutubeOptions_ApiKey},
                    {"YoutubeOptions:ApplicationName", "SectionValue"},
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var userService = A.Fake<IUserService>();
            var videosService = new YoutubeVideosService(videoRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         configuration,
                                                         A.Fake<IPrivateYoutubeChannelsService>(),
                                                         A.Fake<IYoutubeChannelsService>(),
                                                         A.Fake<IMapper>(),
                                                         A.Fake<IMyTasksService>(),
                                                         A.Fake<IBackgroundJobClient>(),
                                                         userService);


            var controller = new VideosController(videosService);

            var request = new LoadManyByIdsRequest()
            {
                Ids = new List<string>()
                {
                    "dQw4w9WgXcQ",
                    "awoFZaSuko4"
                }
            };

            //Act
            var result = await controller.LoadManyByIds(request);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkResult>();
        }
        #endregion

        #region get_by_id
        [Fact]
        public async void VideosController_GetLoadedVideoById_ReturnsOkResult()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var videoRepository = new Repository<Domain.Entities.Youtube.Video>(dbContext);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<VideoProfile>());
            var mapper = new Mapper(mapperConfiguration);

            var userService = A.Fake<IUserService>();
            var videosService = new YoutubeVideosService(videoRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         A.Fake<IConfiguration>(),
                                                         A.Fake<IPrivateYoutubeChannelsService>(),
                                                         A.Fake<IYoutubeChannelsService>(),
                                                         mapper,
                                                         A.Fake<IMyTasksService>(),
                                                         A.Fake<IBackgroundJobClient>(),
                                                         userService);


            var controller = new VideosController(videosService);

            string id = "5";

            //Act
            var result = await controller.GetLoadedById(id) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<SimpleVideoDTO>();

            var value = result.Value as SimpleVideoDTO;

            value.Id.Should().BeSameAs(id);
        }

        [Fact]
        public async void VideosController_GetLoadedVideoById_ThrowErrorNotFound()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var videoRepository = new Repository<Domain.Entities.Youtube.Video>(dbContext);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<VideoProfile>());
            var mapper = new Mapper(mapperConfiguration);

            var userService = A.Fake<IUserService>();
            var videosService = new YoutubeVideosService(videoRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         A.Fake<IConfiguration>(),
                                                         A.Fake<IPrivateYoutubeChannelsService>(),
                                                         A.Fake<IYoutubeChannelsService>(),
                                                         mapper,
                                                         A.Fake<IMyTasksService>(),
                                                         A.Fake<IBackgroundJobClient>(),
                                                         userService);


            var controller = new VideosController(videosService);

            string id = "-1";

            //Act
            Func<Task> result = async () => await controller.GetLoadedById(id);

            //Assert
            await result.Should().ThrowAsync<HttpException>();
        }
        #endregion

        #region get_many
        [Fact]
        public async void VideosController_GetLoadedVideos_Returns10VideosByTimeInc()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var videoRepository = new Repository<Domain.Entities.Youtube.Video>(dbContext);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<VideoProfile>());
            var mapper = new Mapper(mapperConfiguration);

            var userService = A.Fake<IUserService>();
            var videosService = new YoutubeVideosService(videoRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         A.Fake<IConfiguration>(),
                                                         A.Fake<IPrivateYoutubeChannelsService>(),
                                                         A.Fake<IYoutubeChannelsService>(),
                                                         mapper,
                                                         A.Fake<IMyTasksService>(),
                                                         A.Fake<IBackgroundJobClient>(),
                                                         userService);


            var controller = new VideosController(videosService);

            var request = new GetVideosRequest()
            {
                PageParameters = new Domain.DTOs.PageParametersDTO
                {
                    PageNumber = 1,
                    PageSize = 10
                },
                FilterType = VideoFilterTypes.ByTimeInc
            };

            //Act
            var result = await controller.GetLoadedCollection(request) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<List<SimpleVideoDTO>>();

            var videos = result.Value as List<SimpleVideoDTO>;

            videos.Should().HaveCount(10);

            for (int i = 0; i < 10; i++)
            {
                var value = videos[i];

                value.Should().NotBeNull();
                value.LoadedDate.Should().Be(new DateTime(video_minLoadedDateCount + i, 1, 1));
            }
        }
        [Fact]
        public async void VideosController_GetLoadedVideos_Returns10VideosByTimeDesc()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var videoRepository = new Repository<Domain.Entities.Youtube.Video>(dbContext);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<VideoProfile>());
            var mapper = new Mapper(mapperConfiguration);

            var userService = A.Fake<IUserService>();
            var videosService = new YoutubeVideosService(videoRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         A.Fake<IConfiguration>(),
                                                         A.Fake<IPrivateYoutubeChannelsService>(),
                                                         A.Fake<IYoutubeChannelsService>(),
                                                         mapper,
                                                         A.Fake<IMyTasksService>(),
                                                         A.Fake<IBackgroundJobClient>(),
                                                         userService);


            var controller = new VideosController(videosService);

            var request = new GetVideosRequest()
            {
                PageParameters = new Domain.DTOs.PageParametersDTO
                {
                    PageNumber = 1,
                    PageSize = 10
                },
                FilterType = VideoFilterTypes.ByTimeDesc
            };

            //Act
            var result = await controller.GetLoadedCollection(request) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<List<SimpleVideoDTO>>();

            var videos = result.Value as List<SimpleVideoDTO>;

            videos.Should().HaveCount(10);

            for (int i = 0; i < 10; i++)
            {
                var value = videos[i];

                value.Should().NotBeNull();
                value.LoadedDate.Should().Be(new DateTime(video_minLoadedDateCount + 99 - i, 1, 1));
            }
        }

        [Fact]
        public async void VideosController_GetLoadedVideos_Returns10VideosByViewsInc()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var videoRepository = new Repository<Domain.Entities.Youtube.Video>(dbContext);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<VideoProfile>());
            var mapper = new Mapper(mapperConfiguration);

            var userService = A.Fake<IUserService>();
            var videosService = new YoutubeVideosService(videoRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         A.Fake<IConfiguration>(),
                                                         A.Fake<IPrivateYoutubeChannelsService>(),
                                                         A.Fake<IYoutubeChannelsService>(),
                                                         mapper,
                                                         A.Fake<IMyTasksService>(),
                                                         A.Fake<IBackgroundJobClient>(),
                                                         userService);


            var controller = new VideosController(videosService);

            var request = new GetVideosRequest()
            {
                PageParameters = new Domain.DTOs.PageParametersDTO
                {
                    PageNumber = 1,
                    PageSize = 10
                },
                FilterType = VideoFilterTypes.ByViewsInc
            };

            //Act
            var result = await controller.GetLoadedCollection(request) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<List<SimpleVideoDTO>>();

            var videos = result.Value as List<SimpleVideoDTO>;

            videos.Should().HaveCount(10);

            for (int i = 0; i < 10; i++)
            {
                var value = videos[i];

                value.Should().NotBeNull();
                value.ViewCount.Should().Be(video_minViewCount + i);
            }
        }
        [Fact]
        public async void VideosController_GetLoadedVideos_Returns10VideosByViewsDesc()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var videoRepository = new Repository<Domain.Entities.Youtube.Video>(dbContext);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<VideoProfile>());
            var mapper = new Mapper(mapperConfiguration);

            var userService = A.Fake<IUserService>();
            var videosService = new YoutubeVideosService(videoRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         A.Fake<IConfiguration>(),
                                                         A.Fake<IPrivateYoutubeChannelsService>(),
                                                         A.Fake<IYoutubeChannelsService>(),
                                                         mapper,
                                                         A.Fake<IMyTasksService>(),
                                                         A.Fake<IBackgroundJobClient>(),
                                                         userService);


            var controller = new VideosController(videosService);

            var request = new GetVideosRequest()
            {
                PageParameters = new Domain.DTOs.PageParametersDTO
                {
                    PageNumber = 1,
                    PageSize = 10
                },
                FilterType = VideoFilterTypes.ByViewsDesc
            };

            //Act
            var result = await controller.GetLoadedCollection(request) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<List<SimpleVideoDTO>>();

            var videos = result.Value as List<SimpleVideoDTO>;

            videos.Should().HaveCount(10);

            for (int i = 0; i < 10; i++)
            {
                var value = videos[i];

                value.Should().NotBeNull();
                value.ViewCount.Should().Be(video_minViewCount + 99 - i);
            }
        }

        [Fact]
        public async void VideosController_GetLoadedVideos_Returns10VideosByCommentsInc()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var videoRepository = new Repository<Domain.Entities.Youtube.Video>(dbContext);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<VideoProfile>());
            var mapper = new Mapper(mapperConfiguration);

            var userService = A.Fake<IUserService>();
            var videosService = new YoutubeVideosService(videoRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         A.Fake<IConfiguration>(),
                                                         A.Fake<IPrivateYoutubeChannelsService>(),
                                                         A.Fake<IYoutubeChannelsService>(),
                                                         mapper,
                                                         A.Fake<IMyTasksService>(),
                                                         A.Fake<IBackgroundJobClient>(),
                                                         userService);


            var controller = new VideosController(videosService);

            var request = new GetVideosRequest()
            {
                PageParameters = new Domain.DTOs.PageParametersDTO
                {
                    PageNumber = 1,
                    PageSize = 10
                },
                FilterType = VideoFilterTypes.ByCommentsInc
            };

            //Act
            var result = await controller.GetLoadedCollection(request) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<List<SimpleVideoDTO>>();

            var videos = result.Value as List<SimpleVideoDTO>;

            videos.Should().HaveCount(10);

            for (int i = 0; i < 10; i++)
            {
                var value = videos[i];

                value.Should().NotBeNull();
                value.CommentCount.Should().Be(video_minCommentsCount + i);
            }
        }
        [Fact]
        public async void VideosController_GetLoadedVideos_Returns10VideosByCommentsDesc()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var videoRepository = new Repository<Domain.Entities.Youtube.Video>(dbContext);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<VideoProfile>());
            var mapper = new Mapper(mapperConfiguration);

            var userService = A.Fake<IUserService>();
            var videosService = new YoutubeVideosService(videoRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         A.Fake<IConfiguration>(),
                                                         A.Fake<IPrivateYoutubeChannelsService>(),
                                                         A.Fake<IYoutubeChannelsService>(),
                                                         mapper,
                                                         A.Fake<IMyTasksService>(),
                                                         A.Fake<IBackgroundJobClient>(),
                                                         userService);


            var controller = new VideosController(videosService);

            var request = new GetVideosRequest()
            {
                PageParameters = new Domain.DTOs.PageParametersDTO
                {
                    PageNumber = 1,
                    PageSize = 10
                },
                FilterType = VideoFilterTypes.ByCommentsDesc
            };

            //Act
            var result = await controller.GetLoadedCollection(request) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<List<SimpleVideoDTO>>();

            var videos = result.Value as List<SimpleVideoDTO>;

            videos.Should().HaveCount(10);

            for (int i = 0; i < 10; i++)
            {
                var value = videos[i];

                value.Should().NotBeNull();
                value.CommentCount.Should().Be(video_minCommentsCount + 99 - i);
            }
        }

        [Fact]
        public async void VideosController_GetLoadedVideos_ReturnsVideoById()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var videoRepository = new Repository<Domain.Entities.Youtube.Video>(dbContext);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<VideoProfile>());
            var mapper = new Mapper(mapperConfiguration);

            var userService = A.Fake<IUserService>();
            var videosService = new YoutubeVideosService(videoRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         A.Fake<IConfiguration>(),
                                                         A.Fake<IPrivateYoutubeChannelsService>(),
                                                         A.Fake<IYoutubeChannelsService>(),
                                                         mapper,
                                                         A.Fake<IMyTasksService>(),
                                                         A.Fake<IBackgroundJobClient>(),
                                                         userService);


            var controller = new VideosController(videosService);

            string videoId = "5";
            var request = new GetVideosRequest()
            {
                PageParameters = new Domain.DTOs.PageParametersDTO
                {
                    PageNumber = 1,
                    PageSize = 10
                },
                FilterType = VideoFilterTypes.ByTimeInc,
                FilterStr = videoId
            };

            //Act
            var result = await controller.GetLoadedCollection(request) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<List<SimpleVideoDTO>>();

            var videos = result.Value as List<SimpleVideoDTO>;

            videos.Should().HaveCount(1);

            var value = videos[0];

            value.Should().NotBeNull();
            value.Id.Should().BeSameAs(videoId);
        }

        [Fact]
        public async void VideosController_GetLoadedVideos_ReturnsVideosByTitle()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var videoRepository = new Repository<Domain.Entities.Youtube.Video>(dbContext);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<VideoProfile>());
            var mapper = new Mapper(mapperConfiguration);

            var userService = A.Fake<IUserService>();
            var videosService = new YoutubeVideosService(videoRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         A.Fake<IConfiguration>(),
                                                         A.Fake<IPrivateYoutubeChannelsService>(),
                                                         A.Fake<IYoutubeChannelsService>(),
                                                         mapper,
                                                         A.Fake<IMyTasksService>(),
                                                         A.Fake<IBackgroundJobClient>(),
                                                         userService);


            var controller = new VideosController(videosService);

            string title = "Title5";
            var request = new GetVideosRequest()
            {
                PageParameters = new Domain.DTOs.PageParametersDTO
                {
                    PageNumber = 1,
                    PageSize = 10
                },
                FilterType = VideoFilterTypes.ByTimeInc,
                FilterStr= title
            };

            //Act
            var result = await controller.GetLoadedCollection(request) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<List<SimpleVideoDTO>>();

            var videos = result.Value as List<SimpleVideoDTO>;

            for (int i = 0; i < 10; i++)
            {
                var value = videos[i];

                value.Should().NotBeNull();
                value.Title.Should().Contain(title);
            }
        }

        [Fact]
        public async void VideosController_GetLoadedVideos_ReturnsVideosByChannelId()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var videoRepository = new Repository<Domain.Entities.Youtube.Video>(dbContext);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<VideoProfile>());
            var mapper = new Mapper(mapperConfiguration);

            var userService = A.Fake<IUserService>();
            var videosService = new YoutubeVideosService(videoRepository,
                                                         A.Fake<IStringLocalizer<ErrorMessages>>(),
                                                         A.Fake<IConfiguration>(),
                                                         A.Fake<IPrivateYoutubeChannelsService>(),
                                                         A.Fake<IYoutubeChannelsService>(),
                                                         mapper,
                                                         A.Fake<IMyTasksService>(),
                                                         A.Fake<IBackgroundJobClient>(),
                                                         userService);


            var controller = new VideosController(videosService);

            string channelId = "5";
            var request = new GetVideosRequest()
            {
                PageParameters = new Domain.DTOs.PageParametersDTO
                {
                    PageNumber = 1,
                    PageSize = 10
                },
                FilterType = VideoFilterTypes.ByTimeInc,
                ChannelId = channelId
            };

            //Act
            var result = await controller.GetLoadedCollection(request) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            result.Value.Should().NotBeNull();
            result.Value.Should().BeOfType<List<SimpleVideoDTO>>();

            var videos = result.Value as List<SimpleVideoDTO>;

            for (int i = 0; i < 10; i++)
            {
                var value = videos[i];

                value.Should().NotBeNull();
            }
        }
        #endregion
    }
}
