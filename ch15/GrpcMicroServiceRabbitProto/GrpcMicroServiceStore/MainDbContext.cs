using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using GrpcMicroServiceStore.Models;
using System.Threading.Tasks;

namespace GrpcMicroServiceStore;

internal class MainDbContext : DbContext, IUnitOfWork
{
    public MainDbContext(DbContextOptions options)
        : base(options) { }
    public DbSet<DayTotal> DayTotals { get; set; }
    public DbSet<QueueItem> QueueItems { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.Entity<QueueItem>()
            .HasIndex(m => m.Time);
        builder.Entity<Purchase>()
            .HasIndex(m => m.Time);
    }
    public async Task<bool> SaveEntitiesAsync()
    {
            return await SaveChangesAsync() > 0;
    }

    public async Task StartAsync()
    {
        await Database.BeginTransactionAsync();
    }

    public Task CommitAsync()
    {
        Database.CommitTransaction();
        return Task.CompletedTask;
    }

    public Task RollbackAsync()
    {
        Database.RollbackTransaction();
        return Task.CompletedTask;
    }
}

