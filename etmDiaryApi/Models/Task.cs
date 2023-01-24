using System.ComponentModel.DataAnnotations.Schema;

namespace etmDiaryApi.Models
{
    public class Task
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Priority { get; set; }
        [Column(TypeName = "TEXT")]
        public string Description { get; set; }
        [Column(TypeName = "TEXT")]
        public string? Result { get; set; }
        [Column(TypeName = "TEXT")]
        public string? History { get; set; }
        public int ThemeId { get; set; }
        public Theme Theme { get; set; }
        public int PartnerId { get; set; }
        public Partner? Partner { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public int ConditionId { get; set; }
        public Condition Condition { get; set; }
        public int BoardId { get; set; }
        public Board Board { get; set; }
        public List<Stick> Sticks { get; set; }
        public List<User> Users { get; set; }
        public int FavoritTasksId { get; set; }
        public List<FavoritTasks> FavoritTasks { get; set; } = new();
    }
}
