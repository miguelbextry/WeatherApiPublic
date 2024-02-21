using Newtonsoft.Json;

namespace WeatherApi.Models
{
    public class ResponseWeather
    {
        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }
        [JsonProperty("estado")]
        public int Estado { get; set; }
        [JsonProperty("datos")]
        public string Datos{ get; set; }
        [JsonProperty("metadatos")]
        public string Metadatos{ get; set; }
    }
}
