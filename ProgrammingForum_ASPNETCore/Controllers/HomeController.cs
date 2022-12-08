using AutoMapper;
using BLL;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using ProgrammingForum_ASPNETCore.Models;
using ProgrammingForum_ASPNETCore.Models.TopicModels;
using System.Diagnostics;

namespace ProgrammingForum_ASPNETCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITopicService _topicService;
        private readonly IMapper _mapper;

        public HomeController(IMapper mapper, ITopicService topicService)
        {
            _topicService = topicService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            //var t =  await _topicService.GetAll();

            //IEnumerable<Topic> topics = await _topicService.GetAll();
            //var topicViews = _mapper.Map<IEnumerable<TopicViewModel>>(topics);
            //return View(topicViews);
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}