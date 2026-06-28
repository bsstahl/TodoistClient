using AZN.TodoistClient.Entities;

namespace AZN.TodoistClient.Interfaces;

/// <summary>
/// Represents a client capable of managing tasks.
/// </summary>
public interface IManageTasks
{
    /// <summary>
    /// Retrieves all projects from the Task management API.
    /// This may result in multiple API calls if the number of 
    /// projects exceeds the API's page size limit.
    /// </summary>
    /// <returns>A task that resolves to an IEnumerable of Project containing all projects.</returns>
    Task<IEnumerable<Project>> GetAllProjects();

    /// <summary>
    /// Retrieves all tasks from the Task management API.
    /// This process may result in multiple API calls if the number of 
    /// tasks exceeds the API's page size limit.
    /// </summary>
    /// <returns>A task that resolves to an IEnumerable of Item containing all tasks.</returns>
    Task<IEnumerable<Item>> GetAllTasks();

    /// <summary>
    /// Retrieves task updates from the Task management API using the provided sync token.
    /// </summary>
    /// <param name="syncToken">The sync token used to request incremental updates. Use "*" (default) to request a full sync.</param>
    /// <returns>A task that resolves to a GetTaskUpdatesApiResults containing update items and the resulting sync token.</returns>
    Task<GetSyncApiResults> GetSyncUpdates(String syncToken = "*");

    /// <summary>
    /// Updates a task in the Task management API using the provided 
    /// ItemUpdate object. The Id property of the ItemUpdate must be 
    /// set to the ID of the task to update.
    /// </summary>
    /// <param name="itemUpdate">The ItemUpdate object containing the updated task information.</param>
    Task UpdateTask(ItemUpdate itemUpdate);
}