using Bonebrake.Monads;

"Hello World"
	.OfMaybe()
	.Invoke(Console.WriteLine);

"Hello World 2"
	.OkResult()
	.InvokeErrors(x => Console.WriteLine("This will not be in the console since the result is ok"))
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
	.Invoke(x => Console.WriteLine("This will not be in the console since the result errored"));
	
"Hello World 3"
	.OkResult()
	.Bind(x => x)
	.Unbind()
	.Unbind()
	.OfMaybe()
	.Invoke(Console.WriteLine);
	