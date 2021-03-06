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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.Azure.Management.ApiManagement;
using Microsoft.Azure.Management.ApiManagement.SmapiModels;

namespace Microsoft.Azure.Management.ApiManagement
{
    /// <summary>
    /// .Net client wrapper for the REST API for Azure ApiManagement Service
    /// </summary>
    public static partial class GroupsOperationsExtensions
    {
        /// <summary>
        /// Creates new group.
        /// </summary>
        /// <param name='operations'>
        /// Reference to the
        /// Microsoft.Azure.Management.ApiManagement.IGroupsOperations.
        /// </param>
        /// <param name='resourceGroupName'>
        /// Required. The name of the resource group.
        /// </param>
        /// <param name='serviceName'>
        /// Required. The name of the Api Management service.
        /// </param>
        /// <param name='gid'>
        /// Required. Identifier of the group.
        /// </param>
        /// <param name='parameters'>
        /// Required. Create parameters.
        /// </param>
        /// <returns>
        /// A standard service response including an HTTP status code and
        /// request ID.
        /// </returns>
        public static AzureOperationResponse Create(this IGroupsOperations operations, string resourceGroupName, string serviceName, string gid, GroupCreateParameters parameters)
        {
            return Task.Factory.StartNew((object s) => 
            {
                return ((IGroupsOperations)s).CreateAsync(resourceGroupName, serviceName, gid, parameters);
            }
            , operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
        }
        
        /// <summary>
        /// Creates new group.
        /// </summary>
        /// <param name='operations'>
        /// Reference to the
        /// Microsoft.Azure.Management.ApiManagement.IGroupsOperations.
        /// </param>
        /// <param name='resourceGroupName'>
        /// Required. The name of the resource group.
        /// </param>
        /// <param name='serviceName'>
        /// Required. The name of the Api Management service.
        /// </param>
        /// <param name='gid'>
        /// Required. Identifier of the group.
        /// </param>
        /// <param name='parameters'>
        /// Required. Create parameters.
        /// </param>
        /// <returns>
        /// A standard service response including an HTTP status code and
        /// request ID.
        /// </returns>
        public static Task<AzureOperationResponse> CreateAsync(this IGroupsOperations operations, string resourceGroupName, string serviceName, string gid, GroupCreateParameters parameters)
        {
            return operations.CreateAsync(resourceGroupName, serviceName, gid, parameters, CancellationToken.None);
        }
        
        /// <summary>
        /// Deletes specific group of the Api Management service instance.
        /// </summary>
        /// <param name='operations'>
        /// Reference to the
        /// Microsoft.Azure.Management.ApiManagement.IGroupsOperations.
        /// </param>
        /// <param name='resourceGroupName'>
        /// Required. The name of the resource group.
        /// </param>
        /// <param name='serviceName'>
        /// Required. The name of the Api Management service.
        /// </param>
        /// <param name='gid'>
        /// Required. Identifier of the group.
        /// </param>
        /// <param name='etag'>
        /// Required. ETag.
        /// </param>
        /// <returns>
        /// A standard service response including an HTTP status code and
        /// request ID.
        /// </returns>
        public static AzureOperationResponse Delete(this IGroupsOperations operations, string resourceGroupName, string serviceName, string gid, string etag)
        {
            return Task.Factory.StartNew((object s) => 
            {
                return ((IGroupsOperations)s).DeleteAsync(resourceGroupName, serviceName, gid, etag);
            }
            , operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
        }
        
        /// <summary>
        /// Deletes specific group of the Api Management service instance.
        /// </summary>
        /// <param name='operations'>
        /// Reference to the
        /// Microsoft.Azure.Management.ApiManagement.IGroupsOperations.
        /// </param>
        /// <param name='resourceGroupName'>
        /// Required. The name of the resource group.
        /// </param>
        /// <param name='serviceName'>
        /// Required. The name of the Api Management service.
        /// </param>
        /// <param name='gid'>
        /// Required. Identifier of the group.
        /// </param>
        /// <param name='etag'>
        /// Required. ETag.
        /// </param>
        /// <returns>
        /// A standard service response including an HTTP status code and
        /// request ID.
        /// </returns>
        public static Task<AzureOperationResponse> DeleteAsync(this IGroupsOperations operations, string resourceGroupName, string serviceName, string gid, string etag)
        {
            return operations.DeleteAsync(resourceGroupName, serviceName, gid, etag, CancellationToken.None);
        }
        
        /// <summary>
        /// Gets specific group.
        /// </summary>
        /// <param name='operations'>
        /// Reference to the
        /// Microsoft.Azure.Management.ApiManagement.IGroupsOperations.
        /// </param>
        /// <param name='resourceGroupName'>
        /// Required. The name of the resource group.
        /// </param>
        /// <param name='serviceName'>
        /// Required. The name of the Api Management service.
        /// </param>
        /// <param name='gid'>
        /// Required. Identifier of the group.
        /// </param>
        /// <returns>
        /// Get Group operation response details.
        /// </returns>
        public static GroupGetResponse Get(this IGroupsOperations operations, string resourceGroupName, string serviceName, string gid)
        {
            return Task.Factory.StartNew((object s) => 
            {
                return ((IGroupsOperations)s).GetAsync(resourceGroupName, serviceName, gid);
            }
            , operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
        }
        
        /// <summary>
        /// Gets specific group.
        /// </summary>
        /// <param name='operations'>
        /// Reference to the
        /// Microsoft.Azure.Management.ApiManagement.IGroupsOperations.
        /// </param>
        /// <param name='resourceGroupName'>
        /// Required. The name of the resource group.
        /// </param>
        /// <param name='serviceName'>
        /// Required. The name of the Api Management service.
        /// </param>
        /// <param name='gid'>
        /// Required. Identifier of the group.
        /// </param>
        /// <returns>
        /// Get Group operation response details.
        /// </returns>
        public static Task<GroupGetResponse> GetAsync(this IGroupsOperations operations, string resourceGroupName, string serviceName, string gid)
        {
            return operations.GetAsync(resourceGroupName, serviceName, gid, CancellationToken.None);
        }
        
        /// <summary>
        /// List all groups.
        /// </summary>
        /// <param name='operations'>
        /// Reference to the
        /// Microsoft.Azure.Management.ApiManagement.IGroupsOperations.
        /// </param>
        /// <param name='resourceGroupName'>
        /// Required. The name of the resource group.
        /// </param>
        /// <param name='serviceName'>
        /// Required. The name of the Api Management service.
        /// </param>
        /// <param name='query'>
        /// Optional.
        /// </param>
        /// <returns>
        /// List Groups operation response details.
        /// </returns>
        public static GroupListResponse List(this IGroupsOperations operations, string resourceGroupName, string serviceName, QueryParameters query)
        {
            return Task.Factory.StartNew((object s) => 
            {
                return ((IGroupsOperations)s).ListAsync(resourceGroupName, serviceName, query);
            }
            , operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
        }
        
        /// <summary>
        /// List all groups.
        /// </summary>
        /// <param name='operations'>
        /// Reference to the
        /// Microsoft.Azure.Management.ApiManagement.IGroupsOperations.
        /// </param>
        /// <param name='resourceGroupName'>
        /// Required. The name of the resource group.
        /// </param>
        /// <param name='serviceName'>
        /// Required. The name of the Api Management service.
        /// </param>
        /// <param name='query'>
        /// Optional.
        /// </param>
        /// <returns>
        /// List Groups operation response details.
        /// </returns>
        public static Task<GroupListResponse> ListAsync(this IGroupsOperations operations, string resourceGroupName, string serviceName, QueryParameters query)
        {
            return operations.ListAsync(resourceGroupName, serviceName, query, CancellationToken.None);
        }
        
        /// <summary>
        /// List next groups page.
        /// </summary>
        /// <param name='operations'>
        /// Reference to the
        /// Microsoft.Azure.Management.ApiManagement.IGroupsOperations.
        /// </param>
        /// <param name='nextLink'>
        /// Required. NextLink from the previous successful call to List
        /// operation.
        /// </param>
        /// <returns>
        /// List Groups operation response details.
        /// </returns>
        public static GroupListResponse ListNext(this IGroupsOperations operations, string nextLink)
        {
            return Task.Factory.StartNew((object s) => 
            {
                return ((IGroupsOperations)s).ListNextAsync(nextLink);
            }
            , operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
        }
        
        /// <summary>
        /// List next groups page.
        /// </summary>
        /// <param name='operations'>
        /// Reference to the
        /// Microsoft.Azure.Management.ApiManagement.IGroupsOperations.
        /// </param>
        /// <param name='nextLink'>
        /// Required. NextLink from the previous successful call to List
        /// operation.
        /// </param>
        /// <returns>
        /// List Groups operation response details.
        /// </returns>
        public static Task<GroupListResponse> ListNextAsync(this IGroupsOperations operations, string nextLink)
        {
            return operations.ListNextAsync(nextLink, CancellationToken.None);
        }
        
        /// <summary>
        /// Patches specific group.
        /// </summary>
        /// <param name='operations'>
        /// Reference to the
        /// Microsoft.Azure.Management.ApiManagement.IGroupsOperations.
        /// </param>
        /// <param name='resourceGroupName'>
        /// Required. The name of the resource group.
        /// </param>
        /// <param name='serviceName'>
        /// Required. The name of the Api Management service.
        /// </param>
        /// <param name='gid'>
        /// Required. Identifier of the group.
        /// </param>
        /// <param name='parameters'>
        /// Required. Update parameters.
        /// </param>
        /// <param name='etag'>
        /// Required. ETag.
        /// </param>
        /// <returns>
        /// A standard service response including an HTTP status code and
        /// request ID.
        /// </returns>
        public static AzureOperationResponse Update(this IGroupsOperations operations, string resourceGroupName, string serviceName, string gid, GroupUpdateParameters parameters, string etag)
        {
            return Task.Factory.StartNew((object s) => 
            {
                return ((IGroupsOperations)s).UpdateAsync(resourceGroupName, serviceName, gid, parameters, etag);
            }
            , operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
        }
        
        /// <summary>
        /// Patches specific group.
        /// </summary>
        /// <param name='operations'>
        /// Reference to the
        /// Microsoft.Azure.Management.ApiManagement.IGroupsOperations.
        /// </param>
        /// <param name='resourceGroupName'>
        /// Required. The name of the resource group.
        /// </param>
        /// <param name='serviceName'>
        /// Required. The name of the Api Management service.
        /// </param>
        /// <param name='gid'>
        /// Required. Identifier of the group.
        /// </param>
        /// <param name='parameters'>
        /// Required. Update parameters.
        /// </param>
        /// <param name='etag'>
        /// Required. ETag.
        /// </param>
        /// <returns>
        /// A standard service response including an HTTP status code and
        /// request ID.
        /// </returns>
        public static Task<AzureOperationResponse> UpdateAsync(this IGroupsOperations operations, string resourceGroupName, string serviceName, string gid, GroupUpdateParameters parameters, string etag)
        {
            return operations.UpdateAsync(resourceGroupName, serviceName, gid, parameters, etag, CancellationToken.None);
        }
    }
}
