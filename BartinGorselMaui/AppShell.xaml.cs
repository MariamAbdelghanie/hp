namespace BartinGorselMaui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(Views.NewsDetailPage), typeof(Views.NewsDetailPage));
        Routing.RegisterRoute(nameof(Views.KategoriPage), typeof(Views.KategoriPage));
        Routing.RegisterRoute(nameof(Views.NewsPage), typeof(Views.NewsPage));
    }
}




