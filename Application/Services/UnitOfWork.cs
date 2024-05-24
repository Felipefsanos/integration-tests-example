using Application.Context;
using Application.Services.Abstractions;

namespace Application.Services;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}