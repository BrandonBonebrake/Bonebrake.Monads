using BenchmarkDotNet.Attributes;

namespace Bonebrake.Monads.Static.Benchmarks;

[MemoryDiagnoser(false)]
public class MonadBenchmarks
{
	[Benchmark]
	public IMaybe<string> CreateMaybe()
	{
		return "".OfMaybe();
	}

	[Benchmark]
	public IEither<string, string> CreateEitherRight()
	{
		return "".OfEitherRight<string, string>();
	}
	
	[Benchmark]
	public IEither<string, string> CreateEitherLeft()
	{
		return "".OfEitherLeft<string, string>();
	}
	
	[Benchmark]
	public IResult<string> CreateResult()
	{
		return "".OfResult();
	}
}