using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.Management.MachineLearning.WebServices;
using Microsoft.Azure.Management.MachineLearning.Studio.WebService;
using Microsoft.Azure.Management.MachineLearning.WebServices.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;

namespace Microsoft.Azure.MachineLearning
{
    public class WebServiceManagementClient
    {
        private const string MANAGEMENT_URL = "https://management.azure.com";
        private AzureMLWebServicesManagementClient _client;
        private StudioWebService StudioWebServiceClient;

        /// <summary>
        /// Empty constructor. Takes user credentials from the authentication session and uses them to authenticate the internal client.
        /// </summary>
        /// <param name="userCredentials">A users authenticated credentials.</param>
        public WebServiceManagementClient(ServiceClientCredentials userCredentials)
        {
            _client = new AzureMLWebServicesManagementClient(userCredentials);
            StudioWebServiceClient = new StudioWebService(userCredentials);
        }

        /// <summary>
        /// WebServiceManagementClient constructor. Takes a subscription id.
        /// </summary>
        /// <param name="subscriptionId">The id for the user's subscription.</param>
        public WebServiceManagementClient(string subscriptionId, ServiceClientCredentials userCredentials)
        {
            _client = new AzureMLWebServicesManagementClient(userCredentials);
            StudioWebServiceClient = new StudioWebService(userCredentials);

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
        public void DeployWebService(WebService webService)
        {
            this._client.WebServices.CreateOrUpdate(webService.Definition, webService.ResourceGroupName,
                webService.Title);
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

            // Web service from the studio api
            Microsoft.Azure.Management.MachineLearning.Studio.WebService.Models.WebService studioWebService = StudioWebServiceClient.WebServiceDefinition.Get(workspaceId, experimentId);

            // Convert it into an internal management web service model
            Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService internalService =
                CastUtilities.Cast<Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService>(studioWebService);

            // The new web service model
            var actualWebService = new WebService(internalService, "", this._client);

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
                webServiceObjects.Add(new WebService(ws, "", this._client));
            }

            return webServiceObjects;
        }
    }

    public static class CastUtilities
    {
        /// <summary>
        /// This function comes from the following stack overflow thread. http://stackoverflow.com/questions/3672742/cast-class-into-another-class-or-convert-class-to-another
        /// It casts object myobj to type T if they have similar data members.
        /// Used to cast web service models from Studio.WebService to WebServices from the previous AutoRest autogenerated code.
        /// </summary>
        /// <typeparam name="T">The type to which the conversion is happening.</typeparam>
        /// <param name="myobj">The object being converted.</param>
        /// <returns>An object converted to the new type T.</returns>
        public static T Cast<T>(this Object myobj)
        {
            // TODO: Fix this for compatability .NET Core libraries
            // Also look into removing completely

            Type objectType = myobj.GetType();
            Type target = typeof(T);

            var x = Activator.CreateInstance(target, false);

            var z = from source in objectType.GetMembers().ToList()
                where source.MemberType == MemberTypes.Property
                select source;

            var d = from source in target.GetMembers().ToList()
                where source.MemberType == MemberTypes.Property
                select source;

            List<MemberInfo> members = d.Where(memberInfo => d.Select(c => c.Name)
                .ToList().Contains(memberInfo.Name)).ToList();

            PropertyInfo propertyInfo;

            object value;
            foreach (var memberInfo in members)
            {
                propertyInfo = typeof(T).GetProperty(memberInfo.Name);
                value = myobj.GetType().GetProperty(memberInfo.Name).GetValue(myobj, null);

                propertyInfo.SetValue(x, value, null);
            }
            return (T)x;
        }
    }
}
