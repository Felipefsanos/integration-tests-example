using Application.Dto;

namespace Application.Entities;

public class Person()
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string DocumentNumber { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; }

    public Address Address { get; set; } = null!;

    public Person(PersonDto personDto) : this()
    {
        FirstName = personDto.FirstName;
        LastName = personDto.LastName;
        DocumentNumber = personDto.DocumentNumber;
        BirthDate = personDto.BirthDate;
        Address = new Address(personDto.Address);
    }

    public void Update(PersonDto personDto)
    {
        FirstName = personDto.FirstName;
        LastName = personDto.LastName;
        DocumentNumber = personDto.DocumentNumber;
        BirthDate = personDto.BirthDate;
        Address.Update(personDto.Address);
    }
}