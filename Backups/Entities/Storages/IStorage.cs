using Backups.Entities.RepositoryObjects;

namespace Backups.Entities.Storages;

public interface IStorage
{
    IReadOnlyCollection<IRepositoryObject> RepositoryObjects { get; }
}