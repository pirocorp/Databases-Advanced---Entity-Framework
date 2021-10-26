namespace MusicHub.Tests.ModelValidationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using MusicHub;
    using NUnit.Framework;

    [TestFixture]
    public class Model_006
    {
        private static readonly Assembly CurrentAssembly = typeof(StartUp).Assembly;
        private static Type CurrentType;

        [Test]
        public void ValidateDeveloperEntity()
        {
            CurrentType = GetType("Writer");

            AssertPropertyIsOfType("Id", typeof(int));
            AssertPropertyIsOfType("Name", typeof(string));
            AssertPropertyIsOfType("Pseudonym", typeof(string));

            AssertCollectionIsOfType("Songs", GetCollectionType(GetType("Song")));
        }

        private static PropertyInfo GetPropertyByName(Type type, string propName)
        {
            var properties = type.GetProperties();

            var firstOrDefault = properties.FirstOrDefault(p => p.Name == propName);
            return firstOrDefault;
        }

        private static void AssertPropertyIsOfType(string propertyName, Type expectedType)
        {
            var type = CurrentType;

            var property = GetPropertyByName(type, propertyName);
            Assert.IsNotNull(property, $"{type.Name}.{propertyName} property not found.");

            var errorMessage = string.Format($"{type.Name}.{property.Name} property is not {expectedType}!");
            Assert.That(property.PropertyType, Is.EqualTo(expectedType), errorMessage);
        }

        private static void AssertCollectionIsOfType(string propertyName, Type collectionType)
        {
            var type = CurrentType;

            var ordersProperty = GetPropertyByName(type, propertyName);

            var errorMessage = string.Format($"{type.Name}.{propertyName} property does not exist!");

            Assert.IsNotNull(ordersProperty, errorMessage);

            Assert.That(collectionType.IsAssignableFrom(ordersProperty.PropertyType));
        }

        private static Type GetType(string modelName)
        {
            var modelType = CurrentAssembly
                .GetTypes()
                .FirstOrDefault(t => t.Name == modelName);

            Assert.IsNotNull(modelType, $"{modelName} model not found!");

            return modelType;
        }

        private static Type GetCollectionType(Type modelType)
        {
            var collectionType = typeof(IEnumerable<>).MakeGenericType(modelType);
            return collectionType;
        }
    }
}
