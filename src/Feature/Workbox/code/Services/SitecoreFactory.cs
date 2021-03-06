namespace Feature.Workbox.Services
{
    using Feature.Workbox.Interfaces;
    using Sitecore.Configuration;
    using Sitecore.Data;

    /// <summary>
    /// Class SitecoreFactory.
    /// Implements the <see cref="Feature.Workbox.Interfaces.ISitecoreFactory" />
    /// </summary>
    /// <seealso cref="Feature.Workbox.Interfaces.ISitecoreFactory" />
    public class SitecoreFactory : ISitecoreFactory
    {
        /// <summary>
        /// Create Sitecore Database object by Name
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <returns>The Sitecore Database.</returns>
        public Database GetDatabase(string databaseName)
        {
            return Factory.GetDatabase(databaseName);
        }
    }
}