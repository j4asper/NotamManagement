using System.Net;
using System.Text;
using System.Text.Json;
using Moq;
using Moq.Protected;
using NotamManagement.Core.Models;
using NotamManagement.Core.Services;
using NotamManagement.Tests.Helpers;

namespace NotamManagement.Tests.Core.ServiceTests;

public class FlightPlanServiceTests
{
    private readonly Mock<HttpMessageHandler> httpMessageHandlerMock;
    private readonly HttpClient httpClient;
    private readonly FlightPlanService flightPlanService;
    private readonly IReadOnlyList<FlightPlan> flightPlans;

    public FlightPlanServiceTests()
    {
        httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        httpClient = new HttpClient(httpMessageHandlerMock.Object);
        httpClient.BaseAddress = new Uri("http://localhost");
        flightPlanService = new FlightPlanService(httpClient);
        flightPlans = FlightPlanHelper.GetTestData();
    }
    
    [Fact]
    public async Task GetAllFlightPlansAsync_ReturnsFlightPlans_WhenResponseIsSuccessful()
    {
        // Arrange
        var jsonResponse = JsonSerializer.Serialize(flightPlans);

        httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri == new Uri("http://localhost/api/flightplan")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json"),
            })
            .Verifiable();

        // Act
        var result = await flightPlanService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(flightPlans.Count, result.Count);
    }
}