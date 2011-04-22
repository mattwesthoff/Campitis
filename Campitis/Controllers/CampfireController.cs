using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Campitis.Controllers
{
    public class CampfireController : Controller
    {
        public ActionResult PostAlert(int? room)
        {
            if (!room.HasValue)
            {
                return Content("no room provided");
            }

            var request = WebRequest.Create(string.Format("https://zssd.campfirenow.com/room/{0}/speak.xml", room));
            request.Credentials = new NetworkCredential("e1df7b82ed3d3a995d14c231168cd14c3737ed4e", "X");
            request.ContentType = "application/xml";
            request.Method = "POST";
            var stream = request.GetRequestStream();
            var writer = new StreamWriter(stream);
            writer.Write("<message><body>There was a monitis! (not really guys, just a test)</body></message>");
            writer.Close();
            stream.Close();

            var response = request.GetResponse();
            return Content(((HttpWebResponse) response).StatusDescription);
        }
    }
}