using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM_Test2;
public class LoginViewModel : INotifyPropertyChanged
{
    private GameWindow? _gWnd = null;
    private readonly UsersCollection _users = new();
    private string _userNick = "";
    private bool _visible = true;

    public bool IsVisible
    {
        get => _visible;
        set
        {
            _visible = value;
            OnPropertyChanged();
        }
    }

    public string UserNick
    {
        get => _userNick;
        set
        {
            SetField(ref _userNick, value);
            CurrentUser.Nick = _userNick;
            var u = _users.GetUserByNick(_userNick);
            if (u != null)
            {
                CurrentUser.UpdateInfoFrom(u);
                OnPropertyChanged(nameof(CurrentUser));
            }
        }
    }

    public User CurrentUser { get; } = new();

    public LoginViewModel()
    {
        _users.LoadUsers();
    }

    private Command? _loginCommand;
    public Command LoginCommand => _loginCommand ??= new Command(
        currWnd =>
        {
            _users.AddUser(CurrentUser);
            StartGameCommand.Execute(CurrentUser);
            if (currWnd is LoginWindow wnd) wnd.Close();
        }, 
        _ => CurrentUser.IsValid
    );

    private Command? _removeCommand;
    public Command RemoveCommand => _removeCommand ??= new Command(
        _ => _users.RemoveUser(CurrentUser), 
        _ => _users.IsExisting(CurrentUser)
    );

    private Command? _startGameCommand;
    public Command StartGameCommand => _startGameCommand ??= new Command(
        param =>
        {
            if (param is User player)
            {
                _gWnd = new GameWindow(player);
                _gWnd.Show();
            }
        }
    );

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}