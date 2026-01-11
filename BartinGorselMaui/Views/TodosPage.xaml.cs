using BartinGorselMaui.Model;
using BartinGorselMaui.Services;
using System.Collections.ObjectModel;

namespace BartinGorselMaui.Views;

public partial class TodosPage : ContentPage
{
    ObservableCollection<ToDoItem> todos = new();

    public TodosPage()
    {
        InitializeComponent();
    }

    // 🔹 Sayfa her açıldığında görevleri yükle
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadTodos();
    }

    // 🔹 Firebase'den görevleri yükle
    async Task LoadTodos()
    {
        todos.Clear();
        var list = await FirebaseServices.GetAllTodos();
        foreach (var item in list)
            todos.Add(item);

        ToDoListView.ItemsSource = todos;
    }

    // Yeni görev ekleme
    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddToDoPage(new ToDoItem()));
    }

    // Görev düzenleme
    private async void OnEditClicked(object sender, EventArgs e)
    {
        var item = (sender as Button)?.CommandParameter as ToDoItem;
        if (item != null)
            await Navigation.PushAsync(new AddToDoPage(item));
    }

    // Görev silme (onay ile)
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var item = (sender as Button)?.CommandParameter as ToDoItem;
        if (item == null) return;

        bool confirm = await DisplayAlert("Silinsin mi?", "Silmeyi onayla", "OK", "CANCEL");
        if (confirm)
        {
            todos.Remove(item);
            await FirebaseServices.DeleteTodo(item.Id);
        }
    }
}




