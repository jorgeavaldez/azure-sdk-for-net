// 
// Copyright (c) Microsoft and contributors.  All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// 
// See the License for the specific language governing permissions and
// limitations under the License.
// 

// Warning: This code was generated by a tool.
// 
// Changes to this file may cause incorrect behavior and will be lost if the
// code is regenerated.

using System;
using System.Collections.Generic;
using System.Linq;
using Hyak.Common;
using Microsoft.Azure;
using Microsoft.Azure.Management.Dns.Models;

namespace Microsoft.Azure.Management.Dns.Models
{
    /// <summary>
    /// The response to a RecordSet List operation.
    /// </summary>
    public partial class RecordSetListResponse : AzureOperationResponse
    {
        private string _nextLink;
        
        /// <summary>
        /// Optional. Gets or sets the continuation token for the next page.
        /// </summary>
        public string NextLink
        {
            get { return this._nextLink; }
            set { this._nextLink = value; }
        }
        
        private IList<RecordSet> _recordSets;
        
        /// <summary>
        /// Required. Gets or sets information about the RecordSets in the
        /// response.
        /// </summary>
        public IList<RecordSet> RecordSets
        {
            get { return this._recordSets; }
            set { this._recordSets = value; }
        }
        
        /// <summary>
        /// Initializes a new instance of the RecordSetListResponse class.
        /// </summary>
        public RecordSetListResponse()
        {
            this.RecordSets = new LazyList<RecordSet>();
        }
        
        /// <summary>
        /// Initializes a new instance of the RecordSetListResponse class with
        /// required arguments.
        /// </summary>
        public RecordSetListResponse(IList<RecordSet> recordSets)
            : this()
        {
            if (recordSets == null)
            {
                throw new ArgumentNullException("recordSets");
            }
            this.RecordSets = recordSets;
        }
    }
}
