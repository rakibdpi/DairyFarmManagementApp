using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessManagementSystemApp.Core.ViewModels;
using BusinessManagementSystemApp.Service.Menagers;

namespace BMSA.App.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CategoryMenager _categoryMenager;

        public CategoriesController()
        {
            _categoryMenager = new CategoryMenager();
        }

        // GET: Categories
        public ActionResult CategoryForm(int? id)
        {
            return View();
        }
    }
}