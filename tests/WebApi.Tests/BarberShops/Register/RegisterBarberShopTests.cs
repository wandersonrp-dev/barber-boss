using BarberBoss.Exception;
using Common.Tests.Utilities.Requests.BarberShops;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Tests.BarberShops.Register;
public class RegisterBarberShopTests : BarberBossClassFixture
{
    private const string METHOD = "api/barber-shops/signup";    

    public RegisterBarberShopTests(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {       
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterBarberShopJsonBuilder.Build();

        var result = await PostAsync(METHOD, request);
        
        result.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("isCreated").GetBoolean().Should().Be(true);                
    }

    [Fact]
    public async Task Error_Required_Name()
    {
        var request = RequestRegisterBarberShopJsonBuilder.Build();
        request.Name = string.Empty;

        var result = await PostAsync(METHOD, request);

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

        errors.Should().HaveCount(1).And.Contain(errorMessage => errorMessage.GetString()!.Equals(ResourceErrorMessages.REQUIRED_NAME));
    }
}
