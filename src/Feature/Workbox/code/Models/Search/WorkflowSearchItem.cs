namespace Feature.Workbox.Models.Search
{
    using Sitecore.ContentSearch;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class WorkflowSearchItem
    {
        [IndexField("__Workflow")]
        public string WorkflowID { get; set; }

        [IndexField("__Workflow State")]
        public string WorkflowState { get; set; }

        public string ID { get; set; }

        [IndexField("_latestversion")]
        public bool IsLatestVersion { get; set; }
    }
}