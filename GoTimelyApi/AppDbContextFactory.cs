using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using GoTimelyAPI;


public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer("Server=tcp:synthex.database.windows.net,1433;Initial Catalog=Synthex;Persist Security Info=False;User ID=synthex;Password=Adminsql20;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        return new AppDbContext(optionsBuilder.Options);
    }
}
