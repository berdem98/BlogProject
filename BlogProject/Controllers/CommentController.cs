using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogProject.Controllers
{
    public class CommentController : Controller
    {
        BlogManager blogManager = new BlogManager(new EfBlogRepository());
        CommentManager commentManager = new CommentManager(new EfCommentRepository());
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult PartialAddComment(int id)
        {
            var value = blogManager.GetBlogById(id);
            return PartialView(value);
        }
        [HttpPost]
        public IActionResult PartialAddComment(Comment p)
        {
            var userName = User.FindFirstValue(ClaimTypes.GivenName);



            p.CommentUserName = userName;

            p.CommentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.CommentStatus = true;

            commentManager.CommentAdd(p);
            return RedirectToAction("BlogDetails", "Blog", new { id = p.BlogID });
            ;

        }
        public PartialViewResult CommentListByBlog(int id)
        {

            var values = commentManager.GetList(id);
            return PartialView(values);
        }
    }
}