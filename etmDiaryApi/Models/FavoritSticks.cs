namespace etmDiaryApi.Models
{
    public class FavoritSticks
    {
        public int Id { get; set; }
        public Stick? Stick { get; set; }
        public User? User { get; set; }

        public int StickId { get; set; }
        public int UserId { get; set; }
    }
}
