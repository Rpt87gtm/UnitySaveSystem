using System;
using System.Collections.Generic;
using System.Reflection;

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

                return _innerConverter.ToJson(jsonObject);
            }
            else
            {
                var jsonObject = new Dictionary<string, object>();

                foreach (var field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    jsonObject[field.Name] = field.GetValue(data);
                }

                return _innerConverter.ToJson(jsonObject);
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
                    var jsonObject = _innerConverter.ToObject<Dictionary<string, object>>(data);
                    return CreateAnonymousType<T>(jsonObject);
                }
                else
                {
                    var jsonObject = _innerConverter.ToObject<Dictionary<string, object>>(data);
                    var instance = Activator.CreateInstance<T>();

                    foreach (var field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                    {
                        if (jsonObject.TryGetValue(field.Name, out var value))
                        {
                            var json = _innerConverter.ToJson(value);
                            var convertedValue = _innerConverter.ToObject(json, field.FieldType);
                            field.SetValue(instance, convertedValue);
                        }
                    }

                    return instance;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("The JSON string is invalid and could not be deserialized.", ex);
            }
        }

        public object ToObject(string data, Type type)
        {
            return _innerConverter.ToObject(data, type);
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
                    var json = _innerConverter.ToJson(value);
                    args[i] = _innerConverter.ToObject(json, parameters[i].ParameterType);
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