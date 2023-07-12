using System;

namespace DataGridPage;

public abstract class ViewModel : Model 
{
    public Command GetCommand(Action action, Predicate<object?>? predicate = default)
    {
        return new Command(action, predicate);
    }
}