using System.Text.Json;
using System.Collections.ObjectModel;
using System.Globalization;

namespace BartinGorselMaui.Views
{
    public partial class CurrenciesPage : ContentPage
    {
        private readonly HttpClient _client = new();
        public ObservableCollection<KurRow> Dovizler { get; set; } = new();

        public CurrenciesPage()
        {
            InitializeComponent(); // XAML bileşenlerini yükler
            RatesList.ItemsSource = Dovizler; // Listeyi bağla
            _ = KurlariGetirAsync(); // Sayfa açıldığında verileri getir
        }

        // API'den verileri çekme
        private async Task KurlariGetirAsync()
        {
            try
            {
                Loader.IsRunning = Loader.IsVisible = true; // Yükleniyor göstergesi aç
                Dovizler.Clear(); // Eski verileri temizle

                string url = "https://finans.truncgil.com/today.json";

                if (!_client.DefaultRequestHeaders.Contains("User-Agent"))
                    _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");

                var response = await _client.GetStringAsync(url);
                using var doc = JsonDocument.Parse(response);
                var root = doc.RootElement;

                foreach (var prop in root.EnumerateObject())
                {
                    if (prop.Name == "Update_Date") continue; // Tarih alanını atla

                    var data = prop.Value;

                    // Alış fiyatı
                    string alis = TryReadText(data, "Alış");

                    // Satış fiyatı
                    string satis = TryReadText(data, "Satış");

                    // Değişim oranı
                    string fark = TryReadText(data, "Değişim");

                    // Yön oku: fark "-" içeriyorsa ↓, değilse ↑
                    string yon = (!string.IsNullOrEmpty(fark) && fark.Contains("-")) ? "↓" : "↑";

                    Dovizler.Add(new KurRow
                    {
                        Tur = prop.Name,
                        Alis = alis,
                        Satis = satis,
                        Fark = fark,
                        Yon = yon
                    });
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Kurlar yüklenemedi: {ex.Message}", "Tamam");
            }
            finally
            {
                Loader.IsRunning = Loader.IsVisible = false; // Yükleniyor göstergesi kapat
            }
        }

        // Güncelle butonuna tıklandığında çalışır
        private async void OnRefreshClicked(object sender, EventArgs e) => await KurlariGetirAsync();

        // JSON'dan metin okuma (Türkçe sayı formatı ve özel karakterler için)
        private static string TryReadText(JsonElement elem, string key)
        {
            if (!elem.TryGetProperty(key, out var val)) return "—";

            var raw = val.GetString()?.Trim() ?? "—";

            // Bazı değerler "$" veya "%" içeriyor, onları temizle
            raw = raw.Replace("$", "").Replace("%", "").Trim();

            // Nokta yerine virgül varsa, Türkçe sayı formatına göre işlem yap
            if (raw.Contains(","))
            {
                if (double.TryParse(raw, NumberStyles.Any, new CultureInfo("tr-TR"), out var num))
                    return num.ToString("F4", CultureInfo.InvariantCulture);
            }

            return raw;
        }
    }

    // UI'de gösterilecek model
    public class KurRow
    {
        public string Tur { get; set; }    // Döviz türü
        public string Alis { get; set; }   // Alış fiyatı
        public string Satis { get; set; }  // Satış fiyatı
        public string Fark { get; set; }   // Değişim oranı
        public string Yon { get; set; }    // Yön oku (↑ veya ↓)
    }
}



