using Hangfire;
var builder = WebApplication.CreateBuilder(args);
var hangfileDB = builder.Configuration.GetConnectionString("CodeIsBug.Admin.HangfileDB");
builder.Services.AddControllersWithViews();
builder.Services.AddHangfire(options =>
{
    options.UseSqlServerStorage(hangfileDB);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseHangfireServer();
app.UseHangfireDashboard();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
