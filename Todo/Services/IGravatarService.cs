using System.Threading.Tasks;

namespace Todo.Services
{
    public interface IGravatarService
    {
        string GetImgUrl(string emailAddress);

        Task<string> GetProfileDisplayName(string emailAddress);
    }
}
