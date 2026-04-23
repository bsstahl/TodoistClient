using Microsoft.Extensions.Configuration;

namespace AZN.TodoistClient.IntegrationTest;

public class Engine_GetAllTasks_Should
{
    [Fact]
    public async Task ReturnAFullSyncUpdate()
    {
        var config = new ConfigurationBuilder()
            .AddUserSecrets<Engine_GetAllTasks_Should>()
            .Build();

        var engine = new Engine(config);
        var result = await engine.GetAllTasks();

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.True(result.Count() > 50);
    }
}
