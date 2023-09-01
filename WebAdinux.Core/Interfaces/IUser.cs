using WebAdinux.Context.Entities;
using WebAdinux.Core.ViewMoels;

namespace WebAdinux.Core.Interfaces
{
    public interface IUser
    {
        Task<User?> FindUser(LoginViewModel viewModel);
    }
}
