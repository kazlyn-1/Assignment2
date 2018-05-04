using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Xml.Linq;

namespace FullContactCSharp
{
    public class FullContactPerson
    {
        public double? Likelihood { get; private set; }

        /// <summary>Gest the contact info for this person.</summary>
        public ContactInfo ContactInfo { get; private set; }

        /// <summary>Gets the social profiles for this person.</summary>
        public List<SocialProfile> SocialProfiles { get; private set; }

        /// <summary>Constructor.</summary>
        /// <param name="element">element to be parsed.</param>
        /// <returns>FullContactperson object.</returns>
        internal static FullContactPerson FromXml(XElement element)
        {
            if (element == null)
                return null;

            FullContactPerson person = new FullContactPerson();

            XElement elmLikelihood = element.Element("likelihood");
            person.Likelihood = elmLikelihood != null ? double.Parse(elmLikelihood.Value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture) : 0;

            XElement elmContactInfo = element.Element("contactInfo");
            if (elmContactInfo != null)
            {
                ContactInfo contactInfo = new ContactInfo(elmContactInfo);
                person.ContactInfo = contactInfo;
            }

            XElement elmSocialProfiles = element.Element("socialProfiles");
            if (elmSocialProfiles != null)
            {
                var socialProfilesElements = elmSocialProfiles.Elements();
                person.SocialProfiles = socialProfilesElements.Select(sp => new SocialProfile(sp)).ToList();
            }

            return person;
        }

        public override string ToString()
        {
            return string.Format("Likelihood: {0:P}\n{1}\n\nSocial Profiles:\n{2}", 
                Likelihood, 
                ContactInfo != null ? ContactInfo.ToString() : "", 
                SocialProfiles != null ? string.Join("\n", SocialProfiles) : ""
                );
        }
    }

    public class ContactInfo
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="element">The element to be parsed.</param>
        internal ContactInfo(XElement element)
        {
            XElement elmFamilyName = element.Elements().FirstOrDefault(e => e.Name == "familyName");
            if (elmFamilyName != null)
                FamilyName = elmFamilyName.Value;

            XElement elmFullName = element.Elements().FirstOrDefault(e => e.Name == "fullName");
            if (elmFullName != null)
                FullName = elmFullName.Value;

            XElement elmGivenName = element.Elements().FirstOrDefault(e => e.Name == "givenName");
            if (elmGivenName != null)
                GivenName = elmGivenName.Value;

            XElement elmWebsites = element.Elements().FirstOrDefault(e => e.Name == "websites");
            if (elmWebsites != null)
            {
                var websites = elmWebsites.Elements("website");
                Websites = websites.Elements("url").Select(elm => elm.Value).ToList();
            }
        }

        /// <summary>Gets the websites for this ContactInfo object.</summary>
        public List<string> Websites { get; }

        /// <summary>Gets the family name.</summary>
        public string FamilyName { get; }

        /// <summary>Gets the full name.</summary>
        public string FullName { get; }

        /// <summary>Gets the given name.</summary>
        public string GivenName { get; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("FamilyName: {0}, Given Name: {1}, Full Name: {2}\n", 
                FamilyName,
                GivenName,
                FullName);

            if (Websites != null && Websites.Count != 0)
            {
                sb.AppendLine("Websites:");
                sb.AppendJoin("\n", Websites);
            }

            return sb.ToString();
        }
    }

    public class SocialProfile
    {
        internal SocialProfile(XElement element)
        {
            XElement elmType = element.Elements().FirstOrDefault(e => e.Name == "type");
            if (elmType != null)
                Type = elmType.Value;

            XElement elmTypeId = element.Elements().FirstOrDefault(e => e.Name == "typeId");
            if (elmTypeId != null)
                TypeId = elmTypeId.Value;

            XElement elmTypeName = element.Elements().FirstOrDefault(e => e.Name == "typeName");
            if (elmTypeName != null)
                TypeName = elmTypeName.Value;

            XElement elmUrl = element.Elements().FirstOrDefault(e => e.Name == "url");
            if (elmUrl != null)
                Url = elmUrl.Value;

            XElement elmBio = element.Elements().FirstOrDefault(e => e.Name == "bio");
            if (elmBio != null)
                Bio = elmBio.Value;

            XElement elmUsername = element.Elements().FirstOrDefault(e => e.Name == "username");
            if (elmUsername != null)
                UserName = elmUsername.Value;
        }
        
        /// <summary>Gets the type.</summary>
        public string Type { get; }

        /// <summary>Gets the type id.</summary>
        public string TypeId { get; }

        /// <summary>Gets the type name. 'FriendlyName'</summary>
        public string TypeName { get; }

        /// <summary>Gets the bio.</summary>
        public string Bio { get; }

        /// <summary>Gets the url.</summary>
        public string Url { get; }

        /// <summary>Gets the username.</summary>
        public string UserName { get; }

        private PropertyInfo[] _pi;

        public override string ToString()
        {
            _pi = GetType().GetProperties();
            var sb = new StringBuilder();
            
            foreach (var info in _pi)
            {
                var value = info.GetValue(this, null);
                if (value != null)
                    sb.AppendLine(info.Name + ": " + value);
            }

            return sb.ToString();
        }
    }

}
