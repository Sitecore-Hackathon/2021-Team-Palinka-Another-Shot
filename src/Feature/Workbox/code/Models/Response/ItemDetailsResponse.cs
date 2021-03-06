namespace Feature.Workbox.Models.Response
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class ItemDetailsResponse DTO.
    /// </summary>
    public class ItemDetailsResponse
    {
        public ItemDetailsResponse()
        {
            this.PersonalizedRenderings = new List<PersonalizationDetailResponse>();
            this.MultiVariateTestedRenderings = new List<MultiVariateTestDetailResponse>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the full path.
        /// </summary>
        /// <value>The full path.</value>
        public string FullPath { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the name of the template.
        /// </summary>
        /// <value>The name of the template.</value>
        public string TemplateName { get; set; }

        /// <summary>
        /// Gets or sets the template identifier.
        /// </summary>
        /// <value>The template identifier.</value>
        public string TemplateId { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>The created.</value>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>The created by.</value>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updated.
        /// </summary>
        /// <value>The updated.</value>
        public DateTime Updated { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>The updated by.</value>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is success.
        /// </summary>
        /// <value><c>true</c> if this instance is success; otherwise, <c>false</c>.</value>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the history.
        /// </summary>
        /// <value>The history.</value>
        public List<WorkflowHistoryResponse> History { get; set; }

        /// <summary>
        /// Gets or sets the personalized renderings.
        /// </summary>
        /// <value>The personalized renderings.</value>
        public List<PersonalizationDetailResponse> PersonalizedRenderings { get; set; }

        /// <summary>
        /// Gets or sets the multi variate tested renderings.
        /// </summary>
        /// <value>The multi variate tested renderings.</value>
        public List<MultiVariateTestDetailResponse> MultiVariateTestedRenderings { get; set; }
    }
}