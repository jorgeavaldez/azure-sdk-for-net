using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Management.MachineLearning.WebServices;
using Microsoft.Azure.Management.MachineLearning.Studio.WebService;
using Microsoft.Azure.Management.MachineLearning.WebServices.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using Microsoft.Rest.Azure;
using Microsoft.Rest.Serialization;
using Newtonsoft.Json;

namespace Microsoft.Azure.MachineLearning
{
    public class WebServiceManagementClient
    {
        private const string MANAGEMENT_URL = "https://management.azure.com";
        private AzureMLWebServicesManagementClient _client;
        private StudioServiceClient StudioWebServiceClient;

        /// <summary>
        /// Empty constructor. Takes user credentials from the authentication session and uses them to authenticate the internal client.
        /// </summary>
        /// <param name="userCredentials">A users authenticated credentials.</param>
        public WebServiceManagementClient(ServiceClientCredentials userCredentials)
        {
            _client = new AzureMLWebServicesManagementClient(userCredentials);
            StudioWebServiceClient = new StudioServiceClient(userCredentials);
        }

        /// <summary>
        /// WebServiceManagementClient constructor. Takes a subscription id.
        /// </summary>
        /// <param name="subscriptionId">The id for the user's subscription.</param>
        public WebServiceManagementClient(string subscriptionId, ServiceClientCredentials userCredentials)
        {
            _client = new AzureMLWebServicesManagementClient(userCredentials);
            StudioWebServiceClient = new StudioServiceClient(userCredentials);

            _client.SubscriptionId = subscriptionId;
        }

        /// <summary>
        /// Creates a web service object from a web service definition file. Also takes the name of the resource group.
        /// </summary>
        /// <param name="webServiceDefintionFilePath">The file path for the web service definition file. Expecting a JSON string within.</param>
        /// <param name="resourceGroup">The resource group in which the web service is deployed.</param>
        /// <returns></returns>
        public WebService CreateWebServiceObject(string webServiceDefintionFilePath, string resourceGroup)
        {
            var ws = new WebService(webServiceDefintionFilePath, resourceGroup,_client);
            return ws;
        }

        /// <summary>
        /// Creates a web service object from an AutoRest generated WebService model. Also takes the name of the resource group.
        /// </summary>
        /// <param name="webService">The other web service from which we are creating a web service object. Serves as the object Definition internal state object.</param>
        /// <param name="resourceGroup">The resource group in which the web service is deployed.</param>
        /// <returns></returns>
        public WebService CreateWebServiceObject(
            Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService webService,
            string resourceGroup)
        {
            var nWebService = new WebService(webService, resourceGroup,this._client);
            return nWebService;
        }

        /// <summary>
        /// Deploys the given web service in the resource group it specifies.
        /// </summary>
        /// <param name="webService">The web service to be deployed.</param>
        /// <param name="force">Optional: If a web service with the same name exists, just go ahead and override it if true. Otherwise, throw an exception.</param>
        public void DeployWebService(WebService webService, bool force = false)
        {
            if (!force)
            {
                try
                {
                    // If this throws an error, then it doesn't exist.
                    var existingWebService = this._client.WebServices.Get(webService.ResourceGroupName, webService.Title);

                    throw new InvalidOperationException("force flag was set to false; a web service in the resource group " + 
                        webService.ResourceGroupName + " with the name " + webService.Title + " already exists.");
                }

                catch (Exception)
                {
                    this._client.WebServices.CreateOrUpdate(webService.Definition, webService.ResourceGroupName,
                        webService.Title);

                    webService.Keys = this._client.WebServices.ListKeys(webService.ResourceGroupName, webService.Title);
                }
            }

            else
            {
                this._client.WebServices.CreateOrUpdate(webService.Definition, webService.ResourceGroupName,
                    webService.Title);

                webService.Keys = this._client.WebServices.ListKeys(webService.ResourceGroupName, webService.Title);
            }
        }

