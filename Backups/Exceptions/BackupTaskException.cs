namespace Backups.Exceptions;

public class BackupTaskException : Exception
{
    private BackupTaskException(string message)
        : base(message)
    {
    }

    public static BackupTaskException AlreadyContains()
    {
        return new BackupTaskException("Backup task already contains given backup object");
    }

    public static BackupTaskException DoesNotContain()
    {
        return new BackupTaskException("Backup task does not contain given backup object");
    }
}