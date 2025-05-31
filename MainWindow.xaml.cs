using EventSourcingDemo.Events;
using EventSourcingDemo.Objects;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EventSourcingDemo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        itemStore.AddEvent(new AddItemEvent(new("Banana", 10, 1.99f)));
        itemStore.StreamEvents();
        DataList.ItemsSource = itemStore.Items;

        EventList.ItemsSource = itemStore.Events;
    }

    private ItemStore itemStore = new();

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        string itemName = ItemNameTextBox.Text;
        int itemAmount = int.TryParse(ItemAmountTextBox.Text, out int amount) ? amount : 0;
        float itemPrice = float.TryParse(ItemPriceTextBox.Text, out float price) ? price : 0.0f;

        Item newItem = new(itemName, itemAmount, itemPrice);
        itemStore.AddEvent(new AddItemEvent(newItem));
        ReloadData();
    }

    private void ChangeAmountButton_Click(object sender, RoutedEventArgs e)
    {
        int itemIndex = DataList.SelectedIndex;
        if (itemIndex < 0 || itemIndex >= itemStore.Items.Count)
        {
            MessageBox.Show("Please select a valid item.");
            return;
        }

        int newAmount = int.TryParse(ChangeAmountTextBox.Text, out int amount) ? amount : 0;
        itemStore.AddEvent(new ChangeItemAmountEvent(itemIndex, newAmount));
        ReloadData();
    }

    private void ChangePriceButton_Click(object sender, RoutedEventArgs e)
    {
        int itemIndex = DataList.SelectedIndex;
        if (itemIndex < 0 || itemIndex >= itemStore.Items.Count)
        {
            MessageBox.Show("Please select a valid item.");
            return;
        }
        float newPrice = float.TryParse(ChangePriceTextBox.Text, out float price) ? price : 0.0f;
        itemStore.AddEvent(new ChangePriceEvent(itemIndex, newPrice));
        ReloadData();
    }

    private void UndoButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            itemStore.Undo();
            ReloadData();
        }
        catch (InvalidOperationException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void ReloadData()
    {
        itemStore.StreamEvents();
        CollectionViewSource.GetDefaultView(DataList.ItemsSource).Refresh();
    }

    private void CheckoutEvent_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int endIndex = EventList.SelectedIndex;
            DataList.ItemsSource = new ObservableCollection<Item>(itemStore.StreamEvents(endIndex));
        }
        catch (ArgumentOutOfRangeException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void RevertEvent_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int endIndex = EventList.SelectedIndex;
            itemStore.ReturnToState(endIndex);
            ReloadData();
        }
        catch (ArgumentOutOfRangeException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}