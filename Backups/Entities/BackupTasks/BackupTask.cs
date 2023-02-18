using Backups.Entities.Backups;
using Backups.Entities.Backups.Factories;
using Backups.Entities.RestorePoints.Builders;
using Backups.Entities.Storages;
using Backups.Exceptions;
using Backups.Models.BackupObjects;
using Backups.Models.Configurations;
using Backups.Models.Paths;

namespace Backups.Entities.BackupTasks;

public class BackupTask : IBackupTask
{
    private const string DateTimePattern = "dd-MM-yyyy_HH-mm-ss_fff";

    private readonly List<IBackupObject> _backupObjects;

    public BackupTask(IPath backupsRootDirectory, IConfiguration configuration)
        : this(backupsRootDirectory, configuration, new BackupFactory())
    {
    }

    public BackupTask(IPath backupsRootDirectory, IConfiguration configuration, IBackupFactory backupFactory)
    {
        ArgumentNullException.ThrowIfNull(backupsRootDirectory);
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(backupFactory);

        BackupRootPath = backupsRootDirectory;
        Configuration = configuration;
        Backup = backupFactory.Create();
        _backupObjects = new List<IBackupObject>();
    }

    public IBackup Backup { get; }
    public IPath BackupRootPath { get; }
    public IConfiguration Configuration { get; }

    public void AddBackupObject(IBackupObject backupObject)
    {
        if (_backupObjects.Contains(backupObject))
        {
            throw BackupTaskException.AlreadyContains();
        }

        _backupObjects.Add(backupObject);
    }

    public void RemoveBackupObject(IBackupObject backupObject)
    {
        if (!_backupObjects.Remove(backupObject))
        {
            throw BackupTaskException.DoesNotContain();
        }
    }

    public void CreateRestorePoint()
    {
        DateTime creationDateTime = DateTime.Now;
        var restorePointBuilder = new RestorePointBuilder();

        IPath path = BackupRootPath.Join(creationDateTime.ToString(DateTimePattern));
        IStorage storage = Configuration.StorageAlgorithm.CreateStorage(
            _backupObjects.Select(backupObject => backupObject.RepositoryObject).ToList().AsReadOnly(),
            path,
            Configuration.Repository);

        Backup.AddRestorePoint(restorePointBuilder.WithPath(path).WithCreationDateTime(creationDateTime)
            .WithStorage(storage).WithBackupObjects(_backupObjects).Build());
    }
}