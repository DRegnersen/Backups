using Backups.Entities.RepositoryObjects;
using Backups.Models.Paths;

namespace Backups.Entities.Repositories;

public interface IRepository
{
    Stream OpenWriteStream(IPath path);

    Stream OpenReadStream(IPath path);

    void Delete(IPath path);

    IRepositoryObject GetObject(IPath path);
}