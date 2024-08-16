using NUnit.Framework;
using System.ComponentModel;
using System.Linq;

namespace SaveSystem.Tests.JsonConverter
{
    [TestFixture]
    public class PrivateFieldsJsonConverterTest : JsonConverterTests
    {
       
        [OneTimeSetUp]
        public override void Init()
        {
            _dataTypes = ConverterConfiguration.GetDataTypesWithPrivateFields();
            _converters = ConverterConfiguration.GetConverters();
        }


        
    }
}
