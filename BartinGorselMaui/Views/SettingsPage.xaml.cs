using Microsoft.Maui.Storage;

namespace BartinGorselMaui.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            // Kullanıcının son tema seçimini oku
            bool isDark = Preferences.Get("IsDarkTheme", false);
            ThemeSwitch.IsToggled = isDark;
            App.Current.UserAppTheme = isDark ? AppTheme.Dark : AppTheme.Light;
        }

        private void OnThemeToggled(object sender, ToggledEventArgs e)
        {
            // Tema değiştir
            App.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;

            // Kullanıcının seçimini kaydet
            Preferences.Set("IsDarkTheme", e.Value);
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            Preferences.Set("IsLoggedIn", false);
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}















