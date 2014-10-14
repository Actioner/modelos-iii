namespace BE.ModelosIII.Mvc.Models.Promotion
{
    public class PromotionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Days { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Poster { get; set; }
    }
}