using System.ComponentModel.DataAnnotations.Schema;

namespace etmDiaryApi.Models
{
    public class Stick
    {
        public int Id { get; set; }
        [Column(TypeName = "TEXT")]
        public string Description { get; set; }
        [Column(TypeName = "DATE")]
        public DateTime Date { get; set; }
        [Column(TypeName = "VARCHAR(30)")]
        public int Status { get; set; }
        public int? TaskId { get; set; }
        public Task? Task { get; set; }
        public int BoardId { get; set; }
        public Board Board { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public int FavoritSticksId { get; set; }
        public List<FavoritSticks> FavoritSticks { get; set; } = new();

    }
}
