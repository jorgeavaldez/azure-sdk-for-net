// 
// Copyright (c) Microsoft.  All rights reserved. 
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
//   http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License. 
//

using System;
using System.Collections.Generic;
using Microsoft.Azure.Management.MachineLearning.WebServices;
using Microsoft.Azure.Management.MachineLearning.WebServices.Models;
using Microsoft.Rest;
using Microsoft.Rest.Azure;

namespace Microsoft.Azure.MachineLearning
{
    public class WebServiceManagementClient
    {
        private const string MANAGEMENT_URL = "https://management.azure.com";
        private AzureMLWebServicesManagementClient _client;
        public StudioServiceClient StudioWebServiceClient;

        public string SubscriptionID
        {
            get { return this._client.SubscriptionId; }
            set { this._client.SubscriptionId = value; }
        }

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
        /// <returns>A new initialized web service object.</returns>
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
            var newWebService = new WebService(webService, resourceGroup,this._client);
            return newWebService;
        }

        /// <summary>
        /// Deploys the given web service in the resource group it specifies.
        /// </summary>
        /// <param name="webService">The web service to be deployed.</param>
        /// <param name="overwrite">Optional: If a web service with the same name exists, just go ahead and override it if true. Otherwise, throw an exception.</param>
        public void DeployWebService(WebService webService, bool overwrite = false)
        {
            webService.ValidateForDeploy();

            if (!overwrite)
            {
                var webServiceExists = false;
                try
                {
                    var existingWebService = this._client.WebServices.Get(webService.ResourceGroupName, webService.Title);
                    webServiceExists = true;
                }

                catch (CloudException cloudException)
                {
                    this._client.WebServices.CreateOrUpdate(webService.Definition, webService.ResourceGroupName,
                        webService.Title);

                    webService.Keys = this._client.WebServices.ListKeys(webService.ResourceGroupName, webService.Title);
                }

                catch (ValidationException validationException)
                {
                    this._client.WebServices.CreateOrUpdate(webService.Definition, webService.ResourceGroupName,
                        webService.Title);

                    webService.Keys = this._client.WebServices.ListKeys(webService.ResourceGroupName, webService.Title);
                }

                if (webServiceExists)
                {
                    // Don't deploy if not there
                    throw new InvalidOperationException("overwrite flag was set to false; a web service in the resource group " + 
                        webService.ResourceGroupName + " with the name " + webService.Title + " already exists.");
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
        public WebService GetWebService(string resourceGroupName, string webServiceName)
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
        public WebService GetWebServiceFromExperiment(string workspaceId, string experimentId, string workspaceAuthorizationToken)
        {
            var subscriptionId = this._client.SubscriptionId;

            var studioDirectWebService = StudioWebServiceClient.GetWebServiceDefinition(workspaceId, experimentId, workspaceAuthorizationToken);

            var editedStudioDirectWebServiceProperties = new WebServicePropertiesForGraph(
                studioDirectWebService.Title,
                studioDirectWebService.Description,
                studioDirectWebService.CreatedOn,
                studioDirectWebService.ModifiedOn,
                "Unknown", // ProvisioningState must be set to unknown here
                studioDirectWebService.Keys,
                studioDirectWebService.ReadOnlyProperty,
                studioDirectWebService.SwaggerLocation,
                studioDirectWebService.ExposeSampleData,
                studioDirectWebService.RealtimeConfiguration,
                studioDirectWebService.Diagnostics,
                studioDirectWebService.StorageAccount,
                studioDirectWebService.MachineLearningWorkspace,
                studioDirectWebService.CommitmentPlan,
                studioDirectWebService.Input,
                studioDirectWebService.Output,
                studioDirectWebService.ExampleRequest,
                studioDirectWebService.Assets,
                studioDirectWebService.Parameters,
                studioDirectWebService.Package);

            // All of the null properties should be filled in by the user before they try to deploy this web service.
            Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService editedStudioDirectWebService = new Management.MachineLearning.WebServices.Models.WebService(
                null,
                editedStudioDirectWebServiceProperties,
                null,
                null,
                null,
                null);

            var actualWebService = new WebService(editedStudioDirectWebService, string.Empty, this._client);

            return actualWebService;
        }

        /// <summary>
        /// Returns a list of web service objects within a resource group.
        /// </summary>
        /// <param name="resourceGroupName">The resource group from which to retrieve the list of web services.</param>
        /// <returns>A list of web services from the specified resource group.</returns>
        public List<WebService> ListWebServicesFromResourceGroup(string resourceGroupName, string skiptoken = default(string))
        {
            var webServicePage = this._client.WebServices.ListInResourceGroup(resourceGroupName, skiptoken);
            var accumulatedWebServices = new List<WebService>();

            // We're gonna implement a linked list accumulation-type method
            // For every link from ListInResourceGroup with the skip token, we push every web service in that link into accumulatedWebServices
            // and call the function again with the nextLink value as the skip token.

            // First let's get the skip token
            var nextLinkToken = webServicePage.NextLink;

            // Now we're going to loop while !token.isnullorwhitespace
            do
            {
                /*
                * And we have to
                * 1. add all of the web services in this page's collection to the accumulated web services list
                * 2. call for the next page
                * 3. replace this page with the next page
                * 4. replace this skip token with the next token
                */

                var servicesCollection = webServicePage.Value;

                foreach (var service in servicesCollection)
                {
                    // 1
                    // Cast the service to one of our web services
                    var castWebService = new WebService(service, resourceGroupName, this._client);
                    accumulatedWebServices.Add(castWebService);
                }

                // 2 + 3
                webServicePage = this._client.WebServices.ListInResourceGroup(resourceGroupName, nextLinkToken);

                // 4 
                nextLinkToken = webServicePage.NextLink;
            } while (!(string.IsNullOrWhiteSpace(nextLinkToken)));

            return accumulatedWebServices;
        }

        /// <summary>
        /// Returns a list of web service objects within the client subscription.
        /// </summary>
        /// <returns>A list of web services from the client subscription.</returns>
        public List<WebService> ListWebServicesFromSubscription()
        {
            var webServicePage = this._client.WebServices.List();
            var accumulatedWebServices = new List<WebService>();
            var nextLinkToken = webServicePage.NextLink;

            do
            {
                var servicesCollection = webServicePage.Value;

                foreach (var service in servicesCollection)
                {
                    var castWebService = new WebService(service, string.Empty, this._client); 
                    accumulatedWebServices.Add(castWebService);
                }

                webServicePage = this._client.WebServices.List(nextLinkToken);

                nextLinkToken = webServicePage.NextLink;
            } while (!(string.IsNullOrWhiteSpace(nextLinkToken)));

            return accumulatedWebServices;
        }
    }
}
