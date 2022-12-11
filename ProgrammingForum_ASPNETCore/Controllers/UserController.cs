using AutoMapper;
using BLL;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Authentication;
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
        public async Task<IActionResult> EditPublicProfile(string id)
        {
            User user = await _userService.GetById(id);
            var usermap = _mapper.Map<UserEditModel>(user);
            return View(usermap);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditPublicProfile(string id, UserEditModel userEdit)
        {
            //User user = _mapper.Map<User>(userEdit);
            User user = await _userService.GetById(id);
            user.UserName = userEdit.UserName;
            user.Email = userEdit.Email;
            await _userService.Update(user.UserName, user);
            return View("UserPage");
        }

        //[Authorize]
        //public async Task<IActionResult> EditPassword(string id)
        //{
        //    User user = await _userService.GetById(User.Identity.Name);

        //    var usermap = _mapper.Map<UserEditModel>(user);

        //    return View(usermap);
        //}

        [Authorize]
        public async Task<IActionResult> Delete()
        {
            return View();
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await HttpContext.SignOutAsync();
            await _userService.Delete(id);
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllUsers()
        {
            IEnumerable<User> users = await _userService.GetAll();
            var userViews = _mapper.Map<IEnumerable<UserViewModel>>(users);
            return View(userViews);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.Delete(id);
            return RedirectToAction("AllUsers");
        }
    }
}
