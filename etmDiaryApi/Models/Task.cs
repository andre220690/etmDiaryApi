using System.ComponentModel.DataAnnotations.Schema;

namespace etmDiaryApi.Models
{
    public class Task
    {
        public int Id { get; set; }
        [Column(TypeName = "DATE")]
        public DateTime Start { get; set; }
        [Column(TypeName = "DATE")]
        public DateTime End { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
        public string? Result { get; set; }
        public string? History { get; set; }
        public int ThemeId { get; set; }
        public Theme? Theme { get; set; }
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
        //public int FavoritTasksId { get; set; }
        public List<FavoritTasks> FavoritTasks { get; set; }
    }
}
