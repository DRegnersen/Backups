namespace Backups.Exceptions;

public class BackupException : Exception
{
    private BackupException(string message)
        : base(message)
    {
    }

    public static BackupException AlreadyContains()
    {
        return new BackupException("Backup already contains given restore point");
    }

    public static BackupException DoesNotContain()
    {
        return new BackupException("Backup does not contain given restore point");
    }
}