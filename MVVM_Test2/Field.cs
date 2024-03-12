using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MVVM_Test2;
public class Field : INotifyPropertyChanged
{
    private int _width;
    private int _height;

    private readonly ushort _rows;
    private readonly ushort _cols;

    public ushort Rows
    {
        get => _rows;
        init => _rows = ushort.Min(ushort.Max(value, 6), 50);
    }

    public ushort Cols
    {
        get => _cols;
        init => _cols = ushort.Min(ushort.Max(value, 6), 50);
    }

    public int Width
    {
        get => _width;
        set
        {
            SetField(ref _width, value);
            RecreateCells();
        } 
    }

    public int Height
    {
        get => _height;
        set
        {
            SetField(ref _height, value);
            RecreateCells();
        }
    }

    private int CellSize => int.Min(Height / Rows, Width / Cols);
    private int LShift => (Width - CellSize * Cols) / 2;
    private int TShift => (Height - CellSize * Rows) / 2;

    public ObservableCollection<CellInfo> Cells { get; } = new();

    private void RecreateCells()
    {
        Cells.Clear();
        if (Height > 0 && Width > 0)
        {
            for (ushort i = 0; i < Rows; i++)
            {
                for (ushort j = 0; j < Cols; j++)
                {
                    Cells.Add(new CellInfo{Left = CellSize * j + LShift, Top = CellSize * i + TShift, Size = CellSize});
                }
            }
        }
    }

    public Field(ushort rows = 30, ushort cols = 15)
    {
        Rows = rows;
        Cols = cols;
    }

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

