using System.ComponentModel.DataAnnotations.Schema;

namespace etmDiaryApi.Models
{
    public class Stick
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "DATE")]
        public DateTime Date { get; set; }
        [Column(TypeName = "NVARCHAR(30)")]
        public string NameStatus { get; set; }
        public bool? isSuccessful { get; set; }
        public int NumStatus { get; set; }
        public int? TaskId { get; set; }
        public Task? Task { get; set; }
        public int BoardId { get; set; }
        public Board Board { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public List<FavoritSticks> FavoritSticks { get; set; } = new();

    }
}
