using Backups.Models.Paths;

namespace Backups.Exceptions;

public class FileSystemRepositoryException : Exception
{
    private FileSystemRepositoryException(string message)
        : base(message)
    {
    }

    public static FileSystemRepositoryException ObjectNotFound(IPath path)
    {
        return new FileSystemRepositoryException(
            $"There is no object with path {path} in current file system repository");
    }
}