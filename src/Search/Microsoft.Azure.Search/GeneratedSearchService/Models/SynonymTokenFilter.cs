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
    /// Matches single or multi-word synonyms in a token stream.
    /// </summary>
    [JsonObject("#Microsoft.Azure.Search.SynonymTokenFilter")]
    public partial class SynonymTokenFilter : TokenFilter
    {
        /// <summary>
        /// Initializes a new instance of the SynonymTokenFilter class.
        /// </summary>
        public SynonymTokenFilter() { }

        /// <summary>
        /// Initializes a new instance of the SynonymTokenFilter class.
        /// </summary>
        public SynonymTokenFilter(string name, IList<string> synonyms, bool? ignoreCase = default(bool?), bool? expand = default(bool?))
            : base(name)
        {
            Synonyms = synonyms;
            IgnoreCase = ignoreCase;
            Expand = expand;
        }

        /// <summary>
        /// Gets or sets a list of synonyms in following one of two formats:
        /// 1. incredible, unbelievable, fabulous =&gt; amazing - all terms
        /// on the left side of =&gt; symbol will be replaced with all terms
        /// on its right side; 2. incredible, unbelievable, fabulous, amazing
        /// - comma separated list of equivalent words. Set the expand option
        /// to change how this list is interpreted.
        /// </summary>
        [JsonProperty(PropertyName = "synonyms")]
        public IList<string> Synonyms { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to case-fold input for
        /// matching. Default is false.
        /// </summary>
        [JsonProperty(PropertyName = "ignoreCase")]
        public bool? IgnoreCase { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether all words in the list of
        /// synonyms (if =&gt; notation is not used) will map to one another.
        /// If true, all words in the list of synonyms (if =&gt; notation is
        /// not used) will map to one another. The following list:
        /// incredible, unbelievable, fabulous, amazing is equivalent to:
        /// incredible, unbelievable, fabulous, amazing =&gt; incredible,
        /// unbelievable, fabulous, amazing. If false, the following list:
        /// incredible, unbelievable, fabulous, amazing will be equivalent
        /// to: incredible, unbelievable, fabulous, amazing =&gt; incredible.
        /// Default is true.
        /// </summary>
        [JsonProperty(PropertyName = "expand")]
        public bool? Expand { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public override void Validate()
        {
            base.Validate();
            if (Synonyms == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Synonyms");
            }
        }
    }
}