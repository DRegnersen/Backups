using Backups.Models.Paths;

namespace Backups.Exceptions;

public class ZipFolderException : Exception
{
    private ZipFolderException(string message)
        : base(message)
    {
    }

    public static ZipFolderException EntryNotFound(IPath path)
    {
        return new ZipFolderException($"There is no entry with path {path} in current zip archive");
    }
}