using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MVVM_Test2;
/// <summary>
/// Логика взаимодействия для GameWindow.xaml
/// </summary>
public partial class GameWindow : Window
{
    private readonly GameViewModel _viewModel;
    public GameWindow(User player)
    {
        _viewModel = new(player);
        InitializeComponent();
        DataContext = _viewModel;
        GameElems.ItemsSource = _viewModel.Field.Cells;
    }

    private void GameElems_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (sender is FrameworkElement field)
        {
            _viewModel.Field.Width = (int)(e.NewSize.Width);
            _viewModel.Field.Height = (int)(e.NewSize.Height);
        }
    }
}