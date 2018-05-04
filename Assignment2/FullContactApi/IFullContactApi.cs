using System.Threading.Tasks;

namespace FullContactApi
{
    interface IFullContactApi
    {
        Task<FullContactPerson> LookupPersonByEmailAsync(string email);
    }
}
