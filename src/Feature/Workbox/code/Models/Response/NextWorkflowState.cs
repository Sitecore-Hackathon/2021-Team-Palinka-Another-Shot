namespace Feature.Workbox.Models.Response
{
    using System.Collections.Generic;

    /// <summary>
    /// Class NextWorkflowState.
    /// </summary>
    public class NextWorkflowState
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
        /// Gets or sets a value indicating whether this instance is final.
        /// </summary>
        /// <value><c>true</c> if this instance is final; otherwise, <c>false</c>.</value>
        public bool IsFinal { get; set; }

        /// <summary>
        /// Gets or sets the actions.
        /// </summary>
        /// <value>The actions.</value>
        public List<WorkboxAction> Actions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NextWorkflowState"/> class.
        /// </summary>
        public NextWorkflowState()
        {
            this.Actions = new List<WorkboxAction>();
        }
    }
}