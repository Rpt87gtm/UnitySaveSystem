using System;

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
            return default;
        }

        public object ToObject(string data, Type type)
        {
            throw new NotImplementedException();
        }
    }
}
