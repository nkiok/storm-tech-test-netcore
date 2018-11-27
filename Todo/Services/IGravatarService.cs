using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services
{
    public interface IGravatarService
    {
        Task<ProfileInfo> GetProfileInfo(string profileIdentifier);
    }
}
