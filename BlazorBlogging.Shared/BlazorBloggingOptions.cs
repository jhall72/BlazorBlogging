namespace BlazorBlogging.Shared;

public class BlazorBloggingOptions
{
    public MongoDbOptions MongoDB { get; set; } = new();
    public AnthropicOptions Anthropic { get; set; } = new();
}

public class MongoDbOptions
{
    public string ConnectionString { get; set; } = "";
    public string DatabaseName { get; set; } = "";
}

public class AnthropicOptions
{
    public string ApiKey { get; set; } = "";
}
