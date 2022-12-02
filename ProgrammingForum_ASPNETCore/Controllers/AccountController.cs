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
using System.Security;
using System.Runtime.InteropServices;
using BLL;

namespace ProgrammingForum_ASPNETCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public AccountController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password, string ReturnUrl)
        {
            var user = _userService.GetById(userName);
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

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userName)
                };

                if (user.Role != null)
                {
                    Claim claim = new Claim(ClaimTypes.Role, user.Role);
                    claims.Add(claim);
                }

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

        [HttpGet("login/google")]
        public async Task LoginGoogle(string returnUrl)
        {
            if(User != null && User.Identities.Any(identity => identity.IsAuthenticated))
            {
                RedirectToAction("Index", "Home");
            }

            var authenticationProperties = new AuthenticationProperties { RedirectUri = returnUrl };

            await HttpContext.ChallengeAsync("google", authenticationProperties).ConfigureAwait(false);
        }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=https://localhost:44350");
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

                var findUser = _userService.GetByIdAndEmail(userCreate.UserName, userCreate.Email);
                if (findUser != null)
                {
                    ViewBag.UserExists = "Username or email already exists";
                    return View(userCreate);
                }

                var usermap = _mapper.Map<User>(userCreate);

                byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: userCreate.Password!,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));

                usermap.HashedPassword = hashed;
                usermap.PasswordSalt = salt;

                _userService.Add(usermap);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, usermap.UserName));

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(claimsPrincipal);

                return RedirectToAction("UserPage", "User"); //
            }
            return View(userCreate);
        }
    }
}
