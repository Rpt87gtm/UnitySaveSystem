using NUnit.Framework;
using UnityEngine;

namespace SaveSystem.Test
{
    [TestFixture]
    public class NewtonSoftJsonConverterTest
    {
        private NewtonSoftJsonConverter _converter;

        [OneTimeSetUp]
        public void Init()
        {
            _converter = new NewtonSoftJsonConverter();
        }

        [Test]
        public void IntTest()
        {
            int five = 5;
            string target = "5";

            string jsonFive = _converter.ToJson(five);

            Assert.AreEqual(target, jsonFive);
        }
        [Test]
        public void SerializatonDeserializationTest()
        {
            int five = 5;

            string jsonFive = _converter.ToJson(five);
            int newFive = _converter.ToObject<int>(jsonFive);

            Assert.AreEqual(five, newFive);
        }
    }
}
