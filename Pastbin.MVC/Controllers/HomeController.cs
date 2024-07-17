using Microsoft.AspNetCore.Mvc;
using Pastbin.MVC.Models;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json;

namespace Pastbin.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Registration([FromForm] UserCreateDTO userCreate)
        {

            UserDTO user = new UserDTO();
            ResponseModel<UserDTO> response = new(user);
            return View("~/Views/Home/Dashboard.cshtml", response);
        }
        public IActionResult Login([FromForm] UserCreateDTO userLogin)
        {

            UserDTO user = new UserDTO();
            user.Username = "Javlon";

            List<int> ints = new() { 1,2,3};
            user.Posts=ints;
            //getallpostbyusername
            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    CreateTime = DateTime.Now,
                    EndTime = DateTime.Now,
                    ExpireHour = 0,
                    HashUrl="ssilka",
                    UrlAWS = "google.com",
                    UserId = 2,
                }
            };


            PostListModel model = new PostListModel();
            model.Username= user.Username;
            model.Posts=posts;

            ResponseModel<PostListModel> response = new(model);
            return View("~/Views/Home/Dashboard.cshtml", response);
        }
        
        public IActionResult CreatePost([FromForm] UserCreateDTO userLogin)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(userLogin);

            //
            UserDTO user = new UserDTO();
            List<int> ints = new() { 1,2,3};
            user.Posts=ints;
            ResponseModel<UserDTO> response = new(user);
            return View("~/Views/Home/Dashboard.cshtml", response);
        }

        public async Task<IActionResult> DeletePost(PostDeleteDTO postDelete)
        {

            string _url = "";
            var response = await DeleteAsync($"{_url}?param1={Uri.EscapeDataString(postDelete.hashUrl)}&param2={Uri.EscapeDataString(postDelete.username)}");

            PostListModel model = new PostListModel();
            model.Username = postDelete.username;
            //GetAllFromUsername
            List<Post> posts = new List<Post>()
                {
                    new Post()
                    {
                        Id = 1,
                        CreateTime = DateTime.Now,
                        EndTime = DateTime.Now,
                        ExpireHour = 0,
                        HashUrl="ssilka",
                        UrlAWS = "google.com",
                        UserId = 2,
                    }
                };
            model.Posts = posts;

            if (response == true)
            {
                ResponseModel<PostListModel> responseModel = new(model);
                return View("~/Views/Home/Dashboard.cshtml", responseModel);
            }

            //ResponseModel<PostListModel> responseModel = new(model);
            // no tut oshibka nado dodelat dlya esli false
            return View("~/Views/Home/Dashboard.cshtml", response);
        }

        public async Task PostAsync(string url, string json)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
        }
        public async Task<bool> DeleteAsync(string url)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            
            string responseString =await response.Content.ReadAsStringAsync();
            var responseAction = JsonConvert.DeserializeObject<bool>(responseString);
            return responseAction;
            
        }

        public IActionResult Index()
        {
            ResponseModel<UserDTO> UserDTO = new(""); 
            return View(UserDTO);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
