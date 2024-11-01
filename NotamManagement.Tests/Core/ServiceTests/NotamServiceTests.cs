using System.Net;
using System.Text;
using System.Text.Json;
using Moq;
using Moq.Protected;
using NotamManagement.Core.Models;
using NotamManagement.Core.Services;
using NotamManagement.Tests.Helpers;

namespace NotamManagement.Tests.Core.ServiceTests;

public class NotamServiceTests
{
    private readonly Mock<HttpMessageHandler> httpMessageHandlerMock;
    private readonly HttpClient httpClient;
    private readonly NotamService notamService;
    private readonly IReadOnlyList<Notam> notams;

    public NotamServiceTests()
    {
        httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        httpClient = new HttpClient(httpMessageHandlerMock.Object);
        httpClient.BaseAddress = new Uri("http://localhost");
        notamService = new NotamService(httpClient);
        notams = NotamHelper.GetTestData();
    }
    
    [Fact]
    public async Task GetAllNotamsAsync_ReturnsNotams_WhenResponseIsSuccessful()
    {
        // Arrange
        var jsonResponse = JsonSerializer.Serialize(notams);

        httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri == new Uri("http://localhost/api/notam")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json"),
            })
            .Verifiable();

        // Act
        var result = await notamService.GetAllNotamsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(notams.Count, result.Count);
    }
    
    [Fact]
    public async Task GetAllNotamsAsAsyncEnumerable_ReturnsNotams_WhenResponseIsSuccessful()
    {
        // Arrange
        var jsonResponse = JsonSerializer.Serialize(notams);

        httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri == new Uri("http://localhost/api/notam/Stream")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json"),
            })
            .Verifiable();

        // Act
        var result = await notamService.GetAllNotamsAsAsyncEnumerable().ToListAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(notams.Count, result.Count);
    }
}