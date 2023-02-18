using Backups.Entities.Archivers;
using Backups.Entities.BackupTasks;
using Backups.Entities.Repositories;
using Backups.Entities.StorageAlgorithms;
using Backups.Models.BackupObjects;
using Backups.Models.Configurations;
using Backups.Models.Paths;
using Xunit;
using Zio.FileSystems;

namespace Backups.Test;

public class TestBackupTask
{
    [Fact]
    public void AddAndRemoveBackupObjects_CreateRestorePointsAndStorages()
    {
        var inMemoryRepository = new InMemoryRepository(new MemoryFileSystem());

        var fileAPath = new UPathAdapter("/Source/a.txt");
        using (Stream fileAStream = inMemoryRepository.OpenWriteStream(fileAPath))
        {
            using var writerOfA = new StreamWriter(fileAStream);
            writerOfA.WriteLine("It is a file 'a");
        }

        var fileBPath = new UPathAdapter("/Source/b.txt");
        using (Stream fileBStream = inMemoryRepository.OpenWriteStream(fileBPath))
        {
            using var writerOfB = new StreamWriter(fileBStream);
            writerOfB.WriteLine("It is a file 'b");
        }

        var objectA = new BackupObject(fileAPath, inMemoryRepository);
        var objectB = new BackupObject(fileBPath, inMemoryRepository);

        var configuration = new Configuration(inMemoryRepository, new SplitStorageAlgorithm(new ZipArchiver()));
        var backupTask = new BackupTask(new UPathAdapter("/Backups"), configuration);

        // Add 2 backup objects
        backupTask.AddBackupObject(objectA);
        backupTask.AddBackupObject(objectB);
        backupTask.CreateRestorePoint();

        // Remove 1 backup object
        backupTask.RemoveBackupObject(objectB);
        backupTask.CreateRestorePoint();

        Assert.True(backupTask.Backup.RestorePoints.Count == 2);
        Assert.True(backupTask.Backup.RestorePoints.Sum(restorePoint => restorePoint.Storage.RepositoryObjects.Count) ==
                    3);
    }
}