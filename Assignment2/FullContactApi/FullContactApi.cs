using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FullContactApi
{
    public class FullContactApi : IFullContactApi
    {
        private string _apiKey = "VY8Vi17Gx5K8U2h7k0dEK0eAHDa61LTm";

        public Task<FullContactPerson> LookupPersonByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
