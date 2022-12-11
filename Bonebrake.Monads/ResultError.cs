namespace Bonebrake.Monads;

public readonly record struct ResultError(string Message);

public static class ResultErrorExt
{
	public static ResultError OfResultError(this string message) => new(message);
}