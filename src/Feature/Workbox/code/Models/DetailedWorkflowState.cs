using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Feature.Workbox.Models
{
    public class DetailedWorkflowState
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public bool IsFinal { get; set; }

        public List<WorkflowItem> Items {get;set;}

        public DetailedWorkflowState()
        {
            this.Items = new List<WorkflowItem>();
        }
    }

}