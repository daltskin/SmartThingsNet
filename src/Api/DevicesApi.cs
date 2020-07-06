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
    public interface IDevicesApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Create Device Events.
        /// </summary>
        /// <remarks>
        /// Create events for a device. When a device is managed by a SmartApp then it is responsible for creating events to update the attributes of the device in the SmartThings platform. The token must be for a SmartApp and it must be the SmartApp that created the Device. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="deviceEventRequest"></param>
        /// <returns>Object</returns>
        Object CreateDeviceEvents (string deviceId, DeviceEventsRequest deviceEventRequest);

        /// <summary>
        /// Create Device Events.
        /// </summary>
        /// <remarks>
        /// Create events for a device. When a device is managed by a SmartApp then it is responsible for creating events to update the attributes of the device in the SmartThings platform. The token must be for a SmartApp and it must be the SmartApp that created the Device. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="deviceEventRequest"></param>
        /// <returns>ApiResponse of Object</returns>
        ApiResponse<Object> CreateDeviceEventsWithHttpInfo (string deviceId, DeviceEventsRequest deviceEventRequest);
        /// <summary>
        /// Delete a Device.
        /// </summary>
        /// <remarks>
        /// Delete a device by device id. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <returns>Object</returns>
        Object DeleteDevice (string deviceId);

        /// <summary>
        /// Delete a Device.
        /// </summary>
        /// <remarks>
        /// Delete a device by device id. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <returns>ApiResponse of Object</returns>
        ApiResponse<Object> DeleteDeviceWithHttpInfo (string deviceId);
        /// <summary>
        /// Execute commands on device.
        /// </summary>
        /// <remarks>
        /// Execute commands on a device.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="executeCapabilityCommand"></param>
        /// <returns>Object</returns>
        Object ExecuteDeviceCommands (string deviceId, DeviceCommandsRequest executeCapabilityCommand);

        /// <summary>
        /// Execute commands on device.
        /// </summary>
        /// <remarks>
        /// Execute commands on a device.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="executeCapabilityCommand"></param>
        /// <returns>ApiResponse of Object</returns>
        ApiResponse<Object> ExecuteDeviceCommandsWithHttpInfo (string deviceId, DeviceCommandsRequest executeCapabilityCommand);
        /// <summary>
        /// Get a device&#39;s description.
        /// </summary>
        /// <remarks>
        /// Get a device&#39;s description.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <returns>Device</returns>
        Device GetDevice (string deviceId);

        /// <summary>
        /// Get a device&#39;s description.
        /// </summary>
        /// <remarks>
        /// Get a device&#39;s description.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <returns>ApiResponse of Device</returns>
        ApiResponse<Device> GetDeviceWithHttpInfo (string deviceId);
        /// <summary>
        /// Get a device component&#39;s status.
        /// </summary>
        /// <remarks>
        /// Get the status of all attributes of a the component. The results may be filtered if the requester only has permission to view a subset of the component&#39;s capabilities. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <returns>Dictionary&lt;string, Dictionary&gt;</returns>
        ComponentStatus GetDeviceComponentStatus (string deviceId, string componentId);

        /// <summary>
        /// Get a device component&#39;s status.
        /// </summary>
        /// <remarks>
        /// Get the status of all attributes of a the component. The results may be filtered if the requester only has permission to view a subset of the component&#39;s capabilities. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <returns>ApiResponse of Dictionary&lt;string, Dictionary&gt;</returns>
        ApiResponse<ComponentStatus> GetDeviceComponentStatusWithHttpInfo (string deviceId, string componentId);
        /// <summary>
        /// Get the full status of a device.
        /// </summary>
        /// <remarks>
        /// Get the current status of all of a device&#39;s component&#39;s attributes. The results may be filtered if the requester only has permission to view a subset of the device&#39;s components or capabilities. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <returns>DeviceStatus</returns>
        DeviceStatus GetDeviceStatus (string deviceId);

        /// <summary>
        /// Get the full status of a device.
        /// </summary>
        /// <remarks>
        /// Get the current status of all of a device&#39;s component&#39;s attributes. The results may be filtered if the requester only has permission to view a subset of the device&#39;s components or capabilities. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <returns>ApiResponse of DeviceStatus</returns>
        ApiResponse<DeviceStatus> GetDeviceStatusWithHttpInfo (string deviceId);
        /// <summary>
        /// Get a capability&#39;s status.
        /// </summary>
        /// <remarks>
        /// Get the current status of a device component&#39;s capability. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <param name="capabilityId">The ID of the capability</param>
        /// <returns>Dictionary&lt;string, AttributeState&gt;</returns>
        Dictionary<string, AttributeState> GetDeviceStatusByCapability (string deviceId, string componentId, string capabilityId);

        /// <summary>
        /// Get a capability&#39;s status.
        /// </summary>
        /// <remarks>
        /// Get the current status of a device component&#39;s capability. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <param name="capabilityId">The ID of the capability</param>
        /// <returns>ApiResponse of Dictionary&lt;string, AttributeState&gt;</returns>
        ApiResponse<Dictionary<string, AttributeState>> GetDeviceStatusByCapabilityWithHttpInfo (string deviceId, string componentId, string capabilityId);
        /// <summary>
        /// List devices.
        /// </summary>
        /// <remarks>
        /// Get a list of devices.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="capability">The device capabilities to filter the results by. The capabilities are treated as an \&quot;and\&quot; so all capabilities must be present.  (optional)</param>
        /// <param name="locationId">The device locations to filter the results by.  (optional)</param>
        /// <param name="deviceId">The device ids to filter the results by.  (optional)</param>
        /// <param name="capabilitiesMode">Treat all capability filter query params as a logical \&quot;or\&quot; or \&quot;and\&quot; with a default of \&quot;and\&quot;.  (optional, default to and)</param>
        /// <returns>PagedDevices</returns>
        PagedDevices GetDevices (List<string> capability = default(List<string>), List<string> locationId = default(List<string>), List<string> deviceId = default(List<string>), string capabilitiesMode = default(string));

        /// <summary>
        /// List devices.
        /// </summary>
        /// <remarks>
        /// Get a list of devices.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="capability">The device capabilities to filter the results by. The capabilities are treated as an \&quot;and\&quot; so all capabilities must be present.  (optional)</param>
        /// <param name="locationId">The device locations to filter the results by.  (optional)</param>
        /// <param name="deviceId">The device ids to filter the results by.  (optional)</param>
        /// <param name="capabilitiesMode">Treat all capability filter query params as a logical \&quot;or\&quot; or \&quot;and\&quot; with a default of \&quot;and\&quot;.  (optional, default to and)</param>
        /// <returns>ApiResponse of PagedDevices</returns>
        ApiResponse<PagedDevices> GetDevicesWithHttpInfo (List<string> capability = default(List<string>), List<string> locationId = default(List<string>), List<string> deviceId = default(List<string>), string capabilitiesMode = default(string));
        /// <summary>
        /// Install a Device.
        /// </summary>
        /// <remarks>
        /// Install a device. This is only available for SmartApp managed devices. The SmartApp that creates the device is responsible for handling commands for the device and updating the status of the device by creating events. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installationRequest">Installation Request</param>
        /// <returns>Device</returns>
        Device InstallDevice (DeviceInstallRequest installationRequest);

        /// <summary>
        /// Install a Device.
        /// </summary>
        /// <remarks>
        /// Install a device. This is only available for SmartApp managed devices. The SmartApp that creates the device is responsible for handling commands for the device and updating the status of the device by creating events. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installationRequest">Installation Request</param>
        /// <returns>ApiResponse of Device</returns>
        ApiResponse<Device> InstallDeviceWithHttpInfo (DeviceInstallRequest installationRequest);
        /// <summary>
        /// Update a device.
        /// </summary>
        /// <remarks>
        /// Update the properties of a device. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="updateDeviceRequest"></param>
        /// <returns>Device</returns>
        Device UpdateDevice (string deviceId, UpdateDeviceRequest updateDeviceRequest);

        /// <summary>
        /// Update a device.
        /// </summary>
        /// <remarks>
        /// Update the properties of a device. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="updateDeviceRequest"></param>
        /// <returns>ApiResponse of Device</returns>
        ApiResponse<Device> UpdateDeviceWithHttpInfo (string deviceId, UpdateDeviceRequest updateDeviceRequest);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IDevicesApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Create Device Events.
        /// </summary>
        /// <remarks>
        /// Create events for a device. When a device is managed by a SmartApp then it is responsible for creating events to update the attributes of the device in the SmartThings platform. The token must be for a SmartApp and it must be the SmartApp that created the Device. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="deviceEventRequest"></param>
        /// <returns>Task of Object</returns>
        System.Threading.Tasks.Task<Object> CreateDeviceEventsAsync (string deviceId, DeviceEventsRequest deviceEventRequest);

        /// <summary>
        /// Create Device Events.
        /// </summary>
        /// <remarks>
        /// Create events for a device. When a device is managed by a SmartApp then it is responsible for creating events to update the attributes of the device in the SmartThings platform. The token must be for a SmartApp and it must be the SmartApp that created the Device. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="deviceEventRequest"></param>
        /// <returns>Task of ApiResponse (Object)</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> CreateDeviceEventsAsyncWithHttpInfo (string deviceId, DeviceEventsRequest deviceEventRequest);
        /// <summary>
        /// Delete a Device.
        /// </summary>
        /// <remarks>
        /// Delete a device by device id. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <returns>Task of Object</returns>
        System.Threading.Tasks.Task<Object> DeleteDeviceAsync (string deviceId);

        /// <summary>
        /// Delete a Device.
        /// </summary>
        /// <remarks>
        /// Delete a device by device id. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <returns>Task of ApiResponse (Object)</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteDeviceAsyncWithHttpInfo (string deviceId);
        /// <summary>
        /// Execute commands on device.
        /// </summary>
        /// <remarks>
        /// Execute commands on a device.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="executeCapabilityCommand"></param>
        /// <returns>Task of Object</returns>
        System.Threading.Tasks.Task<Object> ExecuteDeviceCommandsAsync (string deviceId, DeviceCommandsRequest executeCapabilityCommand);

        /// <summary>
        /// Execute commands on device.
        /// </summary>
        /// <remarks>
        /// Execute commands on a device.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="executeCapabilityCommand"></param>
        /// <returns>Task of ApiResponse (Object)</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> ExecuteDeviceCommandsAsyncWithHttpInfo (string deviceId, DeviceCommandsRequest executeCapabilityCommand);
        /// <summary>
        /// Get a device&#39;s description.
        /// </summary>
        /// <remarks>
        /// Get a device&#39;s description.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <returns>Task of Device</returns>
        System.Threading.Tasks.Task<Device> GetDeviceAsync (string deviceId);

        /// <summary>
        /// Get a device&#39;s description.
        /// </summary>
        /// <remarks>
        /// Get a device&#39;s description.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <returns>Task of ApiResponse (Device)</returns>
        System.Threading.Tasks.Task<ApiResponse<Device>> GetDeviceAsyncWithHttpInfo (string deviceId);
        /// <summary>
        /// Get a device component&#39;s status.
        /// </summary>
        /// <remarks>
        /// Get the status of all attributes of a the component. The results may be filtered if the requester only has permission to view a subset of the component&#39;s capabilities. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <returns>Task of Dictionary&lt;string, Dictionary&gt;</returns>
        System.Threading.Tasks.Task<ComponentStatus> GetDeviceComponentStatusAsync (string deviceId, string componentId);

        /// <summary>
        /// Get a device component&#39;s status.
        /// </summary>
        /// <remarks>
        /// Get the status of all attributes of a the component. The results may be filtered if the requester only has permission to view a subset of the component&#39;s capabilities. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <returns>Task of ApiResponse (Dictionary&lt;string, Dictionary&gt;)</returns>
        System.Threading.Tasks.Task<ApiResponse<ComponentStatus>>GetDeviceComponentStatusAsyncWithHttpInfo (string deviceId, string componentId);
        /// <summary>
        /// Get the full status of a device.
        /// </summary>
        /// <remarks>
        /// Get the current status of all of a device&#39;s component&#39;s attributes. The results may be filtered if the requester only has permission to view a subset of the device&#39;s components or capabilities. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <returns>Task of DeviceStatus</returns>
        System.Threading.Tasks.Task<DeviceStatus> GetDeviceStatusAsync (string deviceId);

        /// <summary>
        /// Get the full status of a device.
        /// </summary>
        /// <remarks>
        /// Get the current status of all of a device&#39;s component&#39;s attributes. The results may be filtered if the requester only has permission to view a subset of the device&#39;s components or capabilities. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <returns>Task of ApiResponse (DeviceStatus)</returns>
        System.Threading.Tasks.Task<ApiResponse<DeviceStatus>> GetDeviceStatusAsyncWithHttpInfo (string deviceId);
        /// <summary>
        /// Get a capability&#39;s status.
        /// </summary>
        /// <remarks>
        /// Get the current status of a device component&#39;s capability. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <param name="capabilityId">The ID of the capability</param>
        /// <returns>Task of Dictionary&lt;string, AttributeState&gt;</returns>
        System.Threading.Tasks.Task<Dictionary<string, AttributeState>> GetDeviceStatusByCapabilityAsync (string deviceId, string componentId, string capabilityId);

        /// <summary>
        /// Get a capability&#39;s status.
        /// </summary>
        /// <remarks>
        /// Get the current status of a device component&#39;s capability. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <param name="capabilityId">The ID of the capability</param>
        /// <returns>Task of ApiResponse (Dictionary&lt;string, AttributeState&gt;)</returns>
        System.Threading.Tasks.Task<ApiResponse<Dictionary<string, AttributeState>>> GetDeviceStatusByCapabilityAsyncWithHttpInfo (string deviceId, string componentId, string capabilityId);
        /// <summary>
        /// List devices.
        /// </summary>
        /// <remarks>
        /// Get a list of devices.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="capability">The device capabilities to filter the results by. The capabilities are treated as an \&quot;and\&quot; so all capabilities must be present.  (optional)</param>
        /// <param name="locationId">The device locations to filter the results by.  (optional)</param>
        /// <param name="deviceId">The device ids to filter the results by.  (optional)</param>
        /// <param name="capabilitiesMode">Treat all capability filter query params as a logical \&quot;or\&quot; or \&quot;and\&quot; with a default of \&quot;and\&quot;.  (optional, default to and)</param>
        /// <returns>Task of PagedDevices</returns>
        System.Threading.Tasks.Task<PagedDevices> GetDevicesAsync (List<string> capability = default(List<string>), List<string> locationId = default(List<string>), List<string> deviceId = default(List<string>), string capabilitiesMode = default(string));

        /// <summary>
        /// List devices.
        /// </summary>
        /// <remarks>
        /// Get a list of devices.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="capability">The device capabilities to filter the results by. The capabilities are treated as an \&quot;and\&quot; so all capabilities must be present.  (optional)</param>
        /// <param name="locationId">The device locations to filter the results by.  (optional)</param>
        /// <param name="deviceId">The device ids to filter the results by.  (optional)</param>
        /// <param name="capabilitiesMode">Treat all capability filter query params as a logical \&quot;or\&quot; or \&quot;and\&quot; with a default of \&quot;and\&quot;.  (optional, default to and)</param>
        /// <returns>Task of ApiResponse (PagedDevices)</returns>
        System.Threading.Tasks.Task<ApiResponse<PagedDevices>> GetDevicesAsyncWithHttpInfo (List<string> capability = default(List<string>), List<string> locationId = default(List<string>), List<string> deviceId = default(List<string>), string capabilitiesMode = default(string));
        /// <summary>
        /// Install a Device.
        /// </summary>
        /// <remarks>
        /// Install a device. This is only available for SmartApp managed devices. The SmartApp that creates the device is responsible for handling commands for the device and updating the status of the device by creating events. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installationRequest">Installation Request</param>
        /// <returns>Task of Device</returns>
        System.Threading.Tasks.Task<Device> InstallDeviceAsync (DeviceInstallRequest installationRequest);

        /// <summary>
        /// Install a Device.
        /// </summary>
        /// <remarks>
        /// Install a device. This is only available for SmartApp managed devices. The SmartApp that creates the device is responsible for handling commands for the device and updating the status of the device by creating events. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installationRequest">Installation Request</param>
        /// <returns>Task of ApiResponse (Device)</returns>
        System.Threading.Tasks.Task<ApiResponse<Device>> InstallDeviceAsyncWithHttpInfo (DeviceInstallRequest installationRequest);
        /// <summary>
        /// Update a device.
        /// </summary>
        /// <remarks>
        /// Update the properties of a device. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="updateDeviceRequest"></param>
        /// <returns>Task of Device</returns>
        System.Threading.Tasks.Task<Device> UpdateDeviceAsync (string deviceId, UpdateDeviceRequest updateDeviceRequest);

        /// <summary>
        /// Update a device.
        /// </summary>
        /// <remarks>
        /// Update the properties of a device. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="updateDeviceRequest"></param>
        /// <returns>Task of ApiResponse (Device)</returns>
        System.Threading.Tasks.Task<ApiResponse<Device>> UpdateDeviceAsyncWithHttpInfo (string deviceId, UpdateDeviceRequest updateDeviceRequest);
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IDevicesApi : IDevicesApiSync, IDevicesApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class DevicesApi : IDevicesApi
    {
        private SmartThingsNet.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public DevicesApi() : this((string) null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public DevicesApi(String basePath)
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
        /// Initializes a new instance of the <see cref="DevicesApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public DevicesApi(SmartThingsNet.Client.Configuration configuration)
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
        /// Initializes a new instance of the <see cref="DevicesApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public DevicesApi(SmartThingsNet.Client.ISynchronousClient client,SmartThingsNet.Client.IAsynchronousClient asyncClient, SmartThingsNet.Client.IReadableConfiguration configuration)
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
        /// Create Device Events. Create events for a device. When a device is managed by a SmartApp then it is responsible for creating events to update the attributes of the device in the SmartThings platform. The token must be for a SmartApp and it must be the SmartApp that created the Device. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="deviceEventRequest"></param>
        /// <returns>Object</returns>
        public Object CreateDeviceEvents (string deviceId, DeviceEventsRequest deviceEventRequest)
        {
             SmartThingsNet.Client.ApiResponse<Object> localVarResponse = CreateDeviceEventsWithHttpInfo(deviceId, deviceEventRequest);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Create Device Events. Create events for a device. When a device is managed by a SmartApp then it is responsible for creating events to update the attributes of the device in the SmartThings platform. The token must be for a SmartApp and it must be the SmartApp that created the Device. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">the device ID</param>
        /// <param name="deviceEventRequest"></param>
        /// <returns>ApiResponse of Object</returns>
        public SmartThingsNet.Client.ApiResponse< Object > CreateDeviceEventsWithHttpInfo (string deviceId, DeviceEventsRequest deviceEventRequest)
        {
            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->CreateDeviceEvents");

            // verify the required parameter 'deviceEventRequest' is set
            if (deviceEventRequest == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceEventRequest' when calling DevicesApi->CreateDeviceEvents");

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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            
            localVarRequestOptions.Data = deviceEventRequest;

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post< Object >("/devices/{deviceId}/events", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateDeviceEvents", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Device Events. Create events for a device. When a device is managed by a SmartApp then it is responsible for creating events to update the attributes of the device in the SmartThings platform. The token must be for a SmartApp and it must be the SmartApp that created the Device. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="deviceEventRequest"></param>
        /// <returns>Task of Object</returns>
        public async System.Threading.Tasks.Task<Object> CreateDeviceEventsAsync (string deviceId, DeviceEventsRequest deviceEventRequest)
        {
             SmartThingsNet.Client.ApiResponse<Object> localVarResponse = await CreateDeviceEventsAsyncWithHttpInfo(deviceId, deviceEventRequest);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Create Device Events. Create events for a device. When a device is managed by a SmartApp then it is responsible for creating events to update the attributes of the device in the SmartThings platform. The token must be for a SmartApp and it must be the SmartApp that created the Device. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">the device ID</param>
        /// <param name="deviceEventRequest"></param>
        /// <returns>Task of ApiResponse (Object)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Object>> CreateDeviceEventsAsyncWithHttpInfo (string deviceId, DeviceEventsRequest deviceEventRequest)
        {
            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->CreateDeviceEvents");

            // verify the required parameter 'deviceEventRequest' is set
            if (deviceEventRequest == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceEventRequest' when calling DevicesApi->CreateDeviceEvents");


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
            
            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            
            localVarRequestOptions.Data = deviceEventRequest;

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/devices/{deviceId}/events", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateDeviceEvents", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete a Device. Delete a device by device id. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">the device ID</param>
        /// <returns>Object</returns>
        public Object DeleteDevice (string deviceId)
        {
             SmartThingsNet.Client.ApiResponse<Object> localVarResponse = DeleteDeviceWithHttpInfo(deviceId);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Delete a Device. Delete a device by device id. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">the device ID</param>
        /// <returns>ApiResponse of Object</returns>
        public SmartThingsNet.Client.ApiResponse< Object > DeleteDeviceWithHttpInfo (string deviceId)
        {
            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->DeleteDevice");

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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete< Object >("/devices/{deviceId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteDevice", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete a Device. Delete a device by device id. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">the device ID</param>
        /// <returns>Task of Object</returns>
        public async System.Threading.Tasks.Task<Object> DeleteDeviceAsync (string deviceId)
        {
             SmartThingsNet.Client.ApiResponse<Object> localVarResponse = await DeleteDeviceAsyncWithHttpInfo(deviceId);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Delete a Device. Delete a device by device id. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">the device ID</param>
        /// <returns>Task of ApiResponse (Object)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Object>> DeleteDeviceAsyncWithHttpInfo (string deviceId)
        {
            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->DeleteDevice");

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
            
            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/devices/{deviceId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteDevice", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Execute commands on device. Execute commands on a device.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="executeCapabilityCommand"></param>
        /// <returns>Object</returns>
        public Object ExecuteDeviceCommands (string deviceId, DeviceCommandsRequest executeCapabilityCommand)
        {
             SmartThingsNet.Client.ApiResponse<Object> localVarResponse = ExecuteDeviceCommandsWithHttpInfo(deviceId, executeCapabilityCommand);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Execute commands on device. Execute commands on a device.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">the device ID</param>
        /// <param name="executeCapabilityCommand"></param>
        /// <returns>ApiResponse of Object</returns>
        public SmartThingsNet.Client.ApiResponse< Object > ExecuteDeviceCommandsWithHttpInfo (string deviceId, DeviceCommandsRequest executeCapabilityCommand)
        {
            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->ExecuteDeviceCommands");

            // verify the required parameter 'executeCapabilityCommand' is set
            if (executeCapabilityCommand == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'executeCapabilityCommand' when calling DevicesApi->ExecuteDeviceCommands");

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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            
            localVarRequestOptions.Data = executeCapabilityCommand;

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post< Object >("/devices/{deviceId}/commands", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ExecuteDeviceCommands", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Execute commands on device. Execute commands on a device.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">the device ID</param>
        /// <param name="executeCapabilityCommand"></param>
        /// <returns>Task of Object</returns>
        public async System.Threading.Tasks.Task<Object> ExecuteDeviceCommandsAsync (string deviceId, DeviceCommandsRequest executeCapabilityCommand)
        {
             SmartThingsNet.Client.ApiResponse<Object> localVarResponse = await ExecuteDeviceCommandsAsyncWithHttpInfo(deviceId, executeCapabilityCommand);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Execute commands on device. Execute commands on a device.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">the device ID</param>
        /// <param name="executeCapabilityCommand"></param>
        /// <returns>Task of ApiResponse (Object)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Object>> ExecuteDeviceCommandsAsyncWithHttpInfo (string deviceId, DeviceCommandsRequest executeCapabilityCommand)
        {
            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->ExecuteDeviceCommands");

            // verify the required parameter 'executeCapabilityCommand' is set
            if (executeCapabilityCommand == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'executeCapabilityCommand' when calling DevicesApi->ExecuteDeviceCommands");


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
            
            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            
            localVarRequestOptions.Data = executeCapabilityCommand;

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/devices/{deviceId}/commands", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ExecuteDeviceCommands", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get a device&#39;s description. Get a device&#39;s description.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">the device ID</param>
        /// <returns>Device</returns>
        public Device GetDevice (string deviceId)
        {
             SmartThingsNet.Client.ApiResponse<Device> localVarResponse = GetDeviceWithHttpInfo(deviceId);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Get a device&#39;s description. Get a device&#39;s description.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">the device ID</param>
        /// <returns>ApiResponse of Device</returns>
        public SmartThingsNet.Client.ApiResponse< Device > GetDeviceWithHttpInfo (string deviceId)
        {
            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->GetDevice");

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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            
            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get< Device >("/devices/{deviceId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDevice", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get a device&#39;s description. Get a device&#39;s description.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">the device ID</param>
        /// <returns>Task of Device</returns>
        public async System.Threading.Tasks.Task<Device> GetDeviceAsync (string deviceId)
        {
             SmartThingsNet.Client.ApiResponse<Device> localVarResponse = await GetDeviceAsyncWithHttpInfo(deviceId);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Get a device&#39;s description. Get a device&#39;s description.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">the device ID</param>
        /// <returns>Task of ApiResponse (Device)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Device>> GetDeviceAsyncWithHttpInfo (string deviceId)
        {
            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->GetDevice");


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
            
            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<Device>("/devices/{deviceId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDevice", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get a device component&#39;s status. Get the status of all attributes of a the component. The results may be filtered if the requester only has permission to view a subset of the component&#39;s capabilities. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">the device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <returns>Dictionary&lt;string, Dictionary&gt;</returns>
        public ComponentStatus GetDeviceComponentStatus (string deviceId, string componentId)
        {
             SmartThingsNet.Client.ApiResponse<ComponentStatus> localVarResponse = GetDeviceComponentStatusWithHttpInfo(deviceId, componentId);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Get a device component&#39;s status. Get the status of all attributes of a the component. The results may be filtered if the requester only has permission to view a subset of the component&#39;s capabilities. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">the device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <returns>ApiResponse of Dictionary&lt;string, Dictionary&gt;</returns>
        public SmartThingsNet.Client.ApiResponse<ComponentStatus> GetDeviceComponentStatusWithHttpInfo (string deviceId, string componentId)
        {
            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->GetDeviceComponentStatus");

            // verify the required parameter 'componentId' is set
            if (componentId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'componentId' when calling DevicesApi->GetDeviceComponentStatus");

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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            localVarRequestOptions.PathParameters.Add("componentId", SmartThingsNet.Client.ClientUtils.ParameterToString(componentId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ComponentStatus>("/devices/{deviceId}/components/{componentId}/status", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDeviceComponentStatus", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get a device component&#39;s status. Get the status of all attributes of a the component. The results may be filtered if the requester only has permission to view a subset of the component&#39;s capabilities. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <returns>Task of Dictionary&lt;string, Dictionary&gt;</returns>
        public async System.Threading.Tasks.Task<ComponentStatus> GetDeviceComponentStatusAsync (string deviceId, string componentId)
        {
             SmartThingsNet.Client.ApiResponse<ComponentStatus> localVarResponse = await GetDeviceComponentStatusAsyncWithHttpInfo(deviceId, componentId);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Get a device component&#39;s status. Get the status of all attributes of a the component. The results may be filtered if the requester only has permission to view a subset of the component&#39;s capabilities. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">the device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <returns>Task of ApiResponse (Dictionary&lt;string, Dictionary&gt;)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<ComponentStatus>> GetDeviceComponentStatusAsyncWithHttpInfo (string deviceId, string componentId)
        {
            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->GetDeviceComponentStatus");

            // verify the required parameter 'componentId' is set
            if (componentId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'componentId' when calling DevicesApi->GetDeviceComponentStatus");


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
            
            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            localVarRequestOptions.PathParameters.Add("componentId", SmartThingsNet.Client.ClientUtils.ParameterToString(componentId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<ComponentStatus>("/devices/{deviceId}/components/{componentId}/status", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDeviceComponentStatus", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get the full status of a device. Get the current status of all of a device&#39;s component&#39;s attributes. The results may be filtered if the requester only has permission to view a subset of the device&#39;s components or capabilities. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <returns>DeviceStatus</returns>
        public DeviceStatus GetDeviceStatus (string deviceId)
        {
             SmartThingsNet.Client.ApiResponse<DeviceStatus> localVarResponse = GetDeviceStatusWithHttpInfo(deviceId);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Get the full status of a device. Get the current status of all of a device&#39;s component&#39;s attributes. The results may be filtered if the requester only has permission to view a subset of the device&#39;s components or capabilities. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <returns>ApiResponse of DeviceStatus</returns>
        public SmartThingsNet.Client.ApiResponse< DeviceStatus > GetDeviceStatusWithHttpInfo (string deviceId)
        {
            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->GetDeviceStatus");

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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get< DeviceStatus >("/devices/{deviceId}/status", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDeviceStatus", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get the full status of a device. Get the current status of all of a device&#39;s component&#39;s attributes. The results may be filtered if the requester only has permission to view a subset of the device&#39;s components or capabilities. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">the device ID</param>
        /// <returns>Task of DeviceStatus</returns>
        public async System.Threading.Tasks.Task<DeviceStatus> GetDeviceStatusAsync (string deviceId)
        {
             SmartThingsNet.Client.ApiResponse<DeviceStatus> localVarResponse = await GetDeviceStatusAsyncWithHttpInfo(deviceId);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Get the full status of a device. Get the current status of all of a device&#39;s component&#39;s attributes. The results may be filtered if the requester only has permission to view a subset of the device&#39;s components or capabilities. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <returns>Task of ApiResponse (DeviceStatus)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<DeviceStatus>> GetDeviceStatusAsyncWithHttpInfo (string deviceId)
        {
            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->GetDeviceStatus");


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
            
            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<DeviceStatus>("/devices/{deviceId}/status", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDeviceStatus", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get a capability&#39;s status. Get the current status of a device component&#39;s capability. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <param name="capabilityId">The ID of the capability</param>
        /// <returns>Dictionary&lt;string, AttributeState&gt;</returns>
        public Dictionary<string, AttributeState> GetDeviceStatusByCapability (string deviceId, string componentId, string capabilityId)
        {
             SmartThingsNet.Client.ApiResponse<Dictionary<string, AttributeState>> localVarResponse = GetDeviceStatusByCapabilityWithHttpInfo(deviceId, componentId, capabilityId);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Get a capability&#39;s status. Get the current status of a device component&#39;s capability. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <param name="capabilityId">The ID of the capability</param>
        /// <returns>ApiResponse of Dictionary&lt;string, AttributeState&gt;</returns>
        public SmartThingsNet.Client.ApiResponse< Dictionary<string, AttributeState> > GetDeviceStatusByCapabilityWithHttpInfo (string deviceId, string componentId, string capabilityId)
        {
            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->GetDeviceStatusByCapability");

            // verify the required parameter 'componentId' is set
            if (componentId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'componentId' when calling DevicesApi->GetDeviceStatusByCapability");

            // verify the required parameter 'capabilityId' is set
            if (capabilityId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityId' when calling DevicesApi->GetDeviceStatusByCapability");

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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            localVarRequestOptions.PathParameters.Add("componentId", SmartThingsNet.Client.ClientUtils.ParameterToString(componentId)); // path parameter
            localVarRequestOptions.PathParameters.Add("capabilityId", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get< Dictionary<string, AttributeState> >("/devices/{deviceId}/components/{componentId}/capabilities/{capabilityId}/status", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDeviceStatusByCapability", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get a capability&#39;s status. Get the current status of a device component&#39;s capability. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <param name="capabilityId">The ID of the capability</param>
        /// <returns>Task of Dictionary&lt;string, AttributeState&gt;</returns>
        public async System.Threading.Tasks.Task<Dictionary<string, AttributeState>> GetDeviceStatusByCapabilityAsync (string deviceId, string componentId, string capabilityId)
        {
             SmartThingsNet.Client.ApiResponse<Dictionary<string, AttributeState>> localVarResponse = await GetDeviceStatusByCapabilityAsyncWithHttpInfo(deviceId, componentId, capabilityId);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Get a capability&#39;s status. Get the current status of a device component&#39;s capability. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="deviceId">the device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <param name="capabilityId">The ID of the capability</param>
        /// <returns>Task of ApiResponse (Dictionary&lt;string, AttributeState&gt;)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Dictionary<string, AttributeState>>> GetDeviceStatusByCapabilityAsyncWithHttpInfo (string deviceId, string componentId, string capabilityId)
        {
            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->GetDeviceStatusByCapability");

            // verify the required parameter 'componentId' is set
            if (componentId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'componentId' when calling DevicesApi->GetDeviceStatusByCapability");

            // verify the required parameter 'capabilityId' is set
            if (capabilityId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityId' when calling DevicesApi->GetDeviceStatusByCapability");


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
            
            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            localVarRequestOptions.PathParameters.Add("componentId", SmartThingsNet.Client.ClientUtils.ParameterToString(componentId)); // path parameter
            localVarRequestOptions.PathParameters.Add("capabilityId", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<Dictionary<string, AttributeState>>("/devices/{deviceId}/components/{componentId}/capabilities/{capabilityId}/status", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDeviceStatusByCapability", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List devices. Get a list of devices.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="capability">The device capabilities to filter the results by. The capabilities are treated as an \&quot;and\&quot; so all capabilities must be present.  (optional)</param>
        /// <param name="locationId">The device locations to filter the results by.  (optional)</param>
        /// <param name="deviceId">The device ids to filter the results by.  (optional)</param>
        /// <param name="capabilitiesMode">Treat all capability filter query params as a logical \&quot;or\&quot; or \&quot;and\&quot; with a default of \&quot;and\&quot;.  (optional, default to and)</param>
        /// <returns>PagedDevices</returns>
        public PagedDevices GetDevices (List<string> capability = default(List<string>), List<string> locationId = default(List<string>), List<string> deviceId = default(List<string>), string capabilitiesMode = default(string))
        {
             SmartThingsNet.Client.ApiResponse<PagedDevices> localVarResponse = GetDevicesWithHttpInfo(capability, locationId, deviceId, capabilitiesMode);
             return localVarResponse.Data;
        }

        /// <summary>
        /// List devices. Get a list of devices.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="capability">The device capabilities to filter the results by. The capabilities are treated as an \&quot;and\&quot; so all capabilities must be present.  (optional)</param>
        /// <param name="locationId">The device locations to filter the results by.  (optional)</param>
        /// <param name="deviceId">The device ids to filter the results by.  (optional)</param>
        /// <param name="capabilitiesMode">Treat all capability filter query params as a logical \&quot;or\&quot; or \&quot;and\&quot; with a default of \&quot;and\&quot;.  (optional, default to and)</param>
        /// <returns>ApiResponse of PagedDevices</returns>
        public SmartThingsNet.Client.ApiResponse< PagedDevices > GetDevicesWithHttpInfo (List<string> capability = default(List<string>), List<string> locationId = default(List<string>), List<string> deviceId = default(List<string>), string capabilitiesMode = default(string))
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

            if (capability != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("multi", "capability", capability));
            }
            if (locationId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("multi", "locationId", locationId));
            }
            if (deviceId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("multi", "deviceId", deviceId));
            }
            if (capabilitiesMode != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "capabilitiesMode", capabilitiesMode));
            }
            //

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get< PagedDevices >("/devices", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDevices", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List devices. Get a list of devices.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="capability">The device capabilities to filter the results by. The capabilities are treated as an \&quot;and\&quot; so all capabilities must be present.  (optional)</param>
        /// <param name="locationId">The device locations to filter the results by.  (optional)</param>
        /// <param name="deviceId">The device ids to filter the results by.  (optional)</param>
        /// <param name="capabilitiesMode">Treat all capability filter query params as a logical \&quot;or\&quot; or \&quot;and\&quot; with a default of \&quot;and\&quot;.  (optional, default to and)</param>
        /// <returns>Task of PagedDevices</returns>
        public async System.Threading.Tasks.Task<PagedDevices> GetDevicesAsync (List<string> capability = default(List<string>), List<string> locationId = default(List<string>), List<string> deviceId = default(List<string>), string capabilitiesMode = default(string))
        {
             SmartThingsNet.Client.ApiResponse<PagedDevices> localVarResponse = await GetDevicesAsyncWithHttpInfo(capability, locationId, deviceId, capabilitiesMode);
             return localVarResponse.Data;

        }

        /// <summary>
        /// List devices. Get a list of devices.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="capability">The device capabilities to filter the results by. The capabilities are treated as an \&quot;and\&quot; so all capabilities must be present.  (optional)</param>
        /// <param name="locationId">The device locations to filter the results by.  (optional)</param>
        /// <param name="deviceId">The device ids to filter the results by.  (optional)</param>
        /// <param name="capabilitiesMode">Treat all capability filter query params as a logical \&quot;or\&quot; or \&quot;and\&quot; with a default of \&quot;and\&quot;.  (optional, default to and)</param>
        /// <returns>Task of ApiResponse (PagedDevices)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<PagedDevices>> GetDevicesAsyncWithHttpInfo (List<string> capability = default(List<string>), List<string> locationId = default(List<string>), List<string> deviceId = default(List<string>), string capabilitiesMode = default(string))
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
            
            if (capability != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("multi", "capability", capability));
            }
            if (locationId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("multi", "locationId", locationId));
            }
            if (deviceId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("multi", "deviceId", deviceId));
            }
            if (capabilitiesMode != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "capabilitiesMode", capabilitiesMode));
            }
            

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<PagedDevices>("/devices", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDevices", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Install a Device. Install a device. This is only available for SmartApp managed devices. The SmartApp that creates the device is responsible for handling commands for the device and updating the status of the device by creating events. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installationRequest">Installation Request</param>
        /// <returns>Device</returns>
        public Device InstallDevice (DeviceInstallRequest installationRequest)
        {
             SmartThingsNet.Client.ApiResponse<Device> localVarResponse = InstallDeviceWithHttpInfo(installationRequest);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Install a Device. Install a device. This is only available for SmartApp managed devices. The SmartApp that creates the device is responsible for handling commands for the device and updating the status of the device by creating events. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installationRequest">Installation Request</param>
        /// <returns>ApiResponse of Device</returns>
        public SmartThingsNet.Client.ApiResponse< Device > InstallDeviceWithHttpInfo (DeviceInstallRequest installationRequest)
        {
            // verify the required parameter 'installationRequest' is set
            if (installationRequest == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installationRequest' when calling DevicesApi->InstallDevice");

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

            
            localVarRequestOptions.Data = installationRequest;

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post< Device >("/devices", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("InstallDevice", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Install a Device. Install a device. This is only available for SmartApp managed devices. The SmartApp that creates the device is responsible for handling commands for the device and updating the status of the device by creating events. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>

        /// <param name="installationRequest">Installation Request</param>
        /// <returns>Task of Device</returns>
        public async System.Threading.Tasks.Task<Device> InstallDeviceAsync (DeviceInstallRequest installationRequest)
        {
             SmartThingsNet.Client.ApiResponse<Device> localVarResponse = await InstallDeviceAsyncWithHttpInfo(installationRequest);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Install a Device. Install a device. This is only available for SmartApp managed devices. The SmartApp that creates the device is responsible for handling commands for the device and updating the status of the device by creating events. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installationRequest">Installation Request</param>
        /// <returns>Task of ApiResponse (Device)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Device>> InstallDeviceAsyncWithHttpInfo (DeviceInstallRequest installationRequest)
        {
            // verify the required parameter 'installationRequest' is set
            if (installationRequest == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installationRequest' when calling DevicesApi->InstallDevice");


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
            
            
            localVarRequestOptions.Data = installationRequest;

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<Device>("/devices", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("InstallDevice", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update a device. Update the properties of a device. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">the device ID</param>
        /// <param name="updateDeviceRequest"></param>
        /// <returns>Device</returns>
        public Device UpdateDevice (string deviceId, UpdateDeviceRequest updateDeviceRequest)
        {
             SmartThingsNet.Client.ApiResponse<Device> localVarResponse = UpdateDeviceWithHttpInfo(deviceId, updateDeviceRequest);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Update a device. Update the properties of a device. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">the device ID</param>
        /// <param name="updateDeviceRequest"></param>
        /// <returns>ApiResponse of Device</returns>
        public SmartThingsNet.Client.ApiResponse< Device > UpdateDeviceWithHttpInfo (string deviceId, UpdateDeviceRequest updateDeviceRequest)
        {
            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->UpdateDevice");

            // verify the required parameter 'updateDeviceRequest' is set
            if (updateDeviceRequest == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'updateDeviceRequest' when calling DevicesApi->UpdateDevice");

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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            
            localVarRequestOptions.Data = updateDeviceRequest;

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put< Device >("/devices/{deviceId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateDevice", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update a device. Update the properties of a device. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">the device ID</param>
        /// <param name="updateDeviceRequest"></param>
        /// <returns>Task of Device</returns>
        public async System.Threading.Tasks.Task<Device> UpdateDeviceAsync (string deviceId, UpdateDeviceRequest updateDeviceRequest)
        {
             SmartThingsNet.Client.ApiResponse<Device> localVarResponse = await UpdateDeviceAsyncWithHttpInfo(deviceId, updateDeviceRequest);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Update a device. Update the properties of a device. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="deviceId">the device ID</param>
        /// <param name="updateDeviceRequest"></param>
        /// <returns>Task of ApiResponse (Device)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Device>> UpdateDeviceAsyncWithHttpInfo (string deviceId, UpdateDeviceRequest updateDeviceRequest)
        {
            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->UpdateDevice");

            // verify the required parameter 'updateDeviceRequest' is set
            if (updateDeviceRequest == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'updateDeviceRequest' when calling DevicesApi->UpdateDevice");


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
            
            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            
            localVarRequestOptions.Data = updateDeviceRequest;

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PutAsync<Device>("/devices/{deviceId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateDevice", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

    }
}
