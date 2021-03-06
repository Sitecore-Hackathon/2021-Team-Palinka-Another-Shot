namespace Feature.Workbox.Models.Response
{
    /// <summary>
    /// Class WorkboxAction.
    /// </summary>
    public class WorkboxAction
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
        /// Gets or sets a value indicating whether [suppress comment].
        /// </summary>
        /// <value><c>true</c> if [suppress comment]; otherwise, <c>false</c>.</value>
        public bool SuppressComment { get; set; }
    }
}