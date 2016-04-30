using LeafSelector.Models;
using LeafSelector.ServiceModels;
using LeafSelector.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeafSelector.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;

        public HomeController()
        {
            _categoryService = new CategoryService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SubCategories(int id = 0)
        {
            var parents = _categoryService.GetWithParents(id).Select(MapFrom).ToList();
            var subCategories = _categoryService.SubCategories(id).Select(MapFrom).ToList();
            return Json(new { Parents = parents, Items = subCategories }, JsonRequestBehavior.AllowGet);
        }

        private LeafSelectorItemViewModel MapFrom(CategoryServiceModel m)
        {
            return m == null ? null : new LeafSelectorItemViewModel()
            {
                Id = m.Id,
                Text = m.Text,
                IsLeaf = m.IsLeaf
            };
        }

    }
}