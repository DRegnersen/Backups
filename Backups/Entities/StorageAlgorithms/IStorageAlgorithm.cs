using Backups.Entities.Repositories;
using Backups.Entities.RepositoryObjects;
using Backups.Entities.Storages;
using Backups.Models.Paths;

namespace Backups.Entities.StorageAlgorithms;

public interface IStorageAlgorithm
{
    public IStorage CreateStorage(
        IReadOnlyCollection<IRepositoryObject> repositoryObjects,
        IPath restorePointPath,
        IRepository repository);
}