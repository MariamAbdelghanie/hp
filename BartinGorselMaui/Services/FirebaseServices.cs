using Firebase.Database;
using Firebase.Database.Query;
using BartinGorselMaui.Model;

namespace BartinGorselMaui.Services
{
    internal static class FirebaseServices
    {
        const string ConnectionString = "https://bartingorselmaui-default-rtdb.firebaseio.com/";
        static FirebaseClient firebaseClient = new FirebaseClient(ConnectionString);

        // Görev ekle veya güncelle
        internal static async Task<(bool, string)> AddOrUpdate(ToDoItem item)
        {
            try
            {
                await firebaseClient.Child("todos").Child(item.Id).PutAsync(item);
                return (true, "Görev başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // Tüm görevleri getir
        internal static async Task<List<ToDoItem>> GetAllTodos()
        {
            var todos = new List<ToDoItem>();
            try
            {
                var result = await firebaseClient.Child("todos").OnceAsync<ToDoItem>();
                foreach (var item in result)
                    todos.Add(item.Object);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Firebase hatası: " + ex.Message);
            }
            return todos;
        }

        // Görev sil
        internal static async Task DeleteTodo(string id)
        {
            await firebaseClient.Child("todos").Child(id).DeleteAsync();
        }
    }
}


