namespace etmDiaryApi.Models
{
    public class FavoritTasks
    {
        public int Id { get; set; }
        public Task Task { get; set; }
        public User User { get; set; }

        public int TaskId { get; set; }
        public int UserId { get; set; }
    }
}
