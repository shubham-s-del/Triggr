using System;

namespace Triggr.Infrastructure
{
    // it should be an abstract class
    public class ScriptStorage : IStorage
    {
        private string _path;
        public string Path => _path;
        public ScriptStorage()
        {

        }
        public ScriptStorage(string path) : this(path, true)
        {
        }

        public ScriptStorage(string path, bool environmentPath)
        {
            Set(path, environmentPath);
        }
        public void Set(string path)
        {
            Set(path, true);
        }

        public void Set(string path, bool environmentPath)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path can't be empty.", nameof(path));
            }
            
            _path = environmentPath ? System.IO.Path.Combine(Environment.CurrentDirectory, path) : path;
        }
        public virtual string Combine(string path)
        {
            return System.IO.Path.Combine(_path, path);
        }

        public virtual string Combine(string path1, string path2)
        {
            return System.IO.Path.Combine(_path, path1, path2);
        }
        public virtual string Combine(string path1, string path2, string path3)
        {
            return System.IO.Path.Combine(_path, path1, path2, path3);
        }
        public virtual string Combine(string path1, string path2, string path3, string path4)
        {
            return System.IO.Path.Combine(_path, path1, path2, path3, path4);
        }
    }
}