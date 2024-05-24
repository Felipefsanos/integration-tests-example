using Application.Dto;
using Application.Entities;
using Application.Repositories.Abstractions;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController(IPeopleRepository repository, IUnitOfWork unitOfWork) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetPeople()
        {
            var people = await repository.GetAll();

            return people.Select(p => new PersonDto(p)).ToList();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PersonDto>> GetPerson(Guid id)
        {
            var person = await repository.GetById(id);

            if (person is null)
                return NotFound(new { Message = "Person not found" });

            return new PersonDto(person);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutPerson(Guid id, PersonDto personRequest)
        {
            var person = await repository.GetById(id);

            if (person is null)
                return NotFound(new { Message = "Person not found" });

            person.Update(personRequest);

            repository.Update(person);

            await unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<PersonDto>> PostPerson(PersonDto personRequest)
        {
            var person = new Person(personRequest);

            await repository.Create(person);
            
            await unitOfWork.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.Id }, personRequest with { Id = person.Id });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            var person = await repository.GetById(id);
            
            if (person is null)
                return NotFound();
            
            repository.Delete(person);
            await unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}