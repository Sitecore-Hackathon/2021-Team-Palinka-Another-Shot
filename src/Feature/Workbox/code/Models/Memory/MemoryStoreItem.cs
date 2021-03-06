namespace Feature.Workbox.Models.Memory
{
    /// <summary>
    /// Class MemoryItem.
    /// </summary>
    public class MemoryStoreItem
    {
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>The item.</value>
        public string ItemId{ get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public string State { get; set; }
    }
}