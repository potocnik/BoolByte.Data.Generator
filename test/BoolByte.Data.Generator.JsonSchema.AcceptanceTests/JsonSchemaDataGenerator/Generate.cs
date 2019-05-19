using BoolByte.Data.Generator.JsonSchema.AcceptanceTests.Models;
using FluentAssertions;
using Newtonsoft.Json;
using NJsonSchema;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoolByte.Data.Generator.JsonSchema.AcceptanceTests.JsonSchemaDataGenerator
{
    public class Generate
    {
        [Test]
        public async Task GeneratesExpectedData()
        {
            using (var stream = new MemoryStream())
            {
                var schema = await JsonSchema4.FromTypeAsync<TestClass>();
                using (var textWriter = new StreamWriter(stream, Encoding.Default, 1024, true))
                {
                    await textWriter.WriteAsync(schema.ToJson());
                }
                var testSubject = new JsonSchemaDataGenerator<TestClass>();
                var actual = await testSubject.GenerateAsync(stream, 5);
                actual.Should().NotBeNull();
                var list = actual.ToList();
                list.Count.Should().Be(5);

                list.ForEach(item =>
                {
                    var json = JsonConvert.SerializeObject(item);
                    var validationErrors = schema.Validate(json);
                    validationErrors.Should().HaveCount(0, because: $"Validation errors found on properties: {string.Join(", ", validationErrors.Select(i => i.Property))}");
                });
            }
        }
    }
}
