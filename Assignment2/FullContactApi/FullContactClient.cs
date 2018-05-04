using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using RestSharp;

namespace FullContactCSharp
{
    public class FullContactClient  : IFullContactApi
    {
        private readonly RestClient _client;

        public FullContactClient(string apiKey)
        {
            _client = new RestClient("https://api.fullcontact.com");
            _client.AddDefaultHeader("X-FullContact-APIKey", apiKey);
            
        }

        //public Task<FullContactPerson> LookupPersonByEmailAsync(string email)
        public async Task<FullContactPerson> LookupPersonByEmailAsync(string email)
        {
            var request = new RestRequest("v2/person.xml?email={email}", Method.POST);
            request.AddUrlSegment("email", email);

            IRestResponse<FullContactPerson> personRestResponse = await _client.ExecuteTaskAsync<FullContactPerson>(request);
            if (personRestResponse.IsSuccessful)
                return personRestResponse.Data;

            return null;
            //XElement rootElm = XElement.Parse(content);
            //return FullContactPerson.FromXml(rootElm);
        }
    }
}