        /// <summary>
        /// Returns a web service from a specified resource group with the specified name.
        /// </summary>
        /// <param name="resourceGroupName">The resource group from which to get the web service.</param>
        /// <param name="webServiceName">The name of the web service to get.</param>
        /// <returns>The web service.</returns>
        public WebService GetWebServiceFromResourceGroup(string resourceGroupName, string webServiceName)
        {
            // Get the definition web service from the inner client
            var webServiceDefinition = this._client.WebServices.Get(resourceGroupName, webServiceName);

            // Create a new web service object and return it
            return new WebService(webServiceDefinition, resourceGroupName, this._client);
        }

        /// <summary>
        /// Returns a web service object built from an experiment.
        /// </summary>
        /// <param name="workspaceId">The workspace ID of the experiment.</param>
        /// <param name="experimentId">The experiment's ID.</param>
        /// <returns>The web service itself.</returns>
        public WebService GetWebServiceFromExperiment(string workspaceId, string experimentId)
        {
            // TODO: Fill in missing info from provided user stuff.

            var subscriptionId = this._client.SubscriptionId;

            Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService studioDirectWebService = StudioWebServiceClient.Get(workspaceId, experimentId);

            Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebServiceProperties editedStudioDirectWebServiceProperties = new WebServiceProperties(
                studioDirectWebService.Properties.Title,
                studioDirectWebService.Properties.Description,
                studioDirectWebService.Properties.CreatedOn,
                studioDirectWebService.Properties.ModifiedOn,
                "Unknown", // ProvisioningState must be set to unknown here
                studioDirectWebService.Properties.Keys,
                studioDirectWebService.Properties.ReadOnlyProperty,
                studioDirectWebService.Properties.SwaggerLocation,
                studioDirectWebService.Properties.ExposeSampleData,
                studioDirectWebService.Properties.RealtimeConfiguration,
                studioDirectWebService.Properties.Diagnostics,
                studioDirectWebService.Properties.StorageAccount,
                studioDirectWebService.Properties.MachineLearningWorkspace,
                studioDirectWebService.Properties.CommitmentPlan,
                studioDirectWebService.Properties.Input,
                studioDirectWebService.Properties.Output,
                studioDirectWebService.Properties.ExampleRequest,
                studioDirectWebService.Properties.Assets,
                studioDirectWebService.Properties.Parameters);

            Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService editedStudioDirectWebService = new Management.MachineLearning.WebServices.Models.WebService(
                studioDirectWebService.Location,
                editedStudioDirectWebServiceProperties,
                studioDirectWebService.Id,
                studioDirectWebService.Name,
                studioDirectWebService.Type,
                studioDirectWebService.Tags);

            var actualWebService = new WebService(editedStudioDirectWebService, "", this._client);

            return actualWebService;
        }

        /// <summary>
        /// Returns a list of web service objects within a resource group.
        /// </summary>
        /// <param name="resourceGroupName">The resource group from which to retrieve the list of web services.</param>
        /// <returns>A list of web services from the specified resource group.</returns>
        public List<WebService> ListWebServicesFromResourceGroup(string resourceGroupName)
        {
            var webServices = this._client.WebServices.ListInResourceGroup(resourceGroupName).Value;
            var webServiceObjects = new List<WebService>();

            foreach (Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService ws in webServices)
            {
                webServiceObjects.Add(new WebService(ws, resourceGroupName, this._client));
            }

            return webServiceObjects;
        }

        /// <summary>
        /// Returns a list of web service objects within the client subscription.
        /// </summary>
        /// <returns>A list of web services from the client subscription.</returns>
        public List<WebService> ListWebServicesFromSubscription()
        {
            var webServices = this._client.WebServices.List().Value;
            var webServiceObjects = new List<WebService>();

            foreach (Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService ws in webServices)
            {
                // TODO: Get resource group from web service ID.
                var resourceGroup = this._client.WebServices.Get("", "");

                // TEST

                webServiceObjects.Add(new WebService(ws, "", this._client));
            }

            return webServiceObjects;
        }

        

    }
}
