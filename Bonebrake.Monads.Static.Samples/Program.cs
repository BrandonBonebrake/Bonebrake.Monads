// See https://aka.ms/new-console-template for more information

using Bonebrake.Monads.Static;

"Hello World 0"
	.OfMaybe()
	.Bind(x => x.OfMaybe())
	.Invoke(Console.WriteLine);

"Hello World 1"
	.OfResult()
	.Invoke(Console.WriteLine);
	
"Hello World 2"
	.OfResult()
	.Bind(x => x.OfMaybe())
	.Invoke(Console.WriteLine);
	
"Hello World 3"
	.OfEitherRight<string, string>()
	.InvokeRight(Console.WriteLine)
	.InvokeLeft(x => Console.WriteLine("Should not appear in the output"));
	
"Hello World 4"
	.OfEitherLeft<string, string>()
	.InvokeLeft(Console.WriteLine)
	.InvokeRight(x => Console.WriteLine("Should not appear in the output"));