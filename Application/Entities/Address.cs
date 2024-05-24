using Application.Dto;

namespace Application.Entities;

public class Address()
{
    public Guid Id { get; set; }
    
    public string? Street { get; set; }
    
    public string? Number { get; set; }
    
    public string? Complement { get; set; }
    
    public string? Neighborhood { get; set; }
    
    public string City { get; set; } = string.Empty;
    
    public string State { get; set; } = string.Empty;
    
    public string Country { get; set; } = string.Empty;
    
    public string ZipCode { get; set; } = string.Empty;
    
    public Guid PersonId { get; set; }
    
    public Person Person { get; set; } = null!;
    
    public Address(AddressDto addressDto) : this()
    {
        Street = addressDto.Street;
        Number = addressDto.Number;
        Complement = addressDto.Complement;
        Neighborhood = addressDto.Neighborhood;
        City = addressDto.City;
        State = addressDto.State;
        Country = addressDto.Country;
        ZipCode = addressDto.ZipCode;
    }

    public void Update(AddressDto addressDto)
    {
        Street = addressDto.Street;
        Number = addressDto.Number;
        Complement = addressDto.Complement;
        Neighborhood = addressDto.Neighborhood;
        City = addressDto.City;
        State = addressDto.State;
        Country = addressDto.Country;
        ZipCode = addressDto.ZipCode;
    }
}