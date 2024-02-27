using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM_Test2;
public class LoginViewModel : INotifyPropertyChanged
{
    private readonly UsersCollection _users = new();
    private string _userNick = "";

    public string UserNick
    {
        get => _userNick;
        set
        {
            SetField(ref _userNick, value);
            CurrentUser.Nick = _userNick;
            var u = GetUserByNick(_userNick);
            if (u != null)
            {
                CurrentUser.UpdateInfoFrom(u);
                OnPropertyChanged(nameof(CurrentUser));
            }
        }
    }

    public User CurrentUser { get; } = new();

    public User? GetUserByNick(string nick) => _users.FirstOrDefault(u => u.Nick.Equals(nick));

    public LoginViewModel()
    {
        _users.LoadUsers();
    }

    private Command? _loginCommand;
    public Command LoginCommand => _loginCommand ??= new Command(
        _ => _users.AddUser(CurrentUser), 
        _ => CurrentUser.IsValid
    );

    private Command? _removeCommand;
    public Command RemoveCommand => _removeCommand ??= new Command(
        _ => _users.RemoveUser(CurrentUser), 
        _ => _users.IsExisting(CurrentUser)
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