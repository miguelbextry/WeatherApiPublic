using Newtonsoft.Json;

namespace WeatherApi.Helper
{
    public class HelperJsonSession
    {
        public static List<T> DeserializeList<T>(string data)
        {
            //MEDIANTE NEWTONSOFT DESERIALIZAMOS EL OBJETO
            List<T> objeto =
                JsonConvert.DeserializeObject<List<T>>(data);
            return objeto;
        }

        public static T DeserializeObject<T>(string data)
        {
            //MEDIANTE NEWTONSOFT DESERIALIZAMOS EL OBJETO
            T objeto =
                JsonConvert.DeserializeObject<T>(data);
            return objeto;
        }
    }
}
