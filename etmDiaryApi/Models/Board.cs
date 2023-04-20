namespace etmDiaryApi.Models
{
    public class Board
    {
        public int Id { get; set; }
        public string Sample { get; set; }
        public List<Task>? Tasks { get; set; }
        public List<Stick>? Sticks { get; set; }
    }
}