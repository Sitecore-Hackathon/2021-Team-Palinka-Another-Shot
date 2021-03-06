namespace Feature.Workbox.Interfaces
{
    using Feature.Workbox.Models.Response;
    using Feature.Workbox.Models.Response.Response;
    using System.Collections.Generic;

    /// <summary>
    /// Interface IWorkflowRepository
    /// </summary>
    public interface IWorkflowRepository
    {
        /// <summary>
        /// Gets the workflows.
        /// </summary>
        /// <returns>List&lt;Workflow&gt;.</returns>
        List<Workflow> GetWorkflows();

        /// <summary>
        /// Gets the detailed workflow.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>DetailedWorkflow.</returns>
        DetailedWorkflow GetDetailedWorkflow(string id);
    }
}