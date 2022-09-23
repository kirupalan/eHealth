using eHealthAPI.Data;
using eHealthAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Kiru: Startup.cs - start
//ConfigurationManager configuration = builder.Configuration;
//IWebHostEnvironment environment = builder.Environment;
builder.Services.AddCors((options) =>
{
    options.AddPolicy("angularApplication", (builder) =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .WithMethods("GET","POST", "PUT", "DELETE")
        .WithExposedHeaders("*");
    });
});
//Kiru: Startup.cs - end

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<eHealthDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

//Kiru: Image
builder.Services.AddScoped<IImageRepository, ImageRepository>();

//Kiru: Interface
builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();

//Kiru: AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Resources")),
    RequestPath = "/Resources"
});

//Kiru: Startup.cs - start
app.UseCors("angularApplication");
//Kiru: Startup.cs - end

app.UseAuthorization();

app.MapControllers();

app.Run();