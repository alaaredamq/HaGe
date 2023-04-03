using HaGe.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HaGe.Infrastructure.Context;

public class HaGeContext : IdentityDbContext<IdentityUser> {
    public HaGeContext(DbContextOptions<HaGeContext> options) : base(options) {
    }

    public HaGeContext() {
    }
    
    public DbSet<CodeList> CodeList { get; set; }
    public DbSet<CodeListValue> CodeListValue { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<Profile> Profile { get; set; }
    public DbSet<Level> Level{ get; set; }
    public DbSet<LevelProgression> LevelProgression{ get; set; }
}   