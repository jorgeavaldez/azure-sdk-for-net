// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
// 
// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Azure.Management.MachineLearning.Studio.WebService.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Microsoft.Rest.Azure;

    /// <summary>
    /// Association link for a graph global parameter to a node in the graph.
    /// </summary>
    public partial class GraphParameterLink
    {
        /// <summary>
        /// Initializes a new instance of the GraphParameterLink class.
        /// </summary>
        public GraphParameterLink() { }

        /// <summary>
        /// Initializes a new instance of the GraphParameterLink class.
        /// </summary>
        public GraphParameterLink(string nodeId, string parameterKey)
        {
            NodeId = nodeId;
            ParameterKey = parameterKey;
        }

        /// <summary>
        /// The graph node's identifier
        /// </summary>
        [JsonProperty(PropertyName = "nodeId")]
        public string NodeId { get; set; }

        /// <summary>
        /// The identifier of the node parameter that the global parameter
        /// maps to.
        /// </summary>
        [JsonProperty(PropertyName = "parameterKey")]
        public string ParameterKey { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (NodeId == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "NodeId");
            }
            if (ParameterKey == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "ParameterKey");
            }
        }
    }
}
