using Clusterization.Middlewares;
using Domain.DTOs.ClusterizationDTOs.AlghorithmDTOs.Non_hierarchical.KMeansDTOs;
using Domain.Interfaces;
using Domain.Interfaces.Clusterization;
using Domain.Interfaces.Clusterization.Algorithms;
using Domain.Interfaces.Embeddings;
using Domain.Interfaces.Tasks;
using Domain.Interfaces.Youtube;
using Domain.Services.Clusterization;
using Domain.Services.Clusterization.Algorithms;
using Domain.Services.Clusterization.Algorithms.Non_hierarchical;
using Domain.Services.Embeddings;
using Domain.Services.TaskServices;
using Domain.Services.Youtube;
using Domain.Validators.Clusterization.Workspaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.SqlServer;
using Infrastructure;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("LocalClusterizationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'LocalClusterizationDbContextConnection' not found.");

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
           DisableGlobalLocks = true
       }));

builder.Services.AddHangfireServer();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(AddWorkspaceValidator)));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddLocalization();
builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IPrivateYoutubeChannelService, PrivateYoutubeChannelService>();
builder.Services.AddScoped<IPrivateYoutubeVideoService, PrivateYoutubeVideoService>();
builder.Services.AddScoped<IYoutubeChannelService, YoutubeChannelService>();
builder.Services.AddScoped<IYoutubeVideoService, YoutubeVideoService>();
builder.Services.AddScoped<IYoutubeCommentsService, YoutubeCommentsService>();
builder.Services.AddScoped<IMyTaskService, MyTaskService>();

builder.Services.AddScoped<IClusterizationDimensionTypeService, ClusterizationDimensionTypeService>();
builder.Services.AddScoped<IClusterizationTypeService, ClusterizationTypeService>();
builder.Services.AddScoped<IClusterizationWorkspaceService, ClusterizationWorkspaceService>();

builder.Services.AddScoped<IEmbeddingsService, EmbeddingsService>();
builder.Services.AddScoped<ILoadEmbeddingsService, LoadEmbeddingsService>();

builder.Services.AddScoped<IGeneralClusterizationAlgorithmService, GeneralClusterizationAlgorithmService>();
builder.Services.AddScoped<IClusterizationAlgorithmTypesService, ClusterizationAlgorithmTypesService>();
builder.Services.AddScoped<IAbstractClusterizationAlgorithmService<AddKMeansAlgorithmDTO,KMeansAlgorithmDTO>, KMeansAlgorithmService>();

builder.Services.AddControllersWithViews();

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
                        "http://user29750.realhost-free.net/");
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
