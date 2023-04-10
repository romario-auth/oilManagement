using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OilManagementMvc.Models;

namespace OilManagementMvc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<OilManagementMvc.Models.CollectPoint>? CollectPoint { get; set; }
        public DbSet<OilManagementMvc.Models.Recycle>? Recycle { get; set; }
    }
}