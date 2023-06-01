using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using $safeprojectname$.Interface;
using $safeprojectname$.Models;
using System.Diagnostics;

namespace $safeprojectname$.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApplicationHelper _applicationHelper;
        private readonly IDatabaseStartup _startup;
        private readonly IToastNotification _toast;

        public HomeController(IApplicationHelper applicationHelper, IDatabaseStartup startup, IToastNotification toast)
        {
            _applicationHelper = applicationHelper;
            _startup = startup;
            _toast = toast;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Startup()
        {
            try
            {
                await _startup.CreateErrorLogsTable();
                _toast.AddSuccessToastMessage("Startup success");
            }
            catch (Exception ex)
            {
                _toast.AddInfoToastMessage("Startup is already done");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ErrorLogsModel model = new ErrorLogsModel();
            if (exceptionHandlerPathFeature != null)
            {
                string user = User.Identity.IsAuthenticated ? User.Identity.Name : null;
                model = _applicationHelper.CreateErrorLogsModel(exceptionHandlerPathFeature?.Error, user);
                await _applicationHelper.InsertErrorLogs(model);
            }
            return View(model);
        }
    }
}