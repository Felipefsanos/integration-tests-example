using Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Person> People { get; init; } = null!;
    public DbSet<Address> Addresses { get; init; } = null!;
}