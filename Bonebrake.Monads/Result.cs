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
		_either = Either<IEnumerable<ResultError>, T>.Neither();
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
	
	public Maybe<TU> BindErrors<TU>(Func<IEnumerable<ResultError>, TU> func)
	{
		return !_isOk ? 
			_either.MaybeLeft().Bind(func) : 
			Maybe<TU>.None();
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
	public static Result<T> OkResult<T>(this T instance) => new(instance);
	public static Result<T> FailureResult<T>(this IEnumerable<ResultError> errors) => new(errors);
	public static Result<T> FailureResult<T>(this ResultError error) => new(error);
}