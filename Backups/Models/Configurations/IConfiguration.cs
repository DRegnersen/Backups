using Backups.Entities.Repositories;
using Backups.Entities.StorageAlgorithms;

namespace Backups.Models.Configurations;

public interface IConfiguration
{
    IRepository Repository { get; }

    IStorageAlgorithm StorageAlgorithm { get; }
}