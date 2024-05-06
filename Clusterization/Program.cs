using Clusterization.Middlewares;
using Clusterization.Seeders;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.DBScanDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.GaussianMixtureDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.OneClusterDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.SpectralClusteringDTOs;
using Domain.Entities.Customers;
using Domain.Interfaces.Clusterization;
using Domain.Interfaces.Clusterization.Algorithms;
using Domain.Interfaces.Clusterization.Dimensions;
using Domain.Interfaces.Clusterization.Displaying;
using Domain.Interfaces.Clusterization.Profiles;
using Domain.Interfaces.Clusterization.Workspaces;
using Domain.Interfaces.Customers;
using Domain.Interfaces.DataSources.ExternalData;
using Domain.Interfaces.DataSources.Telegram;
using Domain.Interfaces.DataSources.Youtube;
using Domain.Interfaces.DimensionalityReduction;
using Domain.Interfaces.EmbeddingModels;
using Domain.Interfaces.Embeddings;
using Domain.Interfaces.Embeddings.EmbeddingsLoading;
using Domain.Interfaces.Other;
using Domain.Interfaces.Quotas;
using Domain.Interfaces.Tasks;
using Domain.Services.Clusterization;
using Domain.Services.Clusterization.Algorithms;
using Domain.Services.Clusterization.Algorithms.Non_hierarchical;
using Domain.Services.Clusterization.Dimensions;
using Domain.Services.Clusterization.Displaying;
using Domain.Services.Clusterization.Profiles;
using Domain.Services.Clusterization.Workspaces;
using Domain.Services.Customers;
using Domain.Services.DataSources.ExternalData;
using Domain.Services.DataSources.Telegram;
using Domain.Services.DataSources.Youtube;
using Domain.Services.DimensionalityReduction;
using Domain.Services.EmbeddingModels;
using Domain.Services.Embeddings;
using Domain.Services.Embeddings.EmbeddingsLoading;
using Domain.Services.Quotas;
using Domain.Services.TaskServices;
using Domain.Validators.Clusterization.Workspaces;
using EmailService;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.SqlServer;
using Infrastructure;
using MathNet.Numerics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenAI.Extensions;
using System.Globalization;
using System.Reflection;
using System.Text;
using TL;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        var connectionString = builder.Configuration.GetConnectionString("LocalClusterizationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'HostClusterizationDbContextConnection' not found.");

        //var vectorConnectionString = builder.Configuration.GetConnectionString("VectorLocalClusterizationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'VectorLocalClusterizationDbContextConnection' not found.");

        ConfigurationManager configuration = builder.Configuration;

        builder.Services.AddDbContext<ClusterizationDbContext>(x => x.UseSqlServer(connectionString, builder =>
        {
            builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        }));


        builder.Services.AddHangfire(configuration => configuration
               .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
               .UseSimpleAssemblyNameTypeSerializer()
               .UseRecommendedSerializerSettings()
               .UseSqlServerStorage(builder.Configuration.GetConnectionString("LocalClusterizationDbContextConnection"), new SqlServerStorageOptions
               {
                   CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                   SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                   QueuePollInterval = TimeSpan.Zero,
                   UseRecommendedIsolationLevel = true,
                   DisableGlobalLocks = true,
               }));

        builder.Services.AddHangfireServer();

        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(AddWorkspaceValidator)));

        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped<IMyEmailSender, EmailSender>();

        builder.Services.AddSingleton<WTelegramService>();
        builder.Services.AddHostedService(provider => provider.GetService<WTelegramService>());

        builder.Services.AddLocalization();
        builder.Services.AddControllers();

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddScoped<IGeneralClusterizationAlgorithmService, GeneralClusterizationAlgorithmService>();
        builder.Services.AddScoped<IClusterizationAlgorithmTypesService, ClusterizationAlgorithmTypesService>();

        builder.Services.AddScoped<IAbstractClusterizationAlgorithmService<AddKMeansAlgorithmRequest, KMeansAlgorithmDTO>, KMeansAlgorithmService>();
        builder.Services.AddScoped<IAbstractClusterizationAlgorithmService<AddOneClusterAlgorithmRequest, OneClusterAlgorithmDTO>, OneClusterAlgorithmService>();
        builder.Services.AddScoped<IAbstractClusterizationAlgorithmService<AddDBSCANAlgorithmRequest, DBSCANAlgorithmDTO>, DBSCANAlgorithmService>();
        builder.Services.AddScoped<IAbstractClusterizationAlgorithmService<AddSpectralClusteringAlgorithmRequest, SpectralClusteringAlgorithmDTO>, SpectralClusteringAlgorithmService>();
        builder.Services.AddScoped<IAbstractClusterizationAlgorithmService<AddGaussianMixtureAlgorithmRequest, GaussianMixtureAlgorithmDTO>, GaussianMixtureAlgorithmService>();

        builder.Services.AddScoped<IClusterizationDimensionTypesService, ClusterizationDimensionTypesService>();
        
        builder.Services.AddScoped<IClusterizationDisplayedPointsService, ClusterizationDisplayedPointsService>();
        builder.Services.AddScoped<IClusterizationTilesService, ClusterizationTilesService>();
        builder.Services.AddScoped<IClusterizationTypesService, ClusterizationTypeService>();
        
        builder.Services.AddScoped<IClusterizationWorkspacesService, ClusterizationWorkspacesService>();
        builder.Services.AddScoped<IWorkspaceDataObjectsAddPacksService, WorkspaceDataObjectsAddPacksService>();
        builder.Services.AddScoped<IClusterizationProfilesService, ClusterizationProfilesService>();

        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IRolesService, RolesService>();

        builder.Services.AddScoped<IPrivateYoutubeChannelsService, PrivateYoutubeChannelsService>();
        builder.Services.AddScoped<IPrivateYoutubeVideosService, PrivateYoutubeVideosService>();
        builder.Services.AddScoped<IYoutubeChannelsService, YoutubeChannelsService>();
        builder.Services.AddScoped<IYoutubeVideoService, YoutubeVideosService>();
        builder.Services.AddScoped<IYoutubeCommentsService, YoutubeCommentsService>();
        builder.Services.AddScoped<IYoutubeDataObjectsService, YoutubeDataObjectsService>();

        builder.Services.AddScoped<ITelegramChannelsService, TelegramChannelsService>();
        builder.Services.AddScoped<ITelegramMessagesService, TelegramMessagesService>();
        builder.Services.AddScoped<ITelegramRepliesService, TelegramRepliesService>();
        builder.Services.AddScoped<ITelegramMessagesDataObjectsService, TelegramMessagesDataObjectsService>();
        builder.Services.AddScoped<ITelegramRepliesDataObjectsService, TelegramRepliesDataObjectsService>();

        builder.Services.AddScoped<IExternalDataObjectsService, ExternalDataObjectsService>();

        builder.Services.AddScoped<IDimensionalityReductionService, DimensionalityReductionService>();
        builder.Services.AddScoped<IDimensionalityReductionTechniquesService, DimensionalityReductionTechniquesService>();

        builder.Services.AddScoped<IEmbeddingModelsService, EmbeddingModelsService>();

        builder.Services.AddScoped<IEmbeddingsService, EmbeddingsService>();
        builder.Services.AddScoped<IEmbeddingsLoadingService, EmbeddingsLoadingService>();
        builder.Services.AddScoped<IEmbeddingLoadingStatesService, EmbeddingLoadingStatesService>();

        builder.Services.AddScoped<IMyTasksService, MyTasksService>();
        builder.Services.AddScoped<IUserTasksService, UserTasksService>();
        builder.Services.AddScoped<IModeratorTasksService, ModeratorTasksService>();

        builder.Services.AddScoped<IQuotasPacksService, QuotasPacksService>();
        builder.Services.AddScoped<ICustomerQuotasService, CustomerQuotasService>();
        builder.Services.AddScoped<IQuotasControllerService, QuotasControllerService>();
        builder.Services.AddScoped<IQuotasLogsService, QuotasLogsService>();
        builder.Services.AddScoped<IQuotasTypesService, QuotasTypesService>();

        builder.Services.AddOpenAIService();

        builder.Services.AddIdentity<Customer, IdentityRole>(o =>
        {
            o.Password.RequireDigit = false;
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = false;
            o.Password.RequireNonAlphanumeric = false;
            o.User.RequireUniqueEmail = true;
            o.User.AllowedUserNameCharacters = "АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯабвгґдеєжзиіїйклмнопрстуфхцчшщьюяĆćČčĎďĐđŁłŃńŇňŐőŘřŚśŠšŤťŽžљњћџђњћџABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþÿĀāĂăĄąĆćĈĉĊċČčĎďĐđĒēĔĕĖėĘęĚěĜĝĞğĠġĢģĤĥĦħĨĩĪīĬĭĮįİıĴĵĶķĹĺĻļĽľĿŀŁłŃńŅņŇňŉŌōŎŏŐőŒœŔŕŖŗŘřŚśŜŝŞşŠšŢţŤťŦŧŨũŪūŬŭŮůŰűŲųŴŵŶŷŸŹźŻżŽžǺǻǼǽǾǿȘșȚțəɐɑɒɓɔɕɖɗəɛɜɡɣɥɨɪɫɬɭɯɰɱɲɳɵɹɻɽɾʀʁʂʃʄʅʉʊʋʌʍʎʏʐʑʒʔμאבגдהוזחטיכלמנסעפצקרשתاآبتثجحخدذرزسشصضطظعغفقكلمنهوياأإآةىءصقفعظعظةلىكسمنتيكى_- ";
            o.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ClusterizationDbContext>()
        .AddDefaultTokenProviders();


        var jwtConfig = configuration.GetSection("JwtOptions");
        var secretKey = jwtConfig["Key"];

        builder.Services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfig["Issuer"],
                ValidAudience = jwtConfig["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];

                    // If the request is for our hub...
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/local_chat"))
                    {
                        // Read the token out of the query string
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "API docs", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseCors(options =>
        {
            options.WithOrigins("https://localhost:44439",
                                "http://user29750.realhost-free.net/",
                                "https://sladkovskygeorge.website/",
                                "https://clusterization.website/");
            options.AllowCredentials();
            options.AllowAnyHeader();
            options.AllowAnyMethod();
        });

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseMiddleware<ExceptionMiddleware>();

        var supportedCultures = new[]
        {
            new CultureInfo("uk-UA"),
            new CultureInfo("en-US")
        };

        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("uk-UA"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        });

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}");

        app.MapFallbackToFile("index.html");

        using (var scope = app.Services.CreateScope())
        {
            UserSeeders.Configure(scope.ServiceProvider).Wait();
        }

        app.Run();

    }
}