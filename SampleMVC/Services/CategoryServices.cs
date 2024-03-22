using MyWebFormApp.BLL.DTOs;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace SampleMVC.Services
{
    public class CategoryServices : ICategoryServices
    {
        private const string BaseUrl = "http://localhost:5272/api/Categories";
        private readonly HttpClient _client;

        public CategoryServices(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            var httpResponse = await _client.GetAsync(BaseUrl);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve category");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var categories = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<CategoryDTO>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return categories;
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            var apiUrl = $"{BaseUrl}/{id}";
            var httpResponse = await _client.GetAsync(apiUrl);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve category");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var categories = System.Text.Json.JsonSerializer.Deserialize<CategoryDTO>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return categories;
        }

        public async Task<int> GetCountCategories(string name)
        {
            var apiUrl = $"{BaseUrl}/forPaging/{name}";

            var httpResponse = await _client.GetAsync(apiUrl);
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve category");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var count = System.Text.Json.JsonSerializer.Deserialize<int>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return count;
        }

        public async Task<IEnumerable<CategoryDTO>> GetWithPaging(int pageNumber, int pageSize, string name)
        {
            var apiUrl = $"{BaseUrl}/withPaging/{name}/{pageNumber}/{pageSize}";
            var httpResponse = await _client.GetAsync(apiUrl);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve category");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var categories = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<CategoryDTO>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return categories;
        }

        async Task<Task> ICategoryServices.Delete(int id)
        {
            var apiUrl = $"{BaseUrl}/{id}";

            var httpResponse = await _client.DeleteAsync(apiUrl);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve category");
            }

            return Task.CompletedTask;
        }

        async Task<Task> ICategoryServices.Insert(CategoryCreateDTO categoryCreateDTO)
        {
            var apiUrl = $"{BaseUrl}";

            var jsonContent = JsonConvert.SerializeObject(categoryCreateDTO);

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var httpResponse = await _client.PostAsync(apiUrl, content);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve category");
            }

            return Task.CompletedTask;
        }

        async Task<Task> ICategoryServices.Update(int id, CategoryUpdateDTO categoryUpdateDTO)
        {
            var apiUrl = $"{BaseUrl}/{id}";

            var jsonContent = JsonConvert.SerializeObject(categoryUpdateDTO);
            
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var httpResponse = await _client.PutAsync(apiUrl, content);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve category");
            }

            return Task.CompletedTask;
        }
    }
}
