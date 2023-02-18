namespace Backups.Entities.Backups.Factories;

public class BackupFactory : IBackupFactory
{
    public IBackup Create() => new Backup();
}