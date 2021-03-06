using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Feature.Workbox.Models
{
    public class Workbox
    {
        public List<Workflow> WorkFlows { get; set; }

        public Workbox()
        {
            this.WorkFlows = new List<Workflow>
            {
                new Workflow()
            };
        }
    }

    public class Workflow
    {

        public string Id { get; set; }

        public string Name { get; set; }
        public List<WorkflowState> States { get; set; }

        public Workflow()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = "Sample workflow";
            this.States = new List<WorkflowState>
            {
                new WorkflowState
                {
                    Id=Guid.NewGuid().ToString(),

                    Name="Draft",
                     Actions = new List<Action>
                     {
                         new Action
                         {
                             Id = Guid.NewGuid().ToString(),
                             Name = "Send for approval"
                         }
                     },
                     IsFinal = false,
                     NextStates = new List<WorkflowState>
                     {
                         new WorkflowState
                         {
                             Name = "Pending Approval",
                             Id = Guid.NewGuid().ToString(),

                         }
                     }
                },
                 new WorkflowState
                {
                    Id=Guid.NewGuid().ToString(),

                    Name="Pending Approval",
                     Actions = new List<Action>
                     {
                         new Action
                         {
                             Id = Guid.NewGuid().ToString(),
                             Name = "Publish"
                         },
                          new Action
                         {
                             Id = Guid.NewGuid().ToString(),
                             Name = "Reject"
                         }
                     },
                     IsFinal = false,
                     NextStates = new List<WorkflowState>
                     {
                         new WorkflowState
                         {
                             Name = "Draft",
                             Id = Guid.NewGuid().ToString(),

                         },
                         new WorkflowState
                         {
                             Name = "Published",
                             Id = Guid.NewGuid().ToString(),

                         }
                     }
                },
                 new WorkflowState
                {
                    Id=Guid.NewGuid().ToString(),

                    Name="Published",
                     Actions = new List<Action>(),
                     IsFinal = true,
                     NextStates = new List<WorkflowState>()
                }
            };
        }
    }

    public class WorkflowState
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public bool IsFinal { get; set; }

        public List<Action> Actions { get; set; }

        public List<WorkflowState> NextStates { get; set; }
    }

    public class Action
    {
        public string Name { get; set; }
        public string Id { get; set; }

    }
}