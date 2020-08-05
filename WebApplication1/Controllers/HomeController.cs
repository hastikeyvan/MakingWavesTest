using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Home Action Gets all the data lists and prepares needed ViewDatas
        /// </summary>
        /// <returns></returns>

        public ActionResult Index()
        {
            try
            {
                DataService service = new DataService();
                List<DataList> WholeData = service.GetDataFromApi();
                List<DataList> GroupOne = service.PrepareGroups(WholeData, 3);
                List<DataList> SourceListForGroup2 = WholeData.Where(p => GroupOne.All(p1 => p.pantone_value != p1.pantone_value)).ToList();
                List<DataList> GroupTwo = service.PrepareGroups(SourceListForGroup2, 2);
                List<DataList> Rest = SourceListForGroup2.Where(p => GroupTwo.All(p1 => p.pantone_value != p1.pantone_value)).ToList();
                ViewData["GroupOne"] = GroupOne;
                ViewData["GroupTwo"] = GroupTwo;
                ViewData["Rest"] = Rest;
            }
            catch(Exception e)
            {
                //log functionality is not prepared if there was a log functionality available we would log the error in log file
                ViewBag.Error = "An Error was occured." + e.Message;
                return View("ParseError");
            }
            return View();

        }


    }
}