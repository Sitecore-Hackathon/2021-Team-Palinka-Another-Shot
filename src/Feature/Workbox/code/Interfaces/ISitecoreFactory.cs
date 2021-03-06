namespace Feature.Workbox.Interfaces
{
    using Sitecore.Data;
    using Sitecore.Data.Items;

    /// <summary>
    /// ISitecoreFactory interface
    /// </summary>
    public interface ISitecoreFactory
    {
        /// <summary>
        /// Create Sitecore Database object by Name
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <returns>The Sitecore Database.</returns>
        Database GetDatabase(string databaseName);

        /// <summary>
        /// Gets the default device.
        /// </summary>
        /// <returns>The default DeviceItem.</returns>
        DeviceItem GetDefaultDevice();
    }
}