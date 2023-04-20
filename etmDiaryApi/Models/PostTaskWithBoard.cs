namespace etmDiaryApi.Models
{
    public class PostTaskWithBoard
    {
        public string Start { get; set; }
        public string End { get; set; }
        public string Descreption { get; set; }
        public string Sample { get; set; }
        public List<string> descriptions { get; set; }
        public List<int> usersCode { get; set; }
    }
}
