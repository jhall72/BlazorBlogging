using BlazorBlogging.Data;
using BlazorBlogging.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Photino.Blazor;

namespace BlazorBlogging;

public class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var builder = PhotinoBlazorAppBuilder.CreateDefault(args);

        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        builder.Services.AddLogging();

        var connectionString = configuration["MongoDB:ConnectionString"];
        var databaseName = configuration["MongoDB:DatabaseName"];

        var mongoClient = new MongoClient(connectionString);
        builder.Services.AddDbContext<BlogDbContext>(options =>
            options.UseMongoDB(mongoClient, databaseName!));

        builder.Services.AddScoped<BlogService>();

        var anthropicKey = configuration["Anthropic:ApiKey"] ?? "";
        builder.Services.AddSingleton(new AiSuggestionService(anthropicKey));

        builder.RootComponents.Add<App>("app");

        var app = builder.Build();

        app.MainWindow
            .SetTitle("BlazorBlogging")
            .SetUseOsDefaultSize(false)
            .SetSize(1200, 800);

        AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
        {
            app.MainWindow.ShowMessage("Fatal Exception", error.ExceptionObject.ToString());
        };

        app.Run();
    }
}
