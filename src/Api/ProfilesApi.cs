/*
 * SmartThings API
 *
 * # Overview  This is the reference documentation for the SmartThings API.  The SmartThings API, a RESTful API, provides a method for your integration to communicate with the SmartThings Platform. The API is the core of the platform. It is used to control devices, create Automations, manage Locations, retrieve user and device information; if you want to communicate with the SmartThings platform, you’ll be using the SmartThings API. All responses are sent as [JSON](http://www.json.org/).  The SmartThings API consists of several endpoints, including Rules, Locations, Devices, and more. Even though each of these endpoints are not standalone APIs, you may hear them referred to as such. For example, the Rules API is used to build Automations.  # Authentication  Before using the SmartThings API, you’ll need to obtain an Authorization Token. All SmartThings resources are protected with [OAuth 2.0 Bearer Tokens](https://tools.ietf.org/html/rfc6750#section-2.1) sent on the request as an `Authorization: Bearer <TOKEN>` header. Operations require specific OAuth scopes that specify the exact permissions authorized by the user.  ## Authorization token types  There are two types of tokens:   * SmartApp tokens   * Personal access tokens (PAT).  ### SmartApp tokens  SmartApp tokens are used to communicate between third-party integrations, or SmartApps, and the SmartThings API. When a SmartApp is called by the SmartThings platform, it is sent an authorization token that can be used to interact with the SmartThings API.  ### Personal access tokens  Personal access tokens are used to authorize interaction with the API for non-SmartApp use cases. When creating personal access tokens, you can specifiy the permissions granted to the token. These permissions define the OAuth2 scopes for the personal access token.  Personal access tokesn can be created and managed on the [personal access tokens page](https://account.smartthings.com/tokens).  ## OAuth2 scopes  Operations may be protected by one or more OAuth security schemes, which specify the required permissions. Each scope for a given scheme is required. If multiple schemes are specified (uncommon), you may use either scheme.  SmartApp token scopes are derived from the permissions requested by the SmartApp and granted by the end-user during installation. Personal access token scopes are associated with the specific permissions authorized when the token is created.  Scopes are generally in the form `permission:entity-type:entity-id`.  **An `*` used for the `entity-id` specifies that the permission may be applied to all entities that the token type has access to, or may be replaced with a specific ID.**  For more information about authrization and permissions, visit the [Authorization section](https://developer-preview.smartthings.com/docs/advanced/authorization-and-permissions) in the SmartThings documentation.  <!- - ReDoc-Inject: <security-definitions> - ->  # Errors  The SmartThings API uses conventional HTTP response codes to indicate the success or failure of a request.  In general:  * A `2XX` response code indicates success * A `4XX` response code indicates an error given the inputs for the request * A `5XX` response code indicates a failure on the SmartThings platform  API errors will contain a JSON response body with more information about the error:  ```json {   \"requestId\": \"031fec1a-f19f-470a-a7da-710569082846\"   \"error\": {     \"code\": \"ConstraintViolationError\",     \"message\": \"Validation errors occurred while process your request.\",     \"details\": [       { \"code\": \"PatternError\", \"target\": \"latitude\", \"message\": \"Invalid format.\" },       { \"code\": \"SizeError\", \"target\": \"name\", \"message\": \"Too small.\" },       { \"code\": \"SizeError\", \"target\": \"description\", \"message\": \"Too big.\" }     ]   } } ```  ## Error Response Body  The error response attributes are:  | Property | Type | Required | Description | | - -- | - -- | - -- | - -- | | requestId | String | No | A request identifier that can be used to correlate an error to additional logging on the SmartThings servers. | error | Error | **Yes** | The Error object, documented below.  ## Error Object  The Error object contains the following attributes:  | Property | Type | Required | Description | | - -- | - -- | - -- | - -- | | code | String | **Yes** | A SmartThings-defined error code that serves as a more specific indicator of the error than the HTTP error code specified in the response. See [SmartThings Error Codes](#section/Errors/SmartThings-Error-Codes) for more information. | message | String | **Yes** | A description of the error, intended to aid debugging of error responses. | target | String | No | The target of the error. For example, this could be the name of the property that caused the error. | details | Error[] | No | An array of Error objects that typically represent distinct, related errors that occurred during the request. As an optional attribute, this may be null or an empty array.  ## Standard HTTP Error Codes  The following table lists the most common HTTP error responses:  | Code | Name | Description | | - -- | - -- | - -- | | 400 | Bad Request | The client has issued an invalid request. This is commonly used to specify validation errors in a request payload. | 401 | Unauthorized | Authorization for the API is required, but the request has not been authenticated. | 403 | Forbidden | The request has been authenticated but does not have appropriate permissions, or a requested resource is not found. | 404 | Not Found | The requested path does not exist. | 406 | Not Acceptable | The client has requested a MIME type via the Accept header for a value not supported by the server. | 415 | Unsupported Media Type | The client has defined a contentType header that is not supported by the server. | 422 | Unprocessable Entity | The client has made a valid request, but the server cannot process it. This is often used for APIs for which certain limits have been exceeded. | 429 | Too Many Requests | The client has exceeded the number of requests allowed for a given time window. | 500 | Internal Server Error | An unexpected error on the SmartThings servers has occurred. These errors are generally rare. | 501 | Not Implemented | The client request was valid and understood by the server, but the requested feature has yet to be implemented. These errors are generally rare.  ## SmartThings Error Codes  SmartThings specifies several standard custom error codes. These provide more information than the standard HTTP error response codes. The following table lists the standard SmartThings error codes and their descriptions:  | Code | Typical HTTP Status Codes | Description | | - -- | - -- | - -- | | PatternError | 400, 422 | The client has provided input that does not match the expected pattern. | ConstraintViolationError | 422 | The client has provided input that has violated one or more constraints. | NotNullError | 422 | The client has provided a null input for a field that is required to be non-null. | NullError | 422 | The client has provided an input for a field that is required to be null. | NotEmptyError | 422 | The client has provided an empty input for a field that is required to be non-empty. | SizeError | 400, 422 | The client has provided a value that does not meet size restrictions. | Unexpected Error | 500 | A non-recoverable error condition has occurred. A problem occurred on the SmartThings server that is no fault of the client. | UnprocessableEntityError | 422 | The client has sent a malformed request body. | TooManyRequestError | 429 | The client issued too many requests too quickly. | LimitError | 422 | The client has exceeded certain limits an API enforces. | UnsupportedOperationError | 400, 422 | The client has issued a request to a feature that currently isn't supported by the SmartThings platform. These errors are generally rare.  ## Custom Error Codes  An API may define its own error codes where appropriate. Custom error codes are documented in each API endpoint's documentation section.  # Warnings The SmartThings API issues warning messages via standard HTTP Warning headers. These messages do not represent a request failure, but provide additional information that the requester might want to act upon. For example, a warning will be issued if you are using an old API version.  # API Versions  The SmartThings API supports both path and header-based versioning. The following are equivalent:  - https://api.smartthings.com/v1/locations - https://api.smartthings.com/locations with header `Accept: application/vnd.smartthings+json;v=1`  Currently, only version 1 is available.  # Paging  Operations that return a list of objects return a paginated response. The `_links` object contains the items returned, and links to the next and previous result page, if applicable.  ```json {   \"items\": [     {       \"locationId\": \"6b3d1909-1e1c-43ec-adc2-5f941de4fbf9\",       \"name\": \"Home\"     },     {       \"locationId\": \"6b3d1909-1e1c-43ec-adc2-5f94d6g4fbf9\",       \"name\": \"Work\"     }     ....   ],   \"_links\": {     \"next\": {       \"href\": \"https://api.smartthings.com/v1/locations?page=3\"     },     \"previous\": {       \"href\": \"https://api.smartthings.com/v1/locations?page=1\"     }   } } ```  # Localization  Some SmartThings APIs support localization. Specific information regarding localization endpoints are documented in the API itself. However, the following applies to all endpoints that support localization.  ## Fallback Patterns  When making a request with the `Accept-Language` header, the following fallback pattern is observed: 1. Response will be translated with exact locale tag. 2. If a translation does not exist for the requested language and region, the translation for the language will be returned. 3. If a translation does not exist for the language, English (en) will be returned. 4. Finally, an untranslated response will be returned in the absense of the above translations.  ## Accept-Language Header The format of the `Accept-Language` header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5)  ## Content-Language The `Content-Language` header should be set on the response from the server to indicate which translation was given back to the client. The absense of the header indicates that the server did not recieve a request with the `Accept-Language` header set. 
 *
 * The version of the OpenAPI document: 1.0-PREVIEW
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
    public interface IProfilesApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Create a Device Profile
        /// </summary>
        /// <remarks>
        /// Create a Device Profile.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="request">The new Device Profile to Create. (optional)</param>
        /// <returns>DeviceProfileResponse</returns>
        DeviceProfileResponse CreateDeviceProfile(string authorization, string xSTOrganization = default(string), CreateDeviceProfileRequest request = default(CreateDeviceProfileRequest));

        /// <summary>
        /// Create a Device Profile
        /// </summary>
        /// <remarks>
        /// Create a Device Profile.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="request">The new Device Profile to Create. (optional)</param>
        /// <returns>ApiResponse of DeviceProfileResponse</returns>
        ApiResponse<DeviceProfileResponse> CreateDeviceProfileWithHttpInfo(string authorization, string xSTOrganization = default(string), CreateDeviceProfileRequest request = default(CreateDeviceProfileRequest));
        /// <summary>
        /// Delete a Device Profile
        /// </summary>
        /// <remarks>
        /// Delete a Device Profile by Device Profile ID.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <returns>Object</returns>
        Object DeleteDeviceProfile(string authorization, string deviceProfileId, string xSTOrganization = default(string));

        /// <summary>
        /// Delete a Device Profile
        /// </summary>
        /// <remarks>
        /// Delete a Device Profile by Device Profile ID.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <returns>ApiResponse of Object</returns>
        ApiResponse<Object> DeleteDeviceProfileWithHttpInfo(string authorization, string deviceProfileId, string xSTOrganization = default(string));
        /// <summary>
        /// Get a Device Profile
        /// </summary>
        /// <remarks>
        /// Retrieve the Device Profile of a given Device.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <returns>DeviceProfileResponse</returns>
        DeviceProfileResponse GetDeviceProfile(string authorization, string deviceProfileId, string xSTOrganization = default(string), string acceptLanguage = default(string));

        /// <summary>
        /// Get a Device Profile
        /// </summary>
        /// <remarks>
        /// Retrieve the Device Profile of a given Device.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <returns>ApiResponse of DeviceProfileResponse</returns>
        ApiResponse<DeviceProfileResponse> GetDeviceProfileWithHttpInfo(string authorization, string deviceProfileId, string xSTOrganization = default(string), string acceptLanguage = default(string));
        /// <summary>
        /// List all Device Profiles for a User
        /// </summary>
        /// <remarks>
        /// List all Device Profiles in an authenticated user&#39;s account.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="profileId">Filter the results by Device Profile IDs.  (optional)</param>
        /// <returns>PagedDeviceProfiles</returns>
        PagedDeviceProfiles ListDeviceProfiles(string authorization, string xSTOrganization = default(string), List<string> profileId = default(List<string>));

        /// <summary>
        /// List all Device Profiles for a User
        /// </summary>
        /// <remarks>
        /// List all Device Profiles in an authenticated user&#39;s account.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="profileId">Filter the results by Device Profile IDs.  (optional)</param>
        /// <returns>ApiResponse of PagedDeviceProfiles</returns>
        ApiResponse<PagedDeviceProfiles> ListDeviceProfilesWithHttpInfo(string authorization, string xSTOrganization = default(string), List<string> profileId = default(List<string>));
        /// <summary>
        /// Update a Device Profile
        /// </summary>
        /// <remarks>
        /// Update a Device Profile. The Device Profile must be currently deployed.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="request">The Device Profile to update. (optional)</param>
        /// <returns>DeviceProfileResponse</returns>
        DeviceProfileResponse UpdateDeviceProfile(string authorization, string deviceProfileId, string xSTOrganization = default(string), UpdateDeviceProfileRequest request = default(UpdateDeviceProfileRequest));

        /// <summary>
        /// Update a Device Profile
        /// </summary>
        /// <remarks>
        /// Update a Device Profile. The Device Profile must be currently deployed.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="request">The Device Profile to update. (optional)</param>
        /// <returns>ApiResponse of DeviceProfileResponse</returns>
        ApiResponse<DeviceProfileResponse> UpdateDeviceProfileWithHttpInfo(string authorization, string deviceProfileId, string xSTOrganization = default(string), UpdateDeviceProfileRequest request = default(UpdateDeviceProfileRequest));
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IProfilesApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Create a Device Profile
        /// </summary>
        /// <remarks>
        /// Create a Device Profile.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="request">The new Device Profile to Create. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of DeviceProfileResponse</returns>
        System.Threading.Tasks.Task<DeviceProfileResponse> CreateDeviceProfileAsync(string authorization, string xSTOrganization = default(string), CreateDeviceProfileRequest request = default(CreateDeviceProfileRequest), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create a Device Profile
        /// </summary>
        /// <remarks>
        /// Create a Device Profile.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="request">The new Device Profile to Create. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (DeviceProfileResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<DeviceProfileResponse>> CreateDeviceProfileWithHttpInfoAsync(string authorization, string xSTOrganization = default(string), CreateDeviceProfileRequest request = default(CreateDeviceProfileRequest), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete a Device Profile
        /// </summary>
        /// <remarks>
        /// Delete a Device Profile by Device Profile ID.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Object</returns>
        System.Threading.Tasks.Task<Object> DeleteDeviceProfileAsync(string authorization, string deviceProfileId, string xSTOrganization = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete a Device Profile
        /// </summary>
        /// <remarks>
        /// Delete a Device Profile by Device Profile ID.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Object)</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteDeviceProfileWithHttpInfoAsync(string authorization, string deviceProfileId, string xSTOrganization = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get a Device Profile
        /// </summary>
        /// <remarks>
        /// Retrieve the Device Profile of a given Device.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of DeviceProfileResponse</returns>
        System.Threading.Tasks.Task<DeviceProfileResponse> GetDeviceProfileAsync(string authorization, string deviceProfileId, string xSTOrganization = default(string), string acceptLanguage = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get a Device Profile
        /// </summary>
        /// <remarks>
        /// Retrieve the Device Profile of a given Device.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (DeviceProfileResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<DeviceProfileResponse>> GetDeviceProfileWithHttpInfoAsync(string authorization, string deviceProfileId, string xSTOrganization = default(string), string acceptLanguage = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List all Device Profiles for a User
        /// </summary>
        /// <remarks>
        /// List all Device Profiles in an authenticated user&#39;s account.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="profileId">Filter the results by Device Profile IDs.  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PagedDeviceProfiles</returns>
        System.Threading.Tasks.Task<PagedDeviceProfiles> ListDeviceProfilesAsync(string authorization, string xSTOrganization = default(string), List<string> profileId = default(List<string>), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List all Device Profiles for a User
        /// </summary>
        /// <remarks>
        /// List all Device Profiles in an authenticated user&#39;s account.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="profileId">Filter the results by Device Profile IDs.  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PagedDeviceProfiles)</returns>
        System.Threading.Tasks.Task<ApiResponse<PagedDeviceProfiles>> ListDeviceProfilesWithHttpInfoAsync(string authorization, string xSTOrganization = default(string), List<string> profileId = default(List<string>), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update a Device Profile
        /// </summary>
        /// <remarks>
        /// Update a Device Profile. The Device Profile must be currently deployed.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="request">The Device Profile to update. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of DeviceProfileResponse</returns>
        System.Threading.Tasks.Task<DeviceProfileResponse> UpdateDeviceProfileAsync(string authorization, string deviceProfileId, string xSTOrganization = default(string), UpdateDeviceProfileRequest request = default(UpdateDeviceProfileRequest), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update a Device Profile
        /// </summary>
        /// <remarks>
        /// Update a Device Profile. The Device Profile must be currently deployed.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="request">The Device Profile to update. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (DeviceProfileResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<DeviceProfileResponse>> UpdateDeviceProfileWithHttpInfoAsync(string authorization, string deviceProfileId, string xSTOrganization = default(string), UpdateDeviceProfileRequest request = default(UpdateDeviceProfileRequest), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IProfilesApi : IProfilesApiSync, IProfilesApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class ProfilesApi : IProfilesApi
    {
        private SmartThingsNet.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfilesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public ProfilesApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfilesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public ProfilesApi(string basePath)
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
        /// Initializes a new instance of the <see cref="ProfilesApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public ProfilesApi(SmartThingsNet.Client.Configuration configuration)
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
        /// Initializes a new instance of the <see cref="ProfilesApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public ProfilesApi(SmartThingsNet.Client.ISynchronousClient client, SmartThingsNet.Client.IAsynchronousClient asyncClient, SmartThingsNet.Client.IReadableConfiguration configuration)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (asyncClient == null) throw new ArgumentNullException("asyncClient");
            if (configuration == null) throw new ArgumentNullException("configuration");

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
        public string GetBasePath()
        {
            return this.Configuration.BasePath;
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public SmartThingsNet.Client.IReadableConfiguration Configuration { get; set; }

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
        /// Create a Device Profile Create a Device Profile.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="request">The new Device Profile to Create. (optional)</param>
        /// <returns>DeviceProfileResponse</returns>
        public DeviceProfileResponse CreateDeviceProfile(string authorization, string xSTOrganization = default(string), CreateDeviceProfileRequest request = default(CreateDeviceProfileRequest))
        {
            SmartThingsNet.Client.ApiResponse<DeviceProfileResponse> localVarResponse = CreateDeviceProfileWithHttpInfo(authorization, xSTOrganization, request);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create a Device Profile Create a Device Profile.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="request">The new Device Profile to Create. (optional)</param>
        /// <returns>ApiResponse of DeviceProfileResponse</returns>
        public SmartThingsNet.Client.ApiResponse<DeviceProfileResponse> CreateDeviceProfileWithHttpInfo(string authorization, string xSTOrganization = default(string), CreateDeviceProfileRequest request = default(CreateDeviceProfileRequest))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling ProfilesApi->CreateDeviceProfile");
            }

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            
            if (xSTOrganization != null)
            {
                localVarRequestOptions.HeaderParameters.Add("X-ST-Organization", SmartThingsNet.Client.ClientUtils.ParameterToString(xSTOrganization)); // header parameter
            }
            localVarRequestOptions.Data = request;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<DeviceProfileResponse>("/deviceprofiles", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateDeviceProfile", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create a Device Profile Create a Device Profile.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="request">The new Device Profile to Create. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of DeviceProfileResponse</returns>
        public async System.Threading.Tasks.Task<DeviceProfileResponse> CreateDeviceProfileAsync(string authorization, string xSTOrganization = default(string), CreateDeviceProfileRequest request = default(CreateDeviceProfileRequest), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<DeviceProfileResponse> localVarResponse = await CreateDeviceProfileWithHttpInfoAsync(authorization, xSTOrganization, request, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create a Device Profile Create a Device Profile.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="request">The new Device Profile to Create. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (DeviceProfileResponse)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<DeviceProfileResponse>> CreateDeviceProfileWithHttpInfoAsync(string authorization, string xSTOrganization = default(string), CreateDeviceProfileRequest request = default(CreateDeviceProfileRequest), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling ProfilesApi->CreateDeviceProfile");
            }


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            
            if (xSTOrganization != null)
            {
                localVarRequestOptions.HeaderParameters.Add("X-ST-Organization", SmartThingsNet.Client.ClientUtils.ParameterToString(xSTOrganization)); // header parameter
            }
            localVarRequestOptions.Data = request;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<DeviceProfileResponse>("/deviceprofiles", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateDeviceProfile", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete a Device Profile Delete a Device Profile by Device Profile ID.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <returns>Object</returns>
        public Object DeleteDeviceProfile(string authorization, string deviceProfileId, string xSTOrganization = default(string))
        {
            SmartThingsNet.Client.ApiResponse<Object> localVarResponse = DeleteDeviceProfileWithHttpInfo(authorization, deviceProfileId, xSTOrganization);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Delete a Device Profile Delete a Device Profile by Device Profile ID.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <returns>ApiResponse of Object</returns>
        public SmartThingsNet.Client.ApiResponse<Object> DeleteDeviceProfileWithHttpInfo(string authorization, string deviceProfileId, string xSTOrganization = default(string))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling ProfilesApi->DeleteDeviceProfile");
            }

            // verify the required parameter 'deviceProfileId' is set
            if (deviceProfileId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceProfileId' when calling ProfilesApi->DeleteDeviceProfile");
            }

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("deviceProfileId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceProfileId)); // path parameter
            
            if (xSTOrganization != null)
            {
                localVarRequestOptions.HeaderParameters.Add("X-ST-Organization", SmartThingsNet.Client.ClientUtils.ParameterToString(xSTOrganization)); // header parameter
            }

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/deviceprofiles/{deviceProfileId}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteDeviceProfile", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete a Device Profile Delete a Device Profile by Device Profile ID.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Object</returns>
        public async System.Threading.Tasks.Task<Object> DeleteDeviceProfileAsync(string authorization, string deviceProfileId, string xSTOrganization = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<Object> localVarResponse = await DeleteDeviceProfileWithHttpInfoAsync(authorization, deviceProfileId, xSTOrganization, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Delete a Device Profile Delete a Device Profile by Device Profile ID.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Object)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Object>> DeleteDeviceProfileWithHttpInfoAsync(string authorization, string deviceProfileId, string xSTOrganization = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling ProfilesApi->DeleteDeviceProfile");
            }

            // verify the required parameter 'deviceProfileId' is set
            if (deviceProfileId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceProfileId' when calling ProfilesApi->DeleteDeviceProfile");
            }


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("deviceProfileId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceProfileId)); // path parameter
            
            if (xSTOrganization != null)
            {
                localVarRequestOptions.HeaderParameters.Add("X-ST-Organization", SmartThingsNet.Client.ClientUtils.ParameterToString(xSTOrganization)); // header parameter
            }

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/deviceprofiles/{deviceProfileId}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteDeviceProfile", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get a Device Profile Retrieve the Device Profile of a given Device.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <returns>DeviceProfileResponse</returns>
        public DeviceProfileResponse GetDeviceProfile(string authorization, string deviceProfileId, string xSTOrganization = default(string), string acceptLanguage = default(string))
        {
            SmartThingsNet.Client.ApiResponse<DeviceProfileResponse> localVarResponse = GetDeviceProfileWithHttpInfo(authorization, deviceProfileId, xSTOrganization, acceptLanguage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get a Device Profile Retrieve the Device Profile of a given Device.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <returns>ApiResponse of DeviceProfileResponse</returns>
        public SmartThingsNet.Client.ApiResponse<DeviceProfileResponse> GetDeviceProfileWithHttpInfo(string authorization, string deviceProfileId, string xSTOrganization = default(string), string acceptLanguage = default(string))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling ProfilesApi->GetDeviceProfile");
            }

            // verify the required parameter 'deviceProfileId' is set
            if (deviceProfileId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceProfileId' when calling ProfilesApi->GetDeviceProfile");
            }

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("deviceProfileId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceProfileId)); // path parameter
            
            if (xSTOrganization != null)
            {
                localVarRequestOptions.HeaderParameters.Add("X-ST-Organization", SmartThingsNet.Client.ClientUtils.ParameterToString(xSTOrganization)); // header parameter
            }
            if (acceptLanguage != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept-Language", SmartThingsNet.Client.ClientUtils.ParameterToString(acceptLanguage)); // header parameter
            }

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<DeviceProfileResponse>("/deviceprofiles/{deviceProfileId}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDeviceProfile", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get a Device Profile Retrieve the Device Profile of a given Device.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of DeviceProfileResponse</returns>
        public async System.Threading.Tasks.Task<DeviceProfileResponse> GetDeviceProfileAsync(string authorization, string deviceProfileId, string xSTOrganization = default(string), string acceptLanguage = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<DeviceProfileResponse> localVarResponse = await GetDeviceProfileWithHttpInfoAsync(authorization, deviceProfileId, xSTOrganization, acceptLanguage, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get a Device Profile Retrieve the Device Profile of a given Device.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="acceptLanguage">Language header representing the client&#39;s preferred language. The format of the &#x60;Accept-Language&#x60; header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (DeviceProfileResponse)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<DeviceProfileResponse>> GetDeviceProfileWithHttpInfoAsync(string authorization, string deviceProfileId, string xSTOrganization = default(string), string acceptLanguage = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling ProfilesApi->GetDeviceProfile");
            }

            // verify the required parameter 'deviceProfileId' is set
            if (deviceProfileId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceProfileId' when calling ProfilesApi->GetDeviceProfile");
            }


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("deviceProfileId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceProfileId)); // path parameter
            
            if (xSTOrganization != null)
            {
                localVarRequestOptions.HeaderParameters.Add("X-ST-Organization", SmartThingsNet.Client.ClientUtils.ParameterToString(xSTOrganization)); // header parameter
            }
            if (acceptLanguage != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept-Language", SmartThingsNet.Client.ClientUtils.ParameterToString(acceptLanguage)); // header parameter
            }

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<DeviceProfileResponse>("/deviceprofiles/{deviceProfileId}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDeviceProfile", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List all Device Profiles for a User List all Device Profiles in an authenticated user&#39;s account.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="profileId">Filter the results by Device Profile IDs.  (optional)</param>
        /// <returns>PagedDeviceProfiles</returns>
        public PagedDeviceProfiles ListDeviceProfiles(string authorization, string xSTOrganization = default(string), List<string> profileId = default(List<string>))
        {
            SmartThingsNet.Client.ApiResponse<PagedDeviceProfiles> localVarResponse = ListDeviceProfilesWithHttpInfo(authorization, xSTOrganization, profileId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List all Device Profiles for a User List all Device Profiles in an authenticated user&#39;s account.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="profileId">Filter the results by Device Profile IDs.  (optional)</param>
        /// <returns>ApiResponse of PagedDeviceProfiles</returns>
        public SmartThingsNet.Client.ApiResponse<PagedDeviceProfiles> ListDeviceProfilesWithHttpInfo(string authorization, string xSTOrganization = default(string), List<string> profileId = default(List<string>))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling ProfilesApi->ListDeviceProfiles");
            }

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            if (profileId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("multi", "profileId", profileId));
            }
            
            if (xSTOrganization != null)
            {
                localVarRequestOptions.HeaderParameters.Add("X-ST-Organization", SmartThingsNet.Client.ClientUtils.ParameterToString(xSTOrganization)); // header parameter
            }

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<PagedDeviceProfiles>("/deviceprofiles", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListDeviceProfiles", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List all Device Profiles for a User List all Device Profiles in an authenticated user&#39;s account.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="profileId">Filter the results by Device Profile IDs.  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PagedDeviceProfiles</returns>
        public async System.Threading.Tasks.Task<PagedDeviceProfiles> ListDeviceProfilesAsync(string authorization, string xSTOrganization = default(string), List<string> profileId = default(List<string>), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<PagedDeviceProfiles> localVarResponse = await ListDeviceProfilesWithHttpInfoAsync(authorization, xSTOrganization, profileId, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List all Device Profiles for a User List all Device Profiles in an authenticated user&#39;s account.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="profileId">Filter the results by Device Profile IDs.  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PagedDeviceProfiles)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<PagedDeviceProfiles>> ListDeviceProfilesWithHttpInfoAsync(string authorization, string xSTOrganization = default(string), List<string> profileId = default(List<string>), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling ProfilesApi->ListDeviceProfiles");
            }


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            if (profileId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("multi", "profileId", profileId));
            }
            
            if (xSTOrganization != null)
            {
                localVarRequestOptions.HeaderParameters.Add("X-ST-Organization", SmartThingsNet.Client.ClientUtils.ParameterToString(xSTOrganization)); // header parameter
            }

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<PagedDeviceProfiles>("/deviceprofiles", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListDeviceProfiles", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update a Device Profile Update a Device Profile. The Device Profile must be currently deployed.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="request">The Device Profile to update. (optional)</param>
        /// <returns>DeviceProfileResponse</returns>
        public DeviceProfileResponse UpdateDeviceProfile(string authorization, string deviceProfileId, string xSTOrganization = default(string), UpdateDeviceProfileRequest request = default(UpdateDeviceProfileRequest))
        {
            SmartThingsNet.Client.ApiResponse<DeviceProfileResponse> localVarResponse = UpdateDeviceProfileWithHttpInfo(authorization, deviceProfileId, xSTOrganization, request);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update a Device Profile Update a Device Profile. The Device Profile must be currently deployed.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="request">The Device Profile to update. (optional)</param>
        /// <returns>ApiResponse of DeviceProfileResponse</returns>
        public SmartThingsNet.Client.ApiResponse<DeviceProfileResponse> UpdateDeviceProfileWithHttpInfo(string authorization, string deviceProfileId, string xSTOrganization = default(string), UpdateDeviceProfileRequest request = default(UpdateDeviceProfileRequest))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling ProfilesApi->UpdateDeviceProfile");
            }

            // verify the required parameter 'deviceProfileId' is set
            if (deviceProfileId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceProfileId' when calling ProfilesApi->UpdateDeviceProfile");
            }

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("deviceProfileId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceProfileId)); // path parameter
            
            if (xSTOrganization != null)
            {
                localVarRequestOptions.HeaderParameters.Add("X-ST-Organization", SmartThingsNet.Client.ClientUtils.ParameterToString(xSTOrganization)); // header parameter
            }
            localVarRequestOptions.Data = request;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<DeviceProfileResponse>("/deviceprofiles/{deviceProfileId}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateDeviceProfile", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update a Device Profile Update a Device Profile. The Device Profile must be currently deployed.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="request">The Device Profile to update. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of DeviceProfileResponse</returns>
        public async System.Threading.Tasks.Task<DeviceProfileResponse> UpdateDeviceProfileAsync(string authorization, string deviceProfileId, string xSTOrganization = default(string), UpdateDeviceProfileRequest request = default(UpdateDeviceProfileRequest), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<DeviceProfileResponse> localVarResponse = await UpdateDeviceProfileWithHttpInfoAsync(authorization, deviceProfileId, xSTOrganization, request, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update a Device Profile Update a Device Profile. The Device Profile must be currently deployed.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceProfileId">the device profile ID</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="request">The Device Profile to update. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (DeviceProfileResponse)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<DeviceProfileResponse>> UpdateDeviceProfileWithHttpInfoAsync(string authorization, string deviceProfileId, string xSTOrganization = default(string), UpdateDeviceProfileRequest request = default(UpdateDeviceProfileRequest), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling ProfilesApi->UpdateDeviceProfile");
            }

            // verify the required parameter 'deviceProfileId' is set
            if (deviceProfileId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceProfileId' when calling ProfilesApi->UpdateDeviceProfile");
            }


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("deviceProfileId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceProfileId)); // path parameter
            
            if (xSTOrganization != null)
            {
                localVarRequestOptions.HeaderParameters.Add("X-ST-Organization", SmartThingsNet.Client.ClientUtils.ParameterToString(xSTOrganization)); // header parameter
            }
            localVarRequestOptions.Data = request;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<DeviceProfileResponse>("/deviceprofiles/{deviceProfileId}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateDeviceProfile", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

    }
}
