using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogProject.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        BlogManager blogManager = new BlogManager(new EfBlogRepository());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
        public IActionResult Index()
        {
            var blogs = blogManager.GetVerifiedBlogList();
            return View(blogs);
        }
        public IActionResult CategoryBlogList(int id) 
        {
            var blogs = blogManager.GetVerifiedBlogListByCategory(id);
            return View(blogs);
        }
        public IActionResult BlogDetails(int id) 
        {
            var isAdmin = User.IsInRole("admin");
            
            var blog = blogManager.GetBlogByIdWithProperties(id);

            
            if (blog.BlogStatus || isAdmin || blog.WriterId == int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!))
            {
                return View(blog);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
