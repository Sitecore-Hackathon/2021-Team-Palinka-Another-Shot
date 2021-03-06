﻿namespace Feature.Workbox.Interfaces
{
    using Feature.Workbox.Models.Response;
    using Feature.Workbox.Models.Response.Response;
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
        /// <returns>DetailedWorkflow view with items.</returns>
        DetailedWorkflow GetDetailedWorkflow(string id);
    }
}