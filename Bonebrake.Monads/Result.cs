namespace Bonebrake.Monads;

/// <summary>
/// Result monad where there can be a valid result
/// returned or known error(s).
/// </summary>
/// <typeparam name="T">The type that can be in the result</typeparam>
public readonly struct Result<T>
{
	private readonly Either<IEnumerable<ResultError>, T> _either;
	private readonly bool _isOk;

	public Result()
	{
		_either = Either<IEnumerable<ResultError>, T>.EitherLeft(Enumerable.Empty<ResultError>());
		_isOk = false;
	}

	public Result(T instance)
	{
		_either = Either<IEnumerable<ResultError>, T>.EitherRight(instance);
		_isOk = true;
	}
	
	public Result(ResultError error)
	{
		_either = Either<IEnumerable<ResultError>, T>.EitherLeft(new []{ error });
		_isOk = false;
	}
	
	public Result(IEnumerable<ResultError> errors)
	{
		_either = Either<IEnumerable<ResultError>, T>.EitherLeft(errors);
		_isOk = false;
	}

	public Maybe<TU> Bind<TU>(Func<T, TU> func)
	{
		return _isOk ? 
			_either.MaybeRight().Bind(func) : 
			Maybe<TU>.None();
	}
	
	public Result<TU> Bind<TU>(Func<T, Result<TU>> func)
	{
		return _isOk ? 
			func(_either.MapRight(x => x).Map()!) : 
			_either.MapLeft(x => x).Map()!.OfResult<TU>();
	}
	
	public Maybe<TU> BindErrors<TU>(Func<IEnumerable<ResultError>, TU> func)
	{
		return !_isOk ? 
			_either.MaybeLeft().Bind(func) : 
			Maybe<TU>.None();
	}
	
	public Just<TU> Map<TU>(Func<T, TU> func)
	{
		return _isOk ? 
			_either.MaybeRight().Map(func) : 
			new Just<TU>();
	}

	public Just<T> Map()
	{
		return _isOk ? 
			_either.MaybeRight().Map() : 
			new Just<T>();
	}
	
	public Just<TU> MapErrors<TU>(Func<IEnumerable<ResultError>, TU> func)
	{
		return !_isOk ? 
			_either.MaybeLeft().Map(func) : 
			new Just<TU>();
	}
	
	public Just<IEnumerable<ResultError>> MapErrors()
	{
		return !_isOk ? 
			_either.MaybeLeft().Map() : 
			new Just<IEnumerable<ResultError>>();
	}
	
	public Result<T> Invoke(Action<T> action)
	{
		_either.InvokeRight(action);
		return this;
	}

	public Result<T> InvokeErrors(Action<IEnumerable<ResultError>> action)
	{
		_either.InvokeLeft(action);
		return this;
	}
}

public static class ResultExt
{
	public static Result<T> OfResult<T>(this T instance) => new(instance);
	public static Result<T> OfResult<T>(this IEnumerable<ResultError> errors) => new(errors);
	public static Result<T> OfResult<T>(this ResultError error) => new(error);
	
	public static Result<T> OfResult<T>(this Just<T> just, Func<Just<T>, Result<T>> func) => func(just);
	public static Result<T> OfResult<T>(this Maybe<T> maybe, Func<Maybe<T>, Result<T>> func) => func(maybe);
	public static Result<T> OfResult<T>(this Either<IEnumerable<ResultError>, T> either)
	{
		return either.IsLeft() ? 
			either.MapLeft().Map()!.OfResult<T>() : 
			either.MapRight().Map()!.OfResult();
	}
}