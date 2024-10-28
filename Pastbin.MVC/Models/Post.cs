using System.Text.Json.Serialization;

namespace Pastbin.MVC.Models
{
    public class Post
    {
        //

        public int Id { get; set; }
        public string HashUrl { get; set; }
        public string UrlAWS { get; set; }
        public string fileName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ExpireHour { get; set; }
        public int UserId { get; set; }

        //[JsonIgnore]
        //public User User { get; set; }
        //public List<Comment> Comments{ get; set; }

    }
}
