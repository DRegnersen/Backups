using Backups.Entities.Repositories;
using Backups.Entities.StorageAlgorithms;

namespace Backups.Models.Configurations;

public class Configuration : IConfiguration
{
    public Configuration(IRepository repository, IStorageAlgorithm storageAlgorithm)
    {
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(storageAlgorithm);
        Repository = repository;
        StorageAlgorithm = storageAlgorithm;
    }

    public IRepository Repository { get; }

    public IStorageAlgorithm StorageAlgorithm { get; }
}