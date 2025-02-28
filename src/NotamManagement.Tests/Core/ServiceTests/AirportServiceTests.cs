using System.Net;
using System.Text;
using System.Text.Json;
using Moq;
using Moq.Protected;
using NotamManagement.Core.Models;
using NotamManagement.Core.Services;
using NotamManagement.Tests.Helpers;

namespace NotamManagement.Tests.Core.ServiceTests;

public class AirportServiceTests
{
    private readonly Mock<HttpMessageHandler> httpMessageHandlerMock;
    private readonly HttpClient httpClient;
    private readonly AirportService airportService;
    private readonly IReadOnlyList<Airport> airports;

    public AirportServiceTests()
    {
        httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        httpClient = new HttpClient(httpMessageHandlerMock.Object);
        httpClient.BaseAddress = new Uri("http://localhost");
        airportService = new AirportService(httpClient);
        airports = AirportHelper.GetTestData();
    }
    
    [Fact]
    public async Task GetAllAirportsAsync_ReturnsAirports_WhenResponseIsSuccessful()
    {
        // Arrange
        var jsonResponse = JsonSerializer.Serialize(airports);

        httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri == new Uri("http://localhost/api/airport")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json"),
            })
            .Verifiable();

        // Act
        var result = await airportService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(airports.Count, result.Count);
    }
}