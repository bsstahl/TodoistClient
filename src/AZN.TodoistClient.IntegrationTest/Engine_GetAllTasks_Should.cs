using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;

namespace AZN.TodoistClient.IntegrationTest;

public class Engine_GetAllTasks_Should
{
    [Fact]
    public async Task ReturnAFullSyncUpdate()
    {
        var config = new ConfigurationBuilder()
            .AddUserSecrets<Engine_GetAllTasks_Should>()
            .Build();

        var engine = new Engine(NullLogger<Engine>.Instance, config);
        var result = await engine.GetAllTasks();

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.True(result.Count() > 50);
    }
}
