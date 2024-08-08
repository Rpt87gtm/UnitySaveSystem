using Newtonsoft.Json;
using System;

namespace SaveSystem
{
    public class NewtonSoftJsonConverter : IJsonConverter
    {
        public string ToJson(object data)
        {
            if (data == null) throw new ArgumentNullException("Object is null");
            return JsonConvert.SerializeObject(data);
        }

        public T ToObject<T>(string data)
        {
            if (data == null) throw new ArgumentNullException("Json is null");
          
            try
            {
                return JsonConvert.DeserializeObject<T>(data);
               
            }
            catch (JsonReaderException ex)
            {
                throw new ArgumentException("The JSON string is invalid and could not be deserialized.", ex);
            }
            catch (JsonSerializationException ex)
            {
                throw new ArgumentException("The JSON string is invalid or does not match the target type.", ex);
            }
        }

        public object ToObject(string data, Type type)
        {
            if (data == null) throw new ArgumentNullException("Json is null");
            try
            {
                return JsonConvert.DeserializeObject(data, type);
            }
            catch (JsonReaderException ex)
            {
                throw new ArgumentException("The JSON string is invalid and could not be deserialized.", ex);
            }
            catch (JsonSerializationException ex)
            {
                throw new ArgumentException("The JSON string is invalid or does not match the target type.", ex);
            }
        }
    }
}
