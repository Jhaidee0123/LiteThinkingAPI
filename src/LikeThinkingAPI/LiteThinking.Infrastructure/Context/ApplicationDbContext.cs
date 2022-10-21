using LiteThinking.Domain.Entities.Companies;
using LiteThinking.Domain.Entities.Users;
using LiteThinking.Domain.Ports.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LiteThinking.Infrastructure.Context;

public class ApplicationDbContext : IdentityDbContext<User>, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Inventory> Inventory { get; private set; }

    public DbSet<Company> Company { get; private set; }

    public DbSet<Article> Article { get; private set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
