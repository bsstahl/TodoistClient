using Microsoft.Extensions.Configuration;

namespace AZN.TodoistClient.IntegrationTest;

public class Engine_GetTaskUpdates_Should
{
    [Fact]
    public async Task ReturnAFullSyncUpdate()
    {
        var config = new ConfigurationBuilder()
            .AddUserSecrets<Engine_GetTaskUpdates_Should>()
            .Build();

        var engine = new Engine(config);
        var result = await engine.GetTaskUpdates();

        Assert.NotNull(result);
        Assert.True(result.FullSync);
        Assert.True(result.FullSyncDateUtc >= DateTimeOffset.UtcNow.Date);
        Assert.NotEmpty(result.SyncToken);
        Assert.True(result.ItemUpdates.Count() > 50);
    }
}
