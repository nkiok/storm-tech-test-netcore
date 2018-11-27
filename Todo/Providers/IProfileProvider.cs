using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Todo.Providers
{
    public interface IProfileProvider
    {
        Task<Result<string>> GetImageUrl(string profileIdentifier);

        Task<Result<string>> GetDisplayName(string profileIdentifier);
    }
}
