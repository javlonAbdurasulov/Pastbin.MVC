namespace Pastbin.MVC.Models
{
    public class PostCreateDTO
    {
        public string UserName { get; set; }
        public string Text { get; set; }
        public int ExpireHour { get; set; }
    }
}
