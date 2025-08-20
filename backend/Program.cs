var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Configure to run on port 3000, binding to all interfaces for Codesphere
app.Run("http://0.0.0.0:3000");
