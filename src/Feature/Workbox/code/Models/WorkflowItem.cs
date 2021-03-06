using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Feature.Workbox.Models
{
    public class WorkflowItem
    {
        public string Name { get; set; }

        public string ID { get; set; }

        public List<NextWorkflowState> NextStates { get; set; }

        public WorkflowItem()
        {
            this.NextStates = new List<NextWorkflowState>();
        }
    }
}