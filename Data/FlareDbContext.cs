using Microsoft.EntityFrameworkCore;
using Flare.AccountService.Models;

namespace Flare.AccountService.Data;

public class FlareDbContext : DbContext
{
    public FlareDbContext(DbContextOptions<FlareDbContext> options) : base(options) { }

    public DbSet<Account> Accounts => Set<Account>();
}
