using Backups.Entities.RepositoryObjects;
using Backups.Exceptions;
using Backups.Models.Paths;
using Zio;
using Zio.FileSystems;

namespace Backups.Entities.Repositories;

public class InMemoryRepository : IRepository
{
    private const string AllFilesSearchPattern = "*";

    private readonly MemoryFileSystem _fileSystem;

    public InMemoryRepository(MemoryFileSystem fileSystem)
    {
        ArgumentNullException.ThrowIfNull(fileSystem);
        _fileSystem = fileSystem;
    }

    public Stream OpenWriteStream(IPath path)
    {
        _fileSystem.CreateDirectory(path.GetDirectory().ToString());
        return _fileSystem.OpenFile(path.ToString(), FileMode.Create, FileAccess.Write, FileShare.Write);
    }

    public Stream OpenReadStream(IPath path)
    {
        return _fileSystem.OpenFile(path.ToString(), FileMode.Open, FileAccess.Read, FileShare.Read);
    }

    public void Delete(IPath path)
    {
        if (_fileSystem.FileExists(path.ToString()))
        {
            _fileSystem.DeleteFile(path.ToString());
        }
        else if (_fileSystem.DirectoryExists(path.ToString()))
        {
            _fileSystem.DeleteDirectory(path.ToString(), true);
        }
        else
        {
            throw InMemoryRepositoryException.ObjectNotFound(path);
        }
    }

    public IRepositoryObject GetObject(IPath path)
    {
        if (_fileSystem.DirectoryExists(path.ToString()))
        {
            return new RepositoryFolder(path, GetFilesProvider(path));
        }

        if (!_fileSystem.FileExists(path.ToString()))
        {
            throw InMemoryRepositoryException.ObjectNotFound(path);
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
        return _fileSystem.EnumerateFiles(path.ToString(), AllFilesSearchPattern, SearchOption.AllDirectories)
            .Select(uPath => new UPathAdapter(uPath));
    }
}