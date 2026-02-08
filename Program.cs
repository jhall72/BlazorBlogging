using Microsoft.Extensions.DependencyInjection;
using Photino.Blazor;

namespace BlazorBlogging;

public class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var builder = PhotinoBlazorAppBuilder.CreateDefault(args);

        builder.Services.AddLogging();

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
