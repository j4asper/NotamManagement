using System.Net;
using System.Text;
using System.Text.Json;
using Moq;
using Moq.Protected;
using NotamManagement.Core.Models;
using NotamManagement.Core.Services;
using NotamManagement.Tests.Helpers;

namespace NotamManagement.Tests.Core.ServiceTests;

public class NotamActionServiceTests
{
    private readonly Mock<HttpMessageHandler> httpMessageHandlerMock;
    private readonly HttpClient httpClient;
    private readonly NotamActionService notamActionService;
    private readonly IReadOnlyList<NotamAction> notamActions;

    public NotamActionServiceTests()
    {
        httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        httpClient = new HttpClient(httpMessageHandlerMock.Object);
        httpClient.BaseAddress = new Uri("http://localhost");
        notamActionService = new NotamActionService(httpClient);
        notamActions = NotamActionHelper.GetTestData();
    }
    
    [Fact]
    public async Task AddAsync_SendsPostRequest_WhenCalled()
    {
        // Arrange
        var notamAction = notamActions[0];

        httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && req.RequestUri == new Uri("http://localhost/api/notamaction")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Created,
            })
            .Verifiable();

        // Act
        await notamActionService.AddAsync(notamAction);

        // Assert
        httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>());
    }
    
    [Fact]
    public async Task GetAllNotamActionsAsync_ReturnsNotamActions_WhenResponseIsSuccessful()
    {
        // Arrange
        var jsonResponse = JsonSerializer.Serialize(notamActions);

        httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri == new Uri("http://localhost/api/notamaction")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json"),
            })
            .Verifiable();

        // Act
        var result = await notamActionService.GetAllNotamActionsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(notamActions.Count, result.Count);
    }
}