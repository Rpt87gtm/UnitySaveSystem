
namespace SaveSystem
{
    public interface IJsonConverter
    {
        string ToJson(object data);
        T ToObject<T>(string data);
    }
}
