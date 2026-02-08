using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.IO;

namespace BlazorBlogging.Data
{
    public class PostVersion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string BlogPostId { get; set; }
        
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public double VersionNumber { get; set; }
        public string ChangeDescription { get; set; }
        
    }
}