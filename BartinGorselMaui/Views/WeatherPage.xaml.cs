using BartinGorselMaui.Model;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace BartinGorselMaui.Views;

public partial class WeatherPage : ContentPage
{
    ObservableCollection<Sehir> Sehirler;

    public WeatherPage()
    {
        InitializeComponent();
        // Sayfa açıldığında şehirleri yükle
        lstSehirler.ItemsSource = LoadSehirler();
    }

    // JSON dosyasından şehirleri yükle veya varsayılan şehirleri ekle
    ObservableCollection<Sehir> LoadSehirler()
    {
        string path = Path.Combine(FileSystem.AppDataDirectory, "sehirler.json");
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            Sehirler = JsonSerializer.Deserialize<ObservableCollection<Sehir>>(json);
        }
        else
        {
            Sehirler = new ObservableCollection<Sehir>
            {
                new Sehir { SehirAdi = "İstanbul" },
                new Sehir { SehirAdi = "Ankara" },
                new Sehir { SehirAdi = "İzmir" },
                new Sehir { SehirAdi = "Bartın" }
            };
        }

        return Sehirler;
    }

    // Yeni şehir ekleme
    private async void AddSehir_Clicked(object sender, EventArgs e)
    {
        var sehir = await DisplayPromptAsync("Şehir Ekle", "Lütfen şehir adını giriniz:", "Tamam", "İptal", placeholder: "Örn: Bartın");
        if (!string.IsNullOrWhiteSpace(sehir))
        {
            Sehirler.Add(new Sehir { SehirAdi = sehir });
        }
    }

    // Şehir silme
    private void Remove_Clicked(object sender, EventArgs e)
    {
        var sehir = (sender as Button).CommandParameter as Sehir;
        Sehirler.Remove(sehir);
    }

    // Şehir güncelleme (yeniden yükleme)
    private void Update_Clicked(object sender, EventArgs e)
    {
        var sehir = (sender as Button).CommandParameter as Sehir;
        var ix = Sehirler.IndexOf(sehir);
        Sehirler.Remove(sehir);
        Sehirler.Insert(ix, new Sehir { SehirAdi = sehir.SehirAdi });
    }

    // Sayfa yüklendiğinde yapılacak işlemler
    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        // Burada ekstra işlemler yapabilirsin, şimdilik boş
    }

    // Sayfa kapatıldığında şehirleri JSON dosyasına kaydet
    private void ContentPage_Unloaded(object sender, EventArgs e)
    {
        string path = Path.Combine(FileSystem.AppDataDirectory, "sehirler.json");
        var json = JsonSerializer.Serialize(Sehirler);
        File.WriteAllText(path, json);
    }
}

