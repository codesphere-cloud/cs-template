using frontend.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Configure HttpClient for backend API
builder.Services.AddHttpClient<RateService>(client =>
{
    // Configure base address to point to the backend service via Codesphere routing
    client.BaseAddress = new Uri("http://localhost:5000/"); // Frontend will use relative URLs to backend
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// Configure to bind to all interfaces for Codesphere
app.Run("http://0.0.0.0:5000");
