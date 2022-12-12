using Bonebrake.Monads;

"Hello World"
	.OfMaybe()
	.Invoke(Console.WriteLine);

"Hello World 2"
	.OkResult()
	.InvokeErrors(_ => Console.WriteLine("This will not be in the console since the result is ok"))
	.Bind(x => x.Length)
	.Invoke(Console.WriteLine);
	
"Errored Message"
	.OfResultError()
	.FailureResult<string>()
	.InvokeErrors(x =>
	{
		foreach (var resultError in x)
		{
			Console.WriteLine(resultError);
		}
	})
	.Invoke(_ => Console.WriteLine("This will not be in the console since the result errored"));
	
"Hello World 3"
	.OkResult()
	.Bind(x => x)
	.Map()
	.Map()
	.OfMaybe()
	.Invoke(Console.WriteLine);

string? nullStr = null;
""
	.OfMaybe()
	.Bind(_ => nullStr)
	.Invoke(Console.WriteLine);
	
"D://Test.txt"
	.ToFileIO()
	.Bind(x => x + "Hello World 4\n")
	.Write(x => x + "Hello World 4\n")
	.Map(x => x)
	.Invoke(Console.WriteLine);
	
"D://Test.txt"
	.ToFileIO()
	.MapPath()
	.Invoke(Console.WriteLine);

"D://Test.txt"
	.ToFileIO()
	.Write(_ => string.Empty);
	
new FileIO()
	.Bind(x => x + " This is a test of code that will not execute")
	.Map(x => x)
	.Invoke(Console.WriteLine);

"Hello World 5"
	.OfMaybe()
	.OfMaybe()
	.Merge()
	.Invoke(Console.WriteLine);