namespace etmDiaryApi.Models
{
    public class PostTaskWithBoard
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Descreption { get; set; }
        public string Sample { get; set; }
        public List<string> descriptions { get; set; }
        public List<int> usersCode { get; set; }
    }
}
