using Backups.Entities.RestorePoints;

namespace Backups.Entities.Backups;

public interface IBackup
{
    IReadOnlyCollection<IRestorePoint> RestorePoints { get; }

    void AddRestorePoint(IRestorePoint restorePoint);

    void RemoveRestorePoint(IRestorePoint restorePoint);
}