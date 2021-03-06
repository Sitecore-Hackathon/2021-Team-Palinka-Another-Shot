namespace Feature.Workbox.Models.Search
{
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.SearchTypes;

    /// <summary>
    /// Class WorkflowSearchItem.
    /// Implements the <see cref="Sitecore.ContentSearch.SearchTypes.SearchResultItem" />
    /// </summary>
    /// <seealso cref="Sitecore.ContentSearch.SearchTypes.SearchResultItem" />
    public class WorkflowSearchItem : SearchResultItem
    {
        /// <summary>
        /// Gets or sets the workflow identifier.
        /// </summary>
        /// <value>The workflow identifier.</value>
        [IndexField("__Workflow")]
        public string WorkflowID { get; set; }

        /// <summary>
        /// Gets or sets the state of the workflow.
        /// </summary>
        /// <value>The state of the workflow.</value>
        [IndexField("__Workflow State")]
        public string WorkflowState { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is latest version.
        /// </summary>
        /// <value><c>true</c> if this instance is latest version; otherwise, <c>false</c>.</value>
        [IndexField("_latestversion")]
        public bool IsLatestVersion { get; set; }
    }
}