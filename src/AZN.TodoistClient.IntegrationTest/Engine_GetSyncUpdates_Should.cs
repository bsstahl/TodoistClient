using Microsoft.Extensions.Configuration;

namespace AZN.TodoistClient.IntegrationTest;

public class Engine_GetSyncUpdates_Should
{
    [Fact]
    public async Task ReturnAFullSyncUpdate()
    {
        var config = new ConfigurationBuilder()
            .AddUserSecrets<Engine_GetSyncUpdates_Should>()
            .Build();

        var engine = new Engine(config);
        var result = await engine.GetSyncUpdates();

        Assert.NotNull(result);
        Assert.True(result.FullSync, "FullSync should be true");
        Assert.True(result.FullSyncDateUtc >= DateTime.UtcNow.Date, $"FullSync date should be after midnight this morning. Actual: {result.FullSyncDateUtc}");
        Assert.True(result.ItemUpdates.Count() > 50, "More than 1-page of results (50) should have been returned");
        Assert.True(result.ProjectUpdates.Count() > 10, "All of the Projects (~16) should have been returned");
        Assert.NotEmpty(result.SyncToken);
    }
}
