using System.IO.Compression;
using Backups.Entities.ArchiveObjects;
using Backups.Entities.RepositoryObjects;
using Backups.Exceptions;
using Backups.Models.Paths;
using ZipFile = Backups.Entities.ArchiveObjects.ZipFile;

namespace Backups.Tools.Visitors;

public class ZipPacker : IVisitor
{
    private readonly ZipArchive _archive;
    private IZipObject? _zipObject;

    public ZipPacker(ZipArchive archive)
    {
        _archive = archive;
    }

    public void Visit(IRepositoryFile repositoryFile)
    {
        ZipArchiveEntry zipArchiveEntry = _archive.CreateEntry(repositoryFile.Path.GetName());
        using Stream fileStream = repositoryFile.OpenReadStream();
        using Stream entryStream = zipArchiveEntry.Open();

        fileStream.CopyTo(entryStream);
        _zipObject = new ZipFile(new UPathAdapter(repositoryFile.Path.GetName()));
    }

    public void Visit(IRepositoryFolder repositoryFolder)
    {
        var zipFiles = new List<ZipFile>();

        foreach (IRepositoryFile repositoryFile in repositoryFolder.Files)
        {
            IPath inArchivePath = repositoryFile.Path.ToRelative(repositoryFolder.Path);
            ZipArchiveEntry zipArchiveEntry = _archive.CreateEntry(inArchivePath.ToString());

            using Stream fileStream = repositoryFile.OpenReadStream();
            using Stream entryStream = zipArchiveEntry.Open();

            fileStream.CopyTo(entryStream);
            zipFiles.Add(new ZipFile(inArchivePath));
        }

        _zipObject = new ZipFolder(new UPathAdapter(repositoryFolder.Path.GetName()), zipFiles.AsReadOnly());
    }

    public IZipObject GetZipObject()
    {
        return _zipObject ?? throw ZipPackerException.ObjectNotCreated();
    }
}