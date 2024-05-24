namespace Application.Services.Abstractions;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}