using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace BlogProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        BlogManager blogManager = new BlogManager(new EfBlogRepository());
        public IActionResult Index()
        {
            var values = blogManager.GetBlogListWithCategory();
            return View(values);
        }
        public IActionResult BlogAdd()
        {
            CategoryManager cm = new CategoryManager(new EfCategoryRepository());
            List<SelectListItem> categoryValues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryValues;

            return View();
        }
        [HttpPost]
        public IActionResult BlogAdd(Blog blog)
        {
            var writerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            blog.WriterId = writerId;
            var blogDate = DateTime.Now;
            blog.BlogCreateDate = blogDate;

            blogManager.TAdd(blog);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var value = blogManager.GetBlogById(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult Delete(Blog blog)
        {
            blogManager.TDelete(blog);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            CategoryManager cm = new CategoryManager(new EfCategoryRepository());
            List<SelectListItem> categoryValues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryValues;

            var value = blogManager.GetBlogById(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult Edit(Blog blog)
        {
            var blogDate = DateTime.Now;
            blog.BlogCreateDate = blogDate;
            blogManager.TUpdate(blog);
            return RedirectToAction("Index");
        }
    }

}
