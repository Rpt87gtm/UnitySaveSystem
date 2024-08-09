
using System;

namespace SaveSystem
{
    public interface IJsonConverter
    {
        string ToJson(object data);
        T ToObject<T>(string data);

        object ToObject(string data, Type type);
    }
}
