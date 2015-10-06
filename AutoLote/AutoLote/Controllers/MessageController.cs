using AutoLote.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoLote.Controllers
{
    public class MessageController : Controller
    {
        //// GET: Message
        //[ChildActionOnly]
        //public ActionResult Message()
        //{
        //    return PartialView();
        //}
        public void Success(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable);
        }

        public void Information(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, message, dismissable);
        }

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, dismissable);
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, dismissable);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(clsAlerts.TempDataKey) ? (List<clsAlerts>)TempData[clsAlerts.TempDataKey] : new List<clsAlerts>();
            alerts.Add(new clsAlerts
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            TempData[clsAlerts.TempDataKey] = alerts;
        }

    }
}