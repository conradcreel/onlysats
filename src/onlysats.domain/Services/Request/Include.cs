namespace onlysats.domain.Services.Request;

/// <summary>
/// Used for signaling a property changed in a Request object
/// </summary>
public class Include<T>
{
    private readonly T _Value;

    public T Value
    {
        get
        {
            return _Value;
        }
    }

    public Include(T value)
    {
        _Value = value;
    }
}