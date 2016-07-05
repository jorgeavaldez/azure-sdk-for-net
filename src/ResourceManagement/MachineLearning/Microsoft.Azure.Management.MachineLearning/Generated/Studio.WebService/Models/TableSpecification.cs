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
    /// The swagger 2.0 schema describing a single service input or output.
    /// See Swagger specification: http://swagger.io/specification/
    /// </summary>
    public partial class TableSpecification
    {
        /// <summary>
        /// Initializes a new instance of the TableSpecification class.
        /// </summary>
        public TableSpecification() { }

        /// <summary>
        /// Initializes a new instance of the TableSpecification class.
        /// </summary>
        public TableSpecification(string type, string title = default(string), string description = default(string), string format = default(string), IDictionary<string, ColumnSpecification> properties = default(IDictionary<string, ColumnSpecification>))
        {
            Title = title;
            Description = description;
            Type = type;
            Format = format;
            Properties = properties;
        }

        /// <summary>
        /// Swagger schema title.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Swagger schema description.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// The type of the entity described in swagger.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// The format, if 'type' is not 'object'
        /// </summary>
        [JsonProperty(PropertyName = "format")]
        public string Format { get; set; }

        /// <summary>
        /// The set of columns within the data table.
        /// </summary>
        [JsonProperty(PropertyName = "properties")]
        public IDictionary<string, ColumnSpecification> Properties { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (Type == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Type");
            }
            if (this.Properties != null)
            {
                foreach (var valueElement in this.Properties.Values)
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
