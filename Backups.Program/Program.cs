using Backups.Entities.Archivers;
using Backups.Entities.BackupTasks;
using Backups.Entities.Repositories;
using Backups.Entities.StorageAlgorithms;
using Backups.Models.BackupObjects;
using Backups.Models.Configurations;
using Backups.Models.Paths;

namespace Backups.Program;

public static class Program
{
    public static void Main()
    {
        var fileSystemRepository = new FileSystemRepository();
        var configuration = new Configuration(fileSystemRepository, new SingleStorageAlgorithm(new ZipArchiver()));

        var backupTask = new BackupTask(new UPathAdapter("Files/Backups"), configuration);

        var objectA = new BackupObject(new UPathAdapter("Files/Source/a.txt"), fileSystemRepository);
        var objectB = new BackupObject(new UPathAdapter("Files/Source/b.txt"), fileSystemRepository);
        var objectC = new BackupObject(new UPathAdapter("Files/source/java"), fileSystemRepository);

        backupTask.AddBackupObject(objectA);
        backupTask.AddBackupObject(objectB);
        backupTask.AddBackupObject(objectC);
        backupTask.CreateRestorePoint();
    }
}