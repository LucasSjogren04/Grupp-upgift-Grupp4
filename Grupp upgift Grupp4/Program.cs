using Grupp_upgift_Grupp4.Repository.Repo;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.SignalR;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//builder.Services.AddAuthentication("BasicAuthentication")
    //.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

//builder.Services.AddControllers(options =>
//{
//    options.ModelBinderProviders.Insert(0, new HeaderModelBinderProvider());
//});

//builder.Services.AddEndpointsApiExplorer();




//Denna tjänst sätt upp som singleton det finns bara en instance
//builder.Services.AddSingleton<IDBContext, DbContext>();



builder.Services.AddHttpContextAccessor();
//Information son tex: en läggas ofta i en konfigfilr som heter

// Add authorization
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // Configure Basic Authentication
    c.AddSecurityDefinition("BasicAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using the Bearer scheme.",
    });

   // Make sure Swagger UI requires a Bearer token to be provided
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "BasicAuth",
                },
            },
            System.Array.Empty<string>()
        }
    });
});


var app = builder.Build();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();

app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
//app.UseAuthorization();

app.Run();





