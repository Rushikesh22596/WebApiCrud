using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Security;
using WebApiCrud.Models;

namespace WebApiCrud.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-NLRDVF2;Initial Catalog=WebApi;Integrated Security=True");
        public ActionResult Index()
        {
            return Content("This is update");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }      
        [HttpPost]
        public ActionResult Login(CustomerViewModel obj)
        {
            BALWebApi objbal = new BALWebApi();
            SqlDataReader dr;
            dr = objbal.Login(obj);
            if(dr.Read())
            {
                FormsAuthentication.SetAuthCookie(obj.Email, true);
                Session["Email"] = obj.Email.ToString();
                //Session["Password"] = obj.Password.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Message"] = "Username and Password are wrong";
            }
            return View();
        }
        
    }
}
