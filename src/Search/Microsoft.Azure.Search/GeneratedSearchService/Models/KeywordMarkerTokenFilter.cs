// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
// 
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Azure.Search.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Microsoft.Rest.Azure;

    /// <summary>
    /// Marks terms as keywords.
    /// </summary>
    [JsonObject("#Microsoft.Azure.Search.KeywordMarkerTokenFilter")]
    public partial class KeywordMarkerTokenFilter : TokenFilter
    {
        /// <summary>
        /// Initializes a new instance of the KeywordMarkerTokenFilter class.
        /// </summary>
        public KeywordMarkerTokenFilter() { }

        /// <summary>
        /// Initializes a new instance of the KeywordMarkerTokenFilter class.
        /// </summary>
        public KeywordMarkerTokenFilter(string name, IList<string> keywords = default(IList<string>), bool? ignoreCase = default(bool?))
            : base(name)
        {
            Keywords = keywords;
            IgnoreCase = ignoreCase;
        }

        /// <summary>
        /// Gets or sets a list of words to mark as keywords.
        /// </summary>
        [JsonProperty(PropertyName = "keywords")]
        public IList<string> Keywords { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to ignore case. If true,
        /// all words are converted to lower case first. Default is false.
        /// </summary>
        [JsonProperty(PropertyName = "ignoreCase")]
        public bool? IgnoreCase { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public override void Validate()
        {
            base.Validate();
        }
    }
}