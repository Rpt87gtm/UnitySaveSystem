using Newtonsoft.Json;

namespace SaveSystem
{
    public class NotJsonConverter : IJsonConverter
    {
        public string ToJson(object data)
        {
            return "asdfasdfa";
        }

        public T ToObject<T>(string data)
        {
            return default(T);
        }
    }
}
