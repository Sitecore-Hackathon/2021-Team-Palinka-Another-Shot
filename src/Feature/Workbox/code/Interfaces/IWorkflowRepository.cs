namespace Feature.Workbox.Interfaces
{
    using Feature.Workbox.Models.Response;
    using Feature.Workbox.Models.Response.Response;
    using Sitecore.Data.Items;
    using System.Collections.Generic;

    /// <summary>
    /// IWorkflowRepository interface.
    /// </summary>
    public interface IWorkflowRepository
    {
        /// <summary>
        /// Gets the all workflows.
        /// </summary>
        /// <returns>List all available workflows in the system</returns>
        List<Workflow> GetWorkflows();

        /// <summary>
        /// Gets the detailed workflow view with items
        /// </summary>
        /// <param name="id">The workflow identifier.</param>
        /// <returns>Detailed Workflow view with items.</returns>
        DetailedWorkflow GetDetailedWorkflow(string id);

        /// <summary>
        /// Gets the item by id.
        /// </summary>
        /// <param name="id">The item identifier.</param>
        /// <param name="language">The language.</param>
        /// <returns>The item.</returns>
        Item GetItem(string id, string language);

        /// <summary>
        /// Gets the item by id.
        /// </summary>
        /// <param name="id">The item identifier.</param>
        /// <returns>The item.</returns>
        Item GetItem(string id);

        /// <summary>
        /// Gets the workflow history for an Item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>List of workflow history events</returns>
        Sitecore.Workflows.WorkflowEvent[] GetHistory(Item item);

        /// <summary>
        /// Gets the workflow.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The workflow for item</returns>
        Sitecore.Workflows.IWorkflow GetWorkflow(Item item);

        /// <summary>
        /// Gets the icon URL.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The icon url.</returns>
        string GetIconUrl(Item item);
    }
}