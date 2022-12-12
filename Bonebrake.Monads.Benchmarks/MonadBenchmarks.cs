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
	public string? JustMap()
	{
		return string.Empty.OfJust()
			.Map();
	}
	
	[Benchmark]
	public string? JustMapOneToOne()
	{
		return string.Empty.OfJust()
			.Map(x => x);
	}
	
	[Benchmark]
	public int? JustMapToDifferentType()
	{
		return string.Empty.OfJust()
			.Map(x => x.Length);
	}
	
	[Benchmark]
	public Maybe<string> CreateMaybe()
	{
		return string.Empty.OfMaybe();
	}
	
	[Benchmark]
	public Maybe<string> MaybeMerge()
	{
		return ""
			.OfMaybe()
			.OfMaybe()
			.Merge();
	}
	
	[Benchmark]
	public Just<string> MaybeMapEmpty()
	{
		return ""
			.OfMaybe()
			.Map();
	}
	
	[Benchmark]
	public Just<string> MaybeMapOneToOne()
	{
		return ""
			.OfMaybe()
			.Map(x => x);
	}
	
	[Benchmark]
	public Just<int> MaybeMapToDifferentType()
	{
		return ""
			.OfMaybe()
			.Map(x => x.Length);
	}
	
	[Benchmark]
	public Either<string, string> CreateEitherLeft()
	{
		return Either<string, string>.EitherLeft(string.Empty);
	}
	
	[Benchmark]
	public Just<string> EitherLeftMap()
	{
		return Either<string, string>.EitherLeft(string.Empty)
			.MapLeft();
	}
	
	[Benchmark]
	public Just<string> EitherLeftMapOneToOne()
	{
		return Either<string, string>.EitherLeft(string.Empty)
			.MapLeft(x => x);
	}
	
	[Benchmark]
	public Just<int> EitherLeftMapToDifferentType()
	{
		return Either<string, string>.EitherLeft(string.Empty)
			.MapLeft(x => x.Length);
	}
	
	[Benchmark]
	public Either<string, string> CreateEitherRight()
	{
		return Either<string, string>.EitherLeft(string.Empty);
	}
	
	[Benchmark]
	public Just<string> EitherRightMap()
	{
		return Either<string, string>.EitherRight(string.Empty)
			.MapRight();
	}
	
	[Benchmark]
	public Just<string> EitherRightMapOneToOne()
	{
		return Either<string, string>.EitherRight(string.Empty)
			.MapRight(x => x);
	}
	
	[Benchmark]
	public Just<int> EitherRightMapToDifferentType()
	{
		return Either<string, string>.EitherRight(string.Empty)
			.MapRight(x => x.Length);
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
	
	[Benchmark]
	public Just<string> ResultOkMap()
	{
		return string.Empty.OkResult()
			.Map();
	}
	
	[Benchmark]
	public Just<string> ResultOkMapOneToOne()
	{
		return string.Empty.OkResult()
			.Map(x => x);
	}
	
	[Benchmark]
	public Just<int> ResultOkMapToDifferentType()
	{
		return string.Empty.OkResult()
			.Map(x => x.Length);
	}
	
	[Benchmark]
	public Just<IEnumerable<ResultError>> ResultFailureMap()
	{
		return Errors.FailureResult<string>()
			.MapErrors();
	}
	
	[Benchmark]
	public Just<IEnumerable<ResultError>> ResultFailureMapOneToOne()
	{
		return Errors.FailureResult<string>()
			.MapErrors(x => x);
	}
	
	[Benchmark]
	public Just<bool> ResultFailureMapToDifferentType()
	{
		return Errors.FailureResult<string>()
			.MapErrors(x => x.Equals(null));
	}
}