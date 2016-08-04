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
using System.Collections.Generic;
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
        /// TODO: Call it webservicedefinition in the swagger
        /// </summary>
        internal Management.MachineLearning.WebServices.Models.WebService Definition { get; set; }

        /// <summary>
        /// The name of this web service.
        /// </summary>
        public string Title
        {
            get { return this.Definition.Properties.Title; }
            set { this.Definition.Properties.Title = value; }
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
        public DateTime? CreatedOn
        {
            get { return this.Definition.Properties.CreatedOn; }
        }

        /// <summary>
        /// DateTime for when this web service was last modified. Is readonly.
        /// </summary>
        public DateTime? ModifiedOn
        {
            get { return this.Definition.Properties.ModifiedOn.Value; }
        }

        /// <summary>
        /// Provisioning state of this web service. One of 'Unknown', 'Provisioning', 'Succeeded', 'Failed', or 'Canceled'.
        /// </summary>
        public string ProvisioningState 
        {
            get { return this.Definition.Properties.ProvisioningState; }
        }

        /// <summary>
        /// Keys for this web service.
        /// </summary>
        public WebServiceKeys Keys
        {
            get { return this.Definition.Properties.Keys; }
            internal set { this.Definition.Properties.Keys = value; }
        }

        /// <summary>
        /// Whether or not this web service is read only.
        /// </summary>
        public bool ReadOnlyProperty
        {
            get { return this.Definition.Properties.ReadOnlyProperty.Value; }
            set
            {
                if (!this.ReadOnlyProperty)
                {
                    this.Definition.Properties.ReadOnlyProperty = value;
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
        public WebService(string webServiceDefinitionFilePath, string resourceGroup, AzureMLWebServicesManagementClient client)
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
        public WebService(Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService definitionWebService, string resourceGroup, AzureMLWebServicesManagementClient client)
        {
            this.Definition = definitionWebService;
            this._client = client;
            this.ResourceGroupName = resourceGroup;
        }

        /// <summary>
        /// Reads in a web service from a web service definition json file.
        /// </summary>
        /// <param name="webServiceParameters">Parameters to replace within the file if interpolation parameters exist.</param>
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
        /// <param name="otherWebService">The other web service from which this web service is being updated.</param>
        public void Update()
        {
            Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService ws = this._client.WebServices.Patch(this.Definition, this.ResourceGroupName, this.Title);

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
        /// Synchronizes the internal state keys with the keys on the server side.
        /// </summary>
        public void SyncKeysWithCloud()
        {
            // TODO: change this to update remotely
            // Could have a sync up and a sync down function and call them in setter and getter
            // Or could have just one that does both somehow
            //
            // ...
            //
            // Regardless, this is wrong...
            this.Keys = this._client.WebServices.ListKeys(this.ResourceGroupName, this.Title);
        }
    }
}
