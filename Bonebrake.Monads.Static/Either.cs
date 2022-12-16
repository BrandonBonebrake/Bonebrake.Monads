namespace Bonebrake.Monads.Static;

public static class EitherStatic
{
	public static IEither<TLeft, TRight> OfEitherLeft<TLeft, TRight>(this TLeft left) => new Either<TLeft, TRight>(left);
	public static IEither<TLeft, TRight> OfEitherRight<TLeft, TRight>(this TRight right) => new Either<TLeft, TRight>(right);

	private sealed record Either<TLeft, TRight> : IEither<TLeft, TRight>
	{
		private readonly IMaybe<TLeft> _left;
		private readonly IMaybe<TRight> _right;

		public Either(TLeft left)
		{
			_left = left.OfMaybe();
			_right = MaybeStatic.OfEmpty<TRight>();
		}
		
		public Either(TRight right)
		{
			_left = MaybeStatic.OfEmpty<TLeft>();
			_right = right.OfMaybe();
		}

		public IMaybe<TL> BindLeft<TL>(Func<TLeft, IMaybe<TL>> func)
		{
			return _left.Bind(func);
		}

		public IMaybe<TR> BindRight<TR>(Func<TRight, IMaybe<TR>> func)
		{
			return _right.Bind(func);
		}

		public TL? MapLeft<TL>(Func<TLeft, TL> func)
		{
			return _left.Map(func);
		}

		public TR? MapRight<TR>(Func<TRight, TR> func)
		{
			return _right.Map(func);
		}
	}
}

public interface IEither<out TLeft, out TRight>
{
	IMaybe<TL> BindLeft<TL>(Func<TLeft, IMaybe<TL>> func);
	IMaybe<TR> BindRight<TR>(Func<TRight, IMaybe<TR>> func);

	TL? MapLeft<TL>(Func<TLeft, TL> func);
	TR? MapRight<TR>(Func<TRight, TR> func);
}