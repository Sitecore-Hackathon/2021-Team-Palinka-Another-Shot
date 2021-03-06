namespace Feature.Workbox.Models.Response
{
    using System.Collections.Generic;

    /// <summary>
    /// Class Workflow.
    /// </summary>
    public class Workflow
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
        public List<WorkflowState> States { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
        public bool IsSelected { get; set; } = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Workflow"/> class.
        /// </summary>
        public Workflow()
        {
            this.States = new List<WorkflowState>();
        }
    }
}