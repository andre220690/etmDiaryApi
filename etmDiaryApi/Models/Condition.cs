using System.ComponentModel.DataAnnotations.Schema;

namespace etmDiaryApi.Models
{
    public class Condition
    {
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR(30)")]
        public string Name { get; set; }
        public List<Task>? Tasks { get; set; }
    }
}