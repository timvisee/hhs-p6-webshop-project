using System;
using System.Collections.Generic;

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
        /// Get the error status as dictionary.
        /// </summary>
        /// <returns>Dictionary.</returns>
        public Dictionary<string, object> ToDictionary() {
            // Create the dictionary, and return it
            return new Dictionary<string, object>() {
                { "code", Code },
                { "message", Message }
            };
        }
    }
}