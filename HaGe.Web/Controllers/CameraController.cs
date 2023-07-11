using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace YourProject.Controllers
{
    [ApiController]
    [Route("api/camera")]
    public class CameraController : ControllerBase
    {
        private readonly HttpClient _client;

        public CameraController(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient();
        }
        
        [AllowAnonymous]
        [HttpPost("capture")]
        public async Task<JObject> CaptureFrames()
        {
            var response = await _client.GetAsync("http://localhost:5000/trigger");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var responseObject = JObject.Parse(responseString);

            return responseObject;
        }
    }
}