using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
            string requestUri = $"v2/person.xml?email={email}";

            var resp = await _client.GetAsync(requestUri);
            if (resp.IsSuccessStatusCode)
            {
                string respString = await resp.Content.ReadAsStringAsync();
                XElement rootElm = XElement.Parse(respString);
                return FullContactPerson.FromXml(rootElm);
            }

            return null;
        }
    }
}
