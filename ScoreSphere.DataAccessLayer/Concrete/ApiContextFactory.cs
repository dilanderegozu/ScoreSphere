using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ScoreSphere.DataAccessLayer.Concrete
{
    public class ApiContextFactory : IDesignTimeDbContextFactory<ApiContext>
    {
        public ApiContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApiContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=ScoreSphere;Trusted_Connection=True;TrustServerCertificate=True;");
            return new ApiContext(optionsBuilder.Options);
        }
    }
}