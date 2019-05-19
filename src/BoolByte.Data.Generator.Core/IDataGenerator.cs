using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BoolByte.Data.Generator.Core
{
    public interface IDataGenerator<T>
    {
        Task<IEnumerable<T>> GenerateAsync(Stream schemaStream, int count);
    }
}
