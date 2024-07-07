using BlazorFastReport.Data;
using FastReport.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container. Entity Framework Core
var Connection = builder.Configuration.GetConnectionString("DefaultConnection");  // Add this line

builder.Services.AddDbContext<BlazorDbContext>(options =>
	options.UseSqlServer(Connection));    // Add this line

builder.Services.AddFastReport();  // Add this line

// Add services to the container. FastReport
FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));  // Add this line

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseFastReport(); // Add this line

app.UseRouting();


app.UseEndpoints(endpoints =>
{
	app.MapControllers();  // Add this line
	app.MapBlazorHub();
	app.MapFallbackToPage("/_Host");
});



app.Run();
