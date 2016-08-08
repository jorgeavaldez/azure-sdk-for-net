using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Microsoft.Azure.Management.MachineLearning.WebServices.Models;

namespace Microsoft.Azure.MachineLearning
{
    /// <summary>
    /// The purpose of this class is to act as a deserialization target from the Studio API to a WebService object.
    /// </summary>
    internal class StudioDeserializationWebService
    {
        /// <summary>
        /// This is only populated at creation time (PUT) for web services
        /// originating from an AzureML Studio experiment.
        /// </summary>
        [JsonProperty(PropertyName = "MachineLearningWorkspace")]
        public MachineLearningWorkspace MachineLearningWorkspace { get; set; }

        /// <summary>
        /// Set of assets associated with the web service.
        /// </summary>
        [JsonProperty(PropertyName = "Assets")]
        public IDictionary<string, AssetItem> Assets { get; set; }

        /// <summary>
        /// The set of global parameters values defined for the web service,
        /// given as a global parameter name to default value map. If no
        /// default value is specified, the parameter is considered to be
        /// required.
        /// </summary>
        [JsonProperty(PropertyName = "Parameters")]
        public IDictionary<string, string> Parameters { get; set; }

        /// <summary>
        /// Swagger schema for the service's input(s), as applicable.
        /// </summary>
        [JsonProperty(PropertyName = "Input")]
        public ServiceInputOutputSpecification Input { get; set; }

        /// <summary>
        /// Swagger schema for the service's output(s), as applicable.
        /// </summary>
        [JsonProperty(PropertyName = "Output")]
        public ServiceInputOutputSpecification Output { get; set; }

        /// <summary>
        /// The definition of the graph package making up this web service.
        /// </summary>
        [JsonProperty(PropertyName = "Package")]
        public GraphPackage Package { get; set; }

        /// <summary>
        /// Sample request data for each of the service's inputs, as
        /// applicable.
        /// </summary>
        [JsonProperty(PropertyName = "ExampleRequest")]
        public ExampleRequest ExampleRequest { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (this.MachineLearningWorkspace != null)
            {
                this.MachineLearningWorkspace.Validate();
            }
            if (this.Input != null)
            {
                this.Input.Validate();
            }
            if (this.Output != null)
            {
                this.Output.Validate();
            }
            if (this.Assets != null)
            {
                foreach (var valueElement in this.Assets.Values)
                {
                    if (valueElement != null)
                    {
                        valueElement.Validate();
                    }
                }
            }
        }
    }
}
