// 
// Copyright (c) Microsoft.  All rights reserved. 
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
//   http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License. 
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Management.MachineLearning.Studio.WebService;
using Microsoft.Rest;
using Microsoft.Rest.Azure;
using Microsoft.Rest.Serialization;
using Newtonsoft.Json;

namespace Microsoft.Azure.MachineLearning
{
    public class StudioServiceClient
    {
        internal StudioWebService _client { get; set; }

        public StudioServiceClient(ServiceClientCredentials credentials)
        {
            this._client = new StudioWebService(credentials);            
        }

        /// <summary>
        /// Gets a web service definition
        /// </summary>
        /// <param name='workspaceId'>
        /// The id of the workspace where the experiment resides.
        /// </param>
        /// <param name='experimentId'>
        /// The id of the experiment within the workspace referred to by workspaceId.
        /// </param>
        /// <param name='customHeaders'>
        /// Headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <return>
        /// A response object containing the response body and response headers.
        /// </return>
        public async Task<AzureOperationResponse<Azure.Management.MachineLearning.WebServices.Models.WebServicePropertiesForGraph>> GetWebServiceDefinitionWithHttpMessagesAsync(string workspaceId, string experimentId, string workspaceAuthorizationToken, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        { 
            // Adding the authorization token as a custom header.
            var newCustomHeaders = customHeaders ?? new Dictionary<string, List<string>>();
            var newCustomHeaderList = new List<string>();

            newCustomHeaderList.Add(workspaceAuthorizationToken);

            newCustomHeaders.Add("x-ms-metaanalytics-authorizationtoken", newCustomHeaderList);

            customHeaders = newCustomHeaders;

            if (workspaceId == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "workspaceId");
            }
            if (experimentId == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "experimentId");
            }
            // Tracing
            bool _shouldTrace = ServiceClientTracing.IsEnabled;
            string _invocationId = null;
            if (_shouldTrace)
            {
                _invocationId = ServiceClientTracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters = new Dictionary<string, object>();
                tracingParameters.Add("workspaceId", workspaceId);
                tracingParameters.Add("experimentId", experimentId);
                tracingParameters.Add("cancellationToken", cancellationToken);
                ServiceClientTracing.Enter(_invocationId, this, "Get", tracingParameters);
            }
            // Construct URL
            var _baseUrl = this._client.BaseUri.AbsoluteUri;
            var _url = new Uri(new Uri(_baseUrl + (_baseUrl.EndsWith("/") ? "" : "/")), "workspaces/{workspaceId}/experiments/{experimentId}/webservicedefinition").ToString();
            _url = _url.Replace("{workspaceId}", Uri.EscapeDataString(workspaceId));
            _url = _url.Replace("{experimentId}", Uri.EscapeDataString(experimentId));
            List<string> _queryParameters = new List<string>();
            if (_queryParameters.Count > 0)
            {
                _url += "?" + string.Join("&", _queryParameters);
            }
            // Create HTTP transport objects
            HttpRequestMessage _httpRequest = new HttpRequestMessage();
            HttpResponseMessage _httpResponse = null;
            _httpRequest.Method = new HttpMethod("GET");
            _httpRequest.RequestUri = new Uri(_url);
            // Set Headers
            if (this._client.GenerateClientRequestId != null && this._client.GenerateClientRequestId.Value)
            {
                _httpRequest.Headers.TryAddWithoutValidation("x-ms-client-request-id", Guid.NewGuid().ToString());
            }
            if (this._client.AcceptLanguage != null)
            {
                if (_httpRequest.Headers.Contains("accept-language"))
                {
                    _httpRequest.Headers.Remove("accept-language");
                }
                _httpRequest.Headers.TryAddWithoutValidation("accept-language", this._client.AcceptLanguage);
            }
            if (customHeaders != null)
            {
                foreach (var _header in customHeaders)
                {
                    if (_httpRequest.Headers.Contains(_header.Key))
                    {
                        _httpRequest.Headers.Remove(_header.Key);
                    }
                    _httpRequest.Headers.TryAddWithoutValidation(_header.Key, _header.Value);
                }
            }

            // Serialize Request
            string _requestContent = null;
            // Convert it into an internal management web service model
            // Now uses straight definition model, removed dependency on autorest generated code for studio api

