using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MVVM_Test2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LoginViewModel _lvm = new();
        public MainWindow()
        {
            InitializeComponent();
            Binding bNick = new(nameof(LoginViewModel.UserNick))
            {
                Source = _lvm,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            TbNick.SetBinding(TextBox.TextProperty, bNick);

            Binding bName = new(nameof(User.Name))
            {
                Source = _lvm.CurrentUser
            };
            TbName.SetBinding(TextBox.TextProperty, bName);
            Binding bBirth = new(nameof(User.Birth))
            {
                Source = _lvm.CurrentUser
            };
            DpBirth.SetBinding(DatePicker.SelectedDateProperty, bBirth);

            BtnLogin.Command = _lvm.LoginCommand;
            BtnRemove.Command = _lvm.RemoveCommand;
        }
    }
}