var builder = WebApplication.CreateBuilder(args);

// Local appsettings.Local.json
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);

// Add services to the container.
builder.Services.AddControllers();

// Configure ProductStoredProcedures from appsettings
builder.Services.Configure<SoftAPINew.Models.ProductStoredProcedures>(
    builder.Configuration.GetSection("ProductStoredProcedures"));

// Register IDataRepository with dependency injection
builder.Services.AddScoped<SoftAPINew.Infrastructure.Interfaces.IDataRepository, SoftAPINew.Infrastructure.Interfaces.Repositories.SqlData.SqlDataRepository>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("AP") ?? throw new InvalidOperationException("Connection string 'AP' not found.");
    return new SoftAPINew.Infrastructure.Interfaces.Repositories.SqlData.SqlDataRepository(connectionString);
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Remove CORS policy for development purposes
app.UseCors(policy => 
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader()
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
