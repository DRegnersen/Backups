using System.IO.Compression;
using Backups.Entities.RepositoryObjects;
using Backups.Exceptions;
using Backups.Models.Paths;

namespace Backups.Entities.ArchiveObjects;

public class ZipFolder : IZipObject
{
    private readonly IReadOnlyCollection<ZipFile> _files;

    public ZipFolder(IPath path, IReadOnlyCollection<ZipFile> files)
    {
        Path = path;
        _files = files;
    }

    public IPath Path { get; }

    public IRepositoryObject GetRepositoryObject(ZipArchive archive)
    {
        return new RepositoryFolder(Path, GetFilesProvider(archive));
    }

    private Func<Stream> GetReadStreamer(ZipArchiveEntry entry)
    {
        Stream ReadStreamer() => entry.Open();
        return ReadStreamer;
    }

    private Func<IReadOnlyCollection<IRepositoryFile>> GetFilesProvider(ZipArchive archive)
    {
        IReadOnlyCollection<IRepositoryFile> FilesProvider() => GetRepositoryFiles(archive);
        return FilesProvider;
    }

    private IReadOnlyCollection<IRepositoryFile> GetRepositoryFiles(ZipArchive archive)
    {
        var repositoryFiles = new List<IRepositoryFile>();

        foreach (ZipFile file in _files)
        {
            ZipArchiveEntry entry = archive.GetEntry(file.Path.ToString()) ??
                                    throw ZipFolderException.EntryNotFound(file.Path);
            repositoryFiles.Add(new RepositoryFile(file.Path, GetReadStreamer(entry)));
        }

        return repositoryFiles.AsReadOnly();
    }
}