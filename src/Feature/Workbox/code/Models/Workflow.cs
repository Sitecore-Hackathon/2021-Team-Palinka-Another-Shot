namespace Feature.Workbox.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;


    public class Workflow
    {

        public string Id { get; set; }

        public string Name { get; set; }
        public List<WorkflowState> States { get; set; }

        public bool IsSelected { get; set; } = false;

        public Workflow()
        {
            this.States = new List<WorkflowState>();
        }
    }
}