using System.IO;

namespace BE.ModelosIII.Infrastructure.ApplicationServices
{
    public class FileSystem : IFileSystem
    {
        public void Save(string path, byte[] bytes)
        {
            File.WriteAllBytes(path, bytes);
        }

        public void Delete(string path)
        {
            File.Delete(path);
        }

        public byte[] Load(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}
