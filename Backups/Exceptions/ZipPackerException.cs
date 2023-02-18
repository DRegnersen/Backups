namespace Backups.Exceptions;

public class ZipPackerException : Exception
{
    private ZipPackerException(string message)
        : base(message)
    {
    }

    public static ZipPackerException ObjectNotCreated()
    {
        return new ZipPackerException("Object has not been created yet");
    }
}