namespace Bonebrake.Monads;

/// <summary>
/// The just monad. It's purpose is to just hold an object
/// </summary>
/// <typeparam name="T">The type of the object being held</typeparam>
public readonly struct Just<T>
{
	private readonly T? _instance;
	
	public Just() => _instance = default;
	public Just(T? instance) => _instance = instance;

	public Just<TU> Bind<TU>(Func<T, TU> func)
	{
		return _instance is null ? 
			new Just<TU>() : 
			new Just<TU>(func(_instance));
	}

	public T? Unbind() => _instance;
	public TU? Unbind<TU>(Func<T, TU> func)
	{
		return _instance is null ? 
			default : 
			func(_instance);
	}
	
	public Just<T> Invoke(Action<T> action)
	{
		Bind(t =>
		{
			action(t);
			return new Just<T>();
		});

		return this;
	}

	public bool DoesExist() => _instance is not null;
	public bool DoesNotExist() => _instance is null;
}

public static class JustExt
{
	public static Just<T> OfJust<T>(this T instance) => new(instance);
}