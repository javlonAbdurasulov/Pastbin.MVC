namespace Pastbin.MVC.Application.Interfaces
{
    public interface IHttpClientService
    {
        public Task<HttpResponseMessage> PostAsync<T>(HttpRequestMessage httpRequest,T obj);
        public Task<HttpResponseMessage> GetAsync(HttpRequestMessage httpRequest);
    }
}
