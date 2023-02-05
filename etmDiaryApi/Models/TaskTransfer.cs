namespace etmDiaryApi.Models
{
    public class TaskTransfer
    {
        public int? Id { get; set; } = null;
        public string Start { get; set; }
        public string End { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
        public string? Result { get; set; } = null;
        public int ThemeId { get; set; }
        public int PartnerId { get; set; }
        public int UserId { get; set; }
        public int ConditionId { get; set; }
    }
}
