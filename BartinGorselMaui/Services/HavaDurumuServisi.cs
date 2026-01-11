namespace BartinGorselMaui.Services
{
    public static class HavaDurumuServisi
    {
        public static string NormalizeCityName(string cityName)
        {
            cityName = cityName.ToUpper()
                .Replace("Ç", "C").Replace("Ö", "O").Replace("Ş", "S").Replace("İ", "I").Replace("Ü", "U").Replace("Ğ", "G")
                .Replace("ç", "C").Replace("ö", "O").Replace("ş", "S").Replace("ı", "I").Replace("ü", "U").Replace("ğ", "G");

            if (cityName == "KAHRAMANMARAS") return "K.MARAS";
            if (cityName == "AFYON") return "AFYONKARAHISAR";

            return cityName;
        }

        public static string HavaDurumuBugun(string sehir)
        {
            return $"http://www.mgm.gov.tr/sunum/sondurum-show-2.aspx?m={sehir}&rC=111&rZ=fff";
        }

        public static string HavaDurumu5gun(string sehir)
        {
            return $"https://www.mgm.gov.tr/sunum/tahmin-show-2.aspx?m={sehir}&basla=1&bitir=5&rC=111&rZ=fff";
        }
    }
}
