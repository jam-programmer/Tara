using Carter;
using TaraService.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
//builder.Services.AddCarter();
builder.Services.Infrastructure(builder.Configuration);
builder.Services.Application(builder.Configuration);

builder.Services.AddCarter();
builder.Services.AddHttpContextAccessor(); 




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

app.UseExceptionMiddleware();
app.UseRouting();
app.UseAuthorization();
app.MapCarter();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
