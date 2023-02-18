namespace Backups.Entities.RepositoryObjects;

public interface IRepositoryFile : IRepositoryObject
{
    Stream OpenReadStream();
}