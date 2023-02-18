using System.IO.Compression;
using Backups.Entities.ArchiveObjects;
using Backups.Entities.Repositories;
using Backups.Entities.RepositoryObjects;
using Backups.Models.Paths;

namespace Backups.Entities.Storages;

public class ZipStorage : IStorage
{
    private readonly IReadOnlyCollection<IZipObject> _zipObjects;
    private readonly IPath _zipArchivePath;
    private readonly IRepository _repository;

    public ZipStorage(IZipObject zipObject, IPath zipArchivePath, IRepository repository)
        : this(new List<IZipObject> { zipObject }, zipArchivePath, repository)
    {
    }

    public ZipStorage(IReadOnlyCollection<IZipObject> zipObjects, IPath zipArchivePath, IRepository repository)
    {
        _zipObjects = zipObjects;
        _zipArchivePath = zipArchivePath;
        _repository = repository;
    }

    public IReadOnlyCollection<IRepositoryObject> RepositoryObjects
    {
        get
        {
            using var archive = new ZipArchive(_repository.OpenReadStream(_zipArchivePath), ZipArchiveMode.Read);
            return _zipObjects.Select(zipObject => zipObject.GetRepositoryObject(archive)).ToList().AsReadOnly();
        }
    }
}