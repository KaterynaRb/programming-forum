using AutoMapper;
using DAL.Entities;
using DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using ProgrammingForum_ASPNETCore.Models.UserModels;

namespace ProgrammingForum_ASPNETCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AccountController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password, string ReturnUrl)
        {
            var user = _context.Users.Where(u => u.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                byte[] salt = user.PasswordSalt;

                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password!,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));

                if (hashed.CompareTo(user.HashedPassword) != 0)
                {
                    TempData["Error"] = "Error: Username or Password is incorrect";
                    return View("Login");
                }

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, userName));

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(claimsPrincipal);
                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                return Redirect("/");
            }
            TempData["Error"] = "Error: Username or Password is incorrect";
            return View("Login");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserCreateModel userCreate, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string extension = Path.GetExtension(file.FileName);
                    List<string> extensions = new List<string>() { ".png", ".jpg", ".jpeg" };

                    if (extensions.Contains(extension))
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            userCreate.Picture = fileBytes;
                        }
                    }
                    else
                    {
                        ViewBag.ImageError = "Error: File extensions can be '.png', '.jpg', '.jpeg' ";
                        return View();
                    }
                }

                // check if user with given email/username exists
                //
                var findUser = _context.Users
                    .Where(u => u.UserName == userCreate.UserName
                    || u.Email == userCreate.Email).FirstOrDefault();

                if (findUser != null)
                {
                    ViewBag.UserExists = "Username or email already exists";
                    return View(userCreate);
                }

                var usermap = _mapper.Map<User>(userCreate);

                // Hash password

                byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: userCreate.Password!,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));

                usermap.HashedPassword = hashed;
                usermap.PasswordSalt = salt;

                //using (var client = new HttpClient())
                //{
                //    client.BaseAddress = new Uri("http://localhost:44362/api/");

                //    //HTTP POST
                //    var postTask = client.PostAsJsonAsync<User>("User", usermap);
                //    postTask.Wait();
                //    var result = postTask.Result;
                //}


                _context.Users.Add(usermap);
                _context.SaveChanges();

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, usermap.UserName));

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(claimsPrincipal);

                return RedirectToAction("UserPage");
            }
            return View(userCreate);
        }

        [Authorize]
        public IActionResult UserPage()
        {
            User user = _context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            if (user != null)
            {
                var usermap = _mapper.Map<UserCreateModel>(user);
                return View(usermap);
            }
            return View("Error");
        }
    }
}
