using System;

namespace hhs_p6_webshop_project.App.Ajax {
    public class ErrorStatus {

        /// <summary>
        /// Error message.
        /// </summary>
        private String Message { get; }

        /// <summary>
        /// Error code.
        /// </summary>
        private int Code { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Error message.</param>
        public ErrorStatus(String message) {
            this.Message = message;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="code">Error code.</param>
        public ErrorStatus(String message, int code) {
            this.Message = message;
            this.Code = code;
        }

        /// <summary>
        /// Get the error as object, to format it using Json.
        /// </summary>
        /// <returns>Object.</returns>
        public Object AsObject() {
            return new {
                message = Message,
                code = Code
            };
        }
    }
}