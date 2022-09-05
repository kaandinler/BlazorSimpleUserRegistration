using BlazorSimpleUserRegistration.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorSimpleUserRegistration.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users{get;set;}
        
    }
}
