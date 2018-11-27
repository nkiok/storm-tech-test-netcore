using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services
{
    public interface IGravatarService
    {
        Task<string> GetProfileImageUrl(string emailAddress);

        Task<string> GetProfileDisplayName(string emailAddress);

        Task<ProfileInfo> GetProfileInfo(string emailAddress);
    }
}
