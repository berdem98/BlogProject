using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace BlogProject.Controllers
{

    public class WriterController : Controller
    {
        BlogManager blogManager = new BlogManager(new EfBlogRepository());
        public IActionResult Index()
        {
            var writerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var values = blogManager.GetListWithCategoryByWriterBm(writerId);
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
            blog.BlogStatus = false;

            blogManager.TAdd(blog);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var blog = blogManager.GetBlogById(id);

            if (blog != null && blog.WriterId == int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!))
            {
                return View(blog);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPost]
        public IActionResult Delete(Blog blog)
        {
            if (blog != null && blog.WriterId == int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!))
            {
                blogManager.TDelete(blog);
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult Edit(int id)
        {
            var blog = blogManager.GetBlogById(id);
            if (blog != null && blog.WriterId == int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!))
            {
                CategoryManager cm = new CategoryManager(new EfCategoryRepository());
                List<SelectListItem> categoryValues = (from x in cm.GetList()
                                                       select new SelectListItem
                                                       {
                                                           Text = x.CategoryName,
                                                           Value = x.CategoryId.ToString()
                                                       }).ToList();
                ViewBag.cv = categoryValues;


                return View(blog);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPost]
        public IActionResult Edit(Blog blog)
        {
            if (blog != null && blog.WriterId == int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!))
            {
                var blogDate = DateTime.Now;
                blog.BlogCreateDate = blogDate;
                blogManager.TUpdate(blog);
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
                
        }

    }
}
