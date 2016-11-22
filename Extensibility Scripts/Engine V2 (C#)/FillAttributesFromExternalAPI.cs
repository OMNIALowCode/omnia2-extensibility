//#R System.Net.Http.dll
//#R System.Web.Extensions.dll
using MyMis.Scripting.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Script.Serialization;

/*
Developed by: NumbersBelieve
Function: Obtains data from an external API and fills document parameters with it. Should be executed on an "On Change" action on the document's Code.
Parameters: An Agent entity with a Gender attribute. The Code of the entity should match a number on the swapi site.
*/
namespace myMIS
{
    public class Script
    {
        public ScriptResponse Execute(ContextData context, Entity document, Dictionary<string, object> parameters)
        {
            /* **************************************** */
            /* **************************************** */
            /*          ADD YOUR CODE HERE              */


            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync($"http://swapi.co/api/people/{document.Code}/").Result;
            response.EnsureSuccessStatusCode();

            string responseBody = response.Content.ReadAsStringAsync().Result;

            var entity = new JavaScriptSerializer().Deserialize<PersonEntity>(responseBody);

            document.Name = entity.name;
            document.Attributes.Gender = entity.gender;

            document.MarkToApplyChanges();

            /* **************************************** */
            /* **************************************** */

            return new ScriptResponse
            {
                Message = $"Updated"
            };
        }
    }

    public class PersonEntity
    {
        public string name { get; set; }
        public string height { get; set; }
        public string mass { get; set; }
        public string hair_color { get; set; }
        public string skin_color { get; set; }
        public string eye_color { get; set; }
        public string birth_year { get; set; }
        public string gender { get; set; }
        public string homeworld { get; set; }
        public DateTime created { get; set; }
        public DateTime edited { get; set; }
        public string url { get; set; }
    }

}