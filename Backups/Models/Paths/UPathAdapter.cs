using Zio;

namespace Backups.Models.Paths;

public class UPathAdapter : IPath, IEquatable<UPathAdapter>
{
    private const char ExtensionSeparator = '.';

    private readonly UPath _uPath;

    public UPathAdapter(string path)
        : this(new UPath(path))
    {
    }

    public UPathAdapter(UPath uPath)
    {
        ArgumentNullException.ThrowIfNull(uPath);
        _uPath = uPath;
    }

    public IPath GetDirectory()
    {
        return new UPathAdapter(_uPath.GetDirectory());
    }

    public string GetName()
    {
        return _uPath.GetName();
    }

    public IPath Join(IPath path)
    {
        return new UPathAdapter(UPath.Combine(_uPath, path.ToString()));
    }

    public IPath Join(string path)
    {
        return Join(new UPathAdapter(path));
    }

    public IPath ToRelative(IPath parentPath)
    {
        var uParentPath = new UPath(parentPath.ToString());

        List<string> splitPath = _uPath.Split();
        List<string> splitParentPath = uParentPath.Split();

        var relativePath = new UPath(string.Join(UPath.DirectorySeparator, splitPath.Except(splitParentPath)));
        return new UPathAdapter(UPath.Combine(uParentPath.GetName(), relativePath));
    }

    public IPath WithExtension(string extension)
    {
        return new UPathAdapter(_uPath.ToString() + ExtensionSeparator + extension);
    }

    public IPath WithoutExtension()
    {
        return new UPathAdapter(_uPath.ToString().Split(ExtensionSeparator).First());
    }

    public override string ToString()
    {
        return _uPath.ToString();
    }

    public bool Equals(UPathAdapter? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _uPath.Equals(other._uPath);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((UPathAdapter)obj);
    }

    public override int GetHashCode()
    {
        return _uPath.GetHashCode();
    }
}