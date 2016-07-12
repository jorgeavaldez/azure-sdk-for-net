using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Management.MachineLearning.WebServices;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;

namespace Microsoft.Azure.MachineLearning
{
    public class WebServiceManagementClient
    {
        private const string MANAGEMENT_URL = "https://management.azure.com";
        private AzureMLWebServicesManagementClient _client;
        public List<WebService> WebServices;

        WebServiceManagementClient()
        {
            var credentialsToken = new TokenCredentials("");

            // TODO: Authentication goes here, learn what to do to ensure that token authentication ends up working correctly.
            _client = new AzureMLWebServicesManagementClient(credentialsToken);

            this.WebServices = new List<WebService>();
        }

        WebServiceManagementClient(string subscriptionId)
        {
            var credentialsToken = new TokenCredentials("");

            // TODO: Authentication goes here, learn what to do to ensure that token authentication ends up working correctly.
            _client = new AzureMLWebServicesManagementClient(credentialsToken);
            _client.SubscriptionId = subscriptionId;

            this.WebServices = new List<WebService>();
        }

        WebService CreateWebServiceObject(string webServiceDefintionFilePath)
        {
            var ws = new WebService(webServiceDefintionFilePath, _client);
            this.WebServices.Add(ws);

            return ws;
        }
    }
}
