using AutoMapper;
using BLL;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgrammingForum_ASPNETCore.Models.UserModels;

namespace ProgrammingForum_ASPNETCore.Controllers
{
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService= userService;
        }


        [Authorize]
        public async Task<IActionResult> UserPage()
        {
            User user = await _userService.GetById(User.Identity.Name);

            if (user != null)
            {
                var usermap = _mapper.Map<UserViewModel>(user);
                return View(usermap);
            }
            return View("Error");
        }

        [Authorize]
        public IActionResult EditUser()
        {
            return View();
        }

        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> EditUser(UserEditModel userEdit, IFormFile file)
        //{
        //}


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllUsers()
        {
            IEnumerable<User> users = await _userService.GetAll();
            var userViews = _mapper.Map<IEnumerable<UserViewModel>>(users);
            return View(userViews);
        }
    }
}
