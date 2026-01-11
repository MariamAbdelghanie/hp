using Microsoft.Maui.Storage;
using BartinGorselMaui.Views;

namespace BartinGorselMaui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Kullanıcının giriş yapıp yapmadığını kontrol et
            bool isLoggedIn = Preferences.Get("IsLoggedIn", false);

            if (isLoggedIn)
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }

            // Tema seçimini uygula
            bool isDark = Preferences.Get("IsDarkTheme", false);
            App.Current.UserAppTheme = isDark ? AppTheme.Dark : AppTheme.Light;
        }
    }
}








