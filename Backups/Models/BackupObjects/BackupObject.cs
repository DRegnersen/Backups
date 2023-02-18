using Backups.Entities.Repositories;
using Backups.Entities.RepositoryObjects;
using Backups.Models.Paths;

namespace Backups.Models.BackupObjects;

public class BackupObject : IBackupObject, IEquatable<BackupObject>
{
    public BackupObject(IPath path, IRepository repository)
    {
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(repository);

        Path = path;
        Repository = repository;
    }

    public IPath Path { get; }

    public IRepository Repository { get; }

    public IRepositoryObject RepositoryObject => Repository.GetObject(Path);

    public bool Equals(BackupObject? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Path.Equals(other.Path) && Repository.Equals(other.Repository);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((BackupObject)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Path, Repository);
    }
}