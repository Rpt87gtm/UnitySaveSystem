using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static UnityEditor.LightingExplorerTableColumn;

namespace SaveSystem.Tests
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
