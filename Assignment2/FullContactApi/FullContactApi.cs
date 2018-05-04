using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FullContactCSharp
{
    public class FullContactApi  : IFullContactApi
    {
        private readonly HttpClient _client;

        public FullContactApi(string apiKey)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://api.fullcontact.com")
            };
            _client.DefaultRequestHeaders.Add("X-FullContact-APIKey", apiKey);
        }

        //public Task<FullContactPerson> LookupPersonByEmailAsync(string email)
        public async Task<FullContactPerson> LookupPersonByEmailAsync(string email)
        {
            var resp = await _client.GetAsync($"v2/person.xml?email={email}");
            if (!resp.IsSuccessStatusCode)
                return null;

            string respString = await resp.Content.ReadAsStringAsync();
            XElement rootElm = XElement.Parse(respString);
            return FullContactPerson.FromXml(rootElm);
        }
    }
}
