namespace MVVM_Test2;
public class GameViewModel
{
    private User _player;
    private Field _field = new();

    public User Player
    {
        get => _player; 
        init => _player = value;
    }

    public GameViewModel(User player)
    {
        _player = player;
    }
}
