namespace Bonebrake.Monads.Static;

public static class ResultStatic
{
	public static IResult<T> OfResult<T>(this T instance) => new Result<T>(instance.OfEitherRight<IEnumerable<IResultError>, T>());
	public static IResult<T> OfResult<T>(this IEnumerable<IResultError> errors) => new Result<T>(errors.OfEitherLeft<IEnumerable<IResultError>, T>());
	public static IResult<T> OfResult<T>(this IResultError error) => new Result<T>(new[] {error}.OfEitherLeft<IEnumerable<IResultError>, T>());

	public static IResultError IntoResultError(string error, string message) => new ResultError(error, message);
	
	private sealed record Result<T>(IEither<IEnumerable<IResultError>, T> Either) : IResult<T>
	{
		public IMaybe<TU> Bind<TU>(Func<T, IMaybe<TU>> func) => Either.BindRight(func);
		public IMaybe<TU> BindErrors<TU>(Func<IEnumerable<IResultError>, IMaybe<TU>> func) => Either.BindLeft(func);

		public TU? Map<TU>(Func<T, TU> func) => Either.MapRight(func);
		public TU? MapErrors<TU>(Func<IEnumerable<IResultError>, TU> func) => Either.MapLeft(func);
	}
	
	private sealed record ResultError(string Error, string Message) : IResultError;
}

public interface IResult<out T>
{
	IMaybe<TU> Bind<TU>(Func<T, IMaybe<TU>> func);
	IMaybe<TU> BindErrors<TU>(Func<IEnumerable<IResultError>, IMaybe<TU>> func);
	TU? Map<TU>(Func<T, TU> func);
	TU? MapErrors<TU>(Func<IEnumerable<IResultError>, TU> func);
}

public interface IResultError
{
	public string Error { get; }
	public string Message { get; }
}