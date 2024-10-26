using Microsoft.AspNetCore.Mvc;
using Pastbin.MVC.Application.Interfaces;
using Pastbin.MVC.Models;
using System.Text.Json;

namespace Pastbin.MVC.Application.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly string? _clientName;

        public HttpClientService(IHttpClientFactory httpClient,IConfiguration configuration)
        {
            _clientName = configuration.GetSection("HttpClientService:ClientName").Value;
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetAsync(HttpRequestMessage httpRequest)
        {
            try
            {
                using HttpClient client = await CreateClient(_clientName);
                var response = await client.SendAsync(httpRequest);
                
                return response;
                //using (var contentStream = await response.Content.ReadAsStreamAsync())
                //{
                //    return await JsonSerializer.DeserializeAsync<HttpResponseMessage>(contentStream);
                //}
                

            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }

        public async Task<HttpResponseMessage> PostAsync<T>(HttpRequestMessage httpRequest,T obj)
        {
            try
            {
                httpRequest.Content = JsonContent.Create(obj);

                using HttpClient client = await CreateClient(_clientName);

                var response = await client.SendAsync(httpRequest);
                response.EnsureSuccessStatusCode();


                using (var contentStream = await response.Content.ReadAsStreamAsync())
                {
                    return await JsonSerializer.DeserializeAsync<HttpResponseMessage>(contentStream);
                }
            }
            catch (HttpRequestException ex)
            {

                throw ex;
            }
        }

        Task<HttpClient> CreateClient(string clientName)
        {
            var client = _httpClient.CreateClient(clientName);
            return Task.FromResult(client);
        }
    }
}
