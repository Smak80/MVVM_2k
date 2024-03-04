namespace MVVM_Test2;
public class Field
{
    private ushort _rows;
    private ushort _cols;
    public ushort Rows
    {
        get => _rows; 
        set => _rows = ushort.Max(ushort.Min(value, 10), 300);
    }

    public ushort Cols
    {
        get => _cols;
        set => _cols = ushort.Max(ushort.Min(value, 10), 300);
    }
    
    public Field(ushort rows, ushort cols)
    {
        Rows = rows;
        Cols = cols;
    }

    public Field() : this(35, 15) { }

}

