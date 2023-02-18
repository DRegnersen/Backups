using Backups.Models.Paths;

namespace Backups.Exceptions;

public class InMemoryRepositoryException : Exception
{
    private InMemoryRepositoryException(string message)
        : base(message)
    {
    }

    public static InMemoryRepositoryException ObjectNotFound(IPath path)
    {
        return new InMemoryRepositoryException($"There is no object with path {path} in current in-memory repository");
    }
}