using Backups.Entities.Archivers;
using Backups.Entities.Repositories;
using Backups.Entities.RepositoryObjects;
using Backups.Entities.Storages;
using Backups.Models.Paths;

namespace Backups.Entities.StorageAlgorithms;

public class SingleStorageAlgorithm : IStorageAlgorithm
{
    private const string ArchiveName = "Backup";

    private readonly IArchiver _archiver;

    public SingleStorageAlgorithm(IArchiver archiver)
    {
        ArgumentNullException.ThrowIfNull(archiver);
        _archiver = archiver;
    }

    public IStorage CreateStorage(
        IReadOnlyCollection<IRepositoryObject> repositoryObjects,
        IPath restorePointPath,
        IRepository repository)
    {
        return _archiver.Pack(repositoryObjects, restorePointPath.Join(ArchiveName), repository);
    }
}