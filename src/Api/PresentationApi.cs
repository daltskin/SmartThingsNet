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
    public interface IPresentationApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Create a device configuration
        /// </summary>
        /// <remarks>
        /// Make an idempotent call to either create or get a device configuration based on the structure of the provided payload Note: This API functionality is in BETA 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="request">The device configuration to be created (optional)</param>
        /// <returns>DeviceConfiguration</returns>
        DeviceConfiguration CreateDeviceConfiguration (CreateDeviceConfigRequest request = default(CreateDeviceConfigRequest));

        /// <summary>
        /// Create a device configuration
        /// </summary>
        /// <remarks>
        /// Make an idempotent call to either create or get a device configuration based on the structure of the provided payload Note: This API functionality is in BETA 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="request">The device configuration to be created (optional)</param>
        /// <returns>ApiResponse of DeviceConfiguration</returns>
        ApiResponse<DeviceConfiguration> CreateDeviceConfigurationWithHttpInfo (CreateDeviceConfigRequest request = default(CreateDeviceConfigRequest));
        /// <summary>
        /// Generate device config from a Device Profile or DTH
        /// </summary>
        /// <remarks>
        /// Examines the profile of the device and constructs a default device configuration. Note: This API functionality is in BETA 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="typeIntegrationId">Device Profile or DTH ID. Assumes profile if the typeIntegration parameter is not set.</param>
        /// <param name="typeIntegration">Represents the way that the provided device type is formatted, either in the form of a \&quot;dth\&quot; or a \&quot;profile\&quot; (optional, default to profile)</param>
        /// <param name="typeShardId">Data Management Shard ID where the device type resides. Only useful for &#x60;DTH&#x60; type integrations. Example: &#x60;na04&#x60; (optional)</param>
        /// <returns>DeviceConfiguration</returns>
        DeviceConfiguration GenerateDeviceConfig (string typeIntegrationId, string typeIntegration = default(string), string typeShardId = default(string));

        /// <summary>
        /// Generate device config from a Device Profile or DTH
        /// </summary>
        /// <remarks>
        /// Examines the profile of the device and constructs a default device configuration. Note: This API functionality is in BETA 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="typeIntegrationId">Device Profile or DTH ID. Assumes profile if the typeIntegration parameter is not set.</param>
        /// <param name="typeIntegration">Represents the way that the provided device type is formatted, either in the form of a \&quot;dth\&quot; or a \&quot;profile\&quot; (optional, default to profile)</param>
        /// <param name="typeShardId">Data Management Shard ID where the device type resides. Only useful for &#x60;DTH&#x60; type integrations. Example: &#x60;na04&#x60; (optional)</param>
        /// <returns>ApiResponse of DeviceConfiguration</returns>
        ApiResponse<DeviceConfiguration> GenerateDeviceConfigWithHttpInfo (string typeIntegrationId, string typeIntegration = default(string), string typeShardId = default(string));
        /// <summary>
        /// Get a device configuration
        /// </summary>
        /// <remarks>
        /// Get a device configuration. Note: This API functionality is in BETA 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="vid">System generated identifier that corresponds to a device presentation. (optional)</param>
        /// <returns>DeviceConfiguration</returns>
        DeviceConfiguration GetDeviceConfiguration (string vid = default(string));

        /// <summary>
        /// Get a device configuration
        /// </summary>
        /// <remarks>
        /// Get a device configuration. Note: This API functionality is in BETA 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="vid">System generated identifier that corresponds to a device presentation. (optional)</param>
        /// <returns>ApiResponse of DeviceConfiguration</returns>
        ApiResponse<DeviceConfiguration> GetDeviceConfigurationWithHttpInfo (string vid = default(string));
        /// <summary>
        /// Get a device presentation
        /// </summary>
        /// <remarks>
        /// Get a device presentation. If MNMN is omitted we will assume the default &#x60;SmartThingsCommunity&#x60; mnmn. Note: This API functionality is in BETA 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="vid">System generated identifier that corresponds to a device presentation. (optional)</param>
        /// <param name="mnmn">Secondary namespacing key for grouping presentations, traditionally specified as \&quot;Manufacturer name\&quot; (optional)</param>
        /// <param name="deviceId">The ID of a device for which we want to load the device presentation. If the device ID is provided, no other fields are required. (optional)</param>
        /// <param name="ifNoneMatch">The ETag for the request. (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <returns>DevicePresentation</returns>
        DevicePresentation GetDevicePresentation (string vid = default(string), string mnmn = default(string), string deviceId = default(string), string ifNoneMatch = default(string), string acceptLanguage = default(string));

        /// <summary>
        /// Get a device presentation
        /// </summary>
        /// <remarks>
        /// Get a device presentation. If MNMN is omitted we will assume the default &#x60;SmartThingsCommunity&#x60; mnmn. Note: This API functionality is in BETA 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="vid">System generated identifier that corresponds to a device presentation. (optional)</param>
        /// <param name="mnmn">Secondary namespacing key for grouping presentations, traditionally specified as \&quot;Manufacturer name\&quot; (optional)</param>
        /// <param name="deviceId">The ID of a device for which we want to load the device presentation. If the device ID is provided, no other fields are required. (optional)</param>
        /// <param name="ifNoneMatch">The ETag for the request. (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <returns>ApiResponse of DevicePresentation</returns>
        ApiResponse<DevicePresentation> GetDevicePresentationWithHttpInfo (string vid = default(string), string mnmn = default(string), string deviceId = default(string), string ifNoneMatch = default(string), string acceptLanguage = default(string));
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IPresentationApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Create a device configuration
        /// </summary>
        /// <remarks>
        /// Make an idempotent call to either create or get a device configuration based on the structure of the provided payload Note: This API functionality is in BETA 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="request">The device configuration to be created (optional)</param>
        /// <returns>Task of DeviceConfiguration</returns>
        System.Threading.Tasks.Task<DeviceConfiguration> CreateDeviceConfigurationAsync (CreateDeviceConfigRequest request = default(CreateDeviceConfigRequest));

        /// <summary>
        /// Create a device configuration
        /// </summary>
        /// <remarks>
        /// Make an idempotent call to either create or get a device configuration based on the structure of the provided payload Note: This API functionality is in BETA 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="request">The device configuration to be created (optional)</param>
        /// <returns>Task of ApiResponse (DeviceConfiguration)</returns>
        System.Threading.Tasks.Task<ApiResponse<DeviceConfiguration>> CreateDeviceConfigurationAsyncWithHttpInfo (CreateDeviceConfigRequest request = default(CreateDeviceConfigRequest));
        /// <summary>
        /// Generate device config from a Device Profile or DTH
        /// </summary>
        /// <remarks>
        /// Examines the profile of the device and constructs a default device configuration. Note: This API functionality is in BETA 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="typeIntegrationId">Device Profile or DTH ID. Assumes profile if the typeIntegration parameter is not set.</param>
        /// <param name="typeIntegration">Represents the way that the provided device type is formatted, either in the form of a \&quot;dth\&quot; or a \&quot;profile\&quot; (optional, default to profile)</param>
        /// <param name="typeShardId">Data Management Shard ID where the device type resides. Only useful for &#x60;DTH&#x60; type integrations. Example: &#x60;na04&#x60; (optional)</param>
        /// <returns>Task of DeviceConfiguration</returns>
        System.Threading.Tasks.Task<DeviceConfiguration> GenerateDeviceConfigAsync (string typeIntegrationId, string typeIntegration = default(string), string typeShardId = default(string));

        /// <summary>
        /// Generate device config from a Device Profile or DTH
        /// </summary>
        /// <remarks>
        /// Examines the profile of the device and constructs a default device configuration. Note: This API functionality is in BETA 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="typeIntegrationId">Device Profile or DTH ID. Assumes profile if the typeIntegration parameter is not set.</param>
        /// <param name="typeIntegration">Represents the way that the provided device type is formatted, either in the form of a \&quot;dth\&quot; or a \&quot;profile\&quot; (optional, default to profile)</param>
        /// <param name="typeShardId">Data Management Shard ID where the device type resides. Only useful for &#x60;DTH&#x60; type integrations. Example: &#x60;na04&#x60; (optional)</param>
        /// <returns>Task of ApiResponse (DeviceConfiguration)</returns>
        System.Threading.Tasks.Task<ApiResponse<DeviceConfiguration>> GenerateDeviceConfigAsyncWithHttpInfo (string typeIntegrationId, string typeIntegration = default(string), string typeShardId = default(string));
        /// <summary>
        /// Get a device configuration
        /// </summary>
        /// <remarks>
        /// Get a device configuration. Note: This API functionality is in BETA 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="vid">System generated identifier that corresponds to a device presentation. (optional)</param>
        /// <returns>Task of DeviceConfiguration</returns>
        System.Threading.Tasks.Task<DeviceConfiguration> GetDeviceConfigurationAsync (string vid = default(string));

        /// <summary>
        /// Get a device configuration
        /// </summary>
        /// <remarks>
        /// Get a device configuration. Note: This API functionality is in BETA 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="vid">System generated identifier that corresponds to a device presentation. (optional)</param>
        /// <returns>Task of ApiResponse (DeviceConfiguration)</returns>
        System.Threading.Tasks.Task<ApiResponse<DeviceConfiguration>> GetDeviceConfigurationAsyncWithHttpInfo (string vid = default(string));
        /// <summary>
        /// Get a device presentation
        /// </summary>
        /// <remarks>
        /// Get a device presentation. If MNMN is omitted we will assume the default &#x60;SmartThingsCommunity&#x60; mnmn. Note: This API functionality is in BETA 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="vid">System generated identifier that corresponds to a device presentation. (optional)</param>
        /// <param name="mnmn">Secondary namespacing key for grouping presentations, traditionally specified as \&quot;Manufacturer name\&quot; (optional)</param>
        /// <param name="deviceId">The ID of a device for which we want to load the device presentation. If the device ID is provided, no other fields are required. (optional)</param>
        /// <param name="ifNoneMatch">The ETag for the request. (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <returns>Task of DevicePresentation</returns>
        System.Threading.Tasks.Task<DevicePresentation> GetDevicePresentationAsync (string vid = default(string), string mnmn = default(string), string deviceId = default(string), string ifNoneMatch = default(string), string acceptLanguage = default(string));

        /// <summary>
        /// Get a device presentation
        /// </summary>
        /// <remarks>
        /// Get a device presentation. If MNMN is omitted we will assume the default &#x60;SmartThingsCommunity&#x60; mnmn. Note: This API functionality is in BETA 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="vid">System generated identifier that corresponds to a device presentation. (optional)</param>
        /// <param name="mnmn">Secondary namespacing key for grouping presentations, traditionally specified as \&quot;Manufacturer name\&quot; (optional)</param>
        /// <param name="deviceId">The ID of a device for which we want to load the device presentation. If the device ID is provided, no other fields are required. (optional)</param>
        /// <param name="ifNoneMatch">The ETag for the request. (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <returns>Task of ApiResponse (DevicePresentation)</returns>
        System.Threading.Tasks.Task<ApiResponse<DevicePresentation>> GetDevicePresentationAsyncWithHttpInfo (string vid = default(string), string mnmn = default(string), string deviceId = default(string), string ifNoneMatch = default(string), string acceptLanguage = default(string));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IPresentationApi : IPresentationApiSync, IPresentationApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class PresentationApi : IPresentationApi
    {
        private SmartThingsNet.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="PresentationApi"/> class.
        /// </summary>
        /// <returns></returns>
        public PresentationApi() : this((string) null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PresentationApi"/> class.
        /// </summary>
        /// <returns></returns>
        public PresentationApi(String basePath)
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
        /// Initializes a new instance of the <see cref="PresentationApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public PresentationApi(SmartThingsNet.Client.Configuration configuration)
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
        /// Initializes a new instance of the <see cref="PresentationApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public PresentationApi(SmartThingsNet.Client.ISynchronousClient client,SmartThingsNet.Client.IAsynchronousClient asyncClient, SmartThingsNet.Client.IReadableConfiguration configuration)
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
        /// Create a device configuration Make an idempotent call to either create or get a device configuration based on the structure of the provided payload Note: This API functionality is in BETA 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="request">The device configuration to be created (optional)</param>
        /// <returns>DeviceConfiguration</returns>
        public DeviceConfiguration CreateDeviceConfiguration (CreateDeviceConfigRequest request = default(CreateDeviceConfigRequest))
        {
             SmartThingsNet.Client.ApiResponse<DeviceConfiguration> localVarResponse = CreateDeviceConfigurationWithHttpInfo(request);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Create a device configuration Make an idempotent call to either create or get a device configuration based on the structure of the provided payload Note: This API functionality is in BETA 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="request">The device configuration to be created (optional)</param>
        /// <returns>ApiResponse of DeviceConfiguration</returns>
        public SmartThingsNet.Client.ApiResponse< DeviceConfiguration > CreateDeviceConfigurationWithHttpInfo (CreateDeviceConfigRequest request = default(CreateDeviceConfigRequest))
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
            var localVarResponse = this.Client.Post< DeviceConfiguration >("/presentation/deviceconfig", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateDeviceConfiguration", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create a device configuration Make an idempotent call to either create or get a device configuration based on the structure of the provided payload Note: This API functionality is in BETA 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="request">The device configuration to be created (optional)</param>
        /// <returns>Task of DeviceConfiguration</returns>
        public async System.Threading.Tasks.Task<DeviceConfiguration> CreateDeviceConfigurationAsync (CreateDeviceConfigRequest request = default(CreateDeviceConfigRequest))
        {
             SmartThingsNet.Client.ApiResponse<DeviceConfiguration> localVarResponse = await CreateDeviceConfigurationAsyncWithHttpInfo(request);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Create a device configuration Make an idempotent call to either create or get a device configuration based on the structure of the provided payload Note: This API functionality is in BETA 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="request">The device configuration to be created (optional)</param>
        /// <returns>Task of ApiResponse (DeviceConfiguration)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<DeviceConfiguration>> CreateDeviceConfigurationAsyncWithHttpInfo (CreateDeviceConfigRequest request = default(CreateDeviceConfigRequest))
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

            var localVarResponse = await this.AsynchronousClient.PostAsync<DeviceConfiguration>("/presentation/deviceconfig", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateDeviceConfiguration", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Generate device config from a Device Profile or DTH Examines the profile of the device and constructs a default device configuration. Note: This API functionality is in BETA 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="typeIntegrationId">Device Profile or DTH ID. Assumes profile if the typeIntegration parameter is not set.</param>
        /// <param name="typeIntegration">Represents the way that the provided device type is formatted, either in the form of a \&quot;dth\&quot; or a \&quot;profile\&quot; (optional, default to profile)</param>
        /// <param name="typeShardId">Data Management Shard ID where the device type resides. Only useful for &#x60;DTH&#x60; type integrations. Example: &#x60;na04&#x60; (optional)</param>
        /// <returns>DeviceConfiguration</returns>
        public DeviceConfiguration GenerateDeviceConfig (string typeIntegrationId, string typeIntegration = default(string), string typeShardId = default(string))
        {
             SmartThingsNet.Client.ApiResponse<DeviceConfiguration> localVarResponse = GenerateDeviceConfigWithHttpInfo(typeIntegrationId, typeIntegration, typeShardId);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Generate device config from a Device Profile or DTH Examines the profile of the device and constructs a default device configuration. Note: This API functionality is in BETA 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="typeIntegrationId">Device Profile or DTH ID. Assumes profile if the typeIntegration parameter is not set.</param>
        /// <param name="typeIntegration">Represents the way that the provided device type is formatted, either in the form of a \&quot;dth\&quot; or a \&quot;profile\&quot; (optional, default to profile)</param>
        /// <param name="typeShardId">Data Management Shard ID where the device type resides. Only useful for &#x60;DTH&#x60; type integrations. Example: &#x60;na04&#x60; (optional)</param>
        /// <returns>ApiResponse of DeviceConfiguration</returns>
        public SmartThingsNet.Client.ApiResponse< DeviceConfiguration > GenerateDeviceConfigWithHttpInfo (string typeIntegrationId, string typeIntegration = default(string), string typeShardId = default(string))
        {
            // verify the required parameter 'typeIntegrationId' is set
            if (typeIntegrationId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'typeIntegrationId' when calling PresentationApi->GenerateDeviceConfig");

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

            localVarRequestOptions.PathParameters.Add("typeIntegrationId", SmartThingsNet.Client.ClientUtils.ParameterToString(typeIntegrationId)); // path parameter
            if (typeIntegration != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "typeIntegration", typeIntegration));
            }
            if (typeShardId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "typeShardId", typeShardId));
            }

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get< DeviceConfiguration >("/presentation/types/{typeIntegrationId}/deviceconfig", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GenerateDeviceConfig", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Generate device config from a Device Profile or DTH Examines the profile of the device and constructs a default device configuration. Note: This API functionality is in BETA 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="typeIntegrationId">Device Profile or DTH ID. Assumes profile if the typeIntegration parameter is not set.</param>
        /// <param name="typeIntegration">Represents the way that the provided device type is formatted, either in the form of a \&quot;dth\&quot; or a \&quot;profile\&quot; (optional, default to profile)</param>
        /// <param name="typeShardId">Data Management Shard ID where the device type resides. Only useful for &#x60;DTH&#x60; type integrations. Example: &#x60;na04&#x60; (optional)</param>
        /// <returns>Task of DeviceConfiguration</returns>
        public async System.Threading.Tasks.Task<DeviceConfiguration> GenerateDeviceConfigAsync (string typeIntegrationId, string typeIntegration = default(string), string typeShardId = default(string))
        {
             SmartThingsNet.Client.ApiResponse<DeviceConfiguration> localVarResponse = await GenerateDeviceConfigAsyncWithHttpInfo(typeIntegrationId, typeIntegration, typeShardId);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Generate device config from a Device Profile or DTH Examines the profile of the device and constructs a default device configuration. Note: This API functionality is in BETA 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="typeIntegrationId">Device Profile or DTH ID. Assumes profile if the typeIntegration parameter is not set.</param>
        /// <param name="typeIntegration">Represents the way that the provided device type is formatted, either in the form of a \&quot;dth\&quot; or a \&quot;profile\&quot; (optional, default to profile)</param>
        /// <param name="typeShardId">Data Management Shard ID where the device type resides. Only useful for &#x60;DTH&#x60; type integrations. Example: &#x60;na04&#x60; (optional)</param>
        /// <returns>Task of ApiResponse (DeviceConfiguration)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<DeviceConfiguration>> GenerateDeviceConfigAsyncWithHttpInfo (string typeIntegrationId, string typeIntegration = default(string), string typeShardId = default(string))
        {
            // verify the required parameter 'typeIntegrationId' is set
            if (typeIntegrationId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'typeIntegrationId' when calling PresentationApi->GenerateDeviceConfig");


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
            
            localVarRequestOptions.PathParameters.Add("typeIntegrationId", SmartThingsNet.Client.ClientUtils.ParameterToString(typeIntegrationId)); // path parameter
            if (typeIntegration != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "typeIntegration", typeIntegration));
            }
            if (typeShardId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "typeShardId", typeShardId));
            }

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<DeviceConfiguration>("/presentation/types/{typeIntegrationId}/deviceconfig", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GenerateDeviceConfig", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get a device configuration Get a device configuration. Note: This API functionality is in BETA 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="vid">System generated identifier that corresponds to a device presentation. (optional)</param>
        /// <returns>DeviceConfiguration</returns>
        public DeviceConfiguration GetDeviceConfiguration (string vid = default(string))
        {
             SmartThingsNet.Client.ApiResponse<DeviceConfiguration> localVarResponse = GetDeviceConfigurationWithHttpInfo(vid);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Get a device configuration Get a device configuration. Note: This API functionality is in BETA 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="vid">System generated identifier that corresponds to a device presentation. (optional)</param>
        /// <returns>ApiResponse of DeviceConfiguration</returns>
        public SmartThingsNet.Client.ApiResponse< DeviceConfiguration > GetDeviceConfigurationWithHttpInfo (string vid = default(string))
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

            if (vid != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "vid", vid));
            }

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get< DeviceConfiguration >("/presentation/deviceconfig", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDeviceConfiguration", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get a device configuration Get a device configuration. Note: This API functionality is in BETA 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="vid">System generated identifier that corresponds to a device presentation. (optional)</param>
        /// <returns>Task of DeviceConfiguration</returns>
        public async System.Threading.Tasks.Task<DeviceConfiguration> GetDeviceConfigurationAsync (string vid = default(string))
        {
             SmartThingsNet.Client.ApiResponse<DeviceConfiguration> localVarResponse = await GetDeviceConfigurationAsyncWithHttpInfo(vid);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Get a device configuration Get a device configuration. Note: This API functionality is in BETA 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="vid">System generated identifier that corresponds to a device presentation. (optional)</param>
        /// <returns>Task of ApiResponse (DeviceConfiguration)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<DeviceConfiguration>> GetDeviceConfigurationAsyncWithHttpInfo (string vid = default(string))
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
            
            if (vid != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "vid", vid));
            }

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<DeviceConfiguration>("/presentation/deviceconfig", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDeviceConfiguration", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get a device presentation Get a device presentation. If MNMN is omitted we will assume the default &#x60;SmartThingsCommunity&#x60; mnmn. Note: This API functionality is in BETA 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="vid">System generated identifier that corresponds to a device presentation. (optional)</param>
        /// <param name="mnmn">Secondary namespacing key for grouping presentations, traditionally specified as \&quot;Manufacturer name\&quot; (optional)</param>
        /// <param name="deviceId">The ID of a device for which we want to load the device presentation. If the device ID is provided, no other fields are required. (optional)</param>
        /// <param name="ifNoneMatch">The ETag for the request. (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <returns>DevicePresentation</returns>
        public DevicePresentation GetDevicePresentation (string vid = default(string), string mnmn = default(string), string deviceId = default(string), string ifNoneMatch = default(string), string acceptLanguage = default(string))
        {
             SmartThingsNet.Client.ApiResponse<DevicePresentation> localVarResponse = GetDevicePresentationWithHttpInfo(vid, mnmn, deviceId, ifNoneMatch, acceptLanguage);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Get a device presentation Get a device presentation. If MNMN is omitted we will assume the default &#x60;SmartThingsCommunity&#x60; mnmn. Note: This API functionality is in BETA 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="vid">System generated identifier that corresponds to a device presentation. (optional)</param>
        /// <param name="mnmn">Secondary namespacing key for grouping presentations, traditionally specified as \&quot;Manufacturer name\&quot; (optional)</param>
        /// <param name="deviceId">The ID of a device for which we want to load the device presentation. If the device ID is provided, no other fields are required. (optional)</param>
        /// <param name="ifNoneMatch">The ETag for the request. (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <returns>ApiResponse of DevicePresentation</returns>
        public SmartThingsNet.Client.ApiResponse< DevicePresentation > GetDevicePresentationWithHttpInfo (string vid = default(string), string mnmn = default(string), string deviceId = default(string), string ifNoneMatch = default(string), string acceptLanguage = default(string))
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

            if (vid != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "vid", vid));
            }
            if (mnmn != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "mnmn", mnmn));
            }
            if (deviceId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "deviceId", deviceId));
            }
            if (ifNoneMatch != null)
            {
                localVarRequestOptions.HeaderParameters.Add("If-None-Match", SmartThingsNet.Client.ClientUtils.ParameterToString(ifNoneMatch)); // header parameter
            }
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
            var localVarResponse = this.Client.Get< DevicePresentation >("/presentation", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDevicePresentation", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get a device presentation Get a device presentation. If MNMN is omitted we will assume the default &#x60;SmartThingsCommunity&#x60; mnmn. Note: This API functionality is in BETA 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="vid">System generated identifier that corresponds to a device presentation. (optional)</param>
        /// <param name="mnmn">Secondary namespacing key for grouping presentations, traditionally specified as \&quot;Manufacturer name\&quot; (optional)</param>
        /// <param name="deviceId">The ID of a device for which we want to load the device presentation. If the device ID is provided, no other fields are required. (optional)</param>
        /// <param name="ifNoneMatch">The ETag for the request. (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <returns>Task of DevicePresentation</returns>
        public async System.Threading.Tasks.Task<DevicePresentation> GetDevicePresentationAsync (string vid = default(string), string mnmn = default(string), string deviceId = default(string), string ifNoneMatch = default(string), string acceptLanguage = default(string))
        {
             SmartThingsNet.Client.ApiResponse<DevicePresentation> localVarResponse = await GetDevicePresentationAsyncWithHttpInfo(vid, mnmn, deviceId, ifNoneMatch, acceptLanguage);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Get a device presentation Get a device presentation. If MNMN is omitted we will assume the default &#x60;SmartThingsCommunity&#x60; mnmn. Note: This API functionality is in BETA 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="vid">System generated identifier that corresponds to a device presentation. (optional)</param>
        /// <param name="mnmn">Secondary namespacing key for grouping presentations, traditionally specified as \&quot;Manufacturer name\&quot; (optional)</param>
        /// <param name="deviceId">The ID of a device for which we want to load the device presentation. If the device ID is provided, no other fields are required. (optional)</param>
        /// <param name="ifNoneMatch">The ETag for the request. (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <returns>Task of ApiResponse (DevicePresentation)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<DevicePresentation>> GetDevicePresentationAsyncWithHttpInfo (string vid = default(string), string mnmn = default(string), string deviceId = default(string), string ifNoneMatch = default(string), string acceptLanguage = default(string))
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
            
            if (vid != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "vid", vid));
            }
            if (mnmn != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "mnmn", mnmn));
            }
            if (deviceId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "deviceId", deviceId));
            }
            if (ifNoneMatch != null)
            {
                localVarRequestOptions.HeaderParameters.Add("If-None-Match", SmartThingsNet.Client.ClientUtils.ParameterToString(ifNoneMatch)); // header parameter
            }
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

            var localVarResponse = await this.AsynchronousClient.GetAsync<DevicePresentation>("/presentation", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDevicePresentation", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

    }
}
