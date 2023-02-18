using Backups.Entities.Storages;
using Backups.Models.BackupObjects;
using Backups.Models.Paths;

namespace Backups.Entities.RestorePoints;

public class RestorePoint : IRestorePoint, IEquatable<RestorePoint>
{
    private readonly List<IBackupObject> _backupObjects;

    public RestorePoint(
        IPath path,
        DateTime creationDateTime,
        IStorage storage,
        IReadOnlyCollection<IBackupObject> backupObjects)
    {
        Path = path;
        CreationDateTime = creationDateTime;
        Storage = storage;
        _backupObjects = new List<IBackupObject>();
        AddBackupObjects(backupObjects);
    }

    public IPath Path { get; }

    public DateTime CreationDateTime { get; }

    public IStorage Storage { get; }

    public IReadOnlyCollection<IBackupObject> BackupObjects => _backupObjects.AsReadOnly();

    public bool Equals(RestorePoint? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Path.Equals(other.Path);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((RestorePoint)obj);
    }

    public override int GetHashCode()
    {
        return Path.GetHashCode();
    }

    private void AddBackupObjects(IReadOnlyCollection<IBackupObject> backupObjects)
    {
        ArgumentNullException.ThrowIfNull(backupObjects);
        foreach (IBackupObject backupObject in backupObjects)
        {
            ArgumentNullException.ThrowIfNull(backupObject);
            _backupObjects.Add(backupObject);
        }
    }
}