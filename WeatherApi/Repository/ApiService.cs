using System.Net.Http.Headers;
using System.Net.Http;
using WeatherApi.Helper;
using WeatherApi.Models;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;

namespace WeatherApi.Repository
{
    public class ApiService
    {

        private readonly HttpClient httpClient;
        private readonly string apiToken;
        private readonly string linkTemp;

        public ApiService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.apiToken = configuration["ApiToken"];
            this.linkTemp = configuration["LinkTemp"];
        }

        //public async Task<List<WeatherData>> GetExternalUrl()
        //{
        public async Task<ResponseWeather> GetExternalUrl()
        {
            this.SetApiToken();
            Console.WriteLine(this.linkTemp);
            HttpResponseMessage response = await httpClient.GetAsync(this.linkTemp);

            if (response.IsSuccessStatusCode)
            {
                string datos = await this.readEncoding(response);

                ResponseWeather data = HelperJsonSession.DeserializeObject<ResponseWeather>(datos);
                //List<WeatherData> data =HelperJsonSession.DeserializeObject<WeatherData>(
                //await response.Content.ReadAsStringAsync());
                return data;
            }
            else
            {
                throw new Exception("Error al obtener los datos");
            }
        }

        public async Task<ResponseData> GetTempData(string url)
        {
            this.SetApiToken();
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string datos = await this.readEncoding(response);

                List<WeatherData> data = HelperJsonSession.DeserializeList<WeatherData>(datos);
                WeatherData ultima = data.LastOrDefault();

                ResponseData finalresponse = new ResponseData
                {
                    Temperatura = Convert.ToInt32(ultima.Ta),
                    TimeOnly = GetHora()
                };
                return finalresponse;
            }
            else
            {
                throw new Exception("Error al obtener los datos");
            }
        }
        private void SetApiToken()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.apiToken);
        }

        private async Task<string> readEncoding (HttpResponseMessage response)
        {
            string datos = "";
            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream, Encoding.GetEncoding("ISO-8859-1")))
            {
                datos = await reader.ReadToEndAsync();
            }
            return datos;
        }

        private TimeSpan GetHora()
        {
            // Obtén la hora actual en UTC
            DateTime utcNow = DateTime.UtcNow;
            utcNow = DateTime.SpecifyKind(utcNow, DateTimeKind.Utc);
            TimeZoneInfo madridTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time");
            DateTime madridTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, madridTimeZone);
            return madridTime.TimeOfDay;

                
        }
    }
}
