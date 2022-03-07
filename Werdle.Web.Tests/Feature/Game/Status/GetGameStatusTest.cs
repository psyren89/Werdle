namespace Werdle.Tests.Feature.Game.Status;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shouldly;
using Web.Responses;
using Xunit;

public class GetGameStatusTest : IAsyncLifetime
{
    private readonly WerdleTestApplication application = new();
    private HttpClient client = null!;

    [Fact]
    public async Task WhenGameDoesNotExist_ReturnsNotFound() =>
        (await client.GetAsync($"/Game/{Guid.NewGuid()}")).StatusCode.ShouldBe(HttpStatusCode.NotFound);

    [Fact]
    public async Task WhenGameExists_ReturnsGameState()
    {
        var postContent = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("gameWord", "apple") });

        var response = await client.PostAsync("/Game/New", postContent);
        var newGameState = await response.Content.ReadFromJsonAsync<GameState>();
        var gameId = newGameState!.Id;

        var status = await client.GetAsync($"/Game/{gameId}");
        status.EnsureSuccessStatusCode();

        var inProgressGameState = await status.Content.ReadFromJsonAsync<GameState>();

        inProgressGameState!.Id.ShouldBe(gameId);
        inProgressGameState.Word.ShouldBeNull();
        inProgressGameState.RemainingGuesses.ShouldBe(6);
    }

    public Task InitializeAsync()
    {
        client = application.CreateClient();
        return Task.CompletedTask;
    }

    public async Task DisposeAsync() => await application.DisposeAsync();
}
