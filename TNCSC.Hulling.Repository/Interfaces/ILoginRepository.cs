#region References
using TNCSC.Hulling.Domain;
#endregion

namespace TNCSC.Hulling.Repository.Interfaces
{
    /// <summary>
    /// ILoginRepository
    /// </summary>
    public interface ILoginRepository
    {
        public Task<User> ValidateUser(string userId, string password);
    }
}
