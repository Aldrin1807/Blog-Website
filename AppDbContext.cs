using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog
{
    public class AppDbContext:IdentityDbContext
    {

        public AppDbContext(DbContextOptions options):base(options)
        {
        }

    }
}
