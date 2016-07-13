﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        public AzureMLWebServicesManagementClient _client { get; set; }

        /// <summary>
        /// Internal autogenerated model that holds properties of a web service.
        /// </summary>
        public Management.MachineLearning.WebServices.Models.WebService Definition { get; private set; }

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
            get
            {
                try
                {
                    var val = this.Definition.Properties.CreatedOn.Value;

                    return val;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// DateTime for when this web service was last modified. Is readonly.
        /// </summary>
        public DateTime? ModifiedOn
        {
            get
            {
                try
                {
                    var val = this.Definition.Properties.ModifiedOn.Value;

                    return val;
                }
                catch (Exception)
                {
                    return null;
                }
            }

            private set { throw new InvalidOperationException("Tried to set WebService.ModifiedOn. Cannot set value."); }
        }

        /// <summary>
        /// Provisioning state of this web service. One of 'Unknown', 'Provisioning', 'Succeeded', 'Failed', or 'Canceled'.
        /// </summary>
        public string ProvisioningState
        {
            get { return this.Definition.Properties.ProvisioningState; }

            private set { throw new InvalidOperationException("Tried to set WebService.ProvisioningState. Cannot set value."); }
        }

        /// <summary>
        /// Keys for this web service.
        /// </summary>
        public WebServiceKeys Keys
        {
            get { return this.Definition.Properties.Keys; }
            set { throw new InvalidOperationException("Tried to set WebService.Keys. Cannot set value."); }
        }

        /// <summary>
        /// Whether or not this web service is read only.
        /// </summary>
        public bool ReadOnlyProperty
        {
            get { return this.Definition.Properties.ReadOnlyProperty.Value; }
            set { this.Definition.Properties.ReadOnlyProperty = value; }
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
            private set { throw new InvalidOperationException("Tried to set WebService.MachineLearningWorkspace. Cannot set value."); }
        }

        /// <summary>
        /// Constructor for a WebService that creates a new Microsoft.Azure.Management.MachineLearning.WebService from a web service definition file.
        /// </summary>
        /// <param name="webServiceDefinitionFilePath">Filepath to the web service definition file.</param>
        /// <param name="resourceGroup">The name of the resource group from which this web service is deployed.</param>
        /// <param name="client">The management services client. Gets passed in by the external management client.</param>
        public WebService(string webServiceDefinitionFilePath, string resourceGroup, AzureMLWebServicesManagementClient client)
        {
            Dictionary<string, string> _paramsDictionary = new Dictionary<string, string>();

            Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService ws =
                this.InputWebServiceFromWSDFile(_paramsDictionary, webServiceDefinitionFilePath);

            this.Definition = ws;

            this._client = client;
        }

        /// <summary>
        /// Constructor for a WebService that takes a Microsoft.Azure.Management.MachineLearning.WebService for the internal Definition.
        /// </summary>
        /// <param name="definitionWebService"></param>
        /// <param name="resourceGroup"></param>
        /// <param name="client"></param>
        public WebService(Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService definitionWebService, string resourceGroup, AzureMLWebServicesManagementClient client)
        {
            this.Definition = definitionWebService;
            this._client = client;
        }

        /// <summary>
        /// Reads in a web service from a web service definition json file.
        /// </summary>
        /// <param name="webServiceParameters">Parameters to replace within the file if interpolation parameters exist.</param>
        /// <param name="webServiceDefinitionFilePath">The path to the web service definition file.</param>
        /// <returns></returns>
        private Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService InputWebServiceFromWSDFile(Dictionary<string, string> webServiceParameters, string webServiceDefinitionFilePath)
        {
            string wsDefinition;

            //StreamReader r = new StreamReader("file.txt");

            using (StreamReader r = new StreamReader(webServiceDefinitionFilePath))
            {
                wsDefinition = r.ReadToEnd();
            }

            foreach (var kvp in webServiceParameters)
            {
                wsDefinition = wsDefinition.Replace($"$({kvp.Key})", kvp.Value);
            }

            // TODO: call this in the constructor and assign 
            return ModelsSerializationUtil.GetAzureMLWebServiceFromJsonDefinition(wsDefinition);
        }

        /// <summary>
        /// Updates this web service and updates the internal state.
        /// </summary>
        /// <param name="otherWebService">The other web service from which this web service is being updated.</param>
        public void Update(WebService otherWebService)
        {
            Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService ws = this._client.WebServices.CreateOrUpdate(otherWebService.Definition, this.ResourceGroupName, this.Title);

            this.Definition = ws;
        }

        /// <summary>
        /// Updates this web service and updates the internal state. Takes an AutoRest web service model.
        /// </summary>
        /// <param name="otherWebService">The other AutoRest web service model from which this web service is being updated.</param>
        public void Update(Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService otherWebService)
        {
            Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebService ws = this._client.WebServices.CreateOrUpdate(otherWebService, this.ResourceGroupName, this.Title);

            this.Definition = ws;
        }

        /// <summary>
        /// Deletes this deployed web service.
        /// </summary>
        public void Delete()
        {
            this._client.WebServices.Remove(this.ResourceGroupName, this.Title);
        }
    }
}
