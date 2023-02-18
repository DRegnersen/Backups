using Backups.Models.Paths;
using Backups.Tools.Visitors;

namespace Backups.Entities.RepositoryObjects;

public interface IRepositoryObject
{
    IPath Path { get; }

    void Accept(IVisitor visitor);
}