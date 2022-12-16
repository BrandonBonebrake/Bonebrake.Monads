namespace Bonebrake.Monads.Static;

public static class Io
{
	public static IMaybe<T> Invoke<T>(this IMaybe<T> maybe, Action<T> action)
	{
		return maybe.Bind(t =>
		{
			action(t);
			return t.OfMaybe();
		});
	}
	
	public static IEither<TLeft, TRight> InvokeLeft<TLeft, TRight>(this IEither<TLeft, TRight> either, Action<TLeft> action)
	{
		either.BindLeft(t =>
		{
			action(t);
			return t.OfMaybe();
		});

		return either;
	}
	
	public static IEither<TLeft, TRight> InvokeRight<TLeft, TRight>(this IEither<TLeft, TRight> either, Action<TRight> action)
	{
		either.BindRight(t =>
		{
			action(t);
			return t.OfMaybe();
		});

		return either;
	}
	
	public static IResult<T> Invoke<T>(this IResult<T> result, Action<T> action)
	{
		result.Bind(t =>
		{
			action(t);
			return t.OfMaybe();
		});

		return result;
	}
	
	public static IResult<T> InvokeErrors<T>(this IResult<T> result, Action<IEnumerable<IResultError>> action)
	{
		result.BindErrors(t =>
		{
			var resultErrors = t.ToList();
			action(resultErrors);
			return resultErrors.OfMaybe();
		});

		return result;
	}
}