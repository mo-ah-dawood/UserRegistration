using Microsoft.EntityFrameworkCore;
using UserRegistration.Infrastructure.Data;
using UserRegistration.Infrastructure;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using UserRegistration.Api.Sawgger;
using UserRegistration.Api;
using Microsoft.AspNetCore.Mvc;
using UserRegistration.Api.Response;
using UserRegistration.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(SharedResources));
    });
builder.Services.AddLocalization();
builder.Services.AddRequestLocalization((options) =>
{
    CultureInfo[] supportedLocales = [new("ar"), new("en")];
    options.DefaultRequestCulture = new RequestCulture("en");
    options.ApplyCurrentCultureToResponseHeaders = true;
    options.SupportedCultures = supportedLocales;
    options.SupportedUICultures = supportedLocales;
});


builder.Services.Configure<ApiBehaviorOptions>(o =>
{
    o.InvalidModelStateResponseFactory = actionContext =>
        new BadRequestObjectResult(GenericResult.ValidationErrors(actionContext.ModelState));
});

builder.Services.AddDbContext<AppDbContext>(options =>
         options.UseInMemoryDatabase("UserRegistrationDB"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen((o) =>
{
    o.OperationFilter<AcceptLanguageFilter>();
});

builder.Services.InjectInfra();

builder.Services.Configure<VerificationCodeConfig>(builder.Configuration.GetSection("VerificationCodeConfig"));



var app = builder.Build();

app.UseRequestLocalization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

