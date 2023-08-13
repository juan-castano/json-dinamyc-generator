namespace ObjectStorageApp.Storage
{
    public interface IStorage 
    {
        public Task Create(string storageName);
        public Task Create(string[] storageNames);
        public Task Delete(string storageName);
        public Task Delete(string[] storageNames);
    }
}