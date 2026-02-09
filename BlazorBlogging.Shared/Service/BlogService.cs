using BlazorBlogging.Shared.Data;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace BlazorBlogging.Shared.Service;

public class BlogService
{
    private readonly BlogDbContext _context;

    public BlogService(BlogDbContext context)
    {
        _context = context;
    }
    public async Task<List<BlogPost>> GetAllPostsAsync()
    {
        return await _context.Posts.OrderByDescending(p => p.CreatedAt).ToListAsync();
    }

    public async Task<List<BlogPost>> GetPublishedPostsAsync()
    {
        return await _context.Posts
            .Where(p => p.Published)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<BlogPost?> GetPostByIdAsync(string id)
    {
        return await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task CreatePostAsync(BlogPost post)
    {
        post.Id = ObjectId.GenerateNewId().ToString();
        post.CreatedAt = DateTime.Now;
        post.UpdatedAt = DateTime.Now;
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePostAsync(BlogPost post)
    {
        post.UpdatedAt = DateTime.Now;
        _context.Posts.Update(post);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePostAsync(string id)
    {
        var post = await GetPostByIdAsync(id);
        if (post is not null)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<PostVersion>> GetVersionsByPostIdAsync(string blogPostId)
    {
        return await _context.PostVersions
            .Where(v => v.BlogPostId == blogPostId)
            .OrderByDescending(v => v.VersionNumber)
            .ToListAsync();
    }

    public async Task<PostVersion?> GetVersionByIdAsync(string id)
    {
        return await _context.PostVersions.FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task CreateVersionAsync(PostVersion version)
    {
        version.Id = ObjectId.GenerateNewId().ToString();
        version.CreatedAt = DateTime.Now;
        _context.PostVersions.Add(version);
        await _context.SaveChangesAsync();

        var post = await GetPostByIdAsync(version.BlogPostId);
        if (post is not null)
        {
            post.LatestVersionId = version.Id;
            post.UpdatedAt = DateTime.Now;
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteVersionAsync(string id)
    {
        var version = await GetVersionByIdAsync(id);
        if (version is not null)
        {
            _context.PostVersions.Remove(version);
            await _context.SaveChangesAsync();
        }
    }
}
