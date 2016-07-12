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
    /// Asset output port
    /// </summary>
    public partial class OutputPort
    {
        /// <summary>
        /// Initializes a new instance of the OutputPort class.
        /// </summary>
        public OutputPort() { }

        /// <summary>
        /// Initializes a new instance of the OutputPort class.
        /// </summary>
        public OutputPort(string type = default(string))
        {
            Type = type;
        }

        /// <summary>
        /// Port data type. Possible values include: 'Dataset'
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

    }
}
