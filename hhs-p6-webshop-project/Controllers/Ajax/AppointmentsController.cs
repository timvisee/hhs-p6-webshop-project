using hhs_p6_webshop_project.App.Ajax;
using Microsoft.AspNetCore.Mvc;

namespace hhs_p6_webshop_project.Controllers.Ajax {

    [Route("Ajax")]
    public class AppointmentController : Controller {

        [HttpGet("")]
        public JsonResult Index() {
            return Json(new {
                message = "AJAX appointment endpoint index",
                status = "ok"
            });
        }

        [HttpGet("GetDates")]
        public JsonResult Blyat() {
            return Json(new {
                message = "AJAX appointment date fetch endpoint, not implemented yet",
                status = "ok"
            });
        }

        [HttpGet("Test")]
        public JsonResult Test() {
            return new AjaxResponse();
        }
    }
}