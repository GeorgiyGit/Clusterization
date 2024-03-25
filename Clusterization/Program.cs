using Clusterization.Middlewares;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.DBScanDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.GaussianMixtureDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.OneClusterDTOs;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.SpectralClusteringDTOs;
using Domain.Entities.Customers;
using Domain.Interfaces;
using Domain.Interfaces.Clusterization;
using Domain.Interfaces.Clusterization.Algorithms;
using Domain.Interfaces.Clusterization.Profiles;
using Domain.Interfaces.Customers;
using Domain.Interfaces.DimensionalityReduction;
using Domain.Interfaces.Embeddings;
using Domain.Interfaces.Tasks;
using Domain.Interfaces.Youtube;
using Domain.Services.Clusterization;
using Domain.Services.Clusterization.Algorithms;
using Domain.Services.Clusterization.Algorithms.Non_hierarchical;
using Domain.Services.Clusterization.Profiles;
using Domain.Services.Customers;
using Domain.Services.DimensionalityReduction;
using Domain.Services.Embeddings;
using Domain.Services.TaskServices;
using Domain.Services.Youtube;
using Domain.Validators.Clusterization.Workspaces;
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
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("LocalClusterizationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'HostClusterizationDbContextConnection' not found.");

//var vectorConnectionString = builder.Configuration.GetConnectionString("VectorLocalClusterizationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'VectorLocalClusterizationDbContextConnection' not found.");

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddDbContext<ClusterizationDbContext>(x => x.UseSqlServer(connectionString, builder =>
{
    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
}));

//builder.Services.AddDbContext<VectorDbContext>(x => x.UseSqlite(vectorConnectionString));


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

builder.Services.AddLocalization();
builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IPrivateYoutubeChannelsService, PrivateYoutubeChannelsService>();
builder.Services.AddScoped<IPrivateYoutubeVideosService, PrivateYoutubeVideosService>();
builder.Services.AddScoped<IYoutubeChannelsService, YoutubeChannelsService>();
builder.Services.AddScoped<IYoutubeVideoService, YoutubeVideosService>();
builder.Services.AddScoped<IYoutubeCommentsService, YoutubeCommentsService>();
builder.Services.AddScoped<IMyTasksService, MyTasksService>();

builder.Services.AddScoped<IClusterizationDimensionTypesService, ClusterizationDimensionTypesService>();
builder.Services.AddScoped<IClusterizationTypesService, ClusterizationTypeService>();
builder.Services.AddScoped<IClusterizationWorkspacesService, ClusterizationWorkspacesService>();
builder.Services.AddScoped<IClusterizationProfilesService, ClusterizationProfilesService>();
builder.Services.AddScoped<IClusterizationTilesService, ClusterizationTilesService>();
builder.Services.AddScoped<IClusterizationDisplayedPointsService, ClusterizationDisplayedPointsService>();

builder.Services.AddScoped<IEmbeddingsService, EmbeddingsService>();
builder.Services.AddScoped<ILoadEmbeddingsService, LoadEmbeddingsService>();

builder.Services.AddScoped<IDimensionalityReductionTechniquesService, DimensionalityReductionTechniquesService>();
builder.Services.AddScoped<IDimensionalityReductionValuesService, DimensionalityReductionValuesService>();

builder.Services.AddScoped<IGeneralClusterizationAlgorithmService, GeneralClusterizationAlgorithmService>();
builder.Services.AddScoped<IClusterizationAlgorithmTypesService, ClusterizationAlgorithmTypesService>();

builder.Services.AddScoped<IAbstractClusterizationAlgorithmService<AddKMeansAlgorithmDTO,KMeansAlgorithmDTO>, KMeansAlgorithmService>();
builder.Services.AddScoped<IAbstractClusterizationAlgorithmService<AddOneClusterAlgorithmDTO, OneClusterAlgorithmDTO>, OneClusterAlgorithmService>();
builder.Services.AddScoped<IAbstractClusterizationAlgorithmService<AddDBScanAlgorithmDTO, DBScanAlgorithmDTO>, DbScanAlgorithmService>();
builder.Services.AddScoped<IAbstractClusterizationAlgorithmService<AddSpectralClusteringAlgorithmDTO, SpectralClusteringAlgorithmDTO>, SpectralClusteringAlgorithmService>();
builder.Services.AddScoped<IAbstractClusterizationAlgorithmService<AddGaussianMixtureAlgorithmDTO, GaussianMixtureAlgorithmDTO>, GaussianMixtureAlgorithmService>();


builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IClusterizationAlgorithmsHelpService, ClusterizationAlgorithmsHelpService>();
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication();

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
            if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/local_chat")))
            {
                // Read the token out of the query string
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
