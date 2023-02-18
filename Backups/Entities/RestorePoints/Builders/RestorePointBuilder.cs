using Backups.Entities.Storages;
using Backups.Exceptions;
using Backups.Models.BackupObjects;
using Backups.Models.Paths;

namespace Backups.Entities.RestorePoints.Builders;

public class RestorePointBuilder
{
    private IPath? _path;
    private DateTime? _creationDateTime;
    private IStorage? _storage;
    private IReadOnlyCollection<IBackupObject>? _backupObjects;

    public RestorePointBuilder WithPath(IPath path)
    {
        _path = path;
        return this;
    }

    public RestorePointBuilder WithCreationDateTime(DateTime creationDateTime)
    {
        _creationDateTime = creationDateTime;
        return this;
    }

    public RestorePointBuilder WithStorage(IStorage storage)
    {
        _storage = storage;
        return this;
    }

    public RestorePointBuilder WithBackupObjects(IReadOnlyCollection<IBackupObject> backupObjects)
    {
        _backupObjects = backupObjects;
        return this;
    }

    public RestorePoint Build()
    {
        return new RestorePoint(
            _path ?? throw RestorePointBuilderException.AbsentPath(),
            _creationDateTime ?? throw RestorePointBuilderException.AbsentCreationDateTime(),
            _storage ?? throw RestorePointBuilderException.AbsentStorage(),
            _backupObjects ?? throw RestorePointBuilderException.AbsentBackupObjects());
    }
}