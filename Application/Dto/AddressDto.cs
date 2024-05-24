using Application.Entities;

namespace Application.Dto;

public record AddressDto()
{
    public string? Street { get; init; }
    public string? Number { get; init; }
    public string? Complement { get; init; }
    public string? Neighborhood { get; init; }
    public string City { get; init; } = string.Empty;
    public string State { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public string ZipCode { get; init; } = string.Empty;

    public AddressDto(Address address) : this()
    {
        Street = address.Street;
        Number = address.Number;
        Complement = address.Complement;
        Neighborhood = address.Neighborhood;
        City = address.City;
        State = address.State;
        Country = address.Country;
        ZipCode = address.ZipCode;
    }
}