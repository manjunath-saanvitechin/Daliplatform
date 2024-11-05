using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AuthorizationGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthorizationController> _logger;

        public AuthorizationController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<AuthorizationController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("AuthService/register")]
        public async Task<IActionResult> ForwardToRegister([FromBody] object model)
        {
            var response = await ForwardPostRequest("AuthService", "register", model);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        [HttpPost("AuthService/login")]
        public async Task<IActionResult> ForwardToLogin([FromBody] object model)
        {
            var response = await ForwardPostRequest("AuthService", "login", model);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        [HttpGet("AuthService/profile/{username}")]
        [Authorize]
        public async Task<IActionResult> ForwardToGetProfile(string username)
        {
            var response = await ForwardGetRequest("AuthService", $"profile/{username}");
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        [HttpPut("AuthService/profile/{username}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> ForwardToUpdateProfile(string username, [FromBody] object model)
        {
            var response = await ForwardPutRequest("AuthService", $"profile/{username}", model);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        [HttpDelete("AuthService/profile/{username}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ForwardToDeleteProfile(string username)
        {
            var response = await ForwardDeleteRequest("AuthService", $"profile/{username}");
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        private async Task<HttpResponseMessage> ForwardGetRequest(string serviceName, string path)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var serviceUrl = _configuration[$"ServiceUrls:{serviceName}"];
                var requestUrl = $"{serviceUrl}/{path}";

                _logger.LogInformation($"Forwarding GET request to {requestUrl}");

                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                if (Request.Headers.ContainsKey("Authorization"))
                    request.Headers.Add("Authorization", Request.Headers["Authorization"].ToString());

                return await client.SendAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error forwarding GET request: {ex.Message}");
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error forwarding GET request: {ex.Message}")
                };
            }
        }

        private async Task<HttpResponseMessage> ForwardPostRequest(string serviceName, string path, object model)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var serviceUrl = _configuration[$"ServiceUrls:{serviceName}"];
                var requestUrl = $"{serviceUrl}/{path}";

                _logger.LogInformation($"Forwarding POST request to {requestUrl}");

                var request = new HttpRequestMessage(HttpMethod.Post, requestUrl)
                {
                    Content = new StringContent(JsonSerializer.Serialize(model), System.Text.Encoding.UTF8, "application/json")
                };
                if (Request.Headers.ContainsKey("Authorization"))
                    request.Headers.Add("Authorization", Request.Headers["Authorization"].ToString());

                return await client.SendAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error forwarding POST request: {ex.Message}");
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error forwarding POST request: {ex.Message}")
                };
            }
        }

        private async Task<HttpResponseMessage> ForwardPutRequest(string serviceName, string path, object model)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var serviceUrl = _configuration[$"ServiceUrls:{serviceName}"];
                var requestUrl = $"{serviceUrl}/{path}";

                _logger.LogInformation($"Forwarding PUT request to {requestUrl}");

                var request = new HttpRequestMessage(HttpMethod.Put, requestUrl)
                {
                    Content = new StringContent(JsonSerializer.Serialize(model), System.Text.Encoding.UTF8, "application/json")
                };
                if (Request.Headers.ContainsKey("Authorization"))
                    request.Headers.Add("Authorization", Request.Headers["Authorization"].ToString());

                return await client.SendAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error forwarding PUT request: {ex.Message}");
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error forwarding PUT request: {ex.Message}")
                };
            }
        }

        private async Task<HttpResponseMessage> ForwardDeleteRequest(string serviceName, string path)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var serviceUrl = _configuration[$"ServiceUrls:{serviceName}"];
                var requestUrl = $"{serviceUrl}/{path}";

                _logger.LogInformation($"Forwarding DELETE request to {requestUrl}");

                var request = new HttpRequestMessage(HttpMethod.Delete, requestUrl);
                if (Request.Headers.ContainsKey("Authorization"))
                    request.Headers.Add("Authorization", Request.Headers["Authorization"].ToString());

                return await client.SendAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error forwarding DELETE request: {ex.Message}");
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent($"Error forwarding DELETE request: {ex.Message}")
                };
            }
        }
    }
}
