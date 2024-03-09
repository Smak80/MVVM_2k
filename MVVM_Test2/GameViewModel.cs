using System.ComponentModel;

namespace MVVM_Test2;

public class GameViewModel
{
    private readonly User _player;
    private readonly List<Test> _t = new();
    public List<Test> T => _t.ToList();

    public User Player
    {
        get => _player.Clone(); 
        init => _player = value;
    }

    public Field Field { get; } = new(30, 17);

    public GameViewModel(User player)
    {
        _player = player;
        _t.Add(new Test {Left = 30, Top = 10, WhSize = 100});
        _t.Add(new Test { Left = 130, Top = 20, WhSize = 100 });
    }
}
