using MyMis.Scripting.Core.Contracts;
using MyMis.Scripting.PlatformApi;
using System.Collections.Generic;
using System.IO;
using System.Linq;


/*
Developed by: NumbersBelieve
Function: Script that is executed afte saving an entity with a "myFile" attribute of type file, that obtains the file in that case and saves it to the machine where the connector is running.
Parameters: Entity with a myFile attribute of type File.
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

            ApiClient api = new ApiClient(context.Api.Endpoint, context.Tenant);
            api.Authenticate(context.Api.Email, context.Api.Password);
            
            var stream = api.BinaryData.GetFile(document.Attributes.myFile);
            string filepath = "C:\\temp\\" + document.Attributes.myFile.Split('/').LastOrDefault();
            var fileStream = File.Create(filepath, (int)stream.Length);

            byte[] bytesInStream = new byte[stream.Length];
            stream.Read(bytesInStream, 0, bytesInStream.Length);
            fileStream.Write(bytesInStream, 0, bytesInStream.Length);

            /* **************************************** */
            /* **************************************** */

            return new ScriptResponse
            {
                Message = $"File saved successfully to {filepath}"
            };
        }
    }
}