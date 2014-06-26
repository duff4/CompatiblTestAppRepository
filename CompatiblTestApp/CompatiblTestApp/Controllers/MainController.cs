using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using CompatiblTestApp.Models;

namespace CompatiblTestApp.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/
        public ActionResult Index()
        {
            return View();
        }

        // 
        // GET: /Main/Welcome/ 

        public ActionResult AfterButtonClick()
        {
            
            FileStream FileStreamForData = new System.IO.FileStream(Server.MapPath("~\\files\\us-500.csv"), FileMode.Open, FileAccess.Read);
            
            Data DataFromDefaultFile = new Data();

            DataFromDefaultFile.UploadFromFile(FileStreamForData);

            ViewBag.NumberOfColumns = DataFromDefaultFile.TableData[0].Count;
            ViewBag.Table = DataFromDefaultFile.TableData;
            ViewBag.NumberOfRows = DataFromDefaultFile.TableData.Count;
            
            return View();
        }

        public ActionResult Search(List<string> arg)
        {
            FileStream FileStreamForData = new System.IO.FileStream(Server.MapPath("~\\files\\us-500.csv"), FileMode.Open, FileAccess.Read);

            Data DataFromDefaultFile = new Data();

            DataFromDefaultFile.UploadFromFile(FileStreamForData);

            ViewBag.NumberOfColumns = DataFromDefaultFile.TableData[0].Count;
            
            var Matches = DataFromDefaultFile.SearchInFile(FileStreamForData, arg);

            ViewBag.Matches = Matches;
            ViewBag.NumberOfMatches = Matches.Count();

            return View();
        }



	}
}