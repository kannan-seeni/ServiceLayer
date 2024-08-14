#region References
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mime;
using System.Text;
using TNCSC.Hulling.Business.Interfaces;
using TNCSC.Hulling.Business.Services;
using TNCSC.Hulling.Components;
using TNCSC.Hulling.Components.Interfaces;
using TNCSC.Hulling.Domain;
using TNCSC.Hulling.Repository.Interfaces;
using TNCSC.Hulling.Repository.Services;
using TNCSC.Hulling.ServiceLayer;
using TNCSC.Hulling.ServiceLayer.Filters;

#endregion

#region PipeLine

#region Services

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Services.AddMvc();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddControllers().AddNewtonsoftJson();

#endregion

#region Components

var dbConnection = builder.Configuration.GetValue<string>("ConfigurationManager:DefaultConnection");

var dbTable = builder.Configuration.GetValue<string>("ConfigurationManager:ConfigTable");

builder.Services.AddConfigManager(dbConnection, dbTable);

var sp = builder.Services.BuildServiceProvider();

var configManager = sp.GetService<IConfigManager>();

builder.Services.AddAuditManager(configManager.GetConfig("AuditLog.DefaultConnection"), Convert.ToBoolean(configManager.GetConfig("AuditLog.isEnabled")), Convert.ToBoolean(configManager.GetConfig("AuditLog.PayLoad")));
 
builder.Services.AddLogger(configManager.GetConfig("Logger.Type"), configManager.GetConfig("Logger.Target"), configManager.GetConfig("Logger.FolderPath"), configManager.GetConfig("Logger.ConnectionString"));
 
builder.Services.AddAuditAttribute(configManager.GetConfig("AuditLog.DefaultConnection"), Convert.ToBoolean(configManager.GetConfig("AuditLog.isEnabled")), Convert.ToBoolean(configManager.GetConfig("AuditLog.PayLoad")));

builder.Services.AddTransient<IDbConnection>((serviceProvider) => new SqlConnection(dbConnection));

#endregion

#region API Behaviors

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.Configure<IISOptions>(options =>
{
    options.AutomaticAuthentication = false;
});
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsDevPolicy", builder =>
    {
        builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
    });

});
builder.Services.AddMvc()
              .AddFluentValidation(mvcConfiguration => mvcConfiguration.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddMvc() 
               .AddMvcOptions(options =>
               {
                   options.Filters.Add(new TokenAuthValidator());
               });

builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition =
    System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddMvc(options =>
        options.Filters.Add(new HttpResponseExceptionFilter()));

builder.Services.AddMvc()

    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var result = new BadRequestObjectResult(context.ModelState);

            // TODO: add `using System.Net.Mime;` to resolve MediaTypeNames
            result.ContentTypes.Add(MediaTypeNames.Application.Json);
            result.ContentTypes.Add(MediaTypeNames.Application.Xml);

            return result;
        };

        options.SuppressConsumesConstraintForFormFileParameters = true;
        options.SuppressInferBindingSourcesForParameters = true;
        options.SuppressModelStateInvalidFilter = true;
        options.SuppressMapClientErrors = true;
        options.ClientErrorMapping[404].Link =
            "https://httpstatuses.com/404";
    });
#endregion

#region TNCSC Hulling Services

builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<ILoginRepository, LoginRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IGodwonService, GodwonService>();
builder.Services.AddTransient<IGodwonRepository, GodwonRepository>();
builder.Services.AddTransient<IPaddyService, PaddyService>();
builder.Services.AddTransient<IPaddyRepository, PaddyRepository>();
builder.Services.AddTransient<IRiceService, RiceService>();
builder.Services.AddTransient<IRiceRepository, RiceRepository>();
builder.Services.AddTransient<IGunnyService, GunnyService>();
builder.Services.AddTransient<IGunnyRepository, GunnyRepository>();
builder.Services.AddTransient<IFRKService, FRKService>();
builder.Services.AddTransient<IFRKRepository, FRKRepository>();
builder.Services.AddTransient<IMillService, MillService>();
builder.Services.AddTransient<IMillRepository, MillRepository>();
builder.Services.AddTransient<IMasterDataService, MasterDataService>();
builder.Services.AddTransient<IMasterDataRepository, MasterDataRepository>();
#endregion

#region JWT
var jwtSettings = new JwtSettings();
 
jwtSettings.Secret = configManager.GetConfig("JwtSettings.Secret");
string[] values = configManager.GetConfig("JwtSettings.TokenLifeTime").Split(':');
TimeSpan timeSpan = new TimeSpan(Convert.ToInt32(values[1]), Convert.ToInt32(values[2]), 0);
jwtSettings.TokenLifeTime = timeSpan;
builder.Services.AddSingleton(jwtSettings);

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key: Encoding.ASCII.GetBytes(jwtSettings.Secret)),
    ValidateIssuer = false,
    ValidateAudience = false,
    RequireExpirationTime = false,
    ValidateLifetime = true
};

builder.Services.AddSingleton(tokenValidationParameters);
builder.Services.AddAuthentication(configureOptions: x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.SaveToken = true;
    x.TokenValidationParameters = tokenValidationParameters;
});

builder.Services.AddSession();

#endregion

#region SwaggerGen
var JsonRoute = builder.Configuration.GetValue<string>("SwaggerOptions:JsonRoute");
var Description = builder.Configuration.GetValue<string>("SwaggerOptions:Description"); 
var UIEndpoint = builder.Configuration.GetValue<string>("SwaggerOptions:UIEndpoint");
var Version = builder.Configuration.GetValue<string>("SwaggerOptions:Version");


builder.Services.AddSwaggerGen(x =>
{
    x.UseAllOfToExtendReferenceSchemas();
    x.SwaggerDoc("v1", new OpenApiInfo { Title = Description, Version = Version });

    x.SchemaFilter<SwaggerIgnore>();

    var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer",new string[0]}
                };
    x.AddSecurityDefinition(name: "Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    x.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                         new OpenApiSecurityScheme
                         {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                         new List<string>()
                    }
                });
});
#endregion

#region App
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 
app.UseSwagger(option => { option.RouteTemplate = JsonRoute; });
app.UseSwaggerUI(option =>
{
    option.SwaggerEndpoint(UIEndpoint, Description);
});

app.UseStaticFiles();

app.UseCors(builder => builder
 .AllowAnyOrigin()
 .AllowAnyMethod()
 .AllowAnyHeader());

app.UseRouting();
app.UseCookiePolicy();
app.UseAuthorization();
app.UseAuthentication();
app.UseSession();
app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseHttpsRedirection();
  
var host = new WebHostBuilder()
.UseKestrel(options =>
{
    options.Limits.MaxRequestBufferSize = 302768;
    options.Limits.MaxRequestLineSize = 302768;
});

 

app.Run();

#endregion

#endregion
