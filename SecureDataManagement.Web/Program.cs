using SecureDataManagement.Web.Extensions;
using SecureDataManagement.Web.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from appsettings.json.
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Using the extension method for database configuration.
builder.Services.AddConnectionString(builder.Configuration);

// Add AutoMapper services
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add application dependency injection.
builder.Services.AddDIScoped();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
