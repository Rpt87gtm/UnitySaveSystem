using Newtonsoft.Json;

namespace SaveSystem
{
    public class NewtonSoftJsonConverter : IJsonConverter
    {
        public string ToJson(object data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public T ToObject<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
