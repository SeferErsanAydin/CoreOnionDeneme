using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project.BLL.ManagerServices.Abstracts;
using Project.COREUI.VMClasses;
using Project.ENTITIES.Models;

namespace Project.COREUI.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryManager _cMan;

        public CategoryController(ICategoryManager cMan)
        {
            _cMan = cMan;
        }
        public IActionResult CategoryList()
        {
            CategoryVM cvm = new CategoryVM()
            {
                Categories = _cMan.GetActives(),
            };
            return View(cvm);
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            TempData["message"] = _cMan.Add(category);
            return RedirectToAction("CategoryList");
        }

        public IActionResult UpdateCategory(int id)
        {
            CategoryVM cvm = new CategoryVM()
            {
                Category = _cMan.Find(id)
            };
            return View(cvm);
        }
        [HttpPost]

        public IActionResult UpdateCategory(Category category)
        {
            _cMan.Update(category);
            return RedirectToAction("CategoryList");
        }

        public ActionResult DeleteCategory(int id) 
        { 
            _cMan.Delete(_cMan.Find(id));
            return RedirectToAction("CategoryList");
        }
    }
}
