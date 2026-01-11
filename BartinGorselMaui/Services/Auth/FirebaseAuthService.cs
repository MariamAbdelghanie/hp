using Firebase.Auth;
using Firebase.Auth.Providers;

namespace BartinGorselMaui.Services.Auth
{
    /// <summary>
    /// Firebase Authentication işlemlerini yöneten servis sınıfı
    /// (Kayıt olma ve giriş yapma)
    /// </summary>
    public class FirebaseAuthService
    {
        // Firebase istemcisi
        private readonly FirebaseAuthClient _client;

        /// <summary>
        /// Firebase yapılandırmasının yapıldığı constructor
        /// </summary>
        public FirebaseAuthService()
        {
            var config = new FirebaseAuthConfig
            {
                // Firebase Console'dan alınan API Key
                ApiKey = Constants.FirebaseApiKey,

                // Firebase proje domain adresi
                AuthDomain = $"{Constants.FirebaseProjectId}.firebaseapp.com",

                // Kullanılacak kimlik doğrulama sağlayıcıları
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider() // Email & Parola ile giriş
                }
            };

            _client = new FirebaseAuthClient(config);
        }

        /// <summary>
        /// Yeni kullanıcı kaydı (Email + Parola)
        /// </summary>
        /// <param name="adSoyad">Kullanıcının adı ve soyadı</param>
        /// <param name="email">Email adresi</param>
        /// <param name="parola">Parola</param>
        /// <returns>Kayıt sonucu ve mesaj</returns>
        public async Task<(bool ok, string mesaj)> RegisterAsync(
            string adSoyad,
            string email,
            string parola)
        {
            try
            {
                var result =
                    await _client.CreateUserWithEmailAndPasswordAsync(
                        email,
                        parola,
                        adSoyad);

                return (result?.User != null, "Kayıt başarılı.");
            }
            catch (Exception ex)
            {
                return (false, $"Kayıt hatası: {ex.Message}");
            }
        }

        /// <summary>
        /// Kullanıcı girişi (Email + Parola)
        /// </summary>
        /// <param name="email">Email adresi</param>
        /// <param name="parola">Parola</param>
        /// <returns>Giriş sonucu ve mesaj</returns>
        public async Task<(bool ok, string mesaj)> LoginAsync(
            string email,
            string parola)
        {
            try
            {
                var result =
                    await _client.SignInWithEmailAndPasswordAsync(
                        email,
                        parola);

                return (result?.User != null, "Giriş başarılı.");
            }
            catch (Exception ex)
            {
                return (false, $"Giriş hatası: {ex.Message}");
            }
        }
    }
}




