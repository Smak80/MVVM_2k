using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace MVVM_Test2
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel _lvm = new();

        public LoginWindow()
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


            var bVisible = new Binding(nameof(LoginViewModel.IsVisible))
            {
                Source = _lvm.IsVisible
            };
            SetBinding(VisibilityProperty, bVisible);
            
            BtnLogin.Command = _lvm.LoginCommand;
            BtnLogin.CommandParameter = this;

            BtnRemove.Command = _lvm.RemoveCommand;
        }
    }
}