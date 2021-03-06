namespace Feature.Workbox.Models.Response
{
    using System.Collections.Generic;

    /// <summary>
    /// Class Workbox.
    /// </summary>
    public class Workbox
    {
        /// <summary>
        /// Gets or sets the work flows.
        /// </summary>
        /// <value>The work flows.</value>
        public List<Workflow> WorkFlows { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Workbox"/> class.
        /// </summary>
        public Workbox()
        {
            this.WorkFlows = new List<Workflow>();
        }
    }
}