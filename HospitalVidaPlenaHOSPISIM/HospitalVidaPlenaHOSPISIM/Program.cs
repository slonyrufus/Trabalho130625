using HospisimContext.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
    new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Swagger HOSPISIM",
        Description = "Sistema Swagger HOSPISIM",
        Contact = new OpenApiContact
        {
            Name = "API de Hospital HOSPISIM",
            Email = "silvasena@unitins.br",
            Url = new Uri("https://unitins.br")
        }
    });

    //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});


//builder.Services.AddDbContext<HospisimContext.Data.HospisimContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()));
builder.Services.AddDbContext<HospisimContext.Data.HospisimContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine));


builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<HospisimContext.Data.HospisimContext>();
    DbInitializer.Initialize(ctx);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
