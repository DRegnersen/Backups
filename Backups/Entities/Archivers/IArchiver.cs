using Backups.Entities.Repositories;
using Backups.Entities.RepositoryObjects;
using Backups.Entities.Storages;
using Backups.Models.Paths;

namespace Backups.Entities.Archivers;

public interface IArchiver
{
    IStorage Pack(IRepositoryObject repositoryObject, IPath path, IRepository repository);
    IStorage Pack(IReadOnlyCollection<IRepositoryObject> repositoryObjects, IPath path, IRepository repository);
}