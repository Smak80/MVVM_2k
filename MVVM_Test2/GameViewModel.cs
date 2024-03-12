using System.ComponentModel;

namespace MVVM_Test2;

public class GameViewModel(User player)
{
    public User Player
    {
        get => player.Clone(); 
        init => player = value;
    }

    public Field Field { get; } = new(30, 17);
}