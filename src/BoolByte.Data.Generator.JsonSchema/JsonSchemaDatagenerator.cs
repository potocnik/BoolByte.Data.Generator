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
            var type = typeof(T);
            return Enumerable
                .Range(1, count)
                .Select(number => Activator.CreateInstance(type) as T);
        }
    }
}
