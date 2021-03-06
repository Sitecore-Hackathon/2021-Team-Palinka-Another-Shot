using Feature.Workbox.Models;
using System.Collections.Generic;

namespace Feature.Workbox.Interfaces
{
    public interface IWorkflowRepository
    {
        List<Workflow> GetWorkflows();

        DetailedWorkflow GetDetailedWorkflow(string id);
    }
}
