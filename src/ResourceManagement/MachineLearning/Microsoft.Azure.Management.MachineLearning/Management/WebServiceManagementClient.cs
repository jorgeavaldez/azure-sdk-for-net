﻿using System;
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
        WebServiceManagementClient(ServiceClientCredentials userCredentials)
        {
            _client = new AzureMLWebServicesManagementClient(userCredentials);
            StudioWebServiceClient = new StudioWebService(userCredentials);
        }

        /// <summary>
        /// WebServiceManagementClient constructor. Takes a subscription id.
        /// </summary>
        /// <param name="subscriptionId">The id for the user's subscription.</param>
        WebServiceManagementClient(string subscriptionId, ServiceClientCredentials userCredentials)
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
        WebService CreateWebServiceObject(string webServiceDefintionFilePath, string resourceGroup)
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
        WebService CreateWebServiceObject(
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

        public WebService GetWebServiceFromExperiment(string workspaceId, string experimentId)
        {
            // Web service from the studio api
            Microsoft.Azure.Management.MachineLearning.Studio.WebService.Models.WebService studioWebService = StudioWebServiceClient.WebServiceDefinition.Get(workspaceId, experimentId);

            // Convert it into an internal management web service model
            Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService internalService =
                CastUtilities.Cast<Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService>(studioWebService);

            // The new web service model
            var actualWebService = new WebService(internalService, "", this._client);

            return actualWebService;
        }
    }

    public static class CastUtilities
    {
        /// <summary>
        /// This function comes from the following stack overflow thread. http://stackoverflow.com/questions/3672742/cast-class-into-another-class-or-convert-class-to-another
        /// </summary>
        /// <typeparam name="T">The type to which the conversion is happening.</typeparam>
        /// <param name="myobj">The object being converted.</param>
        /// <returns>An object converted to the new type T.</returns>
        public static T Cast<T>(this Object myobj)
        {
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
