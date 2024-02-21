using Microsoft.AspNetCore.Mvc;
using WeatherApi.Models;
using WeatherApi.Repository;

namespace WeatherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private ApiService repositorioService;

        public WeatherController(ApiService repositorioService)
        {
            this.repositorioService = repositorioService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetData()
        {
            ResponseWeather response = await this.repositorioService.GetExternalUrl();
            if(response.Estado == 200)
            {
                ResponseData data = await this.repositorioService.GetTempData(response.Datos);
                return Ok(data);
            }
            else
            {
                return StatusCode(response.Estado, response.Descripcion);
            }
        }
    }
}