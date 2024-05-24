using Application.Entities;

namespace Application.Dto;

public record PersonDto()
{
    public Guid? Id { get; set; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string DocumentNumber { get; init; } = string.Empty;
    public DateTime BirthDate { get; init; }
    public AddressDto Address { get; init; } = null!;

    public PersonDto(Person person) : this()
    {
        Id = person.Id;
        FirstName = person.FirstName;
        LastName = person.LastName;
        DocumentNumber = person.DocumentNumber;
        BirthDate = person.BirthDate;
        Address = new AddressDto(person.Address);
    }
}