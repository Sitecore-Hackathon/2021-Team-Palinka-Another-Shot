namespace Feature.Workbox.Services
{
    using Feature.Workbox.Interfaces;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Globalization;
    using Sitecore.SecurityModel;

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

        /// <summary>
        /// Gets the default device.
        /// </summary>
        /// <returns>The default DeviceItem.</returns>
        public DeviceItem GetDefaultDevice()
        {
            using (new SecurityDisabler())
            {
                using (new LanguageSwitcher(Language.Parse(Constants.Languages.En)))
                {
                    using (new DatabaseSwitcher(Factory.GetDatabase(Constants.Databases.Master)))
                    {
                        return Sitecore.Context.Database.GetItem(new ID(Constants.DeviceIds.DefaultDeviceId));
                    }
                }
            }
        }
    }
}