namespace etmDiaryApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public List<Task>? Tasks { get; set; }
        public List<Stick>? Sticks { get; set; }
    }
}
