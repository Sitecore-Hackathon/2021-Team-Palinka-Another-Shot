namespace Feature.Workbox.Models.Response
{
    using Sitecore.Collections;
    using System;

    /// <summary>
    /// Class WorkflowHistoryResponse dto.
    /// </summary>
    public class WorkflowHistoryResponse
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Creates new state.
        /// </summary>
        /// <value>The new state.</value>
        public string NewState { get; set; }

        /// <summary>
        /// Creates new statename.
        /// </summary>
        /// <value>The new name of the state.</value>
        public string NewStateName { get; set; }

        /// <summary>
        /// Gets or sets the old state.
        /// </summary>
        /// <value>The old state.</value>
        public string OldState { get; set; }

        /// <summary>
        /// Gets or sets the old name of the state.
        /// </summary>
        /// <value>The old name of the state.</value>
        public string OldStateName { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the comment fields.
        /// </summary>
        /// <value>The comment fields.</value>
        public StringDictionary CommentFields { get; set; }
    }
}