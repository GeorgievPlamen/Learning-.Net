namespace HR.LeaveManagement.MVC.Contracts
{
    public interface ILocalStorageService
    {
        void ClearStorate(List<string> keys);
        bool Exists(string key);
        T GetStorageValue<T>(string key);
        void SetStorageValue<T>(string key, T value);
    }
}
