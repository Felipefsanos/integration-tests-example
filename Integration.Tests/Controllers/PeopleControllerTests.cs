using System.Net;
using System.Text;
using System.Text.Json;
using Application.Dto;
using Application.Entities;
using Application.Repositories.Abstractions;
using Application.Services.Abstractions;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Integration.Tests.Controllers;

public class PeopleControllerTests : IntegrationTestsFixture
{
    private readonly HttpClient _client;
    private readonly Mock<IPeopleRepository> _mockRepository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly Fixture _fixture = new();
    private readonly JsonSerializerOptions _serializerOptions = new() { PropertyNameCaseInsensitive = true };

    public PeopleControllerTests(ApiWebApplicationFactory appFactory) : base(appFactory)
    {
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _client = appFactory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped(_ => _mockRepository.Object);
                services.AddScoped(_ => _unitOfWork.Object);
            });
        }).CreateClient();
    }

    [Fact]
    public async Task GetPeople_ShouldReturnPeople()
    {
        // Arrange
        var people = new List<Person>
        {
            _fixture.Create<Person>(),
            _fixture.Create<Person>()
        };

        _mockRepository.Setup(r => r.GetAll()).ReturnsAsync(people);

        // Act
        var response = await _client.GetAsync("/api/people");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var peopleResponse = await GetResponseContent<List<PersonDto>>(response);

        peopleResponse.Should().NotBeNullOrEmpty();
        people.Should().HaveCount(peopleResponse!.Count);
    }
    
    [Fact]
    public async Task GetPerson_ShouldReturnPerson()
    {
        // Arrange
        var person = _fixture.Create<Person>();

        _mockRepository.Setup(r => r.GetById(person.Id)).ReturnsAsync(person);

        // Act
        var response = await _client.GetAsync($"/api/people/{person.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var personResponse = await GetResponseContent<PersonDto>(response);

        personResponse.Should().NotBeNull();
        Assert.Equal(person.Id, personResponse!.Id);
    }
    
    [Fact]
    public async Task PutPerson_ShouldUpdatePerson()
    {
        // Arrange
        var person = _fixture.Create<Person>();
        var personDto = new PersonDto(person);

        _mockRepository.Setup(r => r.GetById(person.Id)).ReturnsAsync(person);

        // Act
        var response = await _client.PutAsync($"/api/people/{person.Id}", GetJsonBody(personDto));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task PostPerson_ShouldCreatePerson()
    {
        // Arrange
        var personDto = _fixture.Create<PersonDto>();

        // Act
        var response = await _client.PostAsync("/api/people", GetJsonBody(personDto));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
    
    [Fact]
    public async Task DeletePerson_ShouldDeletePerson()
    {
        // Arrange
        var person = _fixture.Create<Person>();

        _mockRepository.Setup(r => r.GetById(person.Id)).ReturnsAsync(person);

        // Act
        var response = await _client.DeleteAsync($"/api/people/{person.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
    
    private static StringContent GetJsonBody<T>(T value)
    {
        var json = JsonSerializer.Serialize(value);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    private async Task<T?> GetResponseContent<T>(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseContent, _serializerOptions);
    }
}