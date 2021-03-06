using System.Collections.Generic;

namespace Feature.Workbox.Models
{
    public class NextWorkflowState
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public bool IsFinal { get; set; }
        public bool SuppressComment { get; set; }



        public List<WorkboxAction> Actions { get; set; }

        public NextWorkflowState()
        {
            this.Actions = new List<WorkboxAction>();
        }


    }
}