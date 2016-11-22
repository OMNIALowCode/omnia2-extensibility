//#R System.Net.Http.dll
//#R System.Web.Extensions.dll
using MyMis.Scripting.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Script.Serialization;

/*
Developed by: NumbersBelieve
Function: Obtains data from an external API and fills a list with it. Used as the query script in an External Entity, set to run as Local.
Parameters: n/a
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
            HttpResponseMessage response = client.GetAsync($"http://swapi.co/api/people/").Result;
            response.EnsureSuccessStatusCode();

            string responseBody = response.Content.ReadAsStringAsync().Result;

            var entity = new JavaScriptSerializer().Deserialize<Rootobject>(responseBody);

            var queryResult = new QueryResult()
            {
                Headers = new string[] { "Name", "Uri" },
                NumberOfRecords = entity.count
            };

            var data = new object[entity.results.Length, 2];
            for (int i = 0; i < entity.results.Length; i++)
            {
                data[i, 0] = entity.results[i].name;
                data[i, 1] = entity.results[i].url;
            }

            queryResult.Data = data;

            /* **************************************** */
            /* **************************************** */

            return new ScriptResponse
            {
                Message = $"Updated",
                Result = queryResult

            };
        }
    }

    public class Rootobject
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public Result[] results { get; set; }
    }

    public class Result
    {
        public string name { get; set; }
        public string url { get; set; }
    }



}