# BlazorBlogging.Shared

A Razor Class Library providing shared Blazor components and MongoDB-backed services for a blogging platform.

## Components

- **BlogEditor** - Full blog post editor with metadata, thumbnail, and version management
- **RichEditor** - Markdown editor with formatting toolbar and live preview
- **BlogListView** - Card-based blog post listing
- **BlogPostDetail** - Blog post detail/read view
- **BlogContentView** - Markdown-to-HTML content renderer (powered by Markdig)

## Services

- **BlogService** - CRUD operations for blog posts and post versions (MongoDB via EF Core)
- **AiSuggestionService** - AI-powered tag and description suggestions (Claude API)

## Dependencies

- [Markdig](https://github.com/xoofx/markdig) - Markdown processing
- [MongoDB.EntityFrameworkCore](https://www.nuget.org/packages/MongoDB.EntityFrameworkCore) - MongoDB EF Core provider
- Microsoft.AspNetCore.Components.Web

## Installation

```bash
dotnet add package BlazorBlogging.Shared
```

## Usage

Register the required services in your DI container:

```csharp
builder.Services.AddDbContext<BlogDbContext>();
builder.Services.AddScoped<BlogService>();
```

Then use the components in your Razor pages:

```razor
<BlogEditor Post="@post"
            OnSave="HandleSave"
            OnCancel="HandleCancel" />
```
