using System.IO.Compression;
using Backups.Entities.RepositoryObjects;
using Backups.Exceptions;
using Backups.Models.Paths;

namespace Backups.Entities.ArchiveObjects;

public class ZipFile : IZipObject
{
    public ZipFile(IPath path)
    {
        Path = path;
    }

    public IPath Path { get; }

    public IRepositoryObject GetRepositoryObject(ZipArchive archive)
    {
        ZipArchiveEntry entry = archive.GetEntry(Path.ToString()) ?? throw ZipFileException.EntryNotFound(Path);
        return new RepositoryFile(Path, GetReadStreamer(entry));
    }

    private Func<Stream> GetReadStreamer(ZipArchiveEntry entry)
    {
        Stream ReadStreamer() => entry.Open();
        return ReadStreamer;
    }
}