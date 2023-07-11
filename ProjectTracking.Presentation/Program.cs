using Microsoft.EntityFrameworkCore;
using ProjectTracking.Bussiness.Repositories.Classes.ProjectServices.Abstract;
using ProjectTracking.Bussiness.Repositories.Classes.ProjectServices.Concrete;
using ProjectTracking.Bussiness.Repositories.Classes.MissionServices.Abstract;
using ProjectTracking.Bussiness.Repositories.Classes.MissionServices.Concrete;
using ProjectTracking.Bussiness.Repositories.Classes.UserServices.Abstract;
using ProjectTracking.Bussiness.Repositories.Classes.UserServices.Concrete;
using ProjectTracking.DataAccess.Context;
using ProjectTracking.Bussiness.Repositories.Classes.RequisitionServices.Abstract;
using ProjectTracking.Bussiness.Repositories.Classes.RequisitionServices.Concrete;
using ProjectTracking.Bussiness.Repositories.Classes.ProjectNoteService.Abstract;
using ProjectTracking.Bussiness.Repositories.Classes.ProjectNoteService.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSession(s =>
{
    s.IdleTimeout = TimeSpan.FromMinutes(30);

});
builder.Services.AddDbContext<ProjectTrackingDataContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlBaglantim"));
});

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddTransient<IMissionService, MissionService>();
builder.Services.AddTransient<IRequisitionService, RequisitionService>();
builder.Services.AddTransient<IProjectNoteService, ProjectNoteService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
