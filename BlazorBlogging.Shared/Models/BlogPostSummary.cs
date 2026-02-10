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
        public string? ThumbnailUrl { get; set; }
        public string? ThumbnailBase64 { get; set; }

        public string? ThumbnailSrc =>
            !string.IsNullOrEmpty(ThumbnailBase64) ? ThumbnailBase64 :
            !string.IsNullOrEmpty(ThumbnailUrl) ? ThumbnailUrl :
            null;
    }
}
