namespace BlazorBlogging.Shared.Models
{
    public class BlogPostSummary
    {
        public string Id { get; set; } = "";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Published { get; set; }
        public List<string> Tags { get; set; } = new();
    }
}
