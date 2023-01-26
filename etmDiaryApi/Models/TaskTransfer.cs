namespace etmDiaryApi.Models
{
    public class TaskTransfer
    {
        public int? Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
        public string? Result { get; set; }
        public int ThemeId { get; set; }
        public int PartnerId { get; set; }
        public int UserId { get; set; }
        public int ConditionId { get; set; }
    }
}
