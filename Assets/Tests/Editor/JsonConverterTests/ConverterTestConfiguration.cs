using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SaveSystem.Tests
{
    public static class ConverterConfiguration
    {
        public static IEnumerable<IJsonConverter> GetConverters()
        {
            return new List<IJsonConverter>
            {
                new NewtonSoftJsonConverter(), 
                //new NotJsonConverter()  just for test
            };
        }

        // Method to get a list of data types to be tested
        public static IEnumerable<object> GetDataTypes()
        {
            return new List<object>
            {
                new { Name = "Test", Age = 30 },
                new Person { Name = "John", Age = 25 },
                new Product { Id = 1, Price = 99.99 },
                new Dictionary<int, string>{ { 3, "sfd" },{ 5, "sdf"}}
                //new Vector2(3,4) u cannot serialize just Vector2
            };
        }

        // Example of custom classes used in testing
        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                    return false;

                Person other = (Person)obj;
                return Name == other.Name && Age == other.Age;
            }
            public override int GetHashCode()
            {
                return HashCode.Combine(Name, Age);
            }
        }

        public class Product
        {
            public int Id { get; set; }
            public double Price { get; set; }
            public override bool Equals(object obj)
            {

                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }

                Product other = (Product)obj;
                return Id == other.Id && Price == other.Price;
            }
            public override int GetHashCode()
            {
                return HashCode.Combine(Id, Price);
            }

        }

    }
}
