using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BlazorBlogging.Shared.Service;

public class AiSuggestionService
{
    private readonly HttpClient _http;

    public AiSuggestionService(string apiKey)
    {
        _http = new HttpClient();
        _http.BaseAddress = new Uri("https://api.anthropic.com/");
        _http.DefaultRequestHeaders.Add("x-api-key", apiKey);
        _http.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");
    }

    public async Task<string> SuggestTagsAsync(string title, string description, string content)
    {
        var prompt = $"""
            Based on the following blog post, return a single comma-separated string of relevant tags (5-10 tags).
            Return ONLY the comma-separated tags, nothing else. Use lowercase, short tags.

            Title: {title}
            Description: {description}
            Content:
            {content}
            """;

        return await SendAsync(prompt, 200);
    }

    public async Task<string> SuggestDescriptionAsync(string title, string content)
    {
        var prompt = $"""
            Based on the following blog post, write a concise 1-2 sentence description/summary suitable for a blog listing page.
            Return ONLY the description text, nothing else. No quotes around it.

            Title: {title}
            Content:
            {content}
            """;

        return await SendAsync(prompt, 300);
    }

    private async Task<string> SendAsync(string prompt, int maxTokens)
    {
        var body = new
        {
            model = "claude-sonnet-4-5-20250929",
            max_tokens = maxTokens,
            messages = new[]
            {
                new { role = "user", content = prompt }
            }
        };

        var json = JsonSerializer.Serialize(body);
        var request = new HttpRequestMessage(HttpMethod.Post, "v1/messages")
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        var response = await _http.SendAsync(request);
        var responseJson = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Claude API error: {responseJson}");

        using var doc = JsonDocument.Parse(responseJson);
        var text = doc.RootElement
            .GetProperty("content")[0]
            .GetProperty("text")
            .GetString();

        return text?.Trim() ?? "";
    }
}
