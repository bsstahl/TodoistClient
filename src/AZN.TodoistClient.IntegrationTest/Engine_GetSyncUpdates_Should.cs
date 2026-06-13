using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;

namespace AZN.TodoistClient.IntegrationTest;

public class Engine_GetSyncUpdates_Should
{
    [Fact]
    public async Task ReturnAFullSyncUpdate()
    {
        var config = new ConfigurationBuilder()
            .AddUserSecrets<Engine_GetSyncUpdates_Should>()
            .Build();

        var engine = new Engine(NullLogger<Engine>.Instance, config);
        var result = await engine.GetSyncUpdates();

        Assert.NotNull(result);
        Assert.True(result.FullSync, "FullSync should be true");
        Assert.True(result.FullSyncDateUtc >= DateTime.UtcNow.Date, $"FullSync date should be after midnight this morning. Actual: {result.FullSyncDateUtc}");
        Assert.True(result.ItemUpdates.Count() > 50, "More than 1-page of results (50) should have been returned");
        Assert.True(result.ProjectUpdates.Count() > 10, "All of the Projects (~16) should have been returned");
        Assert.NotEmpty(result.SyncToken);
    }

    [Fact]
    public async Task NotFailWhenAProjectIsDeleted()
    {
        var config = new ConfigurationBuilder()
            .AddUserSecrets<Engine_GetSyncUpdates_Should>()
            .Build();

        var engine = new Engine(NullLogger<Engine>.Instance, config);

        // TODO: Replace this test with unit tests that uses this json
        // {"full_sync":false,"items":[{"added_at":"2026-02-12T14:04:05.018650Z","added_by_uid":"2928253","assigned_by_uid":null,"checked":true,"child_order":15,"completed_at":"2026-04-23T16:21:25.062200Z","completed_by_uid":"2928253","content":"Review the [Outcome Engineering Manifesto](o16g.com)","day_order":24,"deadline":null,"description":"","due":{"date":"2026-04-23","is_recurring":false,"lang":"en","string":"Apr 23","timezone":null},"duration":null,"goal_ids":[],"id":"6g23RrRX5p4GmpQ8","is_collapsed":false,"is_deleted":false,"labels":["ImportantToMe"],"note_count":0,"parent_id":null,"priority":2,"project_id":"6CrfqcHQmXv3VQX8","responsible_uid":null,"section_id":null,"updated_at":"2026-04-23T16:21:27.667812Z","user_id":"2928253"}],"projects":[{"access":null,"can_assign_tasks":false,"can_comment":false,"child_order":0,"color":"charcoal","created_at":null,"creator_uid":null,"default_order":0,"description":"","id":"6CrfqcHQqHj5mRHF","is_archived":false,"is_collapsed":false,"is_deleted":true,"is_favorite":false,"is_frozen":false,"is_shared":false,"name":"","parent_id":null,"public_access":false,"public_key":"","role":null,"updated_at":null,"view_style":"list"}],"sync_status":{},"sync_token":"2jOJu8jzN5GdMBjVmMNFVVTh-DflNnxgsd73R1Jv7HO1DrFFcj9G96J4zqD-6OlCGyuJ_PWgozLH9yTzl4glGI6_EuLMnsRYWkYAbfLQJLwlP-uMs0o","temp_id_mapping":{}}
        // {"full_sync":false,"items":[{"added_at":"1970-01-01T00:00:00.000000Z","added_by_uid":null,"assigned_by_uid":null,"checked":false,"child_order":0,"completed_at":null,"completed_by_uid":null,"content":"","day_order":-1,"deadline":null,"description":"","due":null,"duration":null,"goal_ids":[],"id":"6fC7qWPfgGmg98Xq","is_collapsed":false,"is_deleted":true,"labels":[],"note_count":0,"parent_id":null,"priority":1,"project_id":"2222222222222222","responsible_uid":null,"section_id":null,"updated_at":null,"user_id":"2928253"}],"projects":[],"sync_status":{},"sync_token":"ojxaU1CfoMkJO-0wDwFDeVRXKo1E3eGrH2QXy8-GVY0HPPhnUgCq5rggPgxdMEC58ictGdawrNGk58wapiTvi-vqkiAqghIew8QZ-WUxGbdIhjulEQM","temp_id_mapping":{}}

        var result = await engine.GetSyncUpdates("6-ezxpnHn7xbFsE1_1R7pHn8fSGRHaz9lrHR_wD2nV_SCQL1pVWVHI12UFvd9nimIwg9vlBmEQUDlwplcTa1iJkPdUpj_OFguNnyKOGTkNYdL-CAWjw");

        Assert.NotNull(result);
        Assert.False(result.FullSync, "FullSync should be false");
        Assert.NotEmpty(result.SyncToken);
    }
}
