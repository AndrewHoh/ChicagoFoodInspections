using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChicagoSimpleApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.User = DocumentDBRepository<Models.User>.GetUser("anhoh");
            ViewBag.Comments = DocumentDBRepository<Models.Comment>.GetComments("anhoh");
            var items = DocumentDBRepository<Models.Item>.Get10Items(ViewBag.User.Favorites[0]);
            ViewBag.HistoryItems = new List<String>();
            foreach (var item in items)
            {
                ViewBag.HistoryItems.Add(JsonConvert.SerializeObject(item, Formatting.Indented));
            }
            var letterlist = DocumentDBRepository<Models.Letter>.GetLetter("#");
            ViewBag.LetterItems = letterlist.Facilities;

            return View();
        }

        public ActionResult ShowFavorite(String name)
        {
            Models.Result results = new Models.Result();
            results.docs = new List<String>();
            var items = DocumentDBRepository<Models.Item>.Get10Items(name);
            foreach( var item in items ) {
                results.docs.Add(JsonConvert.SerializeObject(item, Formatting.Indented));
            }
            Response.Write(JsonConvert.SerializeObject(results));
            return null;
        }

        public ActionResult ShowClicked(String name, String street)
        {
            Models.Result results = new Models.Result();
            results.docs = new List<String>();
            var items = DocumentDBRepository<Models.Item>.Get10Items(name, street);
            foreach (var item in items)
            {
                results.docs.Add(JsonConvert.SerializeObject(item, Formatting.Indented));
            }
            Response.Write(JsonConvert.SerializeObject(results));
            return null;
        }

        public ActionResult ShowLetter(String letter)
        {
            var letterlist = DocumentDBRepository<Models.Letter>.GetLetter(letter);
            Response.Write(JsonConvert.SerializeObject(letterlist));
            return null;
        }

        public ActionResult ShowHeatMap()
        {
            var heatmap = DocumentDBRepository<Models.HeatMap>.GetHeatMap();
            Response.Write(JsonConvert.SerializeObject(heatmap));
            return null;
        }

        public ActionResult Submit(string id, string comment)
        {
            DocumentDBRepository<Models.Comment>.UpdateCommentAsync(id, comment).Wait();
            Response.Write("");
            return null;
        }

        public ActionResult RSSFeed(int timestamp)
        {
            Models.Result results = new Models.Result();
            results.docs = new List<String>();
            var items = DocumentDBRepository<Models.Feed>.Get10Feed(timestamp);
            foreach (var item in items)
            {
                results.docs.Add(JsonConvert.SerializeObject(item, Formatting.Indented));
            }
            Response.Write(JsonConvert.SerializeObject(results));
            return null;
        }
    }
}
