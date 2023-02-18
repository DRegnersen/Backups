using Backups.Models.Paths;

namespace Backups.Exceptions;

public class ZipFileException : Exception
{
    private ZipFileException(string message)
        : base(message)
    {
    }

    public static ZipFileException EntryNotFound(IPath path)
    {
        return new ZipFileException($"There is no entry with path {path} in current zip archive");
    }
}