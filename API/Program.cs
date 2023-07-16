using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Domain.Features;
using Infrastructure.EF;
using Infrastructure.Entities;
using Library.Common;
using Library.Extensions.ExceptionMiddleware;
using Library.Extensions.ExtensionServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MaleFashionDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("MaleFashionDb")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var mapperConfig = new MapperConfiguration(config =>
{
    config.CreateMapByNamingConvention();
});
IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);
builder.Services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<MaleFashionDbContext>()
                .AddDefaultTokenProviders();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyOrigin()
                                .AllowAnyMethod();
        });
});
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
//ADd services
builder.Services.AddServices();
builder.Services.AddReponsitories();
builder.Services.AddSwagger();
builder.Services.AddControllersWithViews();



string issuer = builder.Configuration.GetValue<string>("Tokens:Issuer");
string signingKey = builder.Configuration.GetValue<string>("Tokens:Key");
byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = false, // on production make it true
        ValidateAudience = false, // on production make it true
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorizeEx();
/*builder.Services.AddAuthorization(options =>
{
    var assembly = Assembly.Load("API");
    var controllerTypes = assembly.GetExportedTypes()
        .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType && t.Name.EndsWith("Controller"))
        .ToList();
    var constFields = new List<FieldInfo>();

    foreach (var type in controllerTypes)
    {
        var controllerType = type;
        var fields = controllerType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.IsLiteral && !f.IsInitOnly)
            .ToList();
        constFields.AddRange(fields);
    }
    foreach (var field in constFields)
    {
        var fieldValue = field.GetValue(null);
        if (fieldValue != null)
        {
            if (!String.IsNullOrEmpty(fieldValue.ToString()))
            {
                options.AddPolicy(fieldValue.ToString(), policy =>
                {
                    policy.RequireClaim(fieldValue.ToString());
                });
            }
        }
    }
});*/
/*var logger = builder.Logging.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();*/
var serviceProvider = builder.Services.BuildServiceProvider();
var logger = serviceProvider.GetService<ILogger<Program>>();
builder.Services.AddSingleton(typeof(ILogger), logger);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

/*app.ConfigureExceptionHandle(logger);*/
app.ConfigureCustomExceptionMiddleware();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStatusCodePages();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger eShopSolution V1");
});
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();
