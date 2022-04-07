/*
 * SmartThings API
 *
 * # Overview  This is the reference documentation for the SmartThings API.  The SmartThings API, a RESTful API, provides a method for your integration to communicate with the SmartThings Platform. The API is the core of the platform. It is used to control devices, create Automations, manage Locations, retrieve user and device information; if you want to communicate with the SmartThings platform, you’ll be using the SmartThings API. All responses are sent as [JSON](http://www.json.org/).  The SmartThings API consists of several endpoints, including Rules, Locations, Devices, and more. Even though each of these endpoints are not standalone APIs, you may hear them referred to as such. For example, the Rules API is used to build Automations.  # Authentication  Before using the SmartThings API, you’ll need to obtain an Authorization Token. All SmartThings resources are protected with [OAuth 2.0 Bearer Tokens](https://tools.ietf.org/html/rfc6750#section-2.1) sent on the request as an `Authorization: Bearer <TOKEN>` header. Operations require specific OAuth scopes that specify the exact permissions authorized by the user.  ## Authorization token types  There are two types of tokens:   * SmartApp tokens   * Personal access tokens (PAT).  ### SmartApp tokens  SmartApp tokens are used to communicate between third-party integrations, or SmartApps, and the SmartThings API. When a SmartApp is called by the SmartThings platform, it is sent an authorization token that can be used to interact with the SmartThings API.  ### Personal access tokens  Personal access tokens are used to authorize interaction with the API for non-SmartApp use cases. When creating personal access tokens, you can specifiy the permissions granted to the token. These permissions define the OAuth2 scopes for the personal access token.  Personal access tokesn can be created and managed on the [personal access tokens page](https://account.smartthings.com/tokens).  ## OAuth2 scopes  Operations may be protected by one or more OAuth security schemes, which specify the required permissions. Each scope for a given scheme is required. If multiple schemes are specified (uncommon), you may use either scheme.  SmartApp token scopes are derived from the permissions requested by the SmartApp and granted by the end-user during installation. Personal access token scopes are associated with the specific permissions authorized when the token is created.  Scopes are generally in the form `permission:entity-type:entity-id`.  **An `*` used for the `entity-id` specifies that the permission may be applied to all entities that the token type has access to, or may be replaced with a specific ID.**  For more information about authrization and permissions, visit the [Authorization section](https://developer-preview.smartthings.com/docs/advanced/authorization-and-permissions) in the SmartThings documentation.  <!- - ReDoc-Inject: <security-definitions> - ->  # Errors  The SmartThings API uses conventional HTTP response codes to indicate the success or failure of a request.  In general:  * A `2XX` response code indicates success * A `4XX` response code indicates an error given the inputs for the request * A `5XX` response code indicates a failure on the SmartThings platform  API errors will contain a JSON response body with more information about the error:  ```json {   \"requestId\": \"031fec1a-f19f-470a-a7da-710569082846\"   \"error\": {     \"code\": \"ConstraintViolationError\",     \"message\": \"Validation errors occurred while process your request.\",     \"details\": [       { \"code\": \"PatternError\", \"target\": \"latitude\", \"message\": \"Invalid format.\" },       { \"code\": \"SizeError\", \"target\": \"name\", \"message\": \"Too small.\" },       { \"code\": \"SizeError\", \"target\": \"description\", \"message\": \"Too big.\" }     ]   } } ```  ## Error Response Body  The error response attributes are:  | Property | Type | Required | Description | | - -- | - -- | - -- | - -- | | requestId | String | No | A request identifier that can be used to correlate an error to additional logging on the SmartThings servers. | error | Error | **Yes** | The Error object, documented below.  ## Error Object  The Error object contains the following attributes:  | Property | Type | Required | Description | | - -- | - -- | - -- | - -- | | code | String | **Yes** | A SmartThings-defined error code that serves as a more specific indicator of the error than the HTTP error code specified in the response. See [SmartThings Error Codes](#section/Errors/SmartThings-Error-Codes) for more information. | message | String | **Yes** | A description of the error, intended to aid debugging of error responses. | target | String | No | The target of the error. For example, this could be the name of the property that caused the error. | details | Error[] | No | An array of Error objects that typically represent distinct, related errors that occurred during the request. As an optional attribute, this may be null or an empty array.  ## Standard HTTP Error Codes  The following table lists the most common HTTP error responses:  | Code | Name | Description | | - -- | - -- | - -- | | 400 | Bad Request | The client has issued an invalid request. This is commonly used to specify validation errors in a request payload. | 401 | Unauthorized | Authorization for the API is required, but the request has not been authenticated. | 403 | Forbidden | The request has been authenticated but does not have appropriate permissions, or a requested resource is not found. | 404 | Not Found | The requested path does not exist. | 406 | Not Acceptable | The client has requested a MIME type via the Accept header for a value not supported by the server. | 415 | Unsupported Media Type | The client has defined a contentType header that is not supported by the server. | 422 | Unprocessable Entity | The client has made a valid request, but the server cannot process it. This is often used for APIs for which certain limits have been exceeded. | 429 | Too Many Requests | The client has exceeded the number of requests allowed for a given time window. | 500 | Internal Server Error | An unexpected error on the SmartThings servers has occurred. These errors are generally rare. | 501 | Not Implemented | The client request was valid and understood by the server, but the requested feature has yet to be implemented. These errors are generally rare.  ## SmartThings Error Codes  SmartThings specifies several standard custom error codes. These provide more information than the standard HTTP error response codes. The following table lists the standard SmartThings error codes and their descriptions:  | Code | Typical HTTP Status Codes | Description | | - -- | - -- | - -- | | PatternError | 400, 422 | The client has provided input that does not match the expected pattern. | ConstraintViolationError | 422 | The client has provided input that has violated one or more constraints. | NotNullError | 422 | The client has provided a null input for a field that is required to be non-null. | NullError | 422 | The client has provided an input for a field that is required to be null. | NotEmptyError | 422 | The client has provided an empty input for a field that is required to be non-empty. | SizeError | 400, 422 | The client has provided a value that does not meet size restrictions. | Unexpected Error | 500 | A non-recoverable error condition has occurred. A problem occurred on the SmartThings server that is no fault of the client. | UnprocessableEntityError | 422 | The client has sent a malformed request body. | TooManyRequestError | 429 | The client issued too many requests too quickly. | LimitError | 422 | The client has exceeded certain limits an API enforces. | UnsupportedOperationError | 400, 422 | The client has issued a request to a feature that currently isn't supported by the SmartThings platform. These errors are generally rare.  ## Custom Error Codes  An API may define its own error codes where appropriate. Custom error codes are documented in each API endpoint's documentation section.  # Warnings The SmartThings API issues warning messages via standard HTTP Warning headers. These messages do not represent a request failure, but provide additional information that the requester might want to act upon. For example, a warning will be issued if you are using an old API version.  # API Versions  The SmartThings API supports both path and header-based versioning. The following are equivalent:  - https://api.smartthings.com/v1/locations - https://api.smartthings.com/locations with header `Accept: application/vnd.smartthings+json;v=1`  Currently, only version 1 is available.  # Paging  Operations that return a list of objects return a paginated response. The `_links` object contains the items returned, and links to the next and previous result page, if applicable.  ```json {   \"items\": [     {       \"locationId\": \"6b3d1909-1e1c-43ec-adc2-5f941de4fbf9\",       \"name\": \"Home\"     },     {       \"locationId\": \"6b3d1909-1e1c-43ec-adc2-5f94d6g4fbf9\",       \"name\": \"Work\"     }     ....   ],   \"_links\": {     \"next\": {       \"href\": \"https://api.smartthings.com/v1/locations?page=3\"     },     \"previous\": {       \"href\": \"https://api.smartthings.com/v1/locations?page=1\"     }   } } ```  # Allowed Permissions  The response payload to a request for a SmartThings entity (e.g. a location or a device) may contain an `allowed` list property.  This list contains strings called **action identifiers** (such as `w:locations`) that provide information about what actions are permitted by the user's token on that entity at the time the request was processed.  The action identifiers are defined in the API documentation for the 200 response of the particular endpoint queried. For each documented action identifier (e.g. `w:locations`), you will find a description of the user action (e.g. \"edit the name of the location\") associated with that identifier. The endpoint documentation will contain a complete list of the action identifiers that may appear in the allowed list property.  If the `allowed` list property is present in the response payload: * the user action documented for an action identifier present in the list is permitted for the user on the entity at the time the request is processed * the user action documented for an action identifier **not** present in the list is **not** permitted to the user on the entity at the time the request is processed  If the `allowed` list property is **not** present or has a `null` value in the response payload, then the response provides **no information** about any user actions being permissible except that the user has permission to view the returned entity.  The response provides **no information** about the permissibility of user actions that are not specifically mentioned in the documentation for the particular endpoint.  The table below is a high-level guide to interpreting action identifiers.  It does not indicate that any given endpoint will document or return any of the action identifiers listed below. Remember that the endpoint API documentation is the final source of truth for interpreting action identifiers in a response payload.  | Action Identifier Format | Examples | Meaning | | - -- | - -- | - -- | | `w:grant:`\\<grant type> | `w:grant:share` on a location payload | User may **bestow** the grant type on the entity to another user (e.g. through an invitation.) | | `r:`\\<child type> | `r:devices` on a location payload | User may **list and view** child type entities of the returned entity.  NOTE: there may be finer-grained controls on the child type entities. | | `l:`\\<child type> | `l:devices` on a location payload | User may **list and summarize** child type entities of the returned entity.  NOTE: there may be finer-grained controls on the child type entities.  This is weaker than `r:`\\<child type> and rarely used. | | `w:`\\<child type> | `w:devices` on a location payload | User may **create** entities of the child type as children of the returned entity. | | `x:`\\<child type> | `x:devices` on a location payload | User may **execute commands** on child type entities of the returned entity.  NOTE: there may be finer-grained controls on the child type entities. | | `r:`\\<entity type> | `r:locations` on a location payload | This will only be returned in a list/summary response and only in a case when the list/summary is designed not to show all the details of the entity. | | `w:`\\<entity type> | `w:locations` on a location payload<br/>`w:devices` on a device payload | User may **edit** specificly-documented properties of the returned entity. | | `x:`\\<entity type> | `x:devices` on a device payload | user may **execute commands** on the returned entity.  NOTE: there may be finer-grained controls on the children of the entity. | | `d:`\\<entity type> | `d:locations` on a location payload | User may **delete** the returned entity. | | `r:`\\<entity type>`:`\\<attribute group> | `r:locations:currentMode` on a location payload | User may **view** the specified (and clearly-documented) attribute or attribute group of the returned entity. | | `w:`\\<entity type>`:`\\<attribute group> | `w:locations:geo` on a location payload | User may **edit** the specified (and clearly-documented) attribute or attribute group of the returned entity. | | `x:`\\<entity type>`:`\\<attribute group> | `x:devices:switch` on a device payload | User may **execute commands** on the specified (and clearly-documented) attribute or attribute group of the returned entity. |  # Localization  Some SmartThings APIs support localization. Specific information regarding localization endpoints are documented in the API itself. However, the following applies to all endpoints that support localization.  ## Fallback Patterns  When making a request with the `Accept-Language` header, the following fallback pattern is observed: 1. Response will be translated with exact locale tag. 2. If a translation does not exist for the requested language and region, the translation for the language will be returned. 3. If a translation does not exist for the language, English (en) will be returned. 4. Finally, an untranslated response will be returned in the absense of the above translations.  ## Accept-Language Header The format of the `Accept-Language` header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5)  ## Content-Language The `Content-Language` header should be set on the response from the server to indicate which translation was given back to the client. The absense of the header indicates that the server did not recieve a request with the `Accept-Language` header set. 
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
    public interface IDevicesApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Create Device Events
        /// </summary>
        /// <remarks>
        /// Create events for a Device.  When a Device is managed by a SmartApp, the Device is responsible for creating events to update the Device attributes on the SmartThings Platform.  The OAuth token used must be for the SmartApp that created the Device. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="deviceEventRequest"></param>
        /// <returns>Object</returns>
        Object CreateDeviceEvents(string authorization, string deviceId, DeviceEventsRequest deviceEventRequest);

        /// <summary>
        /// Create Device Events
        /// </summary>
        /// <remarks>
        /// Create events for a Device.  When a Device is managed by a SmartApp, the Device is responsible for creating events to update the Device attributes on the SmartThings Platform.  The OAuth token used must be for the SmartApp that created the Device. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="deviceEventRequest"></param>
        /// <returns>ApiResponse of Object</returns>
        ApiResponse<Object> CreateDeviceEventsWithHttpInfo(string authorization, string deviceId, DeviceEventsRequest deviceEventRequest);
        /// <summary>
        /// Delete a Device
        /// </summary>
        /// <remarks>
        /// Delete a Device with a given Device ID.  If the OAuth token used for this call is for a SmartApp that created the Device you are attempting to delete, the call implicitly has permission for this API. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <returns>Object</returns>
        Object DeleteDevice(string authorization, string deviceId);

        /// <summary>
        /// Delete a Device
        /// </summary>
        /// <remarks>
        /// Delete a Device with a given Device ID.  If the OAuth token used for this call is for a SmartApp that created the Device you are attempting to delete, the call implicitly has permission for this API. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <returns>ApiResponse of Object</returns>
        ApiResponse<Object> DeleteDeviceWithHttpInfo(string authorization, string deviceId);
        /// <summary>
        /// Execute Commands on a Device
        /// </summary>
        /// <remarks>
        /// Execute a specified command on a Device.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="executeCapabilityCommand"></param>
        /// <param name="ordered">Specifies whether the command should be executed in order or asynchronously. (optional)</param>
        /// <returns>DeviceCommandsResponse</returns>
        DeviceCommandsResponse ExecuteDeviceCommands(string authorization, string deviceId, DeviceCommandsRequest executeCapabilityCommand, bool? ordered = default(bool?));

        /// <summary>
        /// Execute Commands on a Device
        /// </summary>
        /// <remarks>
        /// Execute a specified command on a Device.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="executeCapabilityCommand"></param>
        /// <param name="ordered">Specifies whether the command should be executed in order or asynchronously. (optional)</param>
        /// <returns>ApiResponse of DeviceCommandsResponse</returns>
        ApiResponse<DeviceCommandsResponse> ExecuteDeviceCommandsWithHttpInfo(string authorization, string deviceId, DeviceCommandsRequest executeCapabilityCommand, bool? ordered = default(bool?));
        /// <summary>
        /// Get the Description of a Device
        /// </summary>
        /// <remarks>
        /// Get a Device&#39;s given description.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <returns>Device</returns>
        Device GetDevice(string authorization, string deviceId);

        /// <summary>
        /// Get the Description of a Device
        /// </summary>
        /// <remarks>
        /// Get a Device&#39;s given description.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <returns>ApiResponse of Device</returns>
        ApiResponse<Device> GetDeviceWithHttpInfo(string authorization, string deviceId);
        /// <summary>
        /// Get the Status of a Device Component
        /// </summary>
        /// <remarks>
        /// Get the status of all attributes of a specified component.  The results may be filtered if the requester only has permission to view a subset of the component&#39;s capabilities.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <returns>Dictionary&lt;string, Dictionary&gt;</returns>
        Dictionary<string, string> GetDeviceComponentStatus(string authorization, string deviceId, string componentId);

        /// <summary>
        /// Get the Status of a Device Component
        /// </summary>
        /// <remarks>
        /// Get the status of all attributes of a specified component.  The results may be filtered if the requester only has permission to view a subset of the component&#39;s capabilities.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <returns>ApiResponse of Dictionary&lt;string, Dictionary&gt;</returns>
        ApiResponse<Dictionary<string, string>> GetDeviceComponentStatusWithHttpInfo(string authorization, string deviceId, string componentId);
        /// <summary>
        /// Get the Full Status of a Device
        /// </summary>
        /// <remarks>
        /// Get the current status of all of a Device component&#39;s attributes.  The results may be filtered if the requester only has permission to view a subset of the Device&#39;s components or capabilities.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <returns>DeviceStatus</returns>
        DeviceStatus GetDeviceStatus(string authorization, string deviceId);

        /// <summary>
        /// Get the Full Status of a Device
        /// </summary>
        /// <remarks>
        /// Get the current status of all of a Device component&#39;s attributes.  The results may be filtered if the requester only has permission to view a subset of the Device&#39;s components or capabilities.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <returns>ApiResponse of DeviceStatus</returns>
        ApiResponse<DeviceStatus> GetDeviceStatusWithHttpInfo(string authorization, string deviceId);
        /// <summary>
        /// Get the Status of a Capability
        /// </summary>
        /// <remarks>
        /// Get the current status of a Device component&#39;s capability.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <param name="capabilityId">The ID of the capability</param>
        /// <returns>Dictionary&lt;string, AttributeState&gt;</returns>
        Dictionary<string, AttributeState> GetDeviceStatusByCapability(string authorization, string deviceId, string componentId, string capabilityId);

        /// <summary>
        /// Get the Status of a Capability
        /// </summary>
        /// <remarks>
        /// Get the current status of a Device component&#39;s capability.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <param name="capabilityId">The ID of the capability</param>
        /// <returns>ApiResponse of Dictionary&lt;string, AttributeState&gt;</returns>
        ApiResponse<Dictionary<string, AttributeState>> GetDeviceStatusByCapabilityWithHttpInfo(string authorization, string deviceId, string componentId, string capabilityId);
        /// <summary>
        /// List Devices
        /// </summary>
        /// <remarks>
        /// Get a list of devices.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capability">The Device capabilities to filter the results by. The capabilities are treated as an \&quot;and\&quot; so all capabilities must be present.  (optional)</param>
        /// <param name="locationId">The Device Locations to filter the results by.  (optional)</param>
        /// <param name="deviceId">The Device IDs to filter the results by.  (optional)</param>
        /// <param name="capabilitiesMode">Treat all capability filter query params as a logical \&quot;or\&quot; or \&quot;and\&quot; with a default of \&quot;and\&quot;.  (optional, default to and)</param>
        /// <param name="includeRestricted">Restricted Devices are hidden by default. This query parameter will reveal them. Device status will not be provided if the token does not have sufficient access level to view the device status even if includeStatus parameter is set to true.  (optional)</param>
        /// <param name="accessLevel">Only list Devices accessible by the given accessLevel.  (optional)</param>
        /// <param name="includeAllowedActions">Include the actions permitted by this token&#39;s access for each individual resource.  (optional)</param>
        /// <returns>PagedDevices</returns>
        PagedDevices GetDevices(string authorization, List<string> capability = default(List<string>), List<string> locationId = default(List<string>), List<string> deviceId = default(List<string>), string capabilitiesMode = default(string), bool? includeRestricted = default(bool?), int? accessLevel = default(int?), bool? includeAllowedActions = default(bool?));

        /// <summary>
        /// List Devices
        /// </summary>
        /// <remarks>
        /// Get a list of devices.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capability">The Device capabilities to filter the results by. The capabilities are treated as an \&quot;and\&quot; so all capabilities must be present.  (optional)</param>
        /// <param name="locationId">The Device Locations to filter the results by.  (optional)</param>
        /// <param name="deviceId">The Device IDs to filter the results by.  (optional)</param>
        /// <param name="capabilitiesMode">Treat all capability filter query params as a logical \&quot;or\&quot; or \&quot;and\&quot; with a default of \&quot;and\&quot;.  (optional, default to and)</param>
        /// <param name="includeRestricted">Restricted Devices are hidden by default. This query parameter will reveal them. Device status will not be provided if the token does not have sufficient access level to view the device status even if includeStatus parameter is set to true.  (optional)</param>
        /// <param name="accessLevel">Only list Devices accessible by the given accessLevel.  (optional)</param>
        /// <param name="includeAllowedActions">Include the actions permitted by this token&#39;s access for each individual resource.  (optional)</param>
        /// <returns>ApiResponse of PagedDevices</returns>
        ApiResponse<PagedDevices> GetDevicesWithHttpInfo(string authorization, List<string> capability = default(List<string>), List<string> locationId = default(List<string>), List<string> deviceId = default(List<string>), string capabilitiesMode = default(string), bool? includeRestricted = default(bool?), int? accessLevel = default(int?), bool? includeAllowedActions = default(bool?));
        /// <summary>
        /// Install a Device
        /// </summary>
        /// <remarks>
        /// Install a Device.  This call is only available for SmartApp-managed Devices. The SmartApp that creates the Device is responsible for handling commands for the Device and updating the status of the Device by creating events. Requires installed app principal. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="installationRequest">Installation Request</param>
        /// <returns>Device</returns>
        Device InstallDevice(string authorization, DeviceInstallRequest installationRequest);

        /// <summary>
        /// Install a Device
        /// </summary>
        /// <remarks>
        /// Install a Device.  This call is only available for SmartApp-managed Devices. The SmartApp that creates the Device is responsible for handling commands for the Device and updating the status of the Device by creating events. Requires installed app principal. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="installationRequest">Installation Request</param>
        /// <returns>ApiResponse of Device</returns>
        ApiResponse<Device> InstallDeviceWithHttpInfo(string authorization, DeviceInstallRequest installationRequest);
        /// <summary>
        /// Update a Device
        /// </summary>
        /// <remarks>
        /// Update the properties of a Device.  If the OAuth token used for this call is for a SmartApp that created the Device you are attempting to update, the call implicitly has permission for this API. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="updateDeviceRequest"></param>
        /// <returns>Device</returns>
        Device UpdateDevice(string authorization, string deviceId, UpdateDeviceRequest updateDeviceRequest);

        /// <summary>
        /// Update a Device
        /// </summary>
        /// <remarks>
        /// Update the properties of a Device.  If the OAuth token used for this call is for a SmartApp that created the Device you are attempting to update, the call implicitly has permission for this API. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="updateDeviceRequest"></param>
        /// <returns>ApiResponse of Device</returns>
        ApiResponse<Device> UpdateDeviceWithHttpInfo(string authorization, string deviceId, UpdateDeviceRequest updateDeviceRequest);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IDevicesApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Create Device Events
        /// </summary>
        /// <remarks>
        /// Create events for a Device.  When a Device is managed by a SmartApp, the Device is responsible for creating events to update the Device attributes on the SmartThings Platform.  The OAuth token used must be for the SmartApp that created the Device. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="deviceEventRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Object</returns>
        System.Threading.Tasks.Task<Object> CreateDeviceEventsAsync(string authorization, string deviceId, DeviceEventsRequest deviceEventRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create Device Events
        /// </summary>
        /// <remarks>
        /// Create events for a Device.  When a Device is managed by a SmartApp, the Device is responsible for creating events to update the Device attributes on the SmartThings Platform.  The OAuth token used must be for the SmartApp that created the Device. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="deviceEventRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Object)</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> CreateDeviceEventsWithHttpInfoAsync(string authorization, string deviceId, DeviceEventsRequest deviceEventRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete a Device
        /// </summary>
        /// <remarks>
        /// Delete a Device with a given Device ID.  If the OAuth token used for this call is for a SmartApp that created the Device you are attempting to delete, the call implicitly has permission for this API. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Object</returns>
        System.Threading.Tasks.Task<Object> DeleteDeviceAsync(string authorization, string deviceId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete a Device
        /// </summary>
        /// <remarks>
        /// Delete a Device with a given Device ID.  If the OAuth token used for this call is for a SmartApp that created the Device you are attempting to delete, the call implicitly has permission for this API. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Object)</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteDeviceWithHttpInfoAsync(string authorization, string deviceId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Execute Commands on a Device
        /// </summary>
        /// <remarks>
        /// Execute a specified command on a Device.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="executeCapabilityCommand"></param>
        /// <param name="ordered">Specifies whether the command should be executed in order or asynchronously. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of DeviceCommandsResponse</returns>
        System.Threading.Tasks.Task<DeviceCommandsResponse> ExecuteDeviceCommandsAsync(string authorization, string deviceId, DeviceCommandsRequest executeCapabilityCommand, bool? ordered = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Execute Commands on a Device
        /// </summary>
        /// <remarks>
        /// Execute a specified command on a Device.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="executeCapabilityCommand"></param>
        /// <param name="ordered">Specifies whether the command should be executed in order or asynchronously. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (DeviceCommandsResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<DeviceCommandsResponse>> ExecuteDeviceCommandsWithHttpInfoAsync(string authorization, string deviceId, DeviceCommandsRequest executeCapabilityCommand, bool? ordered = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get the Description of a Device
        /// </summary>
        /// <remarks>
        /// Get a Device&#39;s given description.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Device</returns>
        System.Threading.Tasks.Task<Device> GetDeviceAsync(string authorization, string deviceId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get the Description of a Device
        /// </summary>
        /// <remarks>
        /// Get a Device&#39;s given description.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Device)</returns>
        System.Threading.Tasks.Task<ApiResponse<Device>> GetDeviceWithHttpInfoAsync(string authorization, string deviceId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get the Status of a Device Component
        /// </summary>
        /// <remarks>
        /// Get the status of all attributes of a specified component.  The results may be filtered if the requester only has permission to view a subset of the component&#39;s capabilities.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Dictionary&lt;string, Dictionary&gt;</returns>
        System.Threading.Tasks.Task<Dictionary<string, string>> GetDeviceComponentStatusAsync(string authorization, string deviceId, string componentId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get the Status of a Device Component
        /// </summary>
        /// <remarks>
        /// Get the status of all attributes of a specified component.  The results may be filtered if the requester only has permission to view a subset of the component&#39;s capabilities.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Dictionary&lt;string, Dictionary&gt;)</returns>
        System.Threading.Tasks.Task<ApiResponse<Dictionary<string, string>>> GetDeviceComponentStatusWithHttpInfoAsync(string authorization, string deviceId, string componentId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get the Full Status of a Device
        /// </summary>
        /// <remarks>
        /// Get the current status of all of a Device component&#39;s attributes.  The results may be filtered if the requester only has permission to view a subset of the Device&#39;s components or capabilities.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of DeviceStatus</returns>
        System.Threading.Tasks.Task<DeviceStatus> GetDeviceStatusAsync(string authorization, string deviceId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get the Full Status of a Device
        /// </summary>
        /// <remarks>
        /// Get the current status of all of a Device component&#39;s attributes.  The results may be filtered if the requester only has permission to view a subset of the Device&#39;s components or capabilities.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (DeviceStatus)</returns>
        System.Threading.Tasks.Task<ApiResponse<DeviceStatus>> GetDeviceStatusWithHttpInfoAsync(string authorization, string deviceId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get the Status of a Capability
        /// </summary>
        /// <remarks>
        /// Get the current status of a Device component&#39;s capability.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <param name="capabilityId">The ID of the capability</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Dictionary&lt;string, AttributeState&gt;</returns>
        System.Threading.Tasks.Task<Dictionary<string, AttributeState>> GetDeviceStatusByCapabilityAsync(string authorization, string deviceId, string componentId, string capabilityId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get the Status of a Capability
        /// </summary>
        /// <remarks>
        /// Get the current status of a Device component&#39;s capability.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <param name="capabilityId">The ID of the capability</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Dictionary&lt;string, AttributeState&gt;)</returns>
        System.Threading.Tasks.Task<ApiResponse<Dictionary<string, AttributeState>>> GetDeviceStatusByCapabilityWithHttpInfoAsync(string authorization, string deviceId, string componentId, string capabilityId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Devices
        /// </summary>
        /// <remarks>
        /// Get a list of devices.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capability">The Device capabilities to filter the results by. The capabilities are treated as an \&quot;and\&quot; so all capabilities must be present.  (optional)</param>
        /// <param name="locationId">The Device Locations to filter the results by.  (optional)</param>
        /// <param name="deviceId">The Device IDs to filter the results by.  (optional)</param>
        /// <param name="capabilitiesMode">Treat all capability filter query params as a logical \&quot;or\&quot; or \&quot;and\&quot; with a default of \&quot;and\&quot;.  (optional, default to and)</param>
        /// <param name="includeRestricted">Restricted Devices are hidden by default. This query parameter will reveal them. Device status will not be provided if the token does not have sufficient access level to view the device status even if includeStatus parameter is set to true.  (optional)</param>
        /// <param name="accessLevel">Only list Devices accessible by the given accessLevel.  (optional)</param>
        /// <param name="includeAllowedActions">Include the actions permitted by this token&#39;s access for each individual resource.  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PagedDevices</returns>
        System.Threading.Tasks.Task<PagedDevices> GetDevicesAsync(string authorization, List<string> capability = default(List<string>), List<string> locationId = default(List<string>), List<string> deviceId = default(List<string>), string capabilitiesMode = default(string), bool? includeRestricted = default(bool?), int? accessLevel = default(int?), bool? includeAllowedActions = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Devices
        /// </summary>
        /// <remarks>
        /// Get a list of devices.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capability">The Device capabilities to filter the results by. The capabilities are treated as an \&quot;and\&quot; so all capabilities must be present.  (optional)</param>
        /// <param name="locationId">The Device Locations to filter the results by.  (optional)</param>
        /// <param name="deviceId">The Device IDs to filter the results by.  (optional)</param>
        /// <param name="capabilitiesMode">Treat all capability filter query params as a logical \&quot;or\&quot; or \&quot;and\&quot; with a default of \&quot;and\&quot;.  (optional, default to and)</param>
        /// <param name="includeRestricted">Restricted Devices are hidden by default. This query parameter will reveal them. Device status will not be provided if the token does not have sufficient access level to view the device status even if includeStatus parameter is set to true.  (optional)</param>
        /// <param name="accessLevel">Only list Devices accessible by the given accessLevel.  (optional)</param>
        /// <param name="includeAllowedActions">Include the actions permitted by this token&#39;s access for each individual resource.  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PagedDevices)</returns>
        System.Threading.Tasks.Task<ApiResponse<PagedDevices>> GetDevicesWithHttpInfoAsync(string authorization, List<string> capability = default(List<string>), List<string> locationId = default(List<string>), List<string> deviceId = default(List<string>), string capabilitiesMode = default(string), bool? includeRestricted = default(bool?), int? accessLevel = default(int?), bool? includeAllowedActions = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Install a Device
        /// </summary>
        /// <remarks>
        /// Install a Device.  This call is only available for SmartApp-managed Devices. The SmartApp that creates the Device is responsible for handling commands for the Device and updating the status of the Device by creating events. Requires installed app principal. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="installationRequest">Installation Request</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Device</returns>
        System.Threading.Tasks.Task<Device> InstallDeviceAsync(string authorization, DeviceInstallRequest installationRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Install a Device
        /// </summary>
        /// <remarks>
        /// Install a Device.  This call is only available for SmartApp-managed Devices. The SmartApp that creates the Device is responsible for handling commands for the Device and updating the status of the Device by creating events. Requires installed app principal. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="installationRequest">Installation Request</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Device)</returns>
        System.Threading.Tasks.Task<ApiResponse<Device>> InstallDeviceWithHttpInfoAsync(string authorization, DeviceInstallRequest installationRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update a Device
        /// </summary>
        /// <remarks>
        /// Update the properties of a Device.  If the OAuth token used for this call is for a SmartApp that created the Device you are attempting to update, the call implicitly has permission for this API. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="updateDeviceRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Device</returns>
        System.Threading.Tasks.Task<Device> UpdateDeviceAsync(string authorization, string deviceId, UpdateDeviceRequest updateDeviceRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update a Device
        /// </summary>
        /// <remarks>
        /// Update the properties of a Device.  If the OAuth token used for this call is for a SmartApp that created the Device you are attempting to update, the call implicitly has permission for this API. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="updateDeviceRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Device)</returns>
        System.Threading.Tasks.Task<ApiResponse<Device>> UpdateDeviceWithHttpInfoAsync(string authorization, string deviceId, UpdateDeviceRequest updateDeviceRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
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
        public DevicesApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DevicesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public DevicesApi(string basePath)
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
        public DevicesApi(SmartThingsNet.Client.ISynchronousClient client, SmartThingsNet.Client.IAsynchronousClient asyncClient, SmartThingsNet.Client.IReadableConfiguration configuration)
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
        /// Create Device Events Create events for a Device.  When a Device is managed by a SmartApp, the Device is responsible for creating events to update the Device attributes on the SmartThings Platform.  The OAuth token used must be for the SmartApp that created the Device. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="deviceEventRequest"></param>
        /// <returns>Object</returns>
        public Object CreateDeviceEvents(string authorization, string deviceId, DeviceEventsRequest deviceEventRequest)
        {
            SmartThingsNet.Client.ApiResponse<Object> localVarResponse = CreateDeviceEventsWithHttpInfo(authorization, deviceId, deviceEventRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Device Events Create events for a Device.  When a Device is managed by a SmartApp, the Device is responsible for creating events to update the Device attributes on the SmartThings Platform.  The OAuth token used must be for the SmartApp that created the Device. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="deviceEventRequest"></param>
        /// <returns>ApiResponse of Object</returns>
        public SmartThingsNet.Client.ApiResponse<Object> CreateDeviceEventsWithHttpInfo(string authorization, string deviceId, DeviceEventsRequest deviceEventRequest)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling DevicesApi->CreateDeviceEvents");
            }

            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->CreateDeviceEvents");
            }

            // verify the required parameter 'deviceEventRequest' is set
            if (deviceEventRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceEventRequest' when calling DevicesApi->CreateDeviceEvents");
            }

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            
            localVarRequestOptions.Data = deviceEventRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/devices/{deviceId}/events", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateDeviceEvents", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Device Events Create events for a Device.  When a Device is managed by a SmartApp, the Device is responsible for creating events to update the Device attributes on the SmartThings Platform.  The OAuth token used must be for the SmartApp that created the Device. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="deviceEventRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Object</returns>
        public async System.Threading.Tasks.Task<Object> CreateDeviceEventsAsync(string authorization, string deviceId, DeviceEventsRequest deviceEventRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<Object> localVarResponse = await CreateDeviceEventsWithHttpInfoAsync(authorization, deviceId, deviceEventRequest, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Device Events Create events for a Device.  When a Device is managed by a SmartApp, the Device is responsible for creating events to update the Device attributes on the SmartThings Platform.  The OAuth token used must be for the SmartApp that created the Device. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="deviceEventRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Object)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Object>> CreateDeviceEventsWithHttpInfoAsync(string authorization, string deviceId, DeviceEventsRequest deviceEventRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling DevicesApi->CreateDeviceEvents");
            }

            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->CreateDeviceEvents");
            }

            // verify the required parameter 'deviceEventRequest' is set
            if (deviceEventRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceEventRequest' when calling DevicesApi->CreateDeviceEvents");
            }


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            
            localVarRequestOptions.Data = deviceEventRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/devices/{deviceId}/events", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateDeviceEvents", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete a Device Delete a Device with a given Device ID.  If the OAuth token used for this call is for a SmartApp that created the Device you are attempting to delete, the call implicitly has permission for this API. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <returns>Object</returns>
        public Object DeleteDevice(string authorization, string deviceId)
        {
            SmartThingsNet.Client.ApiResponse<Object> localVarResponse = DeleteDeviceWithHttpInfo(authorization, deviceId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Delete a Device Delete a Device with a given Device ID.  If the OAuth token used for this call is for a SmartApp that created the Device you are attempting to delete, the call implicitly has permission for this API. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <returns>ApiResponse of Object</returns>
        public SmartThingsNet.Client.ApiResponse<Object> DeleteDeviceWithHttpInfo(string authorization, string deviceId)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling DevicesApi->DeleteDevice");
            }

            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->DeleteDevice");
            }

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/devices/{deviceId}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteDevice", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete a Device Delete a Device with a given Device ID.  If the OAuth token used for this call is for a SmartApp that created the Device you are attempting to delete, the call implicitly has permission for this API. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Object</returns>
        public async System.Threading.Tasks.Task<Object> DeleteDeviceAsync(string authorization, string deviceId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<Object> localVarResponse = await DeleteDeviceWithHttpInfoAsync(authorization, deviceId, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Delete a Device Delete a Device with a given Device ID.  If the OAuth token used for this call is for a SmartApp that created the Device you are attempting to delete, the call implicitly has permission for this API. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Object)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Object>> DeleteDeviceWithHttpInfoAsync(string authorization, string deviceId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling DevicesApi->DeleteDevice");
            }

            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->DeleteDevice");
            }


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/devices/{deviceId}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteDevice", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Execute Commands on a Device Execute a specified command on a Device.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="executeCapabilityCommand"></param>
        /// <param name="ordered">Specifies whether the command should be executed in order or asynchronously. (optional)</param>
        /// <returns>DeviceCommandsResponse</returns>
        public DeviceCommandsResponse ExecuteDeviceCommands(string authorization, string deviceId, DeviceCommandsRequest executeCapabilityCommand, bool? ordered = default(bool?))
        {
            SmartThingsNet.Client.ApiResponse<DeviceCommandsResponse> localVarResponse = ExecuteDeviceCommandsWithHttpInfo(authorization, deviceId, executeCapabilityCommand, ordered);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Execute Commands on a Device Execute a specified command on a Device.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="executeCapabilityCommand"></param>
        /// <param name="ordered">Specifies whether the command should be executed in order or asynchronously. (optional)</param>
        /// <returns>ApiResponse of DeviceCommandsResponse</returns>
        public SmartThingsNet.Client.ApiResponse<DeviceCommandsResponse> ExecuteDeviceCommandsWithHttpInfo(string authorization, string deviceId, DeviceCommandsRequest executeCapabilityCommand, bool? ordered = default(bool?))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling DevicesApi->ExecuteDeviceCommands");
            }

            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->ExecuteDeviceCommands");
            }

            // verify the required parameter 'executeCapabilityCommand' is set
            if (executeCapabilityCommand == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'executeCapabilityCommand' when calling DevicesApi->ExecuteDeviceCommands");
            }

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            if (ordered != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "ordered", ordered));
            }
            
            localVarRequestOptions.Data = executeCapabilityCommand;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<DeviceCommandsResponse>("/devices/{deviceId}/commands", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ExecuteDeviceCommands", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Execute Commands on a Device Execute a specified command on a Device.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="executeCapabilityCommand"></param>
        /// <param name="ordered">Specifies whether the command should be executed in order or asynchronously. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of DeviceCommandsResponse</returns>
        public async System.Threading.Tasks.Task<DeviceCommandsResponse> ExecuteDeviceCommandsAsync(string authorization, string deviceId, DeviceCommandsRequest executeCapabilityCommand, bool? ordered = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<DeviceCommandsResponse> localVarResponse = await ExecuteDeviceCommandsWithHttpInfoAsync(authorization, deviceId, executeCapabilityCommand, ordered, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Execute Commands on a Device Execute a specified command on a Device.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="executeCapabilityCommand"></param>
        /// <param name="ordered">Specifies whether the command should be executed in order or asynchronously. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (DeviceCommandsResponse)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<DeviceCommandsResponse>> ExecuteDeviceCommandsWithHttpInfoAsync(string authorization, string deviceId, DeviceCommandsRequest executeCapabilityCommand, bool? ordered = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling DevicesApi->ExecuteDeviceCommands");
            }

            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->ExecuteDeviceCommands");
            }

            // verify the required parameter 'executeCapabilityCommand' is set
            if (executeCapabilityCommand == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'executeCapabilityCommand' when calling DevicesApi->ExecuteDeviceCommands");
            }


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            if (ordered != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "ordered", ordered));
            }
            
            localVarRequestOptions.Data = executeCapabilityCommand;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<DeviceCommandsResponse>("/devices/{deviceId}/commands", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ExecuteDeviceCommands", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get the Description of a Device Get a Device&#39;s given description.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <returns>Device</returns>
        public Device GetDevice(string authorization, string deviceId)
        {
            SmartThingsNet.Client.ApiResponse<Device> localVarResponse = GetDeviceWithHttpInfo(authorization, deviceId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get the Description of a Device Get a Device&#39;s given description.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <returns>ApiResponse of Device</returns>
        public SmartThingsNet.Client.ApiResponse<Device> GetDeviceWithHttpInfo(string authorization, string deviceId)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling DevicesApi->GetDevice");
            }

            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->GetDevice");
            }

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<Device>("/devices/{deviceId}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDevice", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get the Description of a Device Get a Device&#39;s given description.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Device</returns>
        public async System.Threading.Tasks.Task<Device> GetDeviceAsync(string authorization, string deviceId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<Device> localVarResponse = await GetDeviceWithHttpInfoAsync(authorization, deviceId, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get the Description of a Device Get a Device&#39;s given description.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Device)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Device>> GetDeviceWithHttpInfoAsync(string authorization, string deviceId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling DevicesApi->GetDevice");
            }

            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->GetDevice");
            }


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<Device>("/devices/{deviceId}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDevice", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get the Status of a Device Component Get the status of all attributes of a specified component.  The results may be filtered if the requester only has permission to view a subset of the component&#39;s capabilities.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <returns>Dictionary&lt;string, Dictionary&gt;</returns>
        public Dictionary<string, string> GetDeviceComponentStatus(string authorization, string deviceId, string componentId)
        {
            SmartThingsNet.Client.ApiResponse<Dictionary<string, string>> localVarResponse = GetDeviceComponentStatusWithHttpInfo(authorization, deviceId, componentId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get the Status of a Device Component Get the status of all attributes of a specified component.  The results may be filtered if the requester only has permission to view a subset of the component&#39;s capabilities.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <returns>ApiResponse of Dictionary&lt;string, Dictionary&gt;</returns>
        public SmartThingsNet.Client.ApiResponse<Dictionary<string, string>> GetDeviceComponentStatusWithHttpInfo(string authorization, string deviceId, string componentId)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling DevicesApi->GetDeviceComponentStatus");
            }

            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->GetDeviceComponentStatus");
            }

            // verify the required parameter 'componentId' is set
            if (componentId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'componentId' when calling DevicesApi->GetDeviceComponentStatus");
            }

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            localVarRequestOptions.PathParameters.Add("componentId", SmartThingsNet.Client.ClientUtils.ParameterToString(componentId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<Dictionary<string, string>>("/devices/{deviceId}/components/{componentId}/status", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDeviceComponentStatus", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get the Status of a Device Component Get the status of all attributes of a specified component.  The results may be filtered if the requester only has permission to view a subset of the component&#39;s capabilities.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Dictionary&lt;string, Dictionary&gt;</returns>
        public async System.Threading.Tasks.Task<Dictionary<string, string>> GetDeviceComponentStatusAsync(string authorization, string deviceId, string componentId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<Dictionary<string, string>> localVarResponse = await GetDeviceComponentStatusWithHttpInfoAsync(authorization, deviceId, componentId, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get the Status of a Device Component Get the status of all attributes of a specified component.  The results may be filtered if the requester only has permission to view a subset of the component&#39;s capabilities.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Dictionary&lt;string, Dictionary&gt;)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Dictionary<string, string>>> GetDeviceComponentStatusWithHttpInfoAsync(string authorization, string deviceId, string componentId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling DevicesApi->GetDeviceComponentStatus");
            }

            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->GetDeviceComponentStatus");
            }

            // verify the required parameter 'componentId' is set
            if (componentId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'componentId' when calling DevicesApi->GetDeviceComponentStatus");
            }


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            localVarRequestOptions.PathParameters.Add("componentId", SmartThingsNet.Client.ClientUtils.ParameterToString(componentId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<Dictionary<string, string>>("/devices/{deviceId}/components/{componentId}/status", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDeviceComponentStatus", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get the Full Status of a Device Get the current status of all of a Device component&#39;s attributes.  The results may be filtered if the requester only has permission to view a subset of the Device&#39;s components or capabilities.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <returns>DeviceStatus</returns>
        public DeviceStatus GetDeviceStatus(string authorization, string deviceId)
        {
            SmartThingsNet.Client.ApiResponse<DeviceStatus> localVarResponse = GetDeviceStatusWithHttpInfo(authorization, deviceId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get the Full Status of a Device Get the current status of all of a Device component&#39;s attributes.  The results may be filtered if the requester only has permission to view a subset of the Device&#39;s components or capabilities.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <returns>ApiResponse of DeviceStatus</returns>
        public SmartThingsNet.Client.ApiResponse<DeviceStatus> GetDeviceStatusWithHttpInfo(string authorization, string deviceId)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling DevicesApi->GetDeviceStatus");
            }

            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->GetDeviceStatus");
            }

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<DeviceStatus>("/devices/{deviceId}/status", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDeviceStatus", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get the Full Status of a Device Get the current status of all of a Device component&#39;s attributes.  The results may be filtered if the requester only has permission to view a subset of the Device&#39;s components or capabilities.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of DeviceStatus</returns>
        public async System.Threading.Tasks.Task<DeviceStatus> GetDeviceStatusAsync(string authorization, string deviceId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<DeviceStatus> localVarResponse = await GetDeviceStatusWithHttpInfoAsync(authorization, deviceId, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get the Full Status of a Device Get the current status of all of a Device component&#39;s attributes.  The results may be filtered if the requester only has permission to view a subset of the Device&#39;s components or capabilities.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (DeviceStatus)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<DeviceStatus>> GetDeviceStatusWithHttpInfoAsync(string authorization, string deviceId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling DevicesApi->GetDeviceStatus");
            }

            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->GetDeviceStatus");
            }


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<DeviceStatus>("/devices/{deviceId}/status", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDeviceStatus", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get the Status of a Capability Get the current status of a Device component&#39;s capability.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <param name="capabilityId">The ID of the capability</param>
        /// <returns>Dictionary&lt;string, AttributeState&gt;</returns>
        public Dictionary<string, AttributeState> GetDeviceStatusByCapability(string authorization, string deviceId, string componentId, string capabilityId)
        {
            SmartThingsNet.Client.ApiResponse<Dictionary<string, AttributeState>> localVarResponse = GetDeviceStatusByCapabilityWithHttpInfo(authorization, deviceId, componentId, capabilityId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get the Status of a Capability Get the current status of a Device component&#39;s capability.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <param name="capabilityId">The ID of the capability</param>
        /// <returns>ApiResponse of Dictionary&lt;string, AttributeState&gt;</returns>
        public SmartThingsNet.Client.ApiResponse<Dictionary<string, AttributeState>> GetDeviceStatusByCapabilityWithHttpInfo(string authorization, string deviceId, string componentId, string capabilityId)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling DevicesApi->GetDeviceStatusByCapability");
            }

            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->GetDeviceStatusByCapability");
            }

            // verify the required parameter 'componentId' is set
            if (componentId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'componentId' when calling DevicesApi->GetDeviceStatusByCapability");
            }

            // verify the required parameter 'capabilityId' is set
            if (capabilityId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityId' when calling DevicesApi->GetDeviceStatusByCapability");
            }

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            localVarRequestOptions.PathParameters.Add("componentId", SmartThingsNet.Client.ClientUtils.ParameterToString(componentId)); // path parameter
            localVarRequestOptions.PathParameters.Add("capabilityId", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<Dictionary<string, AttributeState>>("/devices/{deviceId}/components/{componentId}/capabilities/{capabilityId}/status", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDeviceStatusByCapability", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get the Status of a Capability Get the current status of a Device component&#39;s capability.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <param name="capabilityId">The ID of the capability</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Dictionary&lt;string, AttributeState&gt;</returns>
        public async System.Threading.Tasks.Task<Dictionary<string, AttributeState>> GetDeviceStatusByCapabilityAsync(string authorization, string deviceId, string componentId, string capabilityId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<Dictionary<string, AttributeState>> localVarResponse = await GetDeviceStatusByCapabilityWithHttpInfoAsync(authorization, deviceId, componentId, capabilityId, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get the Status of a Capability Get the current status of a Device component&#39;s capability.  If the OAuth token used in this API call is for a SmartApp that created the Device, it implicitly has permission for this API. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="componentId">The name of the component.</param>
        /// <param name="capabilityId">The ID of the capability</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Dictionary&lt;string, AttributeState&gt;)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Dictionary<string, AttributeState>>> GetDeviceStatusByCapabilityWithHttpInfoAsync(string authorization, string deviceId, string componentId, string capabilityId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling DevicesApi->GetDeviceStatusByCapability");
            }

            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->GetDeviceStatusByCapability");
            }

            // verify the required parameter 'componentId' is set
            if (componentId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'componentId' when calling DevicesApi->GetDeviceStatusByCapability");
            }

            // verify the required parameter 'capabilityId' is set
            if (capabilityId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityId' when calling DevicesApi->GetDeviceStatusByCapability");
            }


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            localVarRequestOptions.PathParameters.Add("componentId", SmartThingsNet.Client.ClientUtils.ParameterToString(componentId)); // path parameter
            localVarRequestOptions.PathParameters.Add("capabilityId", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<Dictionary<string, AttributeState>>("/devices/{deviceId}/components/{componentId}/capabilities/{capabilityId}/status", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDeviceStatusByCapability", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Devices Get a list of devices.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capability">The Device capabilities to filter the results by. The capabilities are treated as an \&quot;and\&quot; so all capabilities must be present.  (optional)</param>
        /// <param name="locationId">The Device Locations to filter the results by.  (optional)</param>
        /// <param name="deviceId">The Device IDs to filter the results by.  (optional)</param>
        /// <param name="capabilitiesMode">Treat all capability filter query params as a logical \&quot;or\&quot; or \&quot;and\&quot; with a default of \&quot;and\&quot;.  (optional, default to and)</param>
        /// <param name="includeRestricted">Restricted Devices are hidden by default. This query parameter will reveal them. Device status will not be provided if the token does not have sufficient access level to view the device status even if includeStatus parameter is set to true.  (optional)</param>
        /// <param name="accessLevel">Only list Devices accessible by the given accessLevel.  (optional)</param>
        /// <param name="includeAllowedActions">Include the actions permitted by this token&#39;s access for each individual resource.  (optional)</param>
        /// <returns>PagedDevices</returns>
        public PagedDevices GetDevices(string authorization, List<string> capability = default(List<string>), List<string> locationId = default(List<string>), List<string> deviceId = default(List<string>), string capabilitiesMode = default(string), bool? includeRestricted = default(bool?), int? accessLevel = default(int?), bool? includeAllowedActions = default(bool?))
        {
            SmartThingsNet.Client.ApiResponse<PagedDevices> localVarResponse = GetDevicesWithHttpInfo(authorization, capability, locationId, deviceId, capabilitiesMode, includeRestricted, accessLevel, includeAllowedActions);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Devices Get a list of devices.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capability">The Device capabilities to filter the results by. The capabilities are treated as an \&quot;and\&quot; so all capabilities must be present.  (optional)</param>
        /// <param name="locationId">The Device Locations to filter the results by.  (optional)</param>
        /// <param name="deviceId">The Device IDs to filter the results by.  (optional)</param>
        /// <param name="capabilitiesMode">Treat all capability filter query params as a logical \&quot;or\&quot; or \&quot;and\&quot; with a default of \&quot;and\&quot;.  (optional, default to and)</param>
        /// <param name="includeRestricted">Restricted Devices are hidden by default. This query parameter will reveal them. Device status will not be provided if the token does not have sufficient access level to view the device status even if includeStatus parameter is set to true.  (optional)</param>
        /// <param name="accessLevel">Only list Devices accessible by the given accessLevel.  (optional)</param>
        /// <param name="includeAllowedActions">Include the actions permitted by this token&#39;s access for each individual resource.  (optional)</param>
        /// <returns>ApiResponse of PagedDevices</returns>
        public SmartThingsNet.Client.ApiResponse<PagedDevices> GetDevicesWithHttpInfo(string authorization, List<string> capability = default(List<string>), List<string> locationId = default(List<string>), List<string> deviceId = default(List<string>), string capabilitiesMode = default(string), bool? includeRestricted = default(bool?), int? accessLevel = default(int?), bool? includeAllowedActions = default(bool?))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling DevicesApi->GetDevices");
            }

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
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
            if (includeRestricted != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "includeRestricted", includeRestricted));
            }
            if (accessLevel != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "accessLevel", accessLevel));
            }
            if (includeAllowedActions != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "includeAllowedActions", includeAllowedActions));
            }
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<PagedDevices>("/devices", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDevices", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Devices Get a list of devices.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capability">The Device capabilities to filter the results by. The capabilities are treated as an \&quot;and\&quot; so all capabilities must be present.  (optional)</param>
        /// <param name="locationId">The Device Locations to filter the results by.  (optional)</param>
        /// <param name="deviceId">The Device IDs to filter the results by.  (optional)</param>
        /// <param name="capabilitiesMode">Treat all capability filter query params as a logical \&quot;or\&quot; or \&quot;and\&quot; with a default of \&quot;and\&quot;.  (optional, default to and)</param>
        /// <param name="includeRestricted">Restricted Devices are hidden by default. This query parameter will reveal them. Device status will not be provided if the token does not have sufficient access level to view the device status even if includeStatus parameter is set to true.  (optional)</param>
        /// <param name="accessLevel">Only list Devices accessible by the given accessLevel.  (optional)</param>
        /// <param name="includeAllowedActions">Include the actions permitted by this token&#39;s access for each individual resource.  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PagedDevices</returns>
        public async System.Threading.Tasks.Task<PagedDevices> GetDevicesAsync(string authorization, List<string> capability = default(List<string>), List<string> locationId = default(List<string>), List<string> deviceId = default(List<string>), string capabilitiesMode = default(string), bool? includeRestricted = default(bool?), int? accessLevel = default(int?), bool? includeAllowedActions = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<PagedDevices> localVarResponse = await GetDevicesWithHttpInfoAsync(authorization, capability, locationId, deviceId, capabilitiesMode, includeRestricted, accessLevel, includeAllowedActions, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Devices Get a list of devices.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capability">The Device capabilities to filter the results by. The capabilities are treated as an \&quot;and\&quot; so all capabilities must be present.  (optional)</param>
        /// <param name="locationId">The Device Locations to filter the results by.  (optional)</param>
        /// <param name="deviceId">The Device IDs to filter the results by.  (optional)</param>
        /// <param name="capabilitiesMode">Treat all capability filter query params as a logical \&quot;or\&quot; or \&quot;and\&quot; with a default of \&quot;and\&quot;.  (optional, default to and)</param>
        /// <param name="includeRestricted">Restricted Devices are hidden by default. This query parameter will reveal them. Device status will not be provided if the token does not have sufficient access level to view the device status even if includeStatus parameter is set to true.  (optional)</param>
        /// <param name="accessLevel">Only list Devices accessible by the given accessLevel.  (optional)</param>
        /// <param name="includeAllowedActions">Include the actions permitted by this token&#39;s access for each individual resource.  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PagedDevices)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<PagedDevices>> GetDevicesWithHttpInfoAsync(string authorization, List<string> capability = default(List<string>), List<string> locationId = default(List<string>), List<string> deviceId = default(List<string>), string capabilitiesMode = default(string), bool? includeRestricted = default(bool?), int? accessLevel = default(int?), bool? includeAllowedActions = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling DevicesApi->GetDevices");
            }


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
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
            if (includeRestricted != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "includeRestricted", includeRestricted));
            }
            if (accessLevel != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "accessLevel", accessLevel));
            }
            if (includeAllowedActions != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "includeAllowedActions", includeAllowedActions));
            }
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<PagedDevices>("/devices", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDevices", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Install a Device Install a Device.  This call is only available for SmartApp-managed Devices. The SmartApp that creates the Device is responsible for handling commands for the Device and updating the status of the Device by creating events. Requires installed app principal. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="installationRequest">Installation Request</param>
        /// <returns>Device</returns>
        public Device InstallDevice(string authorization, DeviceInstallRequest installationRequest)
        {
            SmartThingsNet.Client.ApiResponse<Device> localVarResponse = InstallDeviceWithHttpInfo(authorization, installationRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Install a Device Install a Device.  This call is only available for SmartApp-managed Devices. The SmartApp that creates the Device is responsible for handling commands for the Device and updating the status of the Device by creating events. Requires installed app principal. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="installationRequest">Installation Request</param>
        /// <returns>ApiResponse of Device</returns>
        public SmartThingsNet.Client.ApiResponse<Device> InstallDeviceWithHttpInfo(string authorization, DeviceInstallRequest installationRequest)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling DevicesApi->InstallDevice");
            }

            // verify the required parameter 'installationRequest' is set
            if (installationRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installationRequest' when calling DevicesApi->InstallDevice");
            }

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
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

            
            localVarRequestOptions.Data = installationRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Device>("/devices", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("InstallDevice", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Install a Device Install a Device.  This call is only available for SmartApp-managed Devices. The SmartApp that creates the Device is responsible for handling commands for the Device and updating the status of the Device by creating events. Requires installed app principal. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="installationRequest">Installation Request</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Device</returns>
        public async System.Threading.Tasks.Task<Device> InstallDeviceAsync(string authorization, DeviceInstallRequest installationRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<Device> localVarResponse = await InstallDeviceWithHttpInfoAsync(authorization, installationRequest, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Install a Device Install a Device.  This call is only available for SmartApp-managed Devices. The SmartApp that creates the Device is responsible for handling commands for the Device and updating the status of the Device by creating events. Requires installed app principal. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="installationRequest">Installation Request</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Device)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Device>> InstallDeviceWithHttpInfoAsync(string authorization, DeviceInstallRequest installationRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling DevicesApi->InstallDevice");
            }

            // verify the required parameter 'installationRequest' is set
            if (installationRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installationRequest' when calling DevicesApi->InstallDevice");
            }


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
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

            
            localVarRequestOptions.Data = installationRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Device>("/devices", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("InstallDevice", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update a Device Update the properties of a Device.  If the OAuth token used for this call is for a SmartApp that created the Device you are attempting to update, the call implicitly has permission for this API. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="updateDeviceRequest"></param>
        /// <returns>Device</returns>
        public Device UpdateDevice(string authorization, string deviceId, UpdateDeviceRequest updateDeviceRequest)
        {
            SmartThingsNet.Client.ApiResponse<Device> localVarResponse = UpdateDeviceWithHttpInfo(authorization, deviceId, updateDeviceRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update a Device Update the properties of a Device.  If the OAuth token used for this call is for a SmartApp that created the Device you are attempting to update, the call implicitly has permission for this API. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="updateDeviceRequest"></param>
        /// <returns>ApiResponse of Device</returns>
        public SmartThingsNet.Client.ApiResponse<Device> UpdateDeviceWithHttpInfo(string authorization, string deviceId, UpdateDeviceRequest updateDeviceRequest)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling DevicesApi->UpdateDevice");
            }

            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->UpdateDevice");
            }

            // verify the required parameter 'updateDeviceRequest' is set
            if (updateDeviceRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'updateDeviceRequest' when calling DevicesApi->UpdateDevice");
            }

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            
            localVarRequestOptions.Data = updateDeviceRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<Device>("/devices/{deviceId}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateDevice", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update a Device Update the properties of a Device.  If the OAuth token used for this call is for a SmartApp that created the Device you are attempting to update, the call implicitly has permission for this API. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="updateDeviceRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Device</returns>
        public async System.Threading.Tasks.Task<Device> UpdateDeviceAsync(string authorization, string deviceId, UpdateDeviceRequest updateDeviceRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<Device> localVarResponse = await UpdateDeviceWithHttpInfoAsync(authorization, deviceId, updateDeviceRequest, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update a Device Update the properties of a Device.  If the OAuth token used for this call is for a SmartApp that created the Device you are attempting to update, the call implicitly has permission for this API. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="deviceId">The Device ID</param>
        /// <param name="updateDeviceRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Device)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Device>> UpdateDeviceWithHttpInfoAsync(string authorization, string deviceId, UpdateDeviceRequest updateDeviceRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling DevicesApi->UpdateDevice");
            }

            // verify the required parameter 'deviceId' is set
            if (deviceId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'deviceId' when calling DevicesApi->UpdateDevice");
            }

            // verify the required parameter 'updateDeviceRequest' is set
            if (updateDeviceRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'updateDeviceRequest' when calling DevicesApi->UpdateDevice");
            }


            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] { "application/json"
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

            localVarRequestOptions.PathParameters.Add("deviceId", SmartThingsNet.Client.ClientUtils.ParameterToString(deviceId)); // path parameter
            
            localVarRequestOptions.Data = updateDeviceRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<Device>("/devices/{deviceId}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateDevice", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

    }
}
