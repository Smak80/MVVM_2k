using System.Windows;
using System.Windows.Controls;

namespace MVVM_Test2;
/// <summary>
/// Логика взаимодействия для GameWindow.xaml
/// </summary>
public partial class GameWindow : Window
{
    private GameViewModel _viewModel;
    public GameWindow(User player)
    {
        InitializeComponent();
        _viewModel = new(player);


    }
}