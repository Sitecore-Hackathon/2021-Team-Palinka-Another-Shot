namespace Feature.Workbox.Models.Request
{
    /// <summary>
    /// Class ChangeWorkflowRequest - DTO
    /// </summary>
    public class ChangeWorkflowRequest
    {
        /// <summary>
        /// Gets or sets the item identifier.
        /// </summary>
        /// <value>The item identifier.</value>
        public string ItemId { get; set; }

        /// <summary>
        /// Gets or sets the command identifier.
        /// </summary>
        /// <value>The command identifier.</value>
        public string CommandId { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>The comment.</value>
        public string Comment { get; set; }
    }
}