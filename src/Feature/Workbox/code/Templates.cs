namespace Feature.Workbox
{
    using Sitecore.Data;

    /// <summary>
    /// Struct Templates
    /// </summary>
    public struct Templates
    {
        /// <summary>
        /// Struct Workflow
        /// </summary>
        public struct Workflow
        {
            /// <summary>
            /// The template identifier
            /// </summary>
            public static ID TemplateId = new ID("{1C0ACC50-37BE-4742-B43C-96A07A7410A5}");

            /// <summary>
            /// Struct Fields
            /// </summary>
            public struct Fields
            {
                public const string InitialStateId = "{B5166B38-E4BF-4410-953C-2037F2BF6A56}";
                public const string InitialState = "Initial state";
            }
        }

        /// <summary>
        /// Struct WorkflowState
        /// </summary>
        public struct WorkflowState
        {
            public static ID TemplateId = new ID("{4B7E2DA9-DE43-4C83-88C3-02F042031D04}");

            public struct Fields
            {
                public const string FinalId = "{FB8ABC73-7ACF-45A0-898C-D3CCB889C3EE}";
                public const string Final = "Final";
            }
        }

        /// <summary>
        /// Struct WorkflowCommand
        /// </summary>
        public struct WorkflowCommand
        {
            public static ID TemplateId = new ID("{CB01F9FC-C187-46B3-AB0B-97A8468D8303}");

            public struct Fields
            {
                public const string NextStateId = "{DCBEBC58-6124-4100-A248-FC717D6C78D5}";
                public const string NextState = "Next state";

                public const string SuppressCommentId = "{82A7C02F-9A55-4BFE-B494-D1713D4BE9FF}";
                public const string SuppressComment = "Suppress Comment";
            }
        }
    }
}