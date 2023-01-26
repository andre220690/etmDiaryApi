using System.ComponentModel.DataAnnotations.Schema;

namespace etmDiaryApi.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Column(TypeName = "NVARCHAR(30)")]
        public string Name { get; set; }
        public List<User>? Users { get; set; }
    }
}