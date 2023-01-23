using System.ComponentModel.DataAnnotations.Schema;

namespace etmDiaryApi.Models
{
    public class Theme
    {
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR(50)")]
        public string Name { get; set; }
        public List<Task>? Tasks { get; set; }
    }
}