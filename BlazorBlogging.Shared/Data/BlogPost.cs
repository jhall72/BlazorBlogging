using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlazorBlogging.Shared.Data
{
    public class BlogPost
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? LatestVersionId { get; set; }
        public string Title { get; set; } = "New Blog Post";
        public string Description { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public bool Published { get; set; } = false;

        public string? ThumbnailUrl { get; set; }
        public string? ThumbnailBase64 { get; set; }
        public List<string> Tags { get; set; } = new();
    }
}
