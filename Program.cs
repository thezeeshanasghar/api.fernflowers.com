using api.fernflowers.com.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options => { options.UseMemberCasing(); });
builder.Services.AddControllers(options => options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider()));
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder => { builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); }));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "api.fernflowers.com", Version = "v1" });
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));



// builder.Services.AddDbContext<VaccineDBContext>( 
// options => options.UseMySql("Server=localhost;Database=ef;User=root;Password=123456;",

//     mySqlOptions =>
//     {
//         mySqlOptions.(new Version(5, 7, 17))
//         .EnableRetryOnFailure(
//         maxRetryCount: 10,
//         maxRetryDelay: TimeSpan.FromSeconds(30),
//         errorNumbersToAdd: null); 
//     }
// ));


builder.Services.AddDbContext<VaccineDBContext>(options =>
{
    options.UseMySql(connectionString, serverVersion, mySqlOptions =>
    {
        mySqlOptions.EnableRetryOnFailure(
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null
        );
    });
});



// builder.Services.AddDbContext<VaccineDBContext>(
//     options => options
    
//         .UseMySql(connectionString, serverVersion)
//         // The following three options help with debugging, but should
//         // be changed or removed for production.
//         .LogTo(Console.WriteLine, LogLevel.Information)
//         .EnableSensitiveDataLogging()
//         .EnableDetailedErrors()
        
//         // {
//         // options.UseMySql(connectionString,serverVersion);
//         // }
//         );
//         builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "api.fernflowers.com v1");
         c.RoutePrefix = string.Empty;
    });
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("corsapp");

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<VaccineDBContext>();
    // dbContext.Database.EnsureCreated(); // Optional: Ensure the database is created before applying the changes
    // dbContext.Database.Migrate(); // Optional: Apply pending migrations before applying the changes
}

app.Run();
