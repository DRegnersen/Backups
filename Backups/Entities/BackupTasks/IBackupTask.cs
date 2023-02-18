using Backups.Entities.Backups;
using Backups.Models.BackupObjects;
using Backups.Models.Configurations;
using Backups.Models.Paths;

namespace Backups.Entities.BackupTasks;

public interface IBackupTask
{
    IBackup Backup { get; }

    IPath BackupRootPath { get; }

    IConfiguration Configuration { get; }

    void AddBackupObject(IBackupObject backupObject);

    void RemoveBackupObject(IBackupObject backupObject);

    void CreateRestorePoint();
}