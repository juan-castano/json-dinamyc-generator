namespace ObjectStorageApp.Authentication
{
    public interface IAuthenticate
    {
        public Task Login(string name, string password, string bucket);
        public Task RefreshToken(string name, string password, string bucket);
    }
}