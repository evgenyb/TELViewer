using System.Web.Mvc;
using TELViewer.Core.Repositories;
using TELViewer.Models;

namespace TELViewer.Controllers
{
    public class LogController : Controller
    {
        public ActionResult List()
        {
            var model = new LogListPageModel(_logRepository);
            model.InitializeModel(Request.QueryString);
            return View(model);
        }

        private readonly ILogRepository _logRepository;

        public LogController(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }       
    }
}