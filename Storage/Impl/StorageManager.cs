using ObjectStorageApp.Authentication;

namespace ObjectStorageApp.Storage.Impl
{
  internal class StorageManager : IStorage
  {
    private readonly IAuthenticate authenticate;
    internal StorageManager(IAuthenticate authenticate)
    {
        this.authenticate = authenticate;
    }

    public async Task Create(string storageName)
    {
      await this.authenticate.Login("", "", "");
    }

    public Task Create(string[] storageNames)
    {
      throw new NotImplementedException();
    }

    public Task Delete(string storageName)
    {
      throw new NotImplementedException();
    }

    public Task Delete(string[] storageNames)
    {
      throw new NotImplementedException();
    }
  }
}