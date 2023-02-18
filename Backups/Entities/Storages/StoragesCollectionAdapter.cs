using Backups.Entities.RepositoryObjects;

namespace Backups.Entities.Storages;

public class StoragesCollectionAdapter : IStorage
{
    private readonly IReadOnlyCollection<IStorage> _storages;

    public StoragesCollectionAdapter(IReadOnlyCollection<IStorage> storages)
    {
        _storages = storages;
    }

    public IReadOnlyCollection<IRepositoryObject> RepositoryObjects =>
        _storages.SelectMany(storage => storage.RepositoryObjects).ToList().AsReadOnly();
}