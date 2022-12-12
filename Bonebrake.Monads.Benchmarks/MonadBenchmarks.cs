using BenchmarkDotNet.Attributes;

namespace Bonebrake.Monads.Benchmarks;

[MemoryDiagnoser(false)]
public class MonadBenchmarks
{
	private static readonly ResultError[] Errors = { new(string.Empty)};
	
	[Benchmark]
	public Just<string> CreateJust()
	{
		return string.Empty.OfJust();
	}
	
	[Benchmark]
	public Maybe<string> CreateMaybe()
	{
		return string.Empty.OfMaybe();
	}
	
	[Benchmark]
	public Either<string, string> CreateEitherLeft()
	{
		return Either<string, string>.EitherLeft(string.Empty);
	}
	
	[Benchmark]
	public Either<string, string> CreateEitherRight()
	{
		return Either<string, string>.EitherLeft(string.Empty);
	}
	
	[Benchmark]
	public Either<string, string> CreateEitherNeither()
	{
		return Either<string, string>.Neither();
	}

	[Benchmark]
	public Result<string> CreateResultOk()
	{
		return string.Empty.OkResult();
	}
	
	[Benchmark]
	public Result<string> CreateResultFailureSingleError()
	{
		return new ResultError(string.Empty).FailureResult<string>();
	}
	
	[Benchmark]
	public Result<string> CreateResultFailureErrorArr()
	{
		return new[] { new ResultError(string.Empty)}.FailureResult<string>();
	}
	
	[Benchmark]
	public Result<string> CreateResultFailureErrorArrCached()
	{
		return Errors.FailureResult<string>();
	}
}