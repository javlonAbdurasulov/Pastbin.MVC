using Microsoft.AspNetCore.Mvc;
using Pastbin.MVC.Models;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json;
using Pastbin.MVC.Application.Interfaces;

namespace Pastbin.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientService _httpClientService;

        public HomeController(ILogger<HomeController> logger, IHttpClientService httpClientService)
        {
            _logger = logger;
            _httpClientService = httpClientService;
        }
        //public async Task<IActionResult> Registration([FromForm] UserCreateDTO userCreate)
        //{
        //    var json = System.Text.Json.JsonSerializer.Serialize(userCreate);
        //    var responseMessage =await PostAsync("User/Create", json);

        //    //ResponseModel<UserDTO> responseUser = JsonConvert.DeserializeObject<ResponseModel<UserDTO>>(responseMessage);
            
        //    //
        //    UserDTO user = new UserDTO();
        //    user.Username = "Javlon";

        //    List<int> ints = new() { 1, 2, 3 };
        //    user.Posts = ints;

        //    ResponseModel<UserDTO> responseUser = new(user);
        //    //
        //    if(responseUser.Result == null)
        //    {
        //        return View("~/Views/Home/Index.cshtml", new ResponseModel<UserDTO>(responseUser.Error));
        //    }

        //    //
        //    List<Post> posts = new List<Post>()
        //    {
        //        new Post()
        //        {
        //            Id = 1,
        //            CreateTime = DateTime.Now,
        //            EndTime = DateTime.Now,
        //            ExpireHour = 0,
        //            HashUrl="ssilka",
        //            UrlAWS = "google.com",
        //            UserId = 2,
        //        }
        //    };
        //    PostListModel model = new PostListModel();
        //    model.Username = user.Username;
        //    model.Posts = posts;

        //    ResponseModel<PostListModel> response = new(model);
        //    //

        //    return View("~/Views/Home/Dashboard.cshtml", response);
        //    //return View("~/Views/Home/Dashboard.cshtml", responseUser);


        //}
        public async Task<IActionResult> Login([FromForm] UserCreateDTO userLogin)
        {

            UserDTO user = new UserDTO();
            user.Username = userLogin.Username;

            HttpRequestMessage httpRequest = new(HttpMethod.Get, $"User/GetByUsername?username={user.Username}");
            var response = await _httpClientService.GetAsync(httpRequest);
            var responseUser = JsonConvert.DeserializeObject<ResponseModel<UserDTO>>(await response.Content.ReadAsStringAsync());
            if (responseUser.Result == null)
            {
                return View("~/Views/Home/Index.cshtml", new ResponseModel<UserCreateDTO>(responseUser.Error)); 
            }
            PostListModel model = new PostListModel();
            model.Username= responseUser.Result.Username;
            HttpRequestMessage httpRequest2 = new(HttpMethod.Get, $"Posts/GetAllFromUsername?username={model.Username}");
            var responsePosts = await _httpClientService.GetAsync(httpRequest2);
            var posts = JsonConvert.DeserializeObject<ResponseModel<IEnumerable<Post>>>(await responsePosts.Content.ReadAsStringAsync());
            if (posts.Result == null)
            {
                model.Posts = new();
            }
            model.Posts = posts.Result.ToList();

            #region MyRegion

            //getallpostbyusername
            //List<Post> posts = new List<Post>()
            //{
            //    new Post()
            //    {
            //        Id = 1,
            //        CreateTime = DateTime.Now,
            //        EndTime = DateTime.Now,
            //        ExpireHour = 0,
            //        HashUrl="ssilka",
            //        UrlAWS = "google.com",
            //        UserId = 2,
            //    }
            //};
            #endregion

            ResponseModel<PostListModel> responseDashModel = new(model);
            return View("~/Views/Home/Dashboard.cshtml", responseDashModel);
        }
        
        public IActionResult CreatePost([FromForm] PostCreateDTO postCreate)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(postCreate);

            //
            UserDTO user = new UserDTO();
            List<int> ints = new() { 1,2,3};
            user.Posts=ints;
            ResponseModel<UserDTO> response = new(user);
            return View("~/Views/Home/Dashboard.cshtml", response);
        }

        //public async Task<IActionResult> DeletePost(PostDeleteDTO postDelete)
        //{

        //    string _url = "";
        //    var response = await DeleteAsync($"{_url}?param1={Uri.EscapeDataString(postDelete.hashUrl)}&param2={Uri.EscapeDataString(postDelete.username)}");

        //    PostListModel model = new PostListModel();
        //    model.Username = postDelete.username;
        //    //GetAllFromUsername
        //    List<Post> posts = new List<Post>()
        //        {
        //            new Post()
        //            {
        //                Id = 1,
        //                CreateTime = DateTime.Now,
        //                EndTime = DateTime.Now,
        //                ExpireHour = 0,
        //                HashUrl="ssilka",
        //                UrlAWS = "google.com",
        //                UserId = 2,
        //            }
        //        };
        //    model.Posts = posts;

        //    if (response == true)
        //    {
        //        ResponseModel<PostListModel> responseModel = new(model);
        //        return View("~/Views/Home/Dashboard.cshtml", responseModel);
        //    }

        //    //ResponseModel<PostListModel> responseModel = new(model);
        //    // no tut oshibka nado dodelat dlya esli false
        //    return View("~/Views/Home/Dashboard.cshtml", response);
        //}

        //public async Task<string> PostAsync(string url, string json)
        //{
        //    using var client = _httpClientFactory.CreateClient("pastbin");
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");

        //    var response = "www";//await client.PostAsync(url, content);

        //    //response.EnsureSuccessStatusCode();

        //    //return await response.Content.ReadAsStringAsync();
        //    return response;
        //}
        //public async Task<string> GetAsync(string url,string json)
        //{
        //    using var client = _httpClientFactory.CreateClient("pastbin");
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    var escapedUsername = Uri.EscapeDataString(json);

        //    var requestUrl = $"{url}?Username={escapedUsername}";
            
        //    //var response = await client.GetAsync(requestUrl);
        //    //response.EnsureSuccessStatusCode();

        //    //return await response.Content.ReadAsStringAsync();
        //    return requestUrl;
        //}
        //public async Task<bool> DeleteAsync(string url)
        //{
        //    var client = _httpClientFactory.CreateClient("pastbin");
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    var response = await client.DeleteAsync(url);
        //    response.EnsureSuccessStatusCode();
            
        //    string responseString =await response.Content.ReadAsStringAsync();
        //    var responseAction = JsonConvert.DeserializeObject<bool>(responseString);
        //    return responseAction;
            
        //}

        public IActionResult Index()
        {
            ResponseModel<UserCreateDTO> UserDTO = new(""); 
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
