using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogProject.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        WriterManager writerManager = new WriterManager(new EfWriterRepository());
        public IActionResult Index()
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public async  Task<IActionResult> Index(Writer writer)
        {
            Context context = new Context();
            if(ModelState.IsValid)
            {
                var isWriter = context.Writers.FirstOrDefault(x => x.WriterMail == writer.WriterMail && x.WriterPassword == writer.WriterPassword);
                if (isWriter != null)
                {
                    var userClaims = new List<Claim>();
                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isWriter.WriterId.ToString()));
                    userClaims.Add(new Claim(ClaimTypes.Name, isWriter.WriterName ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.Surname, isWriter.WriterSurname ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.GivenName, isWriter.WriterUserName ?? ""));
                    if(isWriter.WriterMail == "berk@gmail.com")
					{
                        userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }
                    var userIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                    };
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(userIdentity),
                        authProperties);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Mail adresi veya şifre yanlış");
                }
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
