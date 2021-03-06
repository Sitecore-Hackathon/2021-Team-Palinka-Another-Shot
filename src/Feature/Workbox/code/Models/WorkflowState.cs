using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Feature.Workbox.Models
{
    public class WorkflowState
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public bool IsFinal { get; set; }
    }

}