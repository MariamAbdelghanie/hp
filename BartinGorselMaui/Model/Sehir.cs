using BartinGorselMaui.Services;

namespace BartinGorselMaui.Model
{
    public class Sehir
    {
        public string SehirAdi { get; set; }
        public string BugunUrl => HavaDurumuServisi.HavaDurumuBugun(HavaDurumuServisi.NormalizeCityName(SehirAdi));
        public string BesGunUrl => HavaDurumuServisi.HavaDurumu5gun(HavaDurumuServisi.NormalizeCityName(SehirAdi));
    }
}
