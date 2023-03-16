using AppServer.Data.Models;
using AppServer.Data.Services;
using AppServer.Domain.Services;
using AppServer.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

// Configure service lifetime
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IThirdPartyDataAccessApiService<Post>, PostsDataService>();

builder.Services.AddLogging();

// Add CORS Policy
var corsOriginWhiteList = builder.Configuration.GetSection("CORS:AllowedOrigins")
    .AsEnumerable()
    .Select(x => x.Value)
    .Where(x => x != null)
    .ToArray();

const string DEFAULT_CORS_POLICY_NAME = "default";

builder.Services.AddCors(options =>
{
    options.AddPolicy(DEFAULT_CORS_POLICY_NAME, policy =>
    {
        policy.WithOrigins(corsOriginWhiteList);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ValidateGetPostsRequestMiddleware>();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseRouting();
app.UseCors(DEFAULT_CORS_POLICY_NAME);
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
