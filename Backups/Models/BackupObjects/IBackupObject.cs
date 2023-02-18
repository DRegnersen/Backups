using Backups.Entities.Repositories;
using Backups.Entities.RepositoryObjects;
using Backups.Models.Paths;

namespace Backups.Models.BackupObjects;

public interface IBackupObject
{
    IPath Path { get; }

    IRepository Repository { get; }

    IRepositoryObject RepositoryObject { get; }
}