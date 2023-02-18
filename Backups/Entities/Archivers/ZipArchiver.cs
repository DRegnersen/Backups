using System.IO.Compression;
using Backups.Entities.ArchiveObjects;
using Backups.Entities.Repositories;
using Backups.Entities.RepositoryObjects;
using Backups.Entities.Storages;
using Backups.Models.Paths;
using Backups.Tools.Visitors;

namespace Backups.Entities.Archivers;

public class ZipArchiver : IArchiver
{
    private const string Extension = "zip";

    public IStorage Pack(IRepositoryObject repositoryObject, IPath path, IRepository repository)
    {
        IPath archivePath = path.WithExtension(Extension);

        using var archive = new ZipArchive(repository.OpenWriteStream(archivePath), ZipArchiveMode.Create);

        var packer = new ZipPacker(archive);
        repositoryObject.Accept(packer);

        return new ZipStorage(packer.GetZipObject(), archivePath, repository);
    }

    public IStorage Pack(IReadOnlyCollection<IRepositoryObject> repositoryObjects, IPath path, IRepository repository)
    {
        IPath archivePath = path.WithExtension(Extension);

        using var zipArchive = new ZipArchive(repository.OpenWriteStream(archivePath), ZipArchiveMode.Create);

        var zipObjects = new List<IZipObject>();

        foreach (IRepositoryObject repositoryObject in repositoryObjects)
        {
            var packer = new ZipPacker(zipArchive);
            repositoryObject.Accept(packer);
            zipObjects.Add(packer.GetZipObject());
        }

        return new ZipStorage(zipObjects.AsReadOnly(), archivePath, repository);
    }
}