using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CrazyCustomsWeb.Models.WebModels;
using CrazyCustomsService;
using System.Web.Script.Serialization;

namespace CrazyCustomsWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnURL = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password, string returnUrl)
        {
            ViewBag.ErrorMessage = null;
            DataAccessLib.Users.User user = DataAccessLib.Users.User.Logon(email, password);
            if (user !=null)
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,
                user.Email,
                DateTime.Now,
                DateTime.Now.Add(FormsAuthentication.Timeout),
                true,//model.RememberMe,
                user.UserRole
                );
                HttpCookie cookie = new HttpCookie(
                    FormsAuthentication.FormsCookieName,
                    FormsAuthentication.Encrypt(ticket));
                Response.Cookies.Add(cookie);

                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "用户名密码输入有错，请重试！";
                return View();
            }
        }

        [HttpPost]
        public ActionResult Register(DataAccessLib.Users.User user)
        {
            if (user == null || !user.IsValid)
            {
                ErrorObject eo = new ErrorObject();
                eo.HasError = true;
                eo.ErrorMessage = "请填写完整注册用户信息。";
                return Content(eo.ToJson());
            }
            else
            {
                try
                {
                    DataAccessLib.Users.User.Register(user.Email, user.Password, user.CellPhone);
                    return null;
                }
                catch
                {
                    ErrorObject eo = new ErrorObject();
                    eo.HasError = true;
                    eo.ErrorMessage = "注册用户失败，请重试或与管理员联系。";
                    return Content(eo.ToJson());
                }
            }
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchType)
        {
            try
            {
                if (searchType == "SearchEntryDock")
                {
                    string containNumber = Request.Form["boxNumberSearchEntryDock"];
                    DockEntryService des = new DockEntryService();
                    var lst = des.GetDockEntity(containNumber.Split(','));
                    if (lst != null && lst.Count > 0)
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        string content = js.Serialize(lst);
                        return Content(content);
                    }
                }
                else if (searchType == "SearchAdmission")
                {
                    string inputNumber = Request.Form["boxNumberSearchAdmission"];
                    AdmissionService ads = new AdmissionService();
                    var lst = ads.GetAdmission(inputNumber.Split(','));
                    if (lst != null && lst.Count > 0)
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        string content = js.Serialize(lst);
                        return Content(content);
                    }
                }
                else if (searchType == "SearchPrerecordWarrant")
                {
                    string inputNumber = Request.Form["boxNumberSearchPrerecordWarrant"];
                    PrerecordWarrantService pws = new PrerecordWarrantService();
                    var lst = pws.GetPrerecordWarrant(inputNumber.Split(','));
                    if (lst != null && lst.Count > 0)
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        string content = js.Serialize(lst);
                        return Content(content);
                    }
                }
                else if (searchType == "SearchLeaveDate"){
                    string boxLeaveDateDock = Request.Form["boxLeaveDateDock"];
                    string boxLeaveDateConveyance = Request.Form["boxLeaveDateConveyance"];
                    LeaveDateService lds = new LeaveDateService();
                    var lst = lds.GetLeaveDate(boxLeaveDateDock, boxLeaveDateConveyance);
                    if (lst != null && lst.Count > 0)
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        string content = js.Serialize(lst);
                        return Content(content);
                    }
                }
                else if (searchType == "SearchLanding")
                {
                    string boxSearchLandingNumber = Request.Form["boxSearchLandingNumber"];
                    string boxSearchLandingConveyance = Request.Form["boxSearchLandingConveyance"];
                    string boxSearchLandingVoyageNumber = Request.Form["boxSearchLandingVoyageNumber"];
                    if (string.IsNullOrWhiteSpace(boxSearchLandingNumber))
                        return null;
                    LadingServiceWebData result = new LadingServiceWebData();
                    LadingService ls = new LadingService();
                    var type = ProcessorUtilities.Constant.GetQueryType(boxSearchLandingNumber);
                    if (type == ProcessorUtilities.QueryTypes.ContainerNumber)
                    {
                        if (!string.IsNullOrWhiteSpace(boxSearchLandingConveyance) && !string.IsNullOrWhiteSpace(boxSearchLandingVoyageNumber))
                            result = ls.GetAdmissionByContainerNumber(boxSearchLandingNumber, boxSearchLandingConveyance, boxSearchLandingVoyageNumber);
                        else
                            result = ls.GetAdmissionByContainerNumber(boxSearchLandingNumber);
                    }
                    else if (type == ProcessorUtilities.QueryTypes.BillNumber)
                    {
                        if (!string.IsNullOrWhiteSpace(boxSearchLandingConveyance) && !string.IsNullOrWhiteSpace(boxSearchLandingVoyageNumber))
                            result = ls.GetAdmissionByBillNumber(boxSearchLandingNumber, boxSearchLandingConveyance, boxSearchLandingVoyageNumber);
                        else
                            result = ls.GetAdmissionByBillNumber(boxSearchLandingNumber);
                    }
                    if (result.LadingBillNumberWebEntity != null || result.LadingContainerNumberWebEntity != null)
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        string content = js.Serialize(result);
                        return Content(content);
                    }
                }
                return Content(string.Empty);
            }
            catch (Exception exp)
            {
                ErrorObject eo = new ErrorObject();
                eo.HasError = true;
                eo.ErrorMessage = "搜索出错了，请重试。";
                return Content(eo.ToJson());
            }
        }
    }
}
