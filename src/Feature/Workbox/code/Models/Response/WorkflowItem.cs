namespace Feature.Workbox.Models.Response
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class WorkflowItem.
    /// </summary>
    public class WorkflowItem
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the template.
        /// </summary>
        /// <value>The name of the template.</value>
        public string TemplateName { get; set; }

        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        /// <value>The last updated.</value>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the last updated by.
        /// </summary>
        /// <value>The last updated by.</value>
        public string LastUpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the next states.
        /// </summary>
        /// <value>The next states.</value>
        public List<NextWorkflowState> NextStates { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowItem"/> class.
        /// </summary>
        public WorkflowItem()
        {
            this.NextStates = new List<NextWorkflowState>();
        }
    }
}