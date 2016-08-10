// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
// 
// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Azure.MachineLearning
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Rest.Azure;

    /// <summary>
    /// WebServiceDefinitionOperations operations.
    /// </summary>
    public partial interface IWebServiceDefinitionOperations
    {
        /// <summary>
        /// Gets a web service definition
        /// </summary>
        /// Retrive the web service definition from an experiement.
        /// <param name='workspaceId'>
        /// The id of the workspace where the experiment resides.
        /// </param>
        /// <param name='experimentId'>
        /// The id of the experiment within the workspace referred to by
        /// workspaceId.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<AzureOperationResponse<WebService>> GetWithHttpMessagesAsync(string workspaceId, string experimentId, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
