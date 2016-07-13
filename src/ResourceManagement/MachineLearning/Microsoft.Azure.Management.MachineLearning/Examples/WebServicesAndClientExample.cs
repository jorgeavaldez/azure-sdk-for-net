using System;
using System.Threading;

// Used for authentication
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using Microsoft.Rest.Azure.Authentication;

// The new SDK
using Microsoft.Azure.MachineLearning;

//*
namespace Microsoft.Azure.MachineLearning.Examples
{
    public class WebServicesAndClientExample
    {
        public static void Main(string[] args)
        {
            
        }

        public static void UpdateWebServiceExample()
        {
            // User authentication
            // _cache makes it so tokens can be stored and renewed when they expire
            // settings holds parameters to call the Azure active directory
            var _cache = new TokenCache();
            var settings = ActiveDirectoryServiceSettings.Azure;

            // Sets a synchronization model context that allows the pop up window to run in a new thread
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

            // make sure previous credentials are cleared from the cache
            _cache.Clear();

            // Store the credentials coming from the library method LoginWithPromptAsync
            // The first parameter is domain
            // Second is a method that takes the application id and a redirect uri (these are hardcoded for the powershell client)
            // then the settings object and a reference to the cache we made
            // we want to wait for this to finish so we call GetAwaiter and then GetResult
            ServiceClientCredentials cred =
                    UserTokenProvider.LoginWithPromptAsync("microsoft.com",
                        ActiveDirectoryClientSettings.UsePromptOnly("1950a258-227b-4e31-a9cf-717495945fc2",
                            new Uri("urn:ietf:wg:oauth:2.0:oob")), settings, _cache)
                        .GetAwaiter()
                        .GetResult();

            // We need to make a management client now and pass it our credentials so it can authenticate
            // note that credentials are not saved except for in the cache
            var managementClient = new WebServiceManagementClient("YOUR-SUBSCRIPTION-ID", cred);

            // now we use the management client to get an existing deployed web service
            var webService = managementClient.GetWebServiceFromResourceGroup("YOUR-RESOURCE-GROUP-NAME", "YOUR-WEB-SERVICE-NAME");

            Console.WriteLine("Created web service: " + webService.Title);

            // Notice we're getting a web service from an experiment now
            var updatedWebService = managementClient.CreateWebServiceObject("webservicedefinitionfile.json", "JorgeResourceGroup");

            // webService is now updated using the definition from updatedWebService
            webService.Update(updatedWebService);

            // now we use the management client to create a web service from an experiment
            webService = managementClient.GetWebServiceFromExperiment("YOUR-WORKSPACE-ID", "YOUR-EXPERIMENT-ID");

            // Now we deploy the experiment web service
            managementClient.DeployWebService(webService);

            // We can grab the newly deployed web service now...
            webService = managementClient.GetWebServiceFromResourceGroup("YOUR-RESOURCE-GROUP-NAME", "YOUR-WEB-SERVICE-NAME");

            // ... and delete it!
            webService.Delete();

            // Now we're going to get a list of web services from the resource group
            var webServices = managementClient.ListWebServicesFromResourceGroup("YOUR-RESOURCE-GROUP-NAME");

            // And we're going to print the titles out
            Console.WriteLine("Web services in resource group:\n");

            foreach (var ws in webServices)
            {
                Console.WriteLine(ws.Title);
            }

            // Now we're going to get a list of web services from the current client's subscription
            webServices = managementClient.ListWebServicesFromSubscription();

            // And we're going to delete them all!
            foreach(var ws in webServices)
            {
                Console.WriteLine("Deleted web service " + ws.Title);
                ws.Delete();
            }
        }
    }
}
//*/