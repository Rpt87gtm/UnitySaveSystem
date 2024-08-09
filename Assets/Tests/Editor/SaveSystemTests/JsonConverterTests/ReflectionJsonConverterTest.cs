using NUnit.Framework;
using System.Linq;

namespace SaveSystem.Tests.JsonConverter
{
    [TestFixture]
    public class ReflectionJsonConverterTest : JsonConverterTests
    {
       
        [OneTimeSetUp]
        public override void Init()
        {
            _dataTypes = ConverterConfiguration.GetDataTypesWithPrivateFields();
            _converters = ConverterConfiguration.GetConverters().Select(container => new ReflectionJsonConverter(container));
        }


        
    }
}
