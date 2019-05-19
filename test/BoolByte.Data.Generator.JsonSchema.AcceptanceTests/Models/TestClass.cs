using System;
using System.ComponentModel.DataAnnotations;

namespace BoolByte.Data.Generator.JsonSchema.AcceptanceTests.Models
{
    public class TestClass
    {
        [Required]
        [Range(1, int.MaxValue)]
        public long ItemId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z\\-_]{5,10}")]
        [StringLength(10)]
        public string Identifier { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
