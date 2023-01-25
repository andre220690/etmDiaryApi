namespace etmDiaryApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool isSupervisor { get; set; }
        public string Login { get; set; }

        public int Code { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public List<Stick>? Sticks { get; set; }
        public List<FavoritSticks>? FavoritSticks { get; set; } = new();
        public List<FavoritTasks>? FavoritTasks { get; set; } = new();

    }
}
