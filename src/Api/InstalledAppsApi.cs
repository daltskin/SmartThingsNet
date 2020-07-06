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
    public interface IInstalledappsApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Create Installed App events.
        /// </summary>
        /// <remarks>
        /// Create events for an installed app.  This API allows Apps to create events to trigger custom behavior in installed apps. Requires a SmartApp token. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="createInstalledAppEventsRequest"></param>
        /// <returns>Object</returns>
        Object CreateInstalledAppEvents (string installedAppId, CreateInstalledAppEventsRequest createInstalledAppEventsRequest);

        /// <summary>
        /// Create Installed App events.
        /// </summary>
        /// <remarks>
        /// Create events for an installed app.  This API allows Apps to create events to trigger custom behavior in installed apps. Requires a SmartApp token. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="createInstalledAppEventsRequest"></param>
        /// <returns>ApiResponse of Object</returns>
        ApiResponse<Object> CreateInstalledAppEventsWithHttpInfo (string installedAppId, CreateInstalledAppEventsRequest createInstalledAppEventsRequest);
        /// <summary>
        /// Delete an installed app.
        /// </summary>
        /// <remarks>
        /// Delete an Installed App.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <returns>DeleteInstalledAppResponse</returns>
        DeleteInstalledAppResponse DeleteInstallation (string installedAppId);

        /// <summary>
        /// Delete an installed app.
        /// </summary>
        /// <remarks>
        /// Delete an Installed App.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <returns>ApiResponse of DeleteInstalledAppResponse</returns>
        ApiResponse<DeleteInstalledAppResponse> DeleteInstallationWithHttpInfo (string installedAppId);
        /// <summary>
        /// Get an installed app.
        /// </summary>
        /// <remarks>
        /// Fetch a single installed application.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <returns>InstalledApp</returns>
        InstalledApp GetInstallation (string installedAppId);

        /// <summary>
        /// Get an installed app.
        /// </summary>
        /// <remarks>
        /// Fetch a single installed application.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <returns>ApiResponse of InstalledApp</returns>
        ApiResponse<InstalledApp> GetInstallationWithHttpInfo (string installedAppId);
        /// <summary>
        /// Get an installed app configuration.
        /// </summary>
        /// <remarks>
        /// Fetch a detailed install configuration model containing actual config entries / values.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="configurationId">The ID of the install configuration.</param>
        /// <returns>InstallConfigurationDetail</returns>
        InstallConfigurationDetail GetInstallationConfig (string installedAppId, Guid configurationId);

        /// <summary>
        /// Get an installed app configuration.
        /// </summary>
        /// <remarks>
        /// Fetch a detailed install configuration model containing actual config entries / values.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="configurationId">The ID of the install configuration.</param>
        /// <returns>ApiResponse of InstallConfigurationDetail</returns>
        ApiResponse<InstallConfigurationDetail> GetInstallationConfigWithHttpInfo (string installedAppId, Guid configurationId);
        /// <summary>
        /// List an installed app&#39;s configurations.
        /// </summary>
        /// <remarks>
        /// List all configurations potentially filtered by status for an installed app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="configurationStatus">Filter for configuration status. (optional)</param>
        /// <returns>PagedInstallConfigurations</returns>
        PagedInstallConfigurations ListInstallationConfig (string installedAppId, string configurationStatus = default(string));

        /// <summary>
        /// List an installed app&#39;s configurations.
        /// </summary>
        /// <remarks>
        /// List all configurations potentially filtered by status for an installed app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="configurationStatus">Filter for configuration status. (optional)</param>
        /// <returns>ApiResponse of PagedInstallConfigurations</returns>
        ApiResponse<PagedInstallConfigurations> ListInstallationConfigWithHttpInfo (string installedAppId, string configurationStatus = default(string));
        /// <summary>
        /// List installed apps.
        /// </summary>
        /// <remarks>
        /// List all installed applications within the specified locations. If no locations are provided, then list all installed apps accessible by the principle. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="locationId">The ID of the location that both the installed smart app and source are associated with. (optional)</param>
        /// <param name="installedAppStatus">State of the Installed App. (optional)</param>
        /// <param name="installedAppType">Denotes the type of installed app. (optional)</param>
        /// <param name="tag">May be used to filter a resource by it&#39;s assigned user-tags.  Multiple tag query params are automatically joined with OR.  Example usage in query string: &#x60;&#x60;&#x60; ?tag:key_name&#x3D;value1&amp;tag:key_name&#x3D;value2 &#x60;&#x60;&#x60;  (optional)</param>
        /// <param name="appId">The ID of an App (optional)</param>
        /// <param name="modeId">The ID of the mode (optional)</param>
        /// <param name="deviceId">The ID of the device (optional)</param>
        /// <returns>PagedInstalledApps</returns>
        PagedInstalledApps ListInstallations (string locationId = default(string), string installedAppStatus = default(string), string installedAppType = default(string), string tag = default(string), string appId = default(string), string modeId = default(string), string deviceId = default(string));

        /// <summary>
        /// List installed apps.
        /// </summary>
        /// <remarks>
        /// List all installed applications within the specified locations. If no locations are provided, then list all installed apps accessible by the principle. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="locationId">The ID of the location that both the installed smart app and source are associated with. (optional)</param>
        /// <param name="installedAppStatus">State of the Installed App. (optional)</param>
        /// <param name="installedAppType">Denotes the type of installed app. (optional)</param>
        /// <param name="tag">May be used to filter a resource by it&#39;s assigned user-tags.  Multiple tag query params are automatically joined with OR.  Example usage in query string: &#x60;&#x60;&#x60; ?tag:key_name&#x3D;value1&amp;tag:key_name&#x3D;value2 &#x60;&#x60;&#x60;  (optional)</param>
        /// <param name="appId">The ID of an App (optional)</param>
        /// <param name="modeId">The ID of the mode (optional)</param>
        /// <param name="deviceId">The ID of the device (optional)</param>
        /// <returns>ApiResponse of PagedInstalledApps</returns>
        ApiResponse<PagedInstalledApps> ListInstallationsWithHttpInfo (string locationId = default(string), string installedAppStatus = default(string), string installedAppType = default(string), string tag = default(string), string appId = default(string), string modeId = default(string), string deviceId = default(string));
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IInstalledappsApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Create Installed App events.
        /// </summary>
        /// <remarks>
        /// Create events for an installed app.  This API allows Apps to create events to trigger custom behavior in installed apps. Requires a SmartApp token. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="createInstalledAppEventsRequest"></param>
        /// <returns>Task of Object</returns>
        System.Threading.Tasks.Task<Object> CreateInstalledAppEventsAsync (string installedAppId, CreateInstalledAppEventsRequest createInstalledAppEventsRequest);

        /// <summary>
        /// Create Installed App events.
        /// </summary>
        /// <remarks>
        /// Create events for an installed app.  This API allows Apps to create events to trigger custom behavior in installed apps. Requires a SmartApp token. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="createInstalledAppEventsRequest"></param>
        /// <returns>Task of ApiResponse (Object)</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> CreateInstalledAppEventsAsyncWithHttpInfo (string installedAppId, CreateInstalledAppEventsRequest createInstalledAppEventsRequest);
        /// <summary>
        /// Delete an installed app.
        /// </summary>
        /// <remarks>
        /// Delete an Installed App.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <returns>Task of DeleteInstalledAppResponse</returns>
        System.Threading.Tasks.Task<DeleteInstalledAppResponse> DeleteInstallationAsync (string installedAppId);

        /// <summary>
        /// Delete an installed app.
        /// </summary>
        /// <remarks>
        /// Delete an Installed App.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <returns>Task of ApiResponse (DeleteInstalledAppResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<DeleteInstalledAppResponse>> DeleteInstallationAsyncWithHttpInfo (string installedAppId);
        /// <summary>
        /// Get an installed app.
        /// </summary>
        /// <remarks>
        /// Fetch a single installed application.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <returns>Task of InstalledApp</returns>
        System.Threading.Tasks.Task<InstalledApp> GetInstallationAsync (string installedAppId);

        /// <summary>
        /// Get an installed app.
        /// </summary>
        /// <remarks>
        /// Fetch a single installed application.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <returns>Task of ApiResponse (InstalledApp)</returns>
        System.Threading.Tasks.Task<ApiResponse<InstalledApp>> GetInstallationAsyncWithHttpInfo (string installedAppId);
        /// <summary>
        /// Get an installed app configuration.
        /// </summary>
        /// <remarks>
        /// Fetch a detailed install configuration model containing actual config entries / values.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="configurationId">The ID of the install configuration.</param>
        /// <returns>Task of InstallConfigurationDetail</returns>
        System.Threading.Tasks.Task<InstallConfigurationDetail> GetInstallationConfigAsync (string installedAppId, Guid configurationId);

        /// <summary>
        /// Get an installed app configuration.
        /// </summary>
        /// <remarks>
        /// Fetch a detailed install configuration model containing actual config entries / values.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="configurationId">The ID of the install configuration.</param>
        /// <returns>Task of ApiResponse (InstallConfigurationDetail)</returns>
        System.Threading.Tasks.Task<ApiResponse<InstallConfigurationDetail>> GetInstallationConfigAsyncWithHttpInfo (string installedAppId, Guid configurationId);
        /// <summary>
        /// List an installed app&#39;s configurations.
        /// </summary>
        /// <remarks>
        /// List all configurations potentially filtered by status for an installed app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="configurationStatus">Filter for configuration status. (optional)</param>
        /// <returns>Task of PagedInstallConfigurations</returns>
        System.Threading.Tasks.Task<PagedInstallConfigurations> ListInstallationConfigAsync (string installedAppId, string configurationStatus = default(string));

        /// <summary>
        /// List an installed app&#39;s configurations.
        /// </summary>
        /// <remarks>
        /// List all configurations potentially filtered by status for an installed app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="configurationStatus">Filter for configuration status. (optional)</param>
        /// <returns>Task of ApiResponse (PagedInstallConfigurations)</returns>
        System.Threading.Tasks.Task<ApiResponse<PagedInstallConfigurations>> ListInstallationConfigAsyncWithHttpInfo (string installedAppId, string configurationStatus = default(string));
        /// <summary>
        /// List installed apps.
        /// </summary>
        /// <remarks>
        /// List all installed applications within the specified locations. If no locations are provided, then list all installed apps accessible by the principle. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="locationId">The ID of the location that both the installed smart app and source are associated with. (optional)</param>
        /// <param name="installedAppStatus">State of the Installed App. (optional)</param>
        /// <param name="installedAppType">Denotes the type of installed app. (optional)</param>
        /// <param name="tag">May be used to filter a resource by it&#39;s assigned user-tags.  Multiple tag query params are automatically joined with OR.  Example usage in query string: &#x60;&#x60;&#x60; ?tag:key_name&#x3D;value1&amp;tag:key_name&#x3D;value2 &#x60;&#x60;&#x60;  (optional)</param>
        /// <param name="appId">The ID of an App (optional)</param>
        /// <param name="modeId">The ID of the mode (optional)</param>
        /// <param name="deviceId">The ID of the device (optional)</param>
        /// <returns>Task of PagedInstalledApps</returns>
        System.Threading.Tasks.Task<PagedInstalledApps> ListInstallationsAsync (string locationId = default(string), string installedAppStatus = default(string), string installedAppType = default(string), string tag = default(string), string appId = default(string), string modeId = default(string), string deviceId = default(string));

        /// <summary>
        /// List installed apps.
        /// </summary>
        /// <remarks>
        /// List all installed applications within the specified locations. If no locations are provided, then list all installed apps accessible by the principle. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="locationId">The ID of the location that both the installed smart app and source are associated with. (optional)</param>
        /// <param name="installedAppStatus">State of the Installed App. (optional)</param>
        /// <param name="installedAppType">Denotes the type of installed app. (optional)</param>
        /// <param name="tag">May be used to filter a resource by it&#39;s assigned user-tags.  Multiple tag query params are automatically joined with OR.  Example usage in query string: &#x60;&#x60;&#x60; ?tag:key_name&#x3D;value1&amp;tag:key_name&#x3D;value2 &#x60;&#x60;&#x60;  (optional)</param>
        /// <param name="appId">The ID of an App (optional)</param>
        /// <param name="modeId">The ID of the mode (optional)</param>
        /// <param name="deviceId">The ID of the device (optional)</param>
        /// <returns>Task of ApiResponse (PagedInstalledApps)</returns>
        System.Threading.Tasks.Task<ApiResponse<PagedInstalledApps>> ListInstallationsAsyncWithHttpInfo (string locationId = default(string), string installedAppStatus = default(string), string installedAppType = default(string), string tag = default(string), string appId = default(string), string modeId = default(string), string deviceId = default(string));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IInstalledappsApi : IInstalledappsApiSync, IInstalledappsApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class InstalledAppsApi : IInstalledappsApi
    {
        private SmartThingsNet.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstalledAppsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public InstalledAppsApi() : this((string) null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InstalledAppsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public InstalledAppsApi(String basePath)
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
        /// Initializes a new instance of the <see cref="InstalledAppsApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public InstalledAppsApi(SmartThingsNet.Client.Configuration configuration)
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
        /// Initializes a new instance of the <see cref="InstalledAppsApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public InstalledAppsApi(SmartThingsNet.Client.ISynchronousClient client,SmartThingsNet.Client.IAsynchronousClient asyncClient, SmartThingsNet.Client.IReadableConfiguration configuration)
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
        /// Create Installed App events. Create events for an installed app.  This API allows Apps to create events to trigger custom behavior in installed apps. Requires a SmartApp token. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="createInstalledAppEventsRequest"></param>
        /// <returns>Object</returns>
        public Object CreateInstalledAppEvents (string installedAppId, CreateInstalledAppEventsRequest createInstalledAppEventsRequest)
        {
             SmartThingsNet.Client.ApiResponse<Object> localVarResponse = CreateInstalledAppEventsWithHttpInfo(installedAppId, createInstalledAppEventsRequest);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Create Installed App events. Create events for an installed app.  This API allows Apps to create events to trigger custom behavior in installed apps. Requires a SmartApp token. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="createInstalledAppEventsRequest"></param>
        /// <returns>ApiResponse of Object</returns>
        public SmartThingsNet.Client.ApiResponse< Object > CreateInstalledAppEventsWithHttpInfo (string installedAppId, CreateInstalledAppEventsRequest createInstalledAppEventsRequest)
        {
            // verify the required parameter 'installedAppId' is set
            if (installedAppId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installedAppId' when calling InstalledappsApi->CreateInstalledAppEvents");

            // verify the required parameter 'createInstalledAppEventsRequest' is set
            if (createInstalledAppEventsRequest == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'createInstalledAppEventsRequest' when calling InstalledappsApi->CreateInstalledAppEvents");

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

            localVarRequestOptions.PathParameters.Add("installedAppId", SmartThingsNet.Client.ClientUtils.ParameterToString(installedAppId)); // path parameter
            
            localVarRequestOptions.Data = createInstalledAppEventsRequest;

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post< Object >("/installedapps/{installedAppId}/events", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateInstalledAppEvents", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Installed App events. Create events for an installed app.  This API allows Apps to create events to trigger custom behavior in installed apps. Requires a SmartApp token. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="createInstalledAppEventsRequest"></param>
        /// <returns>Task of Object</returns>
        public async System.Threading.Tasks.Task<Object> CreateInstalledAppEventsAsync (string installedAppId, CreateInstalledAppEventsRequest createInstalledAppEventsRequest)
        {
             SmartThingsNet.Client.ApiResponse<Object> localVarResponse = await CreateInstalledAppEventsAsyncWithHttpInfo(installedAppId, createInstalledAppEventsRequest);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Create Installed App events. Create events for an installed app.  This API allows Apps to create events to trigger custom behavior in installed apps. Requires a SmartApp token. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="createInstalledAppEventsRequest"></param>
        /// <returns>Task of ApiResponse (Object)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Object>> CreateInstalledAppEventsAsyncWithHttpInfo (string installedAppId, CreateInstalledAppEventsRequest createInstalledAppEventsRequest)
        {
            // verify the required parameter 'installedAppId' is set
            if (installedAppId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installedAppId' when calling InstalledappsApi->CreateInstalledAppEvents");

            // verify the required parameter 'createInstalledAppEventsRequest' is set
            if (createInstalledAppEventsRequest == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'createInstalledAppEventsRequest' when calling InstalledappsApi->CreateInstalledAppEvents");


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
            
            localVarRequestOptions.PathParameters.Add("installedAppId", SmartThingsNet.Client.ClientUtils.ParameterToString(installedAppId)); // path parameter
            
            localVarRequestOptions.Data = createInstalledAppEventsRequest;

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/installedapps/{installedAppId}/events", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateInstalledAppEvents", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete an installed app. Delete an Installed App.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <returns>DeleteInstalledAppResponse</returns>
        public DeleteInstalledAppResponse DeleteInstallation (string installedAppId)
        {
             SmartThingsNet.Client.ApiResponse<DeleteInstalledAppResponse> localVarResponse = DeleteInstallationWithHttpInfo(installedAppId);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Delete an installed app. Delete an Installed App.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <returns>ApiResponse of DeleteInstalledAppResponse</returns>
        public SmartThingsNet.Client.ApiResponse< DeleteInstalledAppResponse > DeleteInstallationWithHttpInfo (string installedAppId)
        {
            // verify the required parameter 'installedAppId' is set
            if (installedAppId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installedAppId' when calling InstalledappsApi->DeleteInstallation");

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

            localVarRequestOptions.PathParameters.Add("installedAppId", SmartThingsNet.Client.ClientUtils.ParameterToString(installedAppId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete< DeleteInstalledAppResponse >("/installedapps/{installedAppId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteInstallation", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete an installed app. Delete an Installed App.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <returns>Task of DeleteInstalledAppResponse</returns>
        public async System.Threading.Tasks.Task<DeleteInstalledAppResponse> DeleteInstallationAsync (string installedAppId)
        {
             SmartThingsNet.Client.ApiResponse<DeleteInstalledAppResponse> localVarResponse = await DeleteInstallationAsyncWithHttpInfo(installedAppId);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Delete an installed app. Delete an Installed App.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <returns>Task of ApiResponse (DeleteInstalledAppResponse)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<DeleteInstalledAppResponse>> DeleteInstallationAsyncWithHttpInfo (string installedAppId)
        {
            // verify the required parameter 'installedAppId' is set
            if (installedAppId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installedAppId' when calling InstalledappsApi->DeleteInstallation");


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
            
            localVarRequestOptions.PathParameters.Add("installedAppId", SmartThingsNet.Client.ClientUtils.ParameterToString(installedAppId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.DeleteAsync<DeleteInstalledAppResponse>("/installedapps/{installedAppId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteInstallation", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get an installed app. Fetch a single installed application.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <returns>InstalledApp</returns>
        public InstalledApp GetInstallation (string installedAppId)
        {
             SmartThingsNet.Client.ApiResponse<InstalledApp> localVarResponse = GetInstallationWithHttpInfo(installedAppId);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Get an installed app. Fetch a single installed application.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <returns>ApiResponse of InstalledApp</returns>
        public SmartThingsNet.Client.ApiResponse< InstalledApp > GetInstallationWithHttpInfo (string installedAppId)
        {
            // verify the required parameter 'installedAppId' is set
            if (installedAppId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installedAppId' when calling InstalledappsApi->GetInstallation");

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

            localVarRequestOptions.PathParameters.Add("installedAppId", SmartThingsNet.Client.ClientUtils.ParameterToString(installedAppId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get< InstalledApp >("/installedapps/{installedAppId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetInstallation", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get an installed app. Fetch a single installed application.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <returns>Task of InstalledApp</returns>
        public async System.Threading.Tasks.Task<InstalledApp> GetInstallationAsync (string installedAppId)
        {
             SmartThingsNet.Client.ApiResponse<InstalledApp> localVarResponse = await GetInstallationAsyncWithHttpInfo(installedAppId);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Get an installed app. Fetch a single installed application.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <returns>Task of ApiResponse (InstalledApp)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<InstalledApp>> GetInstallationAsyncWithHttpInfo (string installedAppId)
        {
            // verify the required parameter 'installedAppId' is set
            if (installedAppId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installedAppId' when calling InstalledappsApi->GetInstallation");


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
            
            localVarRequestOptions.PathParameters.Add("installedAppId", SmartThingsNet.Client.ClientUtils.ParameterToString(installedAppId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<InstalledApp>("/installedapps/{installedAppId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetInstallation", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get an installed app configuration. Fetch a detailed install configuration model containing actual config entries / values.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="configurationId">The ID of the install configuration.</param>
        /// <returns>InstallConfigurationDetail</returns>
        public InstallConfigurationDetail GetInstallationConfig (string installedAppId, Guid configurationId)
        {
             SmartThingsNet.Client.ApiResponse<InstallConfigurationDetail> localVarResponse = GetInstallationConfigWithHttpInfo(installedAppId, configurationId);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Get an installed app configuration. Fetch a detailed install configuration model containing actual config entries / values.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="configurationId">The ID of the install configuration.</param>
        /// <returns>ApiResponse of InstallConfigurationDetail</returns>
        public SmartThingsNet.Client.ApiResponse< InstallConfigurationDetail > GetInstallationConfigWithHttpInfo (string installedAppId, Guid configurationId)
        {
            // verify the required parameter 'installedAppId' is set
            if (installedAppId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installedAppId' when calling InstalledappsApi->GetInstallationConfig");

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

            localVarRequestOptions.PathParameters.Add("installedAppId", SmartThingsNet.Client.ClientUtils.ParameterToString(installedAppId)); // path parameter
            localVarRequestOptions.PathParameters.Add("configurationId", SmartThingsNet.Client.ClientUtils.ParameterToString(configurationId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get< InstallConfigurationDetail >("/installedapps/{installedAppId}/configs/{configurationId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetInstallationConfig", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get an installed app configuration. Fetch a detailed install configuration model containing actual config entries / values.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="configurationId">The ID of the install configuration.</param>
        /// <returns>Task of InstallConfigurationDetail</returns>
        public async System.Threading.Tasks.Task<InstallConfigurationDetail> GetInstallationConfigAsync (string installedAppId, Guid configurationId)
        {
             SmartThingsNet.Client.ApiResponse<InstallConfigurationDetail> localVarResponse = await GetInstallationConfigAsyncWithHttpInfo(installedAppId, configurationId);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Get an installed app configuration. Fetch a detailed install configuration model containing actual config entries / values.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="configurationId">The ID of the install configuration.</param>
        /// <returns>Task of ApiResponse (InstallConfigurationDetail)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<InstallConfigurationDetail>> GetInstallationConfigAsyncWithHttpInfo (string installedAppId, Guid configurationId)
        {
            // verify the required parameter 'installedAppId' is set
            if (installedAppId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installedAppId' when calling InstalledappsApi->GetInstallationConfig");


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
            
            localVarRequestOptions.PathParameters.Add("installedAppId", SmartThingsNet.Client.ClientUtils.ParameterToString(installedAppId)); // path parameter
            localVarRequestOptions.PathParameters.Add("configurationId", SmartThingsNet.Client.ClientUtils.ParameterToString(configurationId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<InstallConfigurationDetail>("/installedapps/{installedAppId}/configs/{configurationId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetInstallationConfig", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List an installed app&#39;s configurations. List all configurations potentially filtered by status for an installed app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="configurationStatus">Filter for configuration status. (optional)</param>
        /// <returns>PagedInstallConfigurations</returns>
        public PagedInstallConfigurations ListInstallationConfig (string installedAppId, string configurationStatus = default(string))
        {
             SmartThingsNet.Client.ApiResponse<PagedInstallConfigurations> localVarResponse = ListInstallationConfigWithHttpInfo(installedAppId, configurationStatus);
             return localVarResponse.Data;
        }

        /// <summary>
        /// List an installed app&#39;s configurations. List all configurations potentially filtered by status for an installed app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="configurationStatus">Filter for configuration status. (optional)</param>
        /// <returns>ApiResponse of PagedInstallConfigurations</returns>
        public SmartThingsNet.Client.ApiResponse< PagedInstallConfigurations > ListInstallationConfigWithHttpInfo (string installedAppId, string configurationStatus = default(string))
        {
            // verify the required parameter 'installedAppId' is set
            if (installedAppId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installedAppId' when calling InstalledappsApi->ListInstallationConfig");

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

            localVarRequestOptions.PathParameters.Add("installedAppId", SmartThingsNet.Client.ClientUtils.ParameterToString(installedAppId)); // path parameter
            if (configurationStatus != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "configurationStatus", configurationStatus));
            }
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get< PagedInstallConfigurations >("/installedapps/{installedAppId}/configs", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListInstallationConfig", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List an installed app&#39;s configurations. List all configurations potentially filtered by status for an installed app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="configurationStatus">Filter for configuration status. (optional)</param>
        /// <returns>Task of PagedInstallConfigurations</returns>
        public async System.Threading.Tasks.Task<PagedInstallConfigurations> ListInstallationConfigAsync (string installedAppId, string configurationStatus = default(string))
        {
             SmartThingsNet.Client.ApiResponse<PagedInstallConfigurations> localVarResponse = await ListInstallationConfigAsyncWithHttpInfo(installedAppId, configurationStatus);
             return localVarResponse.Data;

        }

        /// <summary>
        /// List an installed app&#39;s configurations. List all configurations potentially filtered by status for an installed app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="configurationStatus">Filter for configuration status. (optional)</param>
        /// <returns>Task of ApiResponse (PagedInstallConfigurations)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<PagedInstallConfigurations>> ListInstallationConfigAsyncWithHttpInfo (string installedAppId, string configurationStatus = default(string))
        {
            // verify the required parameter 'installedAppId' is set
            if (installedAppId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installedAppId' when calling InstalledappsApi->ListInstallationConfig");


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
            
            localVarRequestOptions.PathParameters.Add("installedAppId", SmartThingsNet.Client.ClientUtils.ParameterToString(installedAppId)); // path parameter
            if (configurationStatus != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "configurationStatus", configurationStatus));
            }
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<PagedInstallConfigurations>("/installedapps/{installedAppId}/configs", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListInstallationConfig", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List installed apps. List all installed applications within the specified locations. If no locations are provided, then list all installed apps accessible by the principle. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="locationId">The ID of the location that both the installed smart app and source are associated with. (optional)</param>
        /// <param name="installedAppStatus">State of the Installed App. (optional)</param>
        /// <param name="installedAppType">Denotes the type of installed app. (optional)</param>
        /// <param name="tag">May be used to filter a resource by it&#39;s assigned user-tags.  Multiple tag query params are automatically joined with OR.  Example usage in query string: &#x60;&#x60;&#x60; ?tag:key_name&#x3D;value1&amp;tag:key_name&#x3D;value2 &#x60;&#x60;&#x60;  (optional)</param>
        /// <param name="appId">The ID of an App (optional)</param>
        /// <param name="modeId">The ID of the mode (optional)</param>
        /// <param name="deviceId">The ID of the device (optional)</param>
        /// <returns>PagedInstalledApps</returns>
        public PagedInstalledApps ListInstallations (string locationId = default(string), string installedAppStatus = default(string), string installedAppType = default(string), string tag = default(string), string appId = default(string), string modeId = default(string), string deviceId = default(string))
        {
             SmartThingsNet.Client.ApiResponse<PagedInstalledApps> localVarResponse = ListInstallationsWithHttpInfo(locationId, installedAppStatus, installedAppType, tag, appId, modeId, deviceId);
             return localVarResponse.Data;
        }

        /// <summary>
        /// List installed apps. List all installed applications within the specified locations. If no locations are provided, then list all installed apps accessible by the principle. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="locationId">The ID of the location that both the installed smart app and source are associated with. (optional)</param>
        /// <param name="installedAppStatus">State of the Installed App. (optional)</param>
        /// <param name="installedAppType">Denotes the type of installed app. (optional)</param>
        /// <param name="tag">May be used to filter a resource by it&#39;s assigned user-tags.  Multiple tag query params are automatically joined with OR.  Example usage in query string: &#x60;&#x60;&#x60; ?tag:key_name&#x3D;value1&amp;tag:key_name&#x3D;value2 &#x60;&#x60;&#x60;  (optional)</param>
        /// <param name="appId">The ID of an App (optional)</param>
        /// <param name="modeId">The ID of the mode (optional)</param>
        /// <param name="deviceId">The ID of the device (optional)</param>
        /// <returns>ApiResponse of PagedInstalledApps</returns>
        public SmartThingsNet.Client.ApiResponse< PagedInstalledApps > ListInstallationsWithHttpInfo (string locationId = default(string), string installedAppStatus = default(string), string installedAppType = default(string), string tag = default(string), string appId = default(string), string modeId = default(string), string deviceId = default(string))
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

            if (locationId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "locationId", locationId));
            }
            if (installedAppStatus != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "installedAppStatus", installedAppStatus));
            }
            if (installedAppType != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "installedAppType", installedAppType));
            }
            if (tag != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "tag", tag));
            }
            if (appId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "appId", appId));
            }
            if (modeId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "modeId", modeId));
            }
            if (deviceId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "deviceId", deviceId));
            }
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get< PagedInstalledApps >("/installedapps", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListInstallations", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List installed apps. List all installed applications within the specified locations. If no locations are provided, then list all installed apps accessible by the principle. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="locationId">The ID of the location that both the installed smart app and source are associated with. (optional)</param>
        /// <param name="installedAppStatus">State of the Installed App. (optional)</param>
        /// <param name="installedAppType">Denotes the type of installed app. (optional)</param>
        /// <param name="tag">May be used to filter a resource by it&#39;s assigned user-tags.  Multiple tag query params are automatically joined with OR.  Example usage in query string: &#x60;&#x60;&#x60; ?tag:key_name&#x3D;value1&amp;tag:key_name&#x3D;value2 &#x60;&#x60;&#x60;  (optional)</param>
        /// <param name="appId">The ID of an App (optional)</param>
        /// <param name="modeId">The ID of the mode (optional)</param>
        /// <param name="deviceId">The ID of the device (optional)</param>
        /// <returns>Task of PagedInstalledApps</returns>
        public async System.Threading.Tasks.Task<PagedInstalledApps> ListInstallationsAsync (string locationId = default(string), string installedAppStatus = default(string), string installedAppType = default(string), string tag = default(string), string appId = default(string), string modeId = default(string), string deviceId = default(string))
        {
             SmartThingsNet.Client.ApiResponse<PagedInstalledApps> localVarResponse = await ListInstallationsAsyncWithHttpInfo(locationId, installedAppStatus, installedAppType, tag, appId, modeId, deviceId);
             return localVarResponse.Data;

        }

        /// <summary>
        /// List installed apps. List all installed applications within the specified locations. If no locations are provided, then list all installed apps accessible by the principle. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="locationId">The ID of the location that both the installed smart app and source are associated with. (optional)</param>
        /// <param name="installedAppStatus">State of the Installed App. (optional)</param>
        /// <param name="installedAppType">Denotes the type of installed app. (optional)</param>
        /// <param name="tag">May be used to filter a resource by it&#39;s assigned user-tags.  Multiple tag query params are automatically joined with OR.  Example usage in query string: &#x60;&#x60;&#x60; ?tag:key_name&#x3D;value1&amp;tag:key_name&#x3D;value2 &#x60;&#x60;&#x60;  (optional)</param>
        /// <param name="appId">The ID of an App (optional)</param>
        /// <param name="modeId">The ID of the mode (optional)</param>
        /// <param name="deviceId">The ID of the device (optional)</param>
        /// <returns>Task of ApiResponse (PagedInstalledApps)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<PagedInstalledApps>> ListInstallationsAsyncWithHttpInfo (string locationId = default(string), string installedAppStatus = default(string), string installedAppType = default(string), string tag = default(string), string appId = default(string), string modeId = default(string), string deviceId = default(string))
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
            
            if (locationId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "locationId", locationId));
            }
            if (installedAppStatus != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "installedAppStatus", installedAppStatus));
            }
            if (installedAppType != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "installedAppType", installedAppType));
            }
            if (tag != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "tag", tag));
            }
            if (appId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "appId", appId));
            }
            if (modeId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "modeId", modeId));
            }
            if (deviceId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "deviceId", deviceId));
            }
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<PagedInstalledApps>("/installedapps", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListInstallations", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

    }
}
