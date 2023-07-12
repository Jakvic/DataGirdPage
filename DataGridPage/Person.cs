namespace DataGridPage;

public class Person : Model
{
    private int _id;

    public int Id
    {
        get => _id;
        set => SetField(ref _id, value);
    }

    private string _name = string.Empty;

    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    private string _job = string.Empty;

    public string Job
    {
        get => _job;
        set => SetField(ref _job, value);
    }

    private int _star;

    public int Star
    {
        get => _star;
        set => SetField(ref _star, value);
    }
}