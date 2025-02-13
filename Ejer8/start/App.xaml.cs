
namespace WeatherClient;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(WeatherClient.MainPage), typeof(WeatherClient.MainPage));
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new AppShell());
#if WINDOWS
        window.Width = 500;
        window.Height = 300;
#endif
			return window;
    }
}
