namespace Feature.Workbox.Services
{
    using Feature.Workbox.Interfaces;
    using System;

    /// <summary>
    /// Class WorkflowLogger.
    /// Implements the <see cref="Feature.Workbox.Interfaces.IWorkflowLogger" />
    /// </summary>
    /// <seealso cref="Feature.Workbox.Interfaces.IWorkflowLogger" />
    public class WorkflowLogger : IWorkflowLogger
    {
        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public void LogError(string message, Exception ex)
        {
            Sitecore.Diagnostics.Log.Error(message, ex);
        }
    }
}