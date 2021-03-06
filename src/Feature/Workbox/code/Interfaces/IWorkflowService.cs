namespace Feature.Workbox.Interfaces
{
    using Feature.Workbox.Models.Request;
    using Feature.Workbox.Models.Response;
    using Feature.Workbox.Models.Response.Response;
    using System.Collections.Generic;

    /// <summary>
    /// IWorkflowService interface
    /// </summary>
    public interface IWorkflowService
    {
        /// <summary>
        /// Changes the workflow state for the given item
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>ChangeWorkflowResponse.</returns>
        ChangeWorkflowResponse ChangeWorkflow(ChangeWorkflowRequest request);

        /// <summary>
        /// Gets the all workflows.
        /// </summary>
        /// <returns>List all available workflows in the system</returns>
        List<Workflow> GetWorkflows();

        /// <summary>
        /// Gets the detailed workflow view with items
        /// </summary>
        /// <param name="id">The workflow identifier.</param>
        /// <returns>DetailedWorkflow view with items.</returns>
        DetailedWorkflow GetDetailedWorkflow(string id);

        /// <summary>
        /// Gets the item details.
        /// </summary>
        /// <param name="id">The item identifier.</param>
        /// <param name="language">The language.</param>
        /// <returns>The detailed item view.</returns>
        ItemDetailsResponse GetItemDetails(string id, string language);
    }
}