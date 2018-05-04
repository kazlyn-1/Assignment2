using System.Threading.Tasks;

namespace FullContactCSharp
{
    interface IFullContactApi
    {
        Task<FullContactPerson> LookupPersonByEmailAsync(string email);
    }
}
