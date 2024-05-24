using Application.Entities;

namespace Application.Repositories.Abstractions;

public interface IPeopleRepository
{
    Task<IEnumerable<Person>> GetAll();
    Task<Person?> GetById(Guid id);
    void Update(Person person);
    Task Create(Person person);
    void Delete(Person person);
}