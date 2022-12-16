namespace Bonebrake.Monads.Static;

public static class MaybeStatic
{
	public static IMaybe<T> OfMaybe<T>(this T? instance) => new Maybe<T>(instance);
	public static IMaybe<T> OfEmpty<T>() => new Maybe<T>(default);

	private sealed record Maybe<T>(T? Instance) : IMaybe<T>
	{
		public IMaybe<TU> Bind<TU>(Func<T, IMaybe<TU>> func)
		{
			return Instance is not null ? 
				func(Instance) :
				new Maybe<TU>(default(TU));
		}

		public TU? Map<TU>(Func<T, TU?> func)
		{
			return Instance is not null ? 
				func(Instance) :
				default;
		}
	}
}

public interface IMaybe<out T>
{
	IMaybe<TU> Bind<TU>(Func<T, IMaybe<TU>> func);
	TU? Map<TU>(Func<T, TU?> func);
}