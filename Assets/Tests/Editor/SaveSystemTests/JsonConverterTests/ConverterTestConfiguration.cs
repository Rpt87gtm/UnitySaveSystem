using SaveSystem.GameData;
using System;
using System.Collections.Generic;

namespace SaveSystem.Tests.JsonConverter
{
    public static class ConverterConfiguration
    {
        public static IEnumerable<IJsonConverter> GetConverters()
        {
            return new List<IJsonConverter>
            {
                new NewtonSoftJsonConverter()
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
                new Dictionary<int, string>{ { 3, "sfd" },{ 5, "sdf"}},
                

            };
        }

        public static IEnumerable<object> GetDataTypesWithPrivateFields()
        {
            return new List<object>
            {
                new PrivateFieldsExample(3,"win"),

                new BoolData(new Dictionary<string,bool>{ { "test", true },{ "test3", false },{ "test2", false } }),
                new ByteData(new Dictionary<string,byte>{ { "test", 0 },{ "test3", 5 },{ "test2", 255 } }),
                new CharData(new Dictionary<string,char>{ { "test", '3' },{ "test3", 'r' },{ "test2", '/' } }),
                new DoubleData(new Dictionary<string,double>{ { "test", 3 },{ "test3", 5.148762384436482 },{ "test2", -322 } }),
                new FloatData(new Dictionary<string,float>{ { "test", 3 },{ "test3", 5.4234324f },{ "test2", -322 } }),
                new IntData(new Dictionary<string,int>{ { "test", 3 },{ "test3", 5 },{ "test2", -322 } }),
                new LongData(new Dictionary<string,long>{ { "test", 3 },{ "test3", 53223343L },{ "test2", -3220000L } }),
                new ShortData(new Dictionary<string,short>{ { "test", 3 },{ "test3", 5 },{ "test2", -322 } }),
                new StringData(new Dictionary<string,string>{ { "test", "3" },{ "test3", "5" },{ "test2", "-322" } }),
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
        public class PrivateFieldsExample
        {
        
            private readonly int privateField1;

            private readonly string privateField2;

            public PrivateFieldsExample(int field1, string field2)
            {
                privateField1 = field1;
                privateField2 = field2;
            }
            public PrivateFieldsExample()
            {
            }

            public override string ToString()
            {
                return $"privateField1: {privateField1}, privateField2: {privateField2}";
            }
           
            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }

                PrivateFieldsExample other = (PrivateFieldsExample)obj;
                return other.privateField1 == privateField1 && other.privateField2 == privateField2;
            }

           
            public override int GetHashCode()
            {
                return HashCode.Combine(privateField1,privateField2);
            }
        }

    }
}
