using System.IO.Compression;
using Backups.Entities.RepositoryObjects;
using Backups.Models.Paths;

namespace Backups.Entities.ArchiveObjects;

public interface IZipObject
{
    IPath Path { get; }

    IRepositoryObject GetRepositoryObject(ZipArchive archive);
}