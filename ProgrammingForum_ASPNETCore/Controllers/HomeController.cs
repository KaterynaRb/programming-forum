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
        private readonly ILogger<HomeController> _logger;
        private readonly ITopic _topicService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IMapper mapper, ITopic topicService)
        {
            _logger = logger;
            _topicService = topicService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            //var topics = _topicService.GetAll(); //
            IEnumerable<Topic> topics = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54962/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Topic");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IEnumerable<Topic>>();
                    readTask.Wait();
                    topics = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            var topicViews = _mapper.Map<IEnumerable<TopicViewModel>>(topics);
            return View(topicViews);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}