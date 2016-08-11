﻿// 
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
using System.IO;
using Microsoft.Azure.Management.MachineLearning.WebServices;
using Microsoft.Azure.Management.MachineLearning.WebServices.Models;
using Microsoft.Azure.Management.MachineLearning.WebServices.Util;

namespace Microsoft.Azure.MachineLearning
{
    /// <summary>
    /// Describes an AzureML Web Service.
    /// </summary>
    public class WebService
    {
        /// <summary>
        /// Internal reference to a AzureMLWebServicesManagementClient.
        /// </summary>
        internal AzureMLWebServicesManagementClient _client { get; private set; }

        /// <summary>
        /// Internal autogenerated model that holds properties of a web service.
        /// </summary>
        internal Management.MachineLearning.WebServices.Models.WebService Definition { get; set; }

        /// <summary>
        /// The name of this web service.
        /// </summary>
        public string Title
        {
            get { return this.Definition.Name; }
            set { this.Definition.Name = value; }
        }

        /// <summary>
        /// The description for this web service.
        /// </summary>
        public string Description
        {
            get { return this.Definition.Properties.Description; }
            set { this.Definition.Properties.Description = value; }
        }

        /// <summary>
        /// DateTime for when this web service was created. Is readonly.
        /// </summary>
        public DateTime? CreatedOn => this.Definition.Properties.CreatedOn;

        /// <summary>
        /// DateTime for when this web service was last modified. Is readonly.
        /// </summary>
        public DateTime? ModifiedOn => this.Definition.Properties.ModifiedOn.Value;

        /// <summary>
        /// Provisioning state of this web service. One of 'Unknown', 'Provisioning', 'Succeeded', 'Failed', or 'Canceled'.
        /// </summary>
        public string ProvisioningState => this.Definition.Properties.ProvisioningState;

        /// <summary>
        /// Keys for this web service.
        /// </summary>
        public WebServiceKeys Keys
        {
            get { return this.Definition.Properties.Keys; }
            set
            {
                this.Definition.Properties.Keys = value;
                SyncKeysUp();
            }
        }

        /// <summary>
        /// Whether or not this web service is read only.
        /// </summary>
        public bool? ReadOnlyProperty
        {
            get { return this.Definition.Properties.ReadOnlyProperty; }
            set
            {
                if (this.ReadOnlyProperty.HasValue && this.ReadOnlyProperty.Value == false)
                { 

                    this.Definition.Properties.ReadOnlyProperty = value;
                }
                
                else if (!this.ReadOnlyProperty.HasValue)
                {
                    this.Definition.Properties.ReadOnlyProperty = false;
                }
            }
        }

        public string ResourceGroupName { get; set; }
        /// <summary>
        /// Flag that controls whether or not to expose sample data in this web service's swagger defintion.
        /// </summary>
        public bool ExposeSampleData
        {
            get { return this.Definition.Properties.ExposeSampleData.Value; }
            set { this.Definition.Properties.ExposeSampleData = value; }
        }

        /// <summary>
        /// Configuration for the service's realtime endpoint.
        /// </summary>
        public RealtimeConfiguration RealtimeConfiguration
        {
            get { return this.Definition.Properties.RealtimeConfiguration; }
            set { this.Definition.Properties.RealtimeConfiguration = value; }
        }

        /// <summary>
        /// Settings controlling the diagnostics traces collection for the web service.
        /// </summary>
        public DiagnosticsConfiguration Diagnostics
        {
            get { return this.Definition.Properties.Diagnostics; }
            set { this.Definition.Properties.Diagnostics = value; }
        }

        /// <summary>
        /// The storage account associated with the service. Stores both datasets and diagnostic traces.
        /// </summary>
        public StorageAccount StorageAccount
        {
            get { return this.Definition.Properties.StorageAccount; }
            set { this.Definition.Properties.StorageAccount = value; }
        }

        /// <summary>
        /// CommitmentPlan's ARM resource ID.
        /// </summary>
        public string CommitmentPlan
        {
            get { return this.Definition.Properties.CommitmentPlan.Id; }
            set { this.Definition.Properties.CommitmentPlan = new CommitmentPlan(value); }
        }

        /// <summary>
        /// The web service location.
        /// </summary>
        public string Location
        {
            get { return this.Definition.Location; }
            set { this.Definition.Location = value; }
        }

        /// <summary>
        /// The workspace from which this web service was deployed if deployed from one.
        /// </summary>
        public MachineLearningWorkspace MachineLearningWorkspaceLearningWorkspace
        {
            get { return this.Definition.Properties.MachineLearningWorkspace; }
        }

