using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace hhs_p6_webshop_project.App.Ajax {
    public class AjaxResponse {

        /// <summary>
        /// Response data.
        /// </summary>
        public Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();

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
        /// Set the given data field.
        /// </summary>
        /// <param name="field">Field name to set.</param>
        /// <param name="value">Field value.</param>
        public void SetDataField(string field, object value) {
            Data.Add(field, value);
        }

        /// <summary>
        /// Create a dictionary for this AJAX response.
        /// </summary>
        /// <returns>Dictionary.</returns>
        public Dictionary<string, object> ToDictionary() {
            // Create a base dictionary
            Dictionary<string, object> Base = new Dictionary<string, object>();

            // Set the status
            Base.Add("status", ErrorStatus == null ? "ok" : "err");

            // Add the data, if there is any
            if(Data.Count > 0)
                Base.Add("data", Data);

            // Add the error status, if any is set
            if (ErrorStatus != null)
                Base.Add("error", ErrorStatus.ToDictionary());

            // Return the dictionary
            return Base;
        }

        /// <summary>
        /// Format the AJAX response to Json data, used to send to the client.
        /// </summary>
        /// <returns>Json formatted AJAX result.</returns>
        public JsonResult FormatJson() {
            return new JsonResult(ToDictionary());
        }

        /// <summary>
        /// Merge two dictionaries.
        /// </summary>
        /// <param name="target">Target dictionary.</param>
        /// <param name="source">Source dictionary.</param>
        /// <returns>Target dictionary.</returns>
        public static Dictionary<string, object> MergeDictionary(Dictionary<string, object> target,
            Dictionary<string, object> source) {
            // Merge the two dictionaries
            source.ToList().ForEach(x => target.Add(x.Key, x.Value));

            // Return the target
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