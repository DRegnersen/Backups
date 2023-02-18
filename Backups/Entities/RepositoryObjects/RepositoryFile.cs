using Backups.Models.Paths;
using Backups.Tools.Visitors;

namespace Backups.Entities.RepositoryObjects;

public class RepositoryFile : IRepositoryFile
{
    private readonly Func<Stream> _readStreamer;

    public RepositoryFile(IPath path, Func<Stream> readStreamer)
    {
        Path = path;
        _readStreamer = readStreamer;
    }

    public IPath Path { get; }

    public Stream OpenReadStream()
    {
        return _readStreamer();
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}