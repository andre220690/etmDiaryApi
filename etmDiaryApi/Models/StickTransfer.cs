namespace etmDiaryApi.Models
{
    public class StickTransfer
    {
        public string Description { get; set; }
        public string NameStatus { get; set; }
        public int NumStatus { get; set; }
        public int? TaskId { get; set; } = null;
        public int BoardId { get; set; }
        public int UserCode { get; set; }

    }
}
