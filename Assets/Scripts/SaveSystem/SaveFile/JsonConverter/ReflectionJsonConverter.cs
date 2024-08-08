using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace SaveSystem
{
    public class ReflectionJsonConverter : IJsonConverter
    {
        private readonly IJsonConverter _innerConverter;

        public ReflectionJsonConverter(IJsonConverter innerConverter)
        {
            _innerConverter = innerConverter ?? throw new ArgumentNullException(nameof(innerConverter));
        }

        public string ToJson(object data)
        {
            if (data == null) throw new ArgumentNullException("Object is null");

            var type = data.GetType();

            if (type.IsAnonymousType())
            {
                var jsonObject = new Dictionary<string, object>();

                foreach (var property in type.GetProperties())
                {
                    jsonObject[property.Name] = property.GetValue(data);
                }

                return JsonConvert.SerializeObject(jsonObject);
            }
            else
            {
                var jsonObject = new Dictionary<string, object>();

                foreach (var field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    jsonObject[field.Name] = field.GetValue(data);
                }

                return JsonConvert.SerializeObject(jsonObject);
            }
        }

        public T ToObject<T>(string data)
        {
            if (data == null) throw new ArgumentNullException("Json is null");

            try
            {
                var type = typeof(T);

                if (type.IsAnonymousType())
                {
                    var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
                    return CreateAnonymousType<T>(jsonObject);
                }
                else
                {
                    var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
                    var instance = Activator.CreateInstance<T>();

                    foreach (var field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                    {
                        if (jsonObject.TryGetValue(field.Name, out var value))
                        {
                            var json = JsonConvert.SerializeObject(value);
                            var convertedValue = JsonConvert.DeserializeObject(json, field.FieldType);
                            field.SetValue(instance, convertedValue);
                        }
                    }

                    return instance;
                }
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

        private T CreateAnonymousType<T>(Dictionary<string, object> jsonObject)
        {
            var type = typeof(T);
            var ctor = type.GetConstructors()[0];
            var parameters = ctor.GetParameters();
            var args = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                if (jsonObject.TryGetValue(parameters[i].Name, out var value))
                {
                    var json = JsonConvert.SerializeObject(value);
                    args[i] = JsonConvert.DeserializeObject(json, parameters[i].ParameterType);
                }
            }

            return (T)ctor.Invoke(args);
        }
    }

    public static class TypeExtensions
    {
        public static bool IsAnonymousType(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return Attribute.IsDefined(type, typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), false)
                && type.IsGenericType && type.Name.Contains("AnonymousType")
                && (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"))
                && (type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic;
        }
    }
}