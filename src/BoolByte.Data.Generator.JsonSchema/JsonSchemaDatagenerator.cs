using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BoolByte.Data.Generator.Core;
using NJsonSchema;

namespace BoolByte.Data.Generator.JsonSchema
{
    public class JsonSchemaDataGenerator<T> : IDataGenerator<T> where T : class
    {
        public object Json4Schema { get; private set; }

        public async Task<IEnumerable<T>> GenerateAsync(Stream schemaStream, int count)
        {
            schemaStream.Seek(0, SeekOrigin.Begin);
            JsonSchema4 schema = null;
            using (var streamReader = new StreamReader(schemaStream))
            {
                schema = await JsonSchema4.FromJsonAsync(streamReader.ReadToEnd());
            }
            return Enumerable
                .Range(1, count)
                .Select(number => Generate(schema));
        }

        private T Generate(JsonSchema4 schema)
        {
            var type = typeof(T);
            var instance = Activator.CreateInstance<T>();
            schema.Properties.ToList().ForEach(schemaProperty =>
            {
                var propertyInfo = type.GetProperty(schemaProperty.Key);
                if (schemaProperty.Value.IsRequired && !schemaProperty.Value.IsReadOnly)
                {
                    propertyInfo.SetValue(instance, Value(propertyInfo.PropertyType));
                }
            });
            return instance;
        }

        private object Value(Type type)
        {
            switch (type.Name)
            {
                case "int":
                case "Int32":
                    return 1;
                case "long":
                case "Int64":
                    return (long)1;
                case "string":
                case "String":
                    return "sample";
                case "DateTime":
                    return DateTime.Now;
                default:
                    return null;
            }
        }
    }
}
