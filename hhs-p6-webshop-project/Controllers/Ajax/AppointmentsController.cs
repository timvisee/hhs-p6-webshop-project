using hhs_p6_webshop_project.App.Ajax;
using Microsoft.AspNetCore.Mvc;

namespace hhs_p6_webshop_project.Controllers.Ajax {

    [Route("Ajax")]
    public class AppointmentController : Controller {

        [HttpGet("")]
        public JsonResult Index() {
            // Respond with a not found error
            return new AjaxResponse(new NotFoundErrorStatus());
        }

        [HttpGet("GetDates")]
        public JsonResult GetDates() {
            // Return the data fields
            // TODO: Implement this!
            return new AjaxResponse().SetDataField(
                "message",
                "AJAX appointment date fetch endpoint, not implemented yet"
            );
        }
    }
}