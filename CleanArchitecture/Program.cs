using Azure.Storage.Blobs;
using CleanArchitecture;
using CleanArchitecture.Extenction_Method;
using CleanArchitecture.GlobalException;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SendGrid.Extensions.DependencyInjection;
using Serilog;
using Stripe;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
//?--------------------------------------SendGrid-------------------------------------------------------

builder.Services.AddSendGrid(options =>
{
    options.ApiKey = builder.Configuration
    .GetSection("SendGridEmailSettings").GetValue<string>("APIKey");
});

//?----------------------BlobClient azure----------------------------------------------

builder.Services.AddScoped(_ =>
{
    return new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorage"));
});

//?-------------------------database Connection ==============================================

builder.Services.AddDatabase(builder.Configuration)
                .AddServices()
                .AddJwt(builder.Configuration)
                .AddStripeInfrastructure(builder.Configuration);

//?----------------------- Serilog Configuraion  --------------------------------
string con = builder.Configuration.GetConnectionString("DefaultConnection");
string table = "Logs";

//var _Logger = new LoggerConfiguration()
//    .MinimumLevel.Information()
//    .WriteTo.MSSqlServer(con, table).CreateLogger();
//builder.Logging.AddSerilog(_Logger);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.MSSqlServer(con, table).CreateLogger();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//?-------------------------Api swager Authorization Connection ==============================================
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
}
);

//?----------------------------smtp mail--------------------------------
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

//builder.Services.AddSwaggerGen(o => o.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
//{
//    Title = "",
//    Version = "v1",
//    Description = "Des1",
//}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//? GLobal Exception handling----------------------------
app.UseMiddleware<GlobalExceptionHandling>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();