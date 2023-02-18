namespace Backups.Models.Paths;

public interface IPath
{
    IPath GetDirectory();

    string GetName();

    IPath Join(IPath path);

    IPath Join(string path);

    IPath ToRelative(IPath parentPath);

    IPath WithExtension(string extension);

    IPath WithoutExtension();

    string ToString();
}