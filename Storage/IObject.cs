namespace ObjectStorageApp.Storage
{
    public interface IObject
    {
        public Task Upload(string path);
        public Task Upload(string[] paths);
        public Task Download(string path);
        public Task Download(string[] paths);
    }
}