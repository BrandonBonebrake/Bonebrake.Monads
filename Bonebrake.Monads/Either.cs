namespace Bonebrake.Monads;

/// <summary>
/// Represents an object that can be either one or another. Also
/// supports neither existing. Is not meant to support both values
/// existing at the same time
/// </summary>
/// <typeparam name="TLeft">Generally the error type</typeparam>
/// <typeparam name="TRight">The valid value</typeparam>
public readonly struct Either<TLeft, TRight>
{
	private readonly Maybe<TLeft> _left;
	private readonly Maybe<TRight> _right;
	
	public  Either() : this(Maybe<TLeft>.None(), Maybe<TRight>.None()) { }
	private Either(TLeft left) : this(left.OfMaybe(), Maybe<TRight>.None()) { }
	private Either(TRight right) : this(Maybe<TLeft>.None(), right.OfMaybe()) { }
	private Either(TLeft? left, TRight? right) : this((left?.OfMaybe() ?? Maybe<TLeft>.None()!)!, (right?.OfMaybe() ?? Maybe<TRight>.None()!)!) { }
	private Either(Maybe<TLeft> left, Maybe<TRight> right)
	{
		_left = left;
		_right = right;
	}
	
	public Maybe<TLeft> MaybeLeft() => _left;
	public Maybe<TRight> MaybeRight() => _right;
	
	public bool IsLeft() => _left.DoesExist();
	public bool IsRight() => _right.DoesExist();

	public Maybe<TU> BindLeft<TU>(Func<TLeft, TU> func) => _left.Bind(func);
	public Maybe<TU> BindLeft<TU>(Func<TLeft, Maybe<TU>> func) => _left.Bind(func);
	
	public Maybe<TU> BindRight<TU>(Func<TRight, TU> func) => _right.Bind(func);
	public Maybe<TU> BindRight<TU>(Func<TRight, Maybe<TU>> func) => _right.Bind(func);

	public Just<TLeft> MapLeft() => _left.Map();
	public Just<TU> MapLeft<TU>(Func<TLeft, TU> func) => _left.Map(func);
	
	public Just<TRight> MapRight() => _right.Map();
	public Just<TU> MapRight<TU>(Func<TRight, TU> func) => _right.Map(func);
	
	public Either<TLeft, TRight> InvokeLeft(Action<TLeft> func)
	{
		_left.Invoke(func);
		return this;
	}

	public Either<TLeft, TRight> InvokeRight(Action<TRight> func)
	{
		_right.Invoke(func);
		return this;
	}
	
	public static Either<TLeft, TRight> EitherLeft(TLeft left) => new(left);
	public static Either<TLeft, TRight> EitherRight(TRight right) => new(right);
}