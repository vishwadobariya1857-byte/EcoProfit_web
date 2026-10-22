using EcoProfit.Data;
using EcoProfit.Services;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var cs = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Missing MySQL connection string");
builder.Services.AddDbContext<AppDbContext>(o => o.UseMySql(cs, ServerVersion.AutoDetect(cs)));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(o => { o.IdleTimeout = TimeSpan.FromMinutes(45); o.Cookie.HttpOnly = true; o.Cookie.IsEssential = true; });
builder.Services.AddScoped<WasteAdvisor>();
var app = builder.Build();
if (!app.Environment.IsDevelopment()) { app.UseExceptionHandler("/Home/Error"); app.UseHsts(); }
app.UseHttpsRedirection();app.UseStaticFiles();app.UseRouting();app.UseSession();
app.MapControllerRoute(name:"default",pattern:"{controller=Account}/{action=Login}/{id?}");
app.Run();