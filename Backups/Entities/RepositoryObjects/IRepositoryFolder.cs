namespace Backups.Entities.RepositoryObjects;

public interface IRepositoryFolder : IRepositoryObject
{
    IReadOnlyCollection<IRepositoryFile> Files { get; }
}