using Backups.Entities.RepositoryObjects;

namespace Backups.Tools.Visitors;

public interface IVisitor
{
    void Visit(IRepositoryFile repositoryFile);

    void Visit(IRepositoryFolder repositoryFolder);
}