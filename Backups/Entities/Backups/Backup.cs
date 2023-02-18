using Backups.Entities.RestorePoints;
using Backups.Exceptions;

namespace Backups.Entities.Backups;

public class Backup : IBackup
{
    private readonly List<IRestorePoint> _restorePoints;

    public Backup()
    {
        _restorePoints = new List<IRestorePoint>();
    }

    public IReadOnlyCollection<IRestorePoint> RestorePoints => _restorePoints.AsReadOnly();

    public void AddRestorePoint(IRestorePoint restorePoint)
    {
        if (_restorePoints.Contains(restorePoint))
        {
            throw BackupException.AlreadyContains();
        }

        _restorePoints.Add(restorePoint);
    }

    public void RemoveRestorePoint(IRestorePoint restorePoint)
    {
        if (!_restorePoints.Remove(restorePoint))
        {
            throw BackupException.DoesNotContain();
        }
    }
}