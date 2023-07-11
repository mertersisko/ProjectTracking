using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.BaseClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.MissionClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.ProjectClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.ProjectNoteClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.RequestClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.UserClasses;

namespace ProjectTracking.DataAccess.Context;

public class ProjectTrackingDataContext :DbContext
{
    public ProjectTrackingDataContext(DbContextOptions<ProjectTrackingDataContext> options) : base(options)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ////Proje tablosunda, Proje notları ile ilişkili bir veri silinirse. Projenin notları setnull olarak kalsın
        //modelBuilder.Entity<ProjectNote>().HasOne(o => o.Project).WithMany(w => w.ProjectNotes).OnDelete(DeleteBehavior.SetNull);
        

        //Seed Data 
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Name = "Admin",
            Surname = "Admin",
            Email = "admin@admin.com",
            Password = "123456",
            CreatedBy = "Admin",
            CreatedDate = DateTime.Now,
            Active = true,
            Deleted = false,

        });
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        ChangeTracker.DetectChanges();

        var entires = ChangeTracker.Entries();

     

        foreach (var item in entires)
        {
            if (item.Entity is BaseEntity entry)
            {
               
                switch (item.State)
                {
                    case EntityState.Added:
                        entry.Active = true;
                        entry.CreatedDate = DateTime.Now;
                        entry.Deleted = false;
                        entry.CreatedBy = "-";
                        entry.ModifiedDate = DateTime.Now;
                        entry.ModifiedBy = "-";
                        break;
                    case EntityState.Modified:
                        entry.ModifiedDate = DateTime.Now;
                        entry.ModifiedBy = "-";
                        break;
                }
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectNote> Notes { get; set; }
    public DbSet<Mission> Missions { get; set; }
    public DbSet<Requisition> Requisitions { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
    //        var connectionString = configuration.GetConnectionString("SqlBaglantim");
    //        optionsBuilder.UseSqlServer(connectionString);
    //    }
    //}
}
