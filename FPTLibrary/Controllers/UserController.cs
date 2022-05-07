using DataAccess.DTO;
using System.Linq;
using System.Web.Mvc;

namespace FPTLibrary.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            try
            {
                var userSession = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];
                if (userSession == null)
                {
                    return RedirectToAction("Login", "Unauthenticate");
                }
                else
                {
                    if (userSession.RoleID != 1)
                    {
                        return RedirectToAction("DoNotHavePermission", "Shared");
                    }
                    else
                    {

                        return View();
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }
        public ActionResult UserManagementParialView()
        {
            try
            {
                var userSession = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];
                if (userSession == null)
                {
                    return RedirectToAction("Login", "Unauthenticate");
                }
                else
                {

                    if (userSession.RoleID != 1)
                    {
                        return RedirectToAction("DoNotHavePermission", "Shared");
                    }
                    else
                    {
                        var result = new DataAccess.DAOImpl.UserDAOImpl().Users_GetList().OrderBy(u => u.RoleID).ToList();
                        foreach (var item in result)
                        {
                            item.RoleName = new DataAccess.DAOImpl.RoleDAOImpl()
                                .Roles_GetList()
                                .FirstOrDefault(r => r.RoleID == item.RoleID)
                                .RoleName;
                            item.UserPassword = DataAccess.Libs
                           .MD5
                           .CreateMD5(item.UserPassword);
                        }
                        return PartialView(result);
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }
        public ActionResult UserDetail(int UserID)
        {
            try
            {
                var userSession = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];
                if (userSession == null)
                {
                    return RedirectToAction("Login", "Unauthenticate");
                }
                else
                {
                    if (userSession.RoleID != 1)
                    {
                        return RedirectToAction("DoNotHavePermission", "Shared");
                    }
                    else
                    {
                        var result = new DataAccess.DAOImpl.UserDAOImpl()
                            .Users_GetList()
                            .FirstOrDefault(u => u.UserID == userSession.UserID);
                        result.RoleName = new DataAccess.DAOImpl.RoleDAOImpl()
                            .Roles_GetList()
                            .FirstOrDefault(r => r.RoleID == result.RoleID).RoleName;
                        result.UserPassword = DataAccess.Libs
                            .MD5
                            .CreateMD5(result.UserPassword);
                        return View(result);
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }
    }
}