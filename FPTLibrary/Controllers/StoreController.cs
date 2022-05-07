using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTLibrary.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            var userSession = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];
            try
            {
                if (userSession == null)
                {
                    return RedirectToAction("Login", "Unauthenticate");
                }
                else
                {
                    if (userSession.RoleID != 2)
                    {
                        return RedirectToAction("DoNotHavePermission", "Shared");
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}