            // Set Credentials
            if (this._client.Credentials != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await this._client.Credentials.ProcessHttpRequestAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
            }
            // Send Request
            if (_shouldTrace)
            {
                ServiceClientTracing.SendRequest(_invocationId, _httpRequest);
            }
            cancellationToken.ThrowIfCancellationRequested();
            _httpResponse = await this._client.HttpClient.SendAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
            if (_shouldTrace)
            {
                ServiceClientTracing.ReceiveResponse(_invocationId, _httpResponse);
            }
            HttpStatusCode _statusCode = _httpResponse.StatusCode;
            cancellationToken.ThrowIfCancellationRequested();
            string _responseContent = null;
            if ((int)_statusCode != 200)
            {
                var ex = new CloudException(string.Format("Operation returned an invalid status code '{0}'", _statusCode));
                try
                {
                    _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                    CloudError _errorBody = SafeJsonConvert.DeserializeObject<CloudError>(_responseContent, this._client.DeserializationSettings);
                    if (_errorBody != null)
                    {
                        ex = new CloudException(_errorBody.Message);
                        ex.Body = _errorBody;
                    }
                }
                catch (JsonException)
                {
                    // Ignore the exception
                }
                ex.Request = new HttpRequestMessageWrapper(_httpRequest, _requestContent);
                ex.Response = new HttpResponseMessageWrapper(_httpResponse, _responseContent);
                if (_httpResponse.Headers.Contains("x-ms-request-id"))
                {
                    ex.RequestId = _httpResponse.Headers.GetValues("x-ms-request-id").FirstOrDefault();
                }
                if (_shouldTrace)
                {
                    ServiceClientTracing.Error(_invocationId, ex);
                }
                _httpRequest.Dispose();
                if (_httpResponse != null)
                {
                    _httpResponse.Dispose();
                }
                throw ex;
            }
            // Create Result
            var _result = new AzureOperationResponse<Azure.Management.MachineLearning.WebServices.Models.WebServicePropertiesForGraph>();
            _result.Request = _httpRequest;
            _result.Response = _httpResponse;
            if (_httpResponse.Headers.Contains("x-ms-request-id"))
            {
                _result.RequestId = _httpResponse.Headers.GetValues("x-ms-request-id").FirstOrDefault();
            }
            // Deserialize Response
            if ((int)_statusCode == 200)
            {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    _result.Body = SafeJsonConvert.DeserializeObject<Azure.Management.MachineLearning.WebServices.Models.WebServicePropertiesForGraph>(_responseContent, this._client.DeserializationSettings);
                }
                catch (JsonException ex)
                {
                    _httpRequest.Dispose();
                    if (_httpResponse != null)
                    {
                        _httpResponse.Dispose();
                    }
                    throw new SerializationException("Unable to deserialize the response.", _responseContent, ex);
                }
            }
            if (_shouldTrace)
            {
                ServiceClientTracing.Exit(_invocationId, _result);
            }
            return _result;
        } 

        /// <summary>
        /// Gets a web service definition
        /// </summary>
        /// <param name='workspaceId'>
        /// The id of the workspace where the experiment resides.
        /// </param>
        /// <param name='experimentId'>
        /// The id of the experiment within the workspace referred to by workspaceId.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public async Task<Microsoft.Azure.Management.MachineLearning.WebServices.Models.WebServicePropertiesForGraph> GetWebServiceDefinitionAsync(string workspaceId, string experimentId, string workspaceAuthorizationToken, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await this.GetWebServiceDefinitionWithHttpMessagesAsync(workspaceId, experimentId, workspaceAuthorizationToken, customHeaders, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }

        /// <summary>
        /// Gets a web service definition
        /// </summary>
        /// <param name='workspaceId'>
        /// The id of the workspace where the experiment resides.
        /// </param>
        /// <param name='experimentId'>
        /// The id of the experiment within the workspace referred to by workspaceId.
        /// </param>
        public Azure.Management.MachineLearning.WebServices.Models.WebServicePropertiesForGraph GetWebServiceDefinition(string workspaceId, string experimentId, string workspaceAuthorizationToken, Dictionary<string, List<string>> customHeaders = null)
        {
            // s is the state object we pass through. Here I pass this as the state object. This then gets passed as an object and so it must be cast to a member of this class.
            return Task.Factory.StartNew(s => ((StudioServiceClient)s).GetWebServiceDefinitionAsync(workspaceId, experimentId, workspaceAuthorizationToken, customHeaders), this, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
        }

    }
}
