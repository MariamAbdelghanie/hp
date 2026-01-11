using BartinGorselMaui.Services.Auth;

namespace BartinGorselMaui.Views;

public partial class LoginPage : ContentPage
{
    private readonly FirebaseAuthService _auth = new(); // Firebase servisi

    public LoginPage()
    {
        InitializeComponent(); // XAML bileşenlerini yükler
    }

    // "Oturum Aç" butonuna tıklandığında çalışır
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text?.Trim();   // Email giriş alanı
        var parola = PasswordEntry.Text;       // Parola giriş alanı

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(parola))
        {
            await DisplayAlert("Uyarı", "Email ve parola zorunludur.", "Tamam");
            return;
        }

        // Firebase ile giriş yap
        var (ok, mesaj) = await _auth.LoginAsync(email, parola);

        if (ok)
        {
            // Başarılı giriş → AppShell'e yönlendir
            Application.Current.MainPage = new NavigationPage(new AppShell());

        }
        else
        {
            await DisplayAlert("Hata", mesaj, "Tamam");
        }
    }

    // "Hesabım Yok" butonuna tıklandığında çalışır
    private async void OnGoRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new RegisterPage());
    }
}


