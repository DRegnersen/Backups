using Backups.Entities.RepositoryObjects;
using Backups.Exceptions;
using Backups.Models.Paths;

namespace Backups.Entities.Repositories;

public class FileSystemRepository : IRepository
{
    private const string AllFilesSearchPattern = "*";

    public Stream OpenWriteStream(IPath path)
    {
        Directory.CreateDirectory(path.GetDirectory().ToString());
        return File.OpenWrite(path.ToString());
    }

    public Stream OpenReadStream(IPath path)
    {
        return File.OpenRead(path.ToString());
    }

    public void Delete(IPath path)
    {
        if (File.Exists(path.ToString()))
        {
            File.Delete(path.ToString());
        }
        else if (Directory.Exists(path.ToString()))
        {
            Directory.Delete(path.ToString(), true);
        }
        else
        {
            throw FileSystemRepositoryException.ObjectNotFound(path);
        }
    }

    public IRepositoryObject GetObject(IPath path)
    {
        if (Directory.Exists(path.ToString()))
        {
            return new RepositoryFolder(path, GetFilesProvider(path));
        }

        if (!File.Exists(path.ToString()))
        {
            throw FileSystemRepositoryException.ObjectNotFound(path);
        }

        return new RepositoryFile(path, GetReadStreamer(path));
    }

    private Func<Stream> GetReadStreamer(IPath path)
    {
        Stream ReadStreamer() => OpenReadStream(path);
        return ReadStreamer;
    }

    private Func<IReadOnlyCollection<IRepositoryFile>> GetFilesProvider(IPath path)
    {
        IReadOnlyCollection<IRepositoryFile> FilesProvider() => GetAllFilesInDirectory(path)
            .Select(filepath => new RepositoryFile(filepath, GetReadStreamer(filepath))).ToList().AsReadOnly();

        return FilesProvider;
    }

    private IEnumerable<IPath> GetAllFilesInDirectory(IPath path)
    {
        return Directory.GetFiles(path.ToString(), AllFilesSearchPattern, SearchOption.AllDirectories)
            .Select(strPath => new UPathAdapter(strPath));
    }
}