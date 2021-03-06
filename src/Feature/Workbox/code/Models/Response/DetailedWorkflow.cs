namespace Feature.Workbox.Models.Response.Response
{
    using System.Collections.Generic;

    /// <summary>
    /// Class DetailedWorkflow.
    /// </summary>
    public class DetailedWorkflow
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the states.
        /// </summary>
        /// <value>The states.</value>
        public List<DetailedWorkflowState> States { get; set; }

        /// <summary>
        /// Gets or sets the quick filters.
        /// </summary>
        /// <value>The quick filters.</value>
        public Dictionary<string, IEnumerable<string>> QuickFilters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailedWorkflow"/> class.
        /// </summary>
        public DetailedWorkflow()
        {
            this.States = new List<DetailedWorkflowState>();
            this.QuickFilters = new Dictionary<string, IEnumerable<string>>();
        }
    }
}