using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using margarita.RecipeBook.Models;
using Tmds.DBus.Protocol;

namespace margarita.RecipeBook.Views;

public partial class RecipeListView : UserControl
{
    public RecipeListView()
    {
        InitializeComponent();
    }

    public void OpenIngredint_Click(object? sender, RoutedEventArgs e)
    {
        var ingr = ((sender as Control)?.DataContext as RecipeComponent)?.MainIngredient;
        if (ingr is null) return;

    }
}