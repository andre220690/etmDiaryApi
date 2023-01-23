using System.ComponentModel.DataAnnotations.Schema;

namespace etmDiaryApi.Models
{
    public class Board
    {
        public int Id { get; set; }
        [Column(TypeName = "TEXT")]
        public string Sample { get; set; }
        public List<Task>? Tasks { get; set; }
        public List<Stick>? Sticks { get; set; }
    }
}