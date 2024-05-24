using Application.Context;
using Application.Entities;
using Application.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

public class PeopleRepository(AppDbContext context) : IPeopleRepository
{
    public async Task<IEnumerable<Person>> GetAll() => 
        await context.People.Include(p => p.Address).ToListAsync();

    public async Task<Person?> GetById(Guid id) => 
        await context.People.Include(p => p.Address).FirstOrDefaultAsync(p => p.Id == id);

    public void Update(Person person) => 
        context.People.Update(person);

    public async Task Create(Person person) => 
        await context.People.AddAsync(person);

    public void Delete(Person person) => 
        context.People.Remove(person);
}