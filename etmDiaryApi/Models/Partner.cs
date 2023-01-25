using System.ComponentModel.DataAnnotations.Schema;

namespace etmDiaryApi.Models
{
    public class Partner
    {
        public int Id { get; set; }
        [Column(TypeName = "NVARCHAR(30)")]
        public string Name { get; set; }
        public List<Task>? Tasks { get; set; }
    }
}