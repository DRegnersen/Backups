namespace Backups.Exceptions;

public class RestorePointBuilderException : Exception
{
    private RestorePointBuilderException(string message)
        : base(message)
    {
    }

    public static RestorePointBuilderException AbsentPath()
    {
        return new RestorePointBuilderException("Path is absent");
    }

    public static RestorePointBuilderException AbsentCreationDateTime()
    {
        return new RestorePointBuilderException("Creation date time is absent");
    }

    public static RestorePointBuilderException AbsentStorage()
    {
        return new RestorePointBuilderException("Storage is absent");
    }

    public static RestorePointBuilderException AbsentBackupObjects()
    {
        return new RestorePointBuilderException("Backup objects are absent");
    }
}