/* 
 * SmartThings API
 *
 * # Overview  This is the reference documentation for the SmartThings API.  The SmartThings API supports [REST](https://en.wikipedia.org/wiki/Representational_state_transfer), resources are protected with [OAuth 2.0 Bearer Tokens](https://tools.ietf.org/html/rfc6750#section-2.1), and all responses are sent as [JSON](http://www.json.org/).  # Authentication  All SmartThings resources are protected with [OAuth 2.0 Bearer Tokens](https://tools.ietf.org/html/rfc6750#section-2.1) sent on the request as an `Authorization: Bearer <TOKEN>` header, and operations require specific OAuth scopes that specify the exact permissions authorized by the user.  ## Token types  There are two types of tokens: SmartApp tokens, and personal access tokens.  SmartApp tokens are used to communicate between third-party integrations, or SmartApps, and the SmartThings API. When a SmartApp is called by the SmartThings platform, it is sent an authorization token that can be used to interact with the SmartThings API.  Personal access tokens are used to interact with the API for non-SmartApp use cases. They can be created and managed on the [personal access tokens page](https://account.smartthings.com/tokens).  ## OAuth2 scopes  Operations may be protected by one or more OAuth security schemes, which specify the required permissions. Each scope for a given scheme is required. If multiple schemes are specified (not common), you may use either scheme.  SmartApp token scopes are derived from the permissions requested by the SmartApp and granted by the end-user during installation. Personal access token scopes are associated with the specific permissions authorized when creating them.  Scopes are generally in the form `permission:entity-type:entity-id`.  **An `*` used for the `entity-id` specifies that the permission may be applied to all entities that the token type has access to, or may be replaced with a specific ID.**  For more information about authrization and permissions, please see the [Authorization and permissions guide](https://smartthings.developer.samsung.com/develop/guides/smartapps/auth-and-permissions.html).  <!- - ReDoc-Inject: <security-definitions> - ->  # Errors  The SmartThings API uses conventional HTTP response codes to indicate the success or failure of a request. In general, a `2XX` response code indicates success, a `4XX` response code indicates an error given the inputs for the request, and a `5XX` response code indicates a failure on the SmartThings platform.  API errors will contain a JSON response body with more information about the error:  ```json {   \"requestId\": \"031fec1a-f19f-470a-a7da-710569082846\"   \"error\": {     \"code\": \"ConstraintViolationError\",     \"message\": \"Validation errors occurred while process your request.\",     \"details\": [       { \"code\": \"PatternError\", \"target\": \"latitude\", \"message\": \"Invalid format.\" },       { \"code\": \"SizeError\", \"target\": \"name\", \"message\": \"Too small.\" },       { \"code\": \"SizeError\", \"target\": \"description\", \"message\": \"Too big.\" }     ]   } } ```  ## Error Response Body  The error response attributes are:  | Property | Type | Required | Description | | - -- | - -- | - -- | - -- | | requestId | String | No | A request identifier that can be used to correlate an error to additional logging on the SmartThings servers. | error | Error | **Yes** | The Error object, documented below.  ## Error Object  The Error object contains the following attributes:  | Property | Type | Required | Description | | - -- | - -- | - -- | - -- | | code | String | **Yes** | A SmartThings-defined error code that serves as a more specific indicator of the error than the HTTP error code specified in the response. See [SmartThings Error Codes](#section/Errors/SmartThings-Error-Codes) for more information. | message | String | **Yes** | A description of the error, intended to aid developers in debugging of error responses. | target | String | No | The target of the particular error. For example, it could be the name of the property that caused the error. | details | Error[] | No | An array of Error objects that typically represent distinct, related errors that occurred during the request. As an optional attribute, this may be null or an empty array.  ## Standard HTTP Error Codes  The following table lists the most common HTTP error response:  | Code | Name | Description | | - -- | - -- | - -- | | 400 | Bad Request | The client has issued an invalid request. This is commonly used to specify validation errors in a request payload. | 401 | Unauthorized | Authorization for the API is required, but the request has not been authenticated. | 403 | Forbidden | The request has been authenticated but does not have appropriate permissions, or a requested resource is not found. | 404 | Not Found | Specifies the requested path does not exist. | 406 | Not Acceptable | The client has requested a MIME type via the Accept header for a value not supported by the server. | 415 | Unsupported Media Type | The client has defined a contentType header that is not supported by the server. | 422 | Unprocessable Entity | The client has made a valid request, but the server cannot process it. This is often used for APIs for which certain limits have been exceeded. | 429 | Too Many Requests | The client has exceeded the number of requests allowed for a given time window. | 500 | Internal Server Error | An unexpected error on the SmartThings servers has occurred. These errors should be rare. | 501 | Not Implemented | The client request was valid and understood by the server, but the requested feature has yet to be implemented. These errors should be rare.  ## SmartThings Error Codes  SmartThings specifies several standard custom error codes. These provide more information than the standard HTTP error response codes. The following table lists the standard SmartThings error codes and their description:  | Code | Typical HTTP Status Codes | Description | | - -- | - -- | - -- | | PatternError | 400, 422 | The client has provided input that does not match the expected pattern. | ConstraintViolationError | 422 | The client has provided input that has violated one or more constraints. | NotNullError | 422 | The client has provided a null input for a field that is required to be non-null. | NullError | 422 | The client has provided an input for a field that is required to be null. | NotEmptyError | 422 | The client has provided an empty input for a field that is required to be non-empty. | SizeError | 400, 422 | The client has provided a value that does not meet size restrictions. | Unexpected Error | 500 | A non-recoverable error condition has occurred. Indicates a problem occurred on the SmartThings server that is no fault of the client. | UnprocessableEntityError | 422 | The client has sent a malformed request body. | TooManyRequestError | 429 | The client issued too many requests too quickly. | LimitError | 422 | The client has exceeded certain limits an API enforces. | UnsupportedOperationError | 400, 422 | The client has issued a request to a feature that currently isn't supported by the SmartThings platform. These should be rare.  ## Custom Error Codes  An API may define its own error codes where appropriate. These custom error codes are documented as part of that specific API's documentation.  # Warnings The SmartThings API issues warning messages via standard HTTP Warning headers. These messages do not represent a request failure, but provide additional information that the requester might want to act upon. For instance a warning will be issued if you are using an old API version.  # API Versions  The SmartThings API supports both path and header-based versioning. The following are equivalent:  - https://api.smartthings.com/v1/locations - https://api.smartthings.com/locations with header `Accept: application/vnd.smartthings+json;v=1`  Currently, only version 1 is available.  # Paging  Operations that return a list of objects return a paginated response. The `_links` object contains the items returned, and links to the next and previous result page, if applicable.  ```json {   \"items\": [     {       \"locationId\": \"6b3d1909-1e1c-43ec-adc2-5f941de4fbf9\",       \"name\": \"Home\"     },     {       \"locationId\": \"6b3d1909-1e1c-43ec-adc2-5f94d6g4fbf9\",       \"name\": \"Work\"     }     ....   ],   \"_links\": {     \"next\": {       \"href\": \"https://api.smartthings.com/v1/locations?page=3\"     },     \"previous\": {       \"href\": \"https://api.smartthings.com/v1/locations?page=1\"     }   } } ```  # Localization  Some SmartThings API's support localization. Specific information regarding localization endpoints are documented in the API itself. However, the following should apply to all endpoints that support localization.  ## Fallback Patterns  When making a request with the `Accept-Language` header, this fallback pattern is observed. * Response will be translated with exact locale tag. * If a translation does not exist for the requested language and region, the translation for the language will be returned. * If a translation does not exist for the language, English (en) will be returned. * Finally, an untranslated response will be returned in the absense of the above translations.  ## Accept-Language Header The format of the `Accept-Language` header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5)  ## Content-Language The `Content-Language` header should be set on the response from the server to indicate which translation was given back to the client. The absense of the header indicates that the server did not recieve a request with the `Accept-Language` header set. 
 *
 * The version of the OpenAPI document: 1.0-PREVIEW
 * 
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mime;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace SmartThingsNet.Api
{

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IDeviceprofilesApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Create a device profile
        /// </summary>
        /// <remarks>
        /// Create a device profile.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="request">The device profile to be created. (optional)</param>
        /// <returns>DeviceProfile</returns>
        DeviceProfile CreateDeviceProfile (CreateDeviceProfileRequest request = default(CreateDeviceProfileRequest));

        /// <summary>
        /// Create a device profile
        /// </summary>
        /// <remarks>
        /// Create a device profile.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="request">The device profile to be created. (optional)</param>
        /// <returns>ApiResponse of DeviceProfile</returns>
        ApiResponse<DeviceProfile> CreateDeviceProfileWithHttpInfo (CreateDeviceProfileRequest request = default(CreateDeviceProfileRequest));
        /// <summary>
        /// Delete a device profile
        /// </summary>
        /// <remarks>
        /// Delete a device profile by ID. Admin use only.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <returns>Object</returns>
        Object DeleteDeviceProfile (string deviceProfileId);

        /// <summary>
        /// Delete a device profile
        /// </summary>
        /// <remarks>
        /// Delete a device profile by ID. Admin use only.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <returns>ApiResponse of Object</returns>
        ApiResponse<Object> DeleteDeviceProfileWithHttpInfo (string deviceProfileId);
        /// <summary>
        /// GET a device profile
        /// </summary>
        /// <remarks>
        /// GET a device profile.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <returns>DeviceProfile</returns>
        DeviceProfile GetDeviceProfile (string deviceProfileId, string acceptLanguage = default(string));

        /// <summary>
        /// GET a device profile
        /// </summary>
        /// <remarks>
        /// GET a device profile.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <returns>ApiResponse of DeviceProfile</returns>
        ApiResponse<DeviceProfile> GetDeviceProfileWithHttpInfo (string deviceProfileId, string acceptLanguage = default(string));
        /// <summary>
        /// List all device profiles for the authenticated user
        /// </summary>
        /// <remarks>
        /// List device profiles.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="profileId">The device profiles IDs to filter the results by.  (optional)</param>
        /// <returns>PagedDeviceProfiles</returns>
        PagedDeviceProfiles ListDeviceProfiles (List<string> profileId = default(List<string>));

        /// <summary>
        /// List all device profiles for the authenticated user
        /// </summary>
        /// <remarks>
        /// List device profiles.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="profileId">The device profiles IDs to filter the results by.  (optional)</param>
        /// <returns>ApiResponse of PagedDeviceProfiles</returns>
        ApiResponse<PagedDeviceProfiles> ListDeviceProfilesWithHttpInfo (List<string> profileId = default(List<string>));
        /// <summary>
        /// Update a device profile.
        /// </summary>
        /// <remarks>
        /// Update a device profile. The device profile has to be in development status
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="request">The device profile to be updated. (optional)</param>
        /// <returns>DeviceProfile</returns>
        DeviceProfile UpdateDeviceProfile (string deviceProfileId, UpdateDeviceProfileRequest request = default(UpdateDeviceProfileRequest));

        /// <summary>
        /// Update a device profile.
        /// </summary>
        /// <remarks>
        /// Update a device profile. The device profile has to be in development status
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="request">The device profile to be updated. (optional)</param>
        /// <returns>ApiResponse of DeviceProfile</returns>
        ApiResponse<DeviceProfile> UpdateDeviceProfileWithHttpInfo (string deviceProfileId, UpdateDeviceProfileRequest request = default(UpdateDeviceProfileRequest));
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IDeviceprofilesApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Create a device profile
        /// </summary>
        /// <remarks>
        /// Create a device profile.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="request">The device profile to be created. (optional)</param>
        /// <returns>Task of DeviceProfile</returns>
        System.Threading.Tasks.Task<DeviceProfile> CreateDeviceProfileAsync (CreateDeviceProfileRequest request = default(CreateDeviceProfileRequest));

        /// <summary>
        /// Create a device profile
        /// </summary>
        /// <remarks>
        /// Create a device profile.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="request">The device profile to be created. (optional)</param>
        /// <returns>Task of ApiResponse (DeviceProfile)</returns>
        System.Threading.Tasks.Task<ApiResponse<DeviceProfile>> CreateDeviceProfileAsyncWithHttpInfo (CreateDeviceProfileRequest request = default(CreateDeviceProfileRequest));
        /// <summary>
        /// Delete a device profile
        /// </summary>
        /// <remarks>
        /// Delete a device profile by ID. Admin use only.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <returns>Task of Object</returns>
        System.Threading.Tasks.Task<Object> DeleteDeviceProfileAsync (string deviceProfileId);

        /// <summary>
        /// Delete a device profile
        /// </summary>
        /// <remarks>
        /// Delete a device profile by ID. Admin use only.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <returns>Task of ApiResponse (Object)</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteDeviceProfileAsyncWithHttpInfo (string deviceProfileId);
        /// <summary>
        /// GET a device profile
        /// </summary>
        /// <remarks>
        /// GET a device profile.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <returns>Task of DeviceProfile</returns>
        System.Threading.Tasks.Task<DeviceProfile> GetDeviceProfileAsync (string deviceProfileId, string acceptLanguage = default(string));

        /// <summary>
        /// GET a device profile
        /// </summary>
        /// <remarks>
        /// GET a device profile.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <returns>Task of ApiResponse (DeviceProfile)</returns>
        System.Threading.Tasks.Task<ApiResponse<DeviceProfile>> GetDeviceProfileAsyncWithHttpInfo (string deviceProfileId, string acceptLanguage = default(string));
        /// <summary>
        /// List all device profiles for the authenticated user
        /// </summary>
        /// <remarks>
        /// List device profiles.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="profileId">The device profiles IDs to filter the results by.  (optional)</param>
        /// <returns>Task of PagedDeviceProfiles</returns>
        System.Threading.Tasks.Task<PagedDeviceProfiles> ListDeviceProfilesAsync (List<string> profileId = default(List<string>));

        /// <summary>
        /// List all device profiles for the authenticated user
        /// </summary>
        /// <remarks>
        /// List device profiles.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="profileId">The device profiles IDs to filter the results by.  (optional)</param>
        /// <returns>Task of ApiResponse (PagedDeviceProfiles)</returns>
        System.Threading.Tasks.Task<ApiResponse<PagedDeviceProfiles>> ListDeviceProfilesAsyncWithHttpInfo (List<string> profileId = default(List<string>));
        /// <summary>
        /// Update a device profile.
        /// </summary>
        /// <remarks>
        /// Update a device profile. The device profile has to be in development status
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="request">The device profile to be updated. (optional)</param>
        /// <returns>Task of DeviceProfile</returns>
        System.Threading.Tasks.Task<DeviceProfile> UpdateDeviceProfileAsync (string deviceProfileId, UpdateDeviceProfileRequest request = default(UpdateDeviceProfileRequest));

        /// <summary>
        /// Update a device profile.
        /// </summary>
        /// <remarks>
        /// Update a device profile. The device profile has to be in development status
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="request">The device profile to be updated. (optional)</param>
        /// <returns>Task of ApiResponse (DeviceProfile)</returns>
        System.Threading.Tasks.Task<ApiResponse<DeviceProfile>> UpdateDeviceProfileAsyncWithHttpInfo (string deviceProfileId, UpdateDeviceProfileRequest request = default(UpdateDeviceProfileRequest));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IDeviceprofilesApi : IDeviceprofilesApiSync, IDeviceprofilesApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class DeviceProfilesApi : IDeviceprofilesApi
    {
        private SmartThingsNet.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceProfilesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public DeviceProfilesApi() : this((string) null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceProfilesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public DeviceProfilesApi(String basePath)
        {
            this.Configuration = SmartThingsNet.Client.Configuration.MergeConfigurations(
                SmartThingsNet.Client.GlobalConfiguration.Instance,
                new SmartThingsNet.Client.Configuration { BasePath = basePath }
            );
            this.Client = new SmartThingsNet.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new SmartThingsNet.Client.ApiClient(this.Configuration.BasePath);
            this.ExceptionFactory = SmartThingsNet.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceProfilesApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public DeviceProfilesApi(SmartThingsNet.Client.Configuration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Configuration = SmartThingsNet.Client.Configuration.MergeConfigurations(
                SmartThingsNet.Client.GlobalConfiguration.Instance,
                configuration
            );
            this.Client = new SmartThingsNet.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new SmartThingsNet.Client.ApiClient(this.Configuration.BasePath);
            ExceptionFactory = SmartThingsNet.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceProfilesApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public DeviceProfilesApi(SmartThingsNet.Client.ISynchronousClient client,SmartThingsNet.Client.IAsynchronousClient asyncClient, SmartThingsNet.Client.IReadableConfiguration configuration)
        {
            if(client == null) throw new ArgumentNullException("client");
            if(asyncClient == null) throw new ArgumentNullException("asyncClient");
            if(configuration == null) throw new ArgumentNullException("configuration");

            this.Client = client;
            this.AsynchronousClient = asyncClient;
            this.Configuration = configuration;
            this.ExceptionFactory = SmartThingsNet.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// The client for accessing this underlying API asynchronously.
        /// </summary>
        public SmartThingsNet.Client.IAsynchronousClient AsynchronousClient { get; set; }

        /// <summary>
        /// The client for accessing this underlying API synchronously.
        /// </summary>
        public SmartThingsNet.Client.ISynchronousClient Client { get; set; }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public String GetBasePath()
        {
            return this.Configuration.BasePath;
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public SmartThingsNet.Client.IReadableConfiguration Configuration {get; set;}

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public SmartThingsNet.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        /// Create a device profile Create a device profile.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="request">The device profile to be created. (optional)</param>
        /// <returns>DeviceProfile</returns>
        public DeviceProfile CreateDeviceProfile (CreateDeviceProfileRequest request = default(CreateDeviceProfileRequest))
        {
             SmartThingsNet.Client.ApiResponse<DeviceProfile> localVarResponse = CreateDeviceProfileWithHttpInfo(request);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Create a device profile Create a device profile.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="request">The device profile to be created. (optional)</param>
        /// <returns>ApiResponse of DeviceProfile</returns>
        public SmartThingsNet.Client.ApiResponse< DeviceProfile > CreateDeviceProfileWithHttpInfo (CreateDeviceProfileRequest request = default(CreateDeviceProfileRequest))
        {
            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            
            localVarRequestOptions.Data = request;

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post< DeviceProfile >("/deviceprofiles", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateDeviceProfile", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create a device profile Create a device profile.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="request">The device profile to be created. (optional)</param>
        /// <returns>Task of DeviceProfile</returns>
        public async System.Threading.Tasks.Task<DeviceProfile> CreateDeviceProfileAsync (CreateDeviceProfileRequest request = default(CreateDeviceProfileRequest))
        {
             SmartThingsNet.Client.ApiResponse<DeviceProfile> localVarResponse = await CreateDeviceProfileAsyncWithHttpInfo(request);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Create a device profile Create a device profile.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="request">The device profile to be created. (optional)</param>
        /// <returns>Task of ApiResponse (DeviceProfile)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<DeviceProfile>> CreateDeviceProfileAsyncWithHttpInfo (CreateDeviceProfileRequest request = default(CreateDeviceProfileRequest))
        {
            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };
            
            foreach (var _contentType in _contentTypes)
                localVarRequestOptions.HeaderParameters.Add("Content-Type", _contentType);
            
            foreach (var _accept in _accepts)
                localVarRequestOptions.HeaderParameters.Add("Accept", _accept);
            
            
            localVarRequestOptions.Data = request;

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<DeviceProfile>("/deviceprofiles", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateDeviceProfile", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete a device profile Delete a device profile by ID. Admin use only.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <returns>Object</returns>
        public Object DeleteDeviceProfile (string deviceProfileId)
        {
             SmartThingsNet.Client.ApiResponse<Object> localVarResponse = DeleteDeviceProfileWithHttpInfo(deviceProfileId);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Delete a device profile Delete a device profile by ID. Admin use only.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <returns>ApiResponse of Object</returns>
        public SmartThingsNet.Client.ApiResponse< Object > DeleteDeviceProfileWithHttpInfo (string deviceProfileId)
        {
            // verify the required parameter 'deviceProfileId' is set
            if (deviceProfileId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceProfileId' when calling DeviceprofilesApi->DeleteDeviceProfile");

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("deviceProfileId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceProfileId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete< Object >("/deviceprofiles/{deviceProfileId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteDeviceProfile", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete a device profile Delete a device profile by ID. Admin use only.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <returns>Task of Object</returns>
        public async System.Threading.Tasks.Task<Object> DeleteDeviceProfileAsync (string deviceProfileId)
        {
             SmartThingsNet.Client.ApiResponse<Object> localVarResponse = await DeleteDeviceProfileAsyncWithHttpInfo(deviceProfileId);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Delete a device profile Delete a device profile by ID. Admin use only.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <returns>Task of ApiResponse (Object)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Object>> DeleteDeviceProfileAsyncWithHttpInfo (string deviceProfileId)
        {
            // verify the required parameter 'deviceProfileId' is set
            if (deviceProfileId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceProfileId' when calling DeviceprofilesApi->DeleteDeviceProfile");


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };
            
            foreach (var _contentType in _contentTypes)
                localVarRequestOptions.HeaderParameters.Add("Content-Type", _contentType);
            
            foreach (var _accept in _accepts)
                localVarRequestOptions.HeaderParameters.Add("Accept", _accept);
            
            localVarRequestOptions.PathParameters.Add("deviceProfileId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceProfileId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/deviceprofiles/{deviceProfileId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteDeviceProfile", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// GET a device profile GET a device profile.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <returns>DeviceProfile</returns>
        public DeviceProfile GetDeviceProfile (string deviceProfileId, string acceptLanguage = default(string))
        {
             SmartThingsNet.Client.ApiResponse<DeviceProfile> localVarResponse = GetDeviceProfileWithHttpInfo(deviceProfileId, acceptLanguage);
             return localVarResponse.Data;
        }

        /// <summary>
        /// GET a device profile GET a device profile.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <returns>ApiResponse of DeviceProfile</returns>
        public SmartThingsNet.Client.ApiResponse< DeviceProfile > GetDeviceProfileWithHttpInfo (string deviceProfileId, string acceptLanguage = default(string))
        {
            // verify the required parameter 'deviceProfileId' is set
            if (deviceProfileId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceProfileId' when calling DeviceprofilesApi->GetDeviceProfile");

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("deviceProfileId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceProfileId)); // path parameter
            
            if (acceptLanguage != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept-Language", SmartThingsNet.Client.ClientUtils.ParameterToString(acceptLanguage)); // header parameter
            }

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get< DeviceProfile >("/deviceprofiles/{deviceProfileId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDeviceProfile", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// GET a device profile GET a device profile.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <returns>Task of DeviceProfile</returns>
        public async System.Threading.Tasks.Task<DeviceProfile> GetDeviceProfileAsync (string deviceProfileId, string acceptLanguage = default(string))
        {
             SmartThingsNet.Client.ApiResponse<DeviceProfile> localVarResponse = await GetDeviceProfileAsyncWithHttpInfo(deviceProfileId, acceptLanguage);
             return localVarResponse.Data;

        }

        /// <summary>
        /// GET a device profile GET a device profile.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <returns>Task of ApiResponse (DeviceProfile)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<DeviceProfile>> GetDeviceProfileAsyncWithHttpInfo (string deviceProfileId, string acceptLanguage = default(string))
        {
            // verify the required parameter 'deviceProfileId' is set
            if (deviceProfileId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceProfileId' when calling DeviceprofilesApi->GetDeviceProfile");


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };
            
            foreach (var _contentType in _contentTypes)
                localVarRequestOptions.HeaderParameters.Add("Content-Type", _contentType);
            
            foreach (var _accept in _accepts)
                localVarRequestOptions.HeaderParameters.Add("Accept", _accept);
            
            localVarRequestOptions.PathParameters.Add("deviceProfileId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceProfileId)); // path parameter
            
            if (acceptLanguage != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept-Language", SmartThingsNet.Client.ClientUtils.ParameterToString(acceptLanguage)); // header parameter
            }

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<DeviceProfile>("/deviceprofiles/{deviceProfileId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDeviceProfile", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List all device profiles for the authenticated user List device profiles.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="profileId">The device profiles IDs to filter the results by.  (optional)</param>
        /// <returns>PagedDeviceProfiles</returns>
        public PagedDeviceProfiles ListDeviceProfiles (List<string> profileId = default(List<string>))
        {
             SmartThingsNet.Client.ApiResponse<PagedDeviceProfiles> localVarResponse = ListDeviceProfilesWithHttpInfo(profileId);
             return localVarResponse.Data;
        }

        /// <summary>
        /// List all device profiles for the authenticated user List device profiles.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="profileId">The device profiles IDs to filter the results by.  (optional)</param>
        /// <returns>ApiResponse of PagedDeviceProfiles</returns>
        public SmartThingsNet.Client.ApiResponse< PagedDeviceProfiles > ListDeviceProfilesWithHttpInfo (List<string> profileId = default(List<string>))
        {
            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            if (profileId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("multi", "profileId", profileId));
            }
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get< PagedDeviceProfiles >("/deviceprofiles", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListDeviceProfiles", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List all device profiles for the authenticated user List device profiles.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="profileId">The device profiles IDs to filter the results by.  (optional)</param>
        /// <returns>Task of PagedDeviceProfiles</returns>
        public async System.Threading.Tasks.Task<PagedDeviceProfiles> ListDeviceProfilesAsync (List<string> profileId = default(List<string>))
        {
             SmartThingsNet.Client.ApiResponse<PagedDeviceProfiles> localVarResponse = await ListDeviceProfilesAsyncWithHttpInfo(profileId);
             return localVarResponse.Data;

        }

        /// <summary>
        /// List all device profiles for the authenticated user List device profiles.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="profileId">The device profiles IDs to filter the results by.  (optional)</param>
        /// <returns>Task of ApiResponse (PagedDeviceProfiles)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<PagedDeviceProfiles>> ListDeviceProfilesAsyncWithHttpInfo (List<string> profileId = default(List<string>))
        {
            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };
            
            foreach (var _contentType in _contentTypes)
                localVarRequestOptions.HeaderParameters.Add("Content-Type", _contentType);
            
            foreach (var _accept in _accepts)
                localVarRequestOptions.HeaderParameters.Add("Accept", _accept);
            
            if (profileId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("multi", "profileId", profileId));
            }
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<PagedDeviceProfiles>("/deviceprofiles", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListDeviceProfiles", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update a device profile. Update a device profile. The device profile has to be in development status
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="request">The device profile to be updated. (optional)</param>
        /// <returns>DeviceProfile</returns>
        public DeviceProfile UpdateDeviceProfile (string deviceProfileId, UpdateDeviceProfileRequest request = default(UpdateDeviceProfileRequest))
        {
             SmartThingsNet.Client.ApiResponse<DeviceProfile> localVarResponse = UpdateDeviceProfileWithHttpInfo(deviceProfileId, request);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Update a device profile. Update a device profile. The device profile has to be in development status
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="request">The device profile to be updated. (optional)</param>
        /// <returns>ApiResponse of DeviceProfile</returns>
        public SmartThingsNet.Client.ApiResponse< DeviceProfile > UpdateDeviceProfileWithHttpInfo (string deviceProfileId, UpdateDeviceProfileRequest request = default(UpdateDeviceProfileRequest))
        {
            // verify the required parameter 'deviceProfileId' is set
            if (deviceProfileId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceProfileId' when calling DeviceprofilesApi->UpdateDeviceProfile");

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("deviceProfileId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceProfileId)); // path parameter
            
            localVarRequestOptions.Data = request;

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put< DeviceProfile >("/deviceprofiles/{deviceProfileId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateDeviceProfile", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update a device profile. Update a device profile. The device profile has to be in development status
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="request">The device profile to be updated. (optional)</param>
        /// <returns>Task of DeviceProfile</returns>
        public async System.Threading.Tasks.Task<DeviceProfile> UpdateDeviceProfileAsync (string deviceProfileId, UpdateDeviceProfileRequest request = default(UpdateDeviceProfileRequest))
        {
             SmartThingsNet.Client.ApiResponse<DeviceProfile> localVarResponse = await UpdateDeviceProfileAsyncWithHttpInfo(deviceProfileId, request);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Update a device profile. Update a device profile. The device profile has to be in development status
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="request">The device profile to be updated. (optional)</param>
        /// <returns>Task of ApiResponse (DeviceProfile)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<DeviceProfile>> UpdateDeviceProfileAsyncWithHttpInfo (string deviceProfileId, UpdateDeviceProfileRequest request = default(UpdateDeviceProfileRequest))
        {
            // verify the required parameter 'deviceProfileId' is set
            if (deviceProfileId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceProfileId' when calling DeviceprofilesApi->UpdateDeviceProfile");


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };
            
            foreach (var _contentType in _contentTypes)
                localVarRequestOptions.HeaderParameters.Add("Content-Type", _contentType);
            
            foreach (var _accept in _accepts)
                localVarRequestOptions.HeaderParameters.Add("Accept", _accept);
            
            localVarRequestOptions.PathParameters.Add("deviceProfileId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceProfileId)); // path parameter
            
            localVarRequestOptions.Data = request;

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PutAsync<DeviceProfile>("/deviceprofiles/{deviceProfileId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateDeviceProfile", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

    }
}
