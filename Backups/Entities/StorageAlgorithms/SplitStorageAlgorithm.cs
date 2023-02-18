using Backups.Entities.Archivers;
using Backups.Entities.Repositories;
using Backups.Entities.RepositoryObjects;
using Backups.Entities.Storages;
using Backups.Models.Paths;

namespace Backups.Entities.StorageAlgorithms;

public class SplitStorageAlgorithm : IStorageAlgorithm
{
    private readonly IArchiver _archiver;

    public SplitStorageAlgorithm(IArchiver archiver)
    {
        ArgumentNullException.ThrowIfNull(archiver);
        _archiver = archiver;
    }

    public IStorage CreateStorage(
        IReadOnlyCollection<IRepositoryObject> repositoryObjects,
        IPath restorePointPath,
        IRepository repository)
    {
        var storages = new List<IStorage>();

        foreach (IRepositoryObject repositoryObject in repositoryObjects)
        {
            IPath archivePath = restorePointPath.Join(repositoryObject.Path.WithoutExtension().GetName());
            storages.Add(_archiver.Pack(repositoryObject, archivePath, repository));
        }

        return new StoragesCollectionAdapter(storages.AsReadOnly());
    }
}