﻿using Bonebrake.Monads.Just;

namespace Bonebrake.Monads.Maybe;

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

	public Just<T> Unbind() => _just;
	public Just<TU> Unbind<TU>(Func<T, TU> func) => new(_just.Unbind(func));
}

public static class MaybeExt
{
	public static Maybe<T> OfMaybe<T>(this T instance) => new(instance);
}