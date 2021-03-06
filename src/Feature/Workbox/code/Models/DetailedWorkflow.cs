namespace Feature.Workbox.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;


    public class DetailedWorkflow
    {

        public string Id { get; set; }

        public string Name { get; set; }
        public List<DetailedWorkflowState> States { get; set; }

        public DetailedWorkflow()
        {
            this.States = new List<DetailedWorkflowState>();
        }
    }
}