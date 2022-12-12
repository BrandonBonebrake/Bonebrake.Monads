namespace Bonebrake.Monads;

public struct FileIO
{
	private readonly Maybe<string> _path;
	private Maybe<string> _maybeFileContent;

	public FileIO()
	{
		_path = Maybe<string>.None();
		_maybeFileContent = Maybe<string>.None();
	}
	
	public FileIO(string path)
	{
		_path = path.OfMaybe();
		_maybeFileContent = Maybe<string>.None();
	}

	public FileIO Bind(Func<string, string> func)
	{
		_maybeFileContent = File.Exists(_path.Map(x => x).Map()) ? 
			BindInternal(func, FileMode.Open) : 
			Maybe<string>.None();

		return this;
	}
	
	public FileIO Write(Func<string, string> func)
	{
		// Cannot write to a path that does not exist
		if (_path.DoesNotExist()) return this;
		
		_maybeFileContent = BindInternal(func, FileMode.Open);
		File.WriteAllText(_path.Map(x => x).Map()!, _maybeFileContent.Map(x => x).Map());
		
		return this;
	}

	public Maybe<string> Map(Func<string, string> func)
	{
		return _maybeFileContent.Bind(func);
	}
	
	public Maybe<string> MapPath()
	{
		return _path;
	}

	private Maybe<string> BindInternal(Func<string, string> func, FileMode fileMode)
	{
		// Cannot bind to a non-existent file
		if (_path.DoesNotExist()) return Maybe<string>.None();

		if (fileMode is FileMode.Create or FileMode.OpenOrCreate)
		{
			File.Open(_path.Map(x => x).Map()!, fileMode).Close();
		}
		
		return _maybeFileContent.DoesExist() ?
			_maybeFileContent.Bind(func) :
			File.ReadAllText(_path.Map(x => x).Map()!).OfMaybe().Bind(func);
		
	}
}

public static class FileIOExt
{
	public static FileIO ToFileIO(this string path) => new(path);
}