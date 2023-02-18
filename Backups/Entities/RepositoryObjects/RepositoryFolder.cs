using Backups.Models.Paths;
using Backups.Tools.Visitors;

namespace Backups.Entities.RepositoryObjects;

public class RepositoryFolder : IRepositoryFolder
{
    private readonly Func<IReadOnlyCollection<IRepositoryFile>> _filesProvider;

    public RepositoryFolder(IPath path, Func<IReadOnlyCollection<IRepositoryFile>> filesProvider)
    {
        Path = path;
        _filesProvider = filesProvider;
    }

    public IReadOnlyCollection<IRepositoryFile> Files => _filesProvider();

    public IPath Path { get; }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}