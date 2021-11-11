using System.Linq;
using System.Web.Mvc;
using BusinessManagementSystemApp.Core.IdentityCore;
using BMSA.App.ViewModels.RoleViewModels;
using BusinessManagementSystemApp.Service.IdentityModules;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BMSA.App.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        readonly RoleManager<ApplicationRole> _roleManager = new RoleManager<ApplicationRole>(
            new RoleStore<ApplicationRole>(new ApplicationDbContext()));

        private readonly UserRoleManager _userRoleManager;

        public RolesController()
        {
            _userRoleManager = new UserRoleManager();
        }

        // GET: Roles
        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRole(CreateRoleViewModel roleVm)
        {
            var idResult = _roleManager.Create(new ApplicationRole(roleVm.Name, roleVm.Description));
            if (idResult.Succeeded)
            {
                ModelState.Clear();
                return View();
            }

            return View(roleVm);

        }

        public ActionResult RoleList()
        {
            return View();
        }

        [Route("Roles/GetAllResultList")]
        [HttpGet]
        public JsonResult GetAllRoleList()
        {
            var roles = _userRoleManager.GetAll().OrderBy(c => c.Name).ToList();
            return Json(roles, JsonRequestBehavior.AllowGet);
        }
    }
}