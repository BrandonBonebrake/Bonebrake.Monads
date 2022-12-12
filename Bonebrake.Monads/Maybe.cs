namespace Bonebrake.Monads;

/// <summary>
/// The maybe monad. Contains an object that may be
/// set or may be null.
/// </summary>
/// <typeparam name="T">The type of the object being held</typeparam>
public readonly struct Maybe<T>
{
	private readonly Just<T> _just;

	public Maybe() => _just = new Just<T>();
	public Maybe(T instance) => _just = new Just<T>(instance);
	public Maybe(Just<T> just) => _just = just;

	public Maybe<TU> Bind<TU>(Func<T, TU> func)
	{
		return new Maybe<TU>(_just.Bind(func));
	}
	
	public Maybe<TU> Bind<TU>(Func<T, Maybe<TU>> func)
	{
		return _just.DoesExist() ? 
			func(_just.Map()!) : 
			Maybe<TU>.None();
	}

	public Just<T> Map() => _just;
	public Just<TU> Map<TU>(Func<T, TU> func) => new(_just.Map(func));
	
	public Maybe<T> Invoke(Action<T> action)
	{
		_just.Invoke(action);
		return this;
	}
	
	public bool DoesExist() => _just.DoesExist();
	public bool DoesNotExist() => _just.DoesNotExist();
	
	public static Maybe<T> None() => new();
}

public static class MaybeExt
{
	public static Maybe<T> OfMaybe<T>(this T instance) => new(instance);

	public static Maybe<T> Merge<T>(this Maybe<Maybe<T>> compound) => new(compound.Map().Map().Map());
}