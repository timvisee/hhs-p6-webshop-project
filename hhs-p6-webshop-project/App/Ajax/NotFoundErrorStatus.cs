namespace hhs_p6_webshop_project.App.Ajax {
    public class NotFoundErrorStatus : ErrorStatus {

        /// <summary>
        /// Error message.
        /// </summary>
        public const string ErrorMessage = "Endpoint not found.";

        /// <summary>
        /// Error code.
        /// </summary>
        public const int ErrorCode = 1;

        /// <summary>
        /// Constructor.
        /// </summary>
        public NotFoundErrorStatus() : base(ErrorMessage, ErrorCode) {}
    }
}