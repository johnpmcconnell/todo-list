using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Todo.WebApp.DataModels;

namespace Todo.WebApp.Controllers
{
    public static class ControllerExtensions
    {
        public static IActionResult RedirectRetrieve(
            this ControllerBase c,
            string location
        )
        {
            c.Response.Headers.Add(HeaderNames.Location, location);
            return c.StatusCode(StatusCodes.Status303SeeOther);
        }

        public static IActionResult RedirectRetrieveToAction(
            this ControllerBase c,
            string action,
            object routeValues
        )
        {
            return c.RedirectRetrieve(c.Url.Action(action, routeValues));
        }

        public static IActionResult ErrorView(
            this Controller c,
            HttpStatusCode code,
            ErrorResponse model
        )
        {
            var response = c.View("~/Views/Errors/GenericError.cshtml", model);
            response.StatusCode = (int)code;
            return response;
        }
    }
}
