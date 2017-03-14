using System;
using MyMis.Scripting.Core.Contracts;
using MyMis.Scripting.PlatformApi;
using MyMis.Scripting.PlatformApi.Models;
using MyMis.Scripting.ConnectorClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

/*
Developed by: NumbersBelieve
Function: Creates an instance of an agent Employee and a new user associated to that agent.
Parameters: A tenant with an Employee agent and a domain role named APPROVER.
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

            //Connection and authentication in the API
            ApiClient api = new ApiClient(context.Api.Endpoint, context.Tenant);
            api.Authenticate(context.Api.Email, context.Api.Password);

            //Structure to create an employee
            EntityModel newEmployee = new EntityModel();

            newEmployee.Operation = "SAVE";
            newEmployee.EntityTypeCode = "Employee";
            newEmployee.MisEntityKind = EntityKind.Agent;

            newEmployee.Code = "E002";
            newEmployee.Name = "Employee Name";

            //Non-base attributes must be sent in the dictionary of Attributes.
            newEmployee.Attributes.Add("Company", "C001");

            //Sending the agent creation request
            var createResult = api.Entity.Create(newEmployee);

            //Evaluating the status of the request
            var result = GetStatus(api, createResult.Operation);

            if (result == false)
                throw new Exception("Could not obtain the status of the employee creation request before timing out. Please try again.");

            //Obtaining the roles in the tenant and selecting one
            var accountRoles = api.Role.GetRoles();
            var appRole = accountRoles.Where(role => role.Code.Equals("APPROVER")).FirstOrDefault();

            if (appRole == null)
                throw new Exception("Could not find a role with code APPROVER.");

            //Structure to create a user
            UserModel newUser = new UserModel();
            newUser.Name = "Employee Name";
            newUser.Email = "email001@example.com";
            newUser.ContactEmail = "email001@example.com";

            newUser.Password = "Omnia2016";
            newUser.Agents.Add(new UserAgent() { EntityTypeCode = "Employee", Code = "E001" });

            newUser.Roles.Add(new UserRole() {ID = appRole.ID });
			
            newUser.Language = "en-US";

            //Sending the user creation request
            var createUserResult = api.User.Create(newUser);

            //Evaluating the status of the request
            var userResult = GetStatus(api, createUserResult.Operation);

            if (userResult == false)
                throw new Exception("Could not obtain the status of the user creation request before timing out. Please try again.");



            /* **************************************** */
            /* **************************************** */

            return new ScriptResponse
            {
                Message = "Agent and user created successfully"
            };
        }

        public bool GetStatus(ApiClient apiClient, string operation)
        {
			//Increase to increase maximum allowed time before operation is marked as having failed.
            int retrys = 5;

            while (retrys > 0)
            {
                string[] operations = new string[] { operation};
                var operationsStatus = apiClient.Operation.TrackOperations(operations);


                foreach (var operationStatus in operationsStatus)
                {

                    if (operationStatus.OperationStatus.Equals(OperationStatus.Completed))
                    {
                        return true;

                    }
                    else if (operationStatus.OperationStatus.Equals(OperationStatus.Error))
                    {

                        throw new Exception("Error in operation: " + operationStatus.OperationStatusMessage);
                    }
                    else {
                        //Operation not yet finished
                        Thread.Sleep(2000);
                    }

                }


                retrys--;
            }
            return false;
        }
    }
}