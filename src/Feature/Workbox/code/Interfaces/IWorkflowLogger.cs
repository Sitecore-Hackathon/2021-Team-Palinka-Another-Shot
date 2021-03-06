namespace Feature.Workbox.Interfaces
{
    using System;

    /// <summary>
    /// IWorkflowLogger interface, wraps the Sitecore.diagnostics.Log namespaces
    /// </summary>
    public interface IWorkflowLogger
    {
        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="message">The message.</param>
        void LogError(string message, Exception ex);
    }
}