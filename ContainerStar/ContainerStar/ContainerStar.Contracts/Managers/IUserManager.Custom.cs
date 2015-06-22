using ContainerStar.Contracts.Entities;

namespace ContainerStar.Contracts.Managers
{
    /// <summary>
    /// </summary>
    public partial interface IUserManager
    {
        /// <summary>
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        User GetByLogin(string userLogin);
    }
}