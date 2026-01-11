using System;

namespace BartinGorselMaui.Model
{
    // Firebase için JSON'a uygun görev modeli
    public class ToDoItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Benzersiz ID
        public string Title { get; set; } // Başlık
        public string Description { get; set; } // Detay
        public bool IsCompleted { get; set; } // Tamamlandı mı?
        public DateTime Date { get; set; } // Tarih ve saat
    }
}



