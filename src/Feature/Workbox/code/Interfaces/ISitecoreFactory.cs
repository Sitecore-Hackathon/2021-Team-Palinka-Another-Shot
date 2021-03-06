namespace Feature.Workbox.Interfaces
{
    using Sitecore.Data;

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
    }
}