using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace hhs_p6_webshop_project.App.Ajax {
    public class AjaxResponse {

        /// <summary>
        /// Response data.
        /// </summary>
        public Dictionary<string, object> Data = new Dictionary<string, object>();

        /// <summary>
        /// Error status, or null if there's no error.
        /// </summary>
        public ErrorStatus ErrorStatus { get; set; } = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public AjaxResponse() {}

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="errorStatus">Error status.</param>
        public AjaxResponse(ErrorStatus errorStatus) {
            ErrorStatus = errorStatus;
        }

        /// <summary>
        /// Format the AJAX response to Json data, used to send to the client.
        /// </summary>
        /// <returns>Json formatted AJAX result.</returns>
        public JsonResult FormatJson() {
            // Create a new base object
            // TODO: The base object is empty here. Fill it immediately for better performance.
            Object result = new {
                status = ErrorStatus == null
            };

            // Add the error response if set
            if (ErrorStatus != null) {
                MergeObjects(result, ErrorStatus.AsObject());
            }

            // TODO: Respond with the actual data here!
            return new JsonResult(result);
        }

        /// <summary>
        /// Copy the properties of the given source, to the given target.
        /// This can be used to merge objects.
        /// </summary>
        /// <param name="target">Target object.</param>
        /// <param name="source">Source object.</param>
        /// <typeparam name="T">Object type.</typeparam>
        /// <returns>Target object, may be used for method chaining.</returns>
        public static T MergeObjects<T>(T target, T source) {
            // Get the actual type
            Type t = typeof(T);

            // Fetch the properties
            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            // Copy the source values to the target
            foreach (var prop in properties) {
                var value = prop.GetValue(source, null);
                if (value != null)
                    prop.SetValue(target, value, null);
            }

            // Return the target, for method chaining
            return target;
        }

        /// <summary>
        /// Define a conversion operator to convert the response to the Json format.
        /// </summary>
        /// <param name="response">Ajax response object.</param>
        /// <returns>Json response.</returns>
        public static implicit operator JsonResult(AjaxResponse response) {
            return response.FormatJson();
        }
    }
}