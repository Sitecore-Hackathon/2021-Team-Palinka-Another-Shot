namespace Feature.Workbox.Models.Search
{
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.SearchTypes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class WorkflowSearchItem : SearchResultItem
    {
        [IndexField("__Workflow")]
        public string WorkflowID { get; set; }

        [IndexField("__Workflow State")]
        public string WorkflowState { get; set; }

        [IndexField("_latestversion")]
        public bool IsLatestVersion { get; set; }
    }
}