        /// <summary>
        /// Constructor for a WebService that creates a new Microsoft.Azure.Management.MachineLearning.WebService from a web service definition file.
        /// </summary>
        /// <param name="webServiceDefinitionFilePath">Filepath to the web service definition file.</param>
        /// <param name="resourceGroup">The name of the resource group from which this web service is deployed.</param>
        /// <param name="client">The management services client. Gets passed in by the external management client.</param>
        internal WebService(string webServiceDefinitionFilePath, string resourceGroup, AzureMLWebServicesManagementClient client)
        {
            Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService ws =
                this.InputWebServiceFromWSDFile( webServiceDefinitionFilePath);

            this.Definition = ws;
            this.ResourceGroupName = resourceGroup;
            this._client = client;
        }

        /// <summary>
        /// Constructor for a WebService that takes a Microsoft.Azure.Management.MachineLearning.WebService for the internal Definition.
        /// </summary>
        /// <param name="definitionWebService">The AutoRest generated web service to use as the definition for this web service.</param>
        /// <param name="resourceGroup">The resource group of the specified web service.</param>
        /// <param name="client">The management services client. Gets passed in by the external management client.</param>
        internal WebService(Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService definitionWebService, string resourceGroup, AzureMLWebServicesManagementClient client)
        {
            this.Definition = definitionWebService;
            this._client = client;
            this.ResourceGroupName = resourceGroup;
        }

        /// <summary>
        /// Reads in a web service from a web service definition json file.
        /// </summary>
        /// <param name="webServiceDefinitionFilePath">The path to the web service definition file.</param>
        /// <returns>The AutoRest generated web service object.</returns>
        private Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService InputWebServiceFromWSDFile(string webServiceDefinitionFilePath)
        {
            string wsDefinition;

            using (StreamReader r = System.IO.File.OpenText(webServiceDefinitionFilePath))
            {
                wsDefinition = r.ReadToEnd();
            }

            return ModelsSerializationUtil.GetAzureMLWebServiceFromJsonDefinition(wsDefinition);
        }

        /// <summary>
        /// Updates this web service and updates the internal state.
        /// </summary>
        public void Update()
        {
            Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService ws = this._client.WebServices.Patch(this.Definition, this.ResourceGroupName, this.Title);

            this.Definition = ws;
        }

        /// <summary>
        /// Updates this web service and updates the internal state asynchronously.
        /// </summary>
        public async void UpdateAsync()
        {
            Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService ws = await this._client.WebServices.PatchAsync(this.Definition, this.ResourceGroupName, this.Title);

            this.Definition = ws;
        }

        /// <summary>
        /// Deletes this deployed web service.
        /// </summary>
        public void Delete()
        {
            this._client.WebServices.Remove(this.ResourceGroupName, this.Title);
        }

        /// <summary>
        /// Deletes this deployed web service asynchronously.
        /// </summary>
        public async void DeleteAsync()
        {
            await this._client.WebServices.RemoveAsync(this.ResourceGroupName, this.Title);
        }

        /// <summary>
        /// Synchronizes the internal state keys with the keys on the server side.
        /// </summary>
        public void SyncKeysUp()
        {
            var cleanWebService = this._client.WebServices.Get(this.ResourceGroupName, this.Title);

            cleanWebService.Properties.Keys = this.Keys;

            this._client.WebServices.CreateOrUpdate(cleanWebService, this.ResourceGroupName, this.Title);
        }

        /// <summary>
        /// Validates this web service to ensure that the appropriate fields are filled before attempting to deploy it.
        /// </summary>
        public void ValidateForDeploy()
        {
            if (string.IsNullOrEmpty(this.CommitmentPlan))
            {
                throw new InvalidOperationException("The web service you're trying to deploy does not have a CommitmentPlan, you must set one to deploy.");
            }

            if (string.IsNullOrEmpty(this.Location))
            {
                throw new InvalidOperationException("The web service you're trying to deploy does not have a Location, you must set one to deploy.");
            }

            if (string.IsNullOrEmpty(this.ResourceGroupName))
            {
                throw new InvalidOperationException("The web service you're trying to deploy does not have a ResourceGroupName, you must set one to deploy.");
            }

            if (string.IsNullOrEmpty(this.StorageAccount.Name) ||
                string.IsNullOrEmpty(this.StorageAccount.Key))
            {
                throw new InvalidOperationException("The web service you're trying to deploy does not have a valid StorageAccount name/key, you must set one to deploy.");
            }
        }
    }
}