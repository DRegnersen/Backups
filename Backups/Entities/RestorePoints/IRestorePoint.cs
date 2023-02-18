using Backups.Entities.Storages;
using Backups.Models.BackupObjects;
using Backups.Models.Paths;

namespace Backups.Entities.RestorePoints;

public interface IRestorePoint
{
    public IPath Path { get; }

    public DateTime CreationDateTime { get; }

    public IStorage Storage { get; }

    public IReadOnlyCollection<IBackupObject> BackupObjects { get; }
}