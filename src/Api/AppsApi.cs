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
    public interface IAppsApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Create an App
        /// </summary>
        /// <remarks>
        /// A single developer account is allowed to contain a maximum of 100 apps.  Upon hitting that limit, a 422 error response is returned with an error code of LimitError. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="createOrUpdateAppRequest"></param>
        /// <param name="signatureType">The Signature Type of the application. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="requireConfirmation">Override default configuration to use either PING or CONFIRMATION lifecycle. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <returns>CreateAppResponse</returns>
        CreateAppResponse CreateApp(string authorization, CreateAppRequest createOrUpdateAppRequest, string signatureType = default(string), bool? requireConfirmation = default(bool?));

        /// <summary>
        /// Create an App
        /// </summary>
        /// <remarks>
        /// A single developer account is allowed to contain a maximum of 100 apps.  Upon hitting that limit, a 422 error response is returned with an error code of LimitError. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="createOrUpdateAppRequest"></param>
        /// <param name="signatureType">The Signature Type of the application. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="requireConfirmation">Override default configuration to use either PING or CONFIRMATION lifecycle. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <returns>ApiResponse of CreateAppResponse</returns>
        ApiResponse<CreateAppResponse> CreateAppWithHttpInfo(string authorization, CreateAppRequest createOrUpdateAppRequest, string signatureType = default(string), bool? requireConfirmation = default(bool?));
        /// <summary>
        /// Delete an App
        /// </summary>
        /// <remarks>
        /// Delete an app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <returns>Object</returns>
        Object DeleteApp(string authorization, string appNameOrId);

        /// <summary>
        /// Delete an App
        /// </summary>
        /// <remarks>
        /// Delete an app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <returns>ApiResponse of Object</returns>
        ApiResponse<Object> DeleteAppWithHttpInfo(string authorization, string appNameOrId);
        /// <summary>
        /// Generate an App&#39;s OAuth Client/Secret
        /// </summary>
        /// <remarks>
        /// When an app is first created an OAuth client/secret are automatically generated for the integration.  However, there are times when it maybe useful to re-generate a client/secret  such as in cases where a secret becomes compromised. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="generateAppOAuthRequest"></param>
        /// <returns>GenerateAppOAuthResponse</returns>
        GenerateAppOAuthResponse GenerateAppOauth(string authorization, string appNameOrId, GenerateAppOAuthRequest generateAppOAuthRequest);

        /// <summary>
        /// Generate an App&#39;s OAuth Client/Secret
        /// </summary>
        /// <remarks>
        /// When an app is first created an OAuth client/secret are automatically generated for the integration.  However, there are times when it maybe useful to re-generate a client/secret  such as in cases where a secret becomes compromised. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="generateAppOAuthRequest"></param>
        /// <returns>ApiResponse of GenerateAppOAuthResponse</returns>
        ApiResponse<GenerateAppOAuthResponse> GenerateAppOauthWithHttpInfo(string authorization, string appNameOrId, GenerateAppOAuthRequest generateAppOAuthRequest);
        /// <summary>
        /// Get an App
        /// </summary>
        /// <remarks>
        /// Get a single app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <returns>App</returns>
        App GetApp(string authorization, string appNameOrId);

        /// <summary>
        /// Get an App
        /// </summary>
        /// <remarks>
        /// Get a single app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <returns>ApiResponse of App</returns>
        ApiResponse<App> GetAppWithHttpInfo(string authorization, string appNameOrId);
        /// <summary>
        /// Get an App&#39;s OAuth Settings
        /// </summary>
        /// <remarks>
        /// Retrieve an app&#39;s oauth settings.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <returns>AppOAuth</returns>
        AppOAuth GetAppOauth(string authorization, string appNameOrId);

        /// <summary>
        /// Get an App&#39;s OAuth Settings
        /// </summary>
        /// <remarks>
        /// Retrieve an app&#39;s oauth settings.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <returns>ApiResponse of AppOAuth</returns>
        ApiResponse<AppOAuth> GetAppOauthWithHttpInfo(string authorization, string appNameOrId);
        /// <summary>
        /// Get an App&#39;s Settings
        /// </summary>
        /// <remarks>
        /// Retrieve the settings for an app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <returns>GetAppSettingsResponse</returns>
        GetAppSettingsResponse GetAppSettings(string authorization, string appNameOrId);

        /// <summary>
        /// Get an App&#39;s Settings
        /// </summary>
        /// <remarks>
        /// Retrieve the settings for an app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <returns>ApiResponse of GetAppSettingsResponse</returns>
        ApiResponse<GetAppSettingsResponse> GetAppSettingsWithHttpInfo(string authorization, string appNameOrId);
        /// <summary>
        /// List All Apps
        /// </summary>
        /// <remarks>
        /// List all apps configured in an account.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appType">The App Type of the application. (optional)</param>
        /// <param name="classification">An App maybe associated to many classifications.  A classification drives how the integration is presented to the user in the SmartThings mobile clients.  These classifications include: * AUTOMATION - Denotes an integration that should display under the \&quot;Automation\&quot; tab in mobile clients. * SERVICE - Denotes an integration that is classified as a \&quot;Service\&quot;. * DEVICE - Denotes an integration that should display under the \&quot;Device\&quot; tab in mobile clients. * CONNECTED_SERVICE - Denotes an integration that should display under the \&quot;Connected Services\&quot; menu in mobile clients.  (optional)</param>
        /// <param name="tag">May be used to filter a resource by it&#39;s assigned user-tags.  Multiple tag query params are automatically joined with OR.  Example usage in query string: &#x60;&#x60;&#x60; ?tag:key_name&#x3D;value1&amp;tag:key_name&#x3D;value2 &#x60;&#x60;&#x60;  (optional)</param>
        /// <returns>PagedApps</returns>
        PagedApps ListApps(string authorization, string appType = default(string), string classification = default(string), string tag = default(string));

        /// <summary>
        /// List All Apps
        /// </summary>
        /// <remarks>
        /// List all apps configured in an account.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appType">The App Type of the application. (optional)</param>
        /// <param name="classification">An App maybe associated to many classifications.  A classification drives how the integration is presented to the user in the SmartThings mobile clients.  These classifications include: * AUTOMATION - Denotes an integration that should display under the \&quot;Automation\&quot; tab in mobile clients. * SERVICE - Denotes an integration that is classified as a \&quot;Service\&quot;. * DEVICE - Denotes an integration that should display under the \&quot;Device\&quot; tab in mobile clients. * CONNECTED_SERVICE - Denotes an integration that should display under the \&quot;Connected Services\&quot; menu in mobile clients.  (optional)</param>
        /// <param name="tag">May be used to filter a resource by it&#39;s assigned user-tags.  Multiple tag query params are automatically joined with OR.  Example usage in query string: &#x60;&#x60;&#x60; ?tag:key_name&#x3D;value1&amp;tag:key_name&#x3D;value2 &#x60;&#x60;&#x60;  (optional)</param>
        /// <returns>ApiResponse of PagedApps</returns>
        ApiResponse<PagedApps> ListAppsWithHttpInfo(string authorization, string appType = default(string), string classification = default(string), string tag = default(string));
        /// <summary>
        /// Send a Confirmation Request to an App
        /// </summary>
        /// <remarks>
        /// Prepare to register an App by sending the endpoint a confirmation message.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="appRegisterRequest"></param>
        /// <returns>Object</returns>
        Object Register(string authorization, string appNameOrId, Object appRegisterRequest);

        /// <summary>
        /// Send a Confirmation Request to an App
        /// </summary>
        /// <remarks>
        /// Prepare to register an App by sending the endpoint a confirmation message.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="appRegisterRequest"></param>
        /// <returns>ApiResponse of Object</returns>
        ApiResponse<Object> RegisterWithHttpInfo(string authorization, string appNameOrId, Object appRegisterRequest);
        /// <summary>
        /// Update an App
        /// </summary>
        /// <remarks>
        /// Update an app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateAppRequest"></param>
        /// <param name="signatureType">The Signature Type of the application. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="requireConfirmation">Override default configuration to use either PING or CONFIRMATION lifecycle. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <returns>App</returns>
        App UpdateApp(string authorization, string appNameOrId, UpdateAppRequest updateAppRequest, string signatureType = default(string), bool? requireConfirmation = default(bool?));

        /// <summary>
        /// Update an App
        /// </summary>
        /// <remarks>
        /// Update an app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateAppRequest"></param>
        /// <param name="signatureType">The Signature Type of the application. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="requireConfirmation">Override default configuration to use either PING or CONFIRMATION lifecycle. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <returns>ApiResponse of App</returns>
        ApiResponse<App> UpdateAppWithHttpInfo(string authorization, string appNameOrId, UpdateAppRequest updateAppRequest, string signatureType = default(string), bool? requireConfirmation = default(bool?));
        /// <summary>
        /// Update an App&#39;s OAuth Settings
        /// </summary>
        /// <remarks>
        /// Update an app&#39;s oauth settings.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateAppOAuthRequest"></param>
        /// <returns>AppOAuth</returns>
        AppOAuth UpdateAppOauth(string authorization, string appNameOrId, UpdateAppOAuthRequest updateAppOAuthRequest);

        /// <summary>
        /// Update an App&#39;s OAuth Settings
        /// </summary>
        /// <remarks>
        /// Update an app&#39;s oauth settings.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateAppOAuthRequest"></param>
        /// <returns>ApiResponse of AppOAuth</returns>
        ApiResponse<AppOAuth> UpdateAppOauthWithHttpInfo(string authorization, string appNameOrId, UpdateAppOAuthRequest updateAppOAuthRequest);
        /// <summary>
        /// Update an App&#39;s Settings
        /// </summary>
        /// <remarks>
        /// Update the settings for an app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId  field of an app.</param>
        /// <param name="updateAppSettingsRequest"></param>
        /// <returns>UpdateAppSettingsResponse</returns>
        UpdateAppSettingsResponse UpdateAppSettings(string authorization, string appNameOrId, UpdateAppSettingsRequest updateAppSettingsRequest);

        /// <summary>
        /// Update an App&#39;s Settings
        /// </summary>
        /// <remarks>
        /// Update the settings for an app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId  field of an app.</param>
        /// <param name="updateAppSettingsRequest"></param>
        /// <returns>ApiResponse of UpdateAppSettingsResponse</returns>
        ApiResponse<UpdateAppSettingsResponse> UpdateAppSettingsWithHttpInfo(string authorization, string appNameOrId, UpdateAppSettingsRequest updateAppSettingsRequest);
        /// <summary>
        /// Update an App&#39;s Signature Type
        /// </summary>
        /// <remarks>
        /// Updates the signature type of an App.  Signature options:   * APP_RSA - Legacy signing mechanism comprised of a public / private key generated for an App during registration.  This mechanism requires an App to download the public key and deploy along side their integration to verify the signature in the authorization header.   * ST_PADLOCK - App callbacks are signed with a SmartThings certificate which is publicly available at https://key.smartthings.com.  App&#39;s authorize callbacks by fetching the certificate over HTTPS and using it to validate the signature in the authorization header.  When upgrading an App from APP_RSA to ST_PADLOCK, it is recommended to implement both verification methods. This will provide the ability to seamlessly switch between mechanisms if a rollback is needed. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateSignatureTypeRequest"></param>
        /// <returns>Object</returns>
        Object UpdateSignatureType(string authorization, string appNameOrId, UpdateSignatureTypeRequest updateSignatureTypeRequest);

        /// <summary>
        /// Update an App&#39;s Signature Type
        /// </summary>
        /// <remarks>
        /// Updates the signature type of an App.  Signature options:   * APP_RSA - Legacy signing mechanism comprised of a public / private key generated for an App during registration.  This mechanism requires an App to download the public key and deploy along side their integration to verify the signature in the authorization header.   * ST_PADLOCK - App callbacks are signed with a SmartThings certificate which is publicly available at https://key.smartthings.com.  App&#39;s authorize callbacks by fetching the certificate over HTTPS and using it to validate the signature in the authorization header.  When upgrading an App from APP_RSA to ST_PADLOCK, it is recommended to implement both verification methods. This will provide the ability to seamlessly switch between mechanisms if a rollback is needed. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateSignatureTypeRequest"></param>
        /// <returns>ApiResponse of Object</returns>
        ApiResponse<Object> UpdateSignatureTypeWithHttpInfo(string authorization, string appNameOrId, UpdateSignatureTypeRequest updateSignatureTypeRequest);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IAppsApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Create an App
        /// </summary>
        /// <remarks>
        /// A single developer account is allowed to contain a maximum of 100 apps.  Upon hitting that limit, a 422 error response is returned with an error code of LimitError. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="createOrUpdateAppRequest"></param>
        /// <param name="signatureType">The Signature Type of the application. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="requireConfirmation">Override default configuration to use either PING or CONFIRMATION lifecycle. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateAppResponse</returns>
        System.Threading.Tasks.Task<CreateAppResponse> CreateAppAsync(string authorization, CreateAppRequest createOrUpdateAppRequest, string signatureType = default(string), bool? requireConfirmation = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create an App
        /// </summary>
        /// <remarks>
        /// A single developer account is allowed to contain a maximum of 100 apps.  Upon hitting that limit, a 422 error response is returned with an error code of LimitError. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="createOrUpdateAppRequest"></param>
        /// <param name="signatureType">The Signature Type of the application. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="requireConfirmation">Override default configuration to use either PING or CONFIRMATION lifecycle. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateAppResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateAppResponse>> CreateAppWithHttpInfoAsync(string authorization, CreateAppRequest createOrUpdateAppRequest, string signatureType = default(string), bool? requireConfirmation = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete an App
        /// </summary>
        /// <remarks>
        /// Delete an app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Object</returns>
        System.Threading.Tasks.Task<Object> DeleteAppAsync(string authorization, string appNameOrId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete an App
        /// </summary>
        /// <remarks>
        /// Delete an app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Object)</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteAppWithHttpInfoAsync(string authorization, string appNameOrId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Generate an App&#39;s OAuth Client/Secret
        /// </summary>
        /// <remarks>
        /// When an app is first created an OAuth client/secret are automatically generated for the integration.  However, there are times when it maybe useful to re-generate a client/secret  such as in cases where a secret becomes compromised. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="generateAppOAuthRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GenerateAppOAuthResponse</returns>
        System.Threading.Tasks.Task<GenerateAppOAuthResponse> GenerateAppOauthAsync(string authorization, string appNameOrId, GenerateAppOAuthRequest generateAppOAuthRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Generate an App&#39;s OAuth Client/Secret
        /// </summary>
        /// <remarks>
        /// When an app is first created an OAuth client/secret are automatically generated for the integration.  However, there are times when it maybe useful to re-generate a client/secret  such as in cases where a secret becomes compromised. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="generateAppOAuthRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GenerateAppOAuthResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<GenerateAppOAuthResponse>> GenerateAppOauthWithHttpInfoAsync(string authorization, string appNameOrId, GenerateAppOAuthRequest generateAppOAuthRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get an App
        /// </summary>
        /// <remarks>
        /// Get a single app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of App</returns>
        System.Threading.Tasks.Task<App> GetAppAsync(string authorization, string appNameOrId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get an App
        /// </summary>
        /// <remarks>
        /// Get a single app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (App)</returns>
        System.Threading.Tasks.Task<ApiResponse<App>> GetAppWithHttpInfoAsync(string authorization, string appNameOrId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get an App&#39;s OAuth Settings
        /// </summary>
        /// <remarks>
        /// Retrieve an app&#39;s oauth settings.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AppOAuth</returns>
        System.Threading.Tasks.Task<AppOAuth> GetAppOauthAsync(string authorization, string appNameOrId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get an App&#39;s OAuth Settings
        /// </summary>
        /// <remarks>
        /// Retrieve an app&#39;s oauth settings.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AppOAuth)</returns>
        System.Threading.Tasks.Task<ApiResponse<AppOAuth>> GetAppOauthWithHttpInfoAsync(string authorization, string appNameOrId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get an App&#39;s Settings
        /// </summary>
        /// <remarks>
        /// Retrieve the settings for an app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetAppSettingsResponse</returns>
        System.Threading.Tasks.Task<GetAppSettingsResponse> GetAppSettingsAsync(string authorization, string appNameOrId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get an App&#39;s Settings
        /// </summary>
        /// <remarks>
        /// Retrieve the settings for an app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetAppSettingsResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetAppSettingsResponse>> GetAppSettingsWithHttpInfoAsync(string authorization, string appNameOrId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List All Apps
        /// </summary>
        /// <remarks>
        /// List all apps configured in an account.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appType">The App Type of the application. (optional)</param>
        /// <param name="classification">An App maybe associated to many classifications.  A classification drives how the integration is presented to the user in the SmartThings mobile clients.  These classifications include: * AUTOMATION - Denotes an integration that should display under the \&quot;Automation\&quot; tab in mobile clients. * SERVICE - Denotes an integration that is classified as a \&quot;Service\&quot;. * DEVICE - Denotes an integration that should display under the \&quot;Device\&quot; tab in mobile clients. * CONNECTED_SERVICE - Denotes an integration that should display under the \&quot;Connected Services\&quot; menu in mobile clients.  (optional)</param>
        /// <param name="tag">May be used to filter a resource by it&#39;s assigned user-tags.  Multiple tag query params are automatically joined with OR.  Example usage in query string: &#x60;&#x60;&#x60; ?tag:key_name&#x3D;value1&amp;tag:key_name&#x3D;value2 &#x60;&#x60;&#x60;  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PagedApps</returns>
        System.Threading.Tasks.Task<PagedApps> ListAppsAsync(string authorization, string appType = default(string), string classification = default(string), string tag = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List All Apps
        /// </summary>
        /// <remarks>
        /// List all apps configured in an account.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appType">The App Type of the application. (optional)</param>
        /// <param name="classification">An App maybe associated to many classifications.  A classification drives how the integration is presented to the user in the SmartThings mobile clients.  These classifications include: * AUTOMATION - Denotes an integration that should display under the \&quot;Automation\&quot; tab in mobile clients. * SERVICE - Denotes an integration that is classified as a \&quot;Service\&quot;. * DEVICE - Denotes an integration that should display under the \&quot;Device\&quot; tab in mobile clients. * CONNECTED_SERVICE - Denotes an integration that should display under the \&quot;Connected Services\&quot; menu in mobile clients.  (optional)</param>
        /// <param name="tag">May be used to filter a resource by it&#39;s assigned user-tags.  Multiple tag query params are automatically joined with OR.  Example usage in query string: &#x60;&#x60;&#x60; ?tag:key_name&#x3D;value1&amp;tag:key_name&#x3D;value2 &#x60;&#x60;&#x60;  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PagedApps)</returns>
        System.Threading.Tasks.Task<ApiResponse<PagedApps>> ListAppsWithHttpInfoAsync(string authorization, string appType = default(string), string classification = default(string), string tag = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Send a Confirmation Request to an App
        /// </summary>
        /// <remarks>
        /// Prepare to register an App by sending the endpoint a confirmation message.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="appRegisterRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Object</returns>
        System.Threading.Tasks.Task<Object> RegisterAsync(string authorization, string appNameOrId, Object appRegisterRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Send a Confirmation Request to an App
        /// </summary>
        /// <remarks>
        /// Prepare to register an App by sending the endpoint a confirmation message.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="appRegisterRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Object)</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> RegisterWithHttpInfoAsync(string authorization, string appNameOrId, Object appRegisterRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update an App
        /// </summary>
        /// <remarks>
        /// Update an app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateAppRequest"></param>
        /// <param name="signatureType">The Signature Type of the application. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="requireConfirmation">Override default configuration to use either PING or CONFIRMATION lifecycle. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of App</returns>
        System.Threading.Tasks.Task<App> UpdateAppAsync(string authorization, string appNameOrId, UpdateAppRequest updateAppRequest, string signatureType = default(string), bool? requireConfirmation = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update an App
        /// </summary>
        /// <remarks>
        /// Update an app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateAppRequest"></param>
        /// <param name="signatureType">The Signature Type of the application. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="requireConfirmation">Override default configuration to use either PING or CONFIRMATION lifecycle. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (App)</returns>
        System.Threading.Tasks.Task<ApiResponse<App>> UpdateAppWithHttpInfoAsync(string authorization, string appNameOrId, UpdateAppRequest updateAppRequest, string signatureType = default(string), bool? requireConfirmation = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update an App&#39;s OAuth Settings
        /// </summary>
        /// <remarks>
        /// Update an app&#39;s oauth settings.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateAppOAuthRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AppOAuth</returns>
        System.Threading.Tasks.Task<AppOAuth> UpdateAppOauthAsync(string authorization, string appNameOrId, UpdateAppOAuthRequest updateAppOAuthRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update an App&#39;s OAuth Settings
        /// </summary>
        /// <remarks>
        /// Update an app&#39;s oauth settings.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateAppOAuthRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AppOAuth)</returns>
        System.Threading.Tasks.Task<ApiResponse<AppOAuth>> UpdateAppOauthWithHttpInfoAsync(string authorization, string appNameOrId, UpdateAppOAuthRequest updateAppOAuthRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update an App&#39;s Settings
        /// </summary>
        /// <remarks>
        /// Update the settings for an app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId  field of an app.</param>
        /// <param name="updateAppSettingsRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of UpdateAppSettingsResponse</returns>
        System.Threading.Tasks.Task<UpdateAppSettingsResponse> UpdateAppSettingsAsync(string authorization, string appNameOrId, UpdateAppSettingsRequest updateAppSettingsRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update an App&#39;s Settings
        /// </summary>
        /// <remarks>
        /// Update the settings for an app.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId  field of an app.</param>
        /// <param name="updateAppSettingsRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (UpdateAppSettingsResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<UpdateAppSettingsResponse>> UpdateAppSettingsWithHttpInfoAsync(string authorization, string appNameOrId, UpdateAppSettingsRequest updateAppSettingsRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update an App&#39;s Signature Type
        /// </summary>
        /// <remarks>
        /// Updates the signature type of an App.  Signature options:   * APP_RSA - Legacy signing mechanism comprised of a public / private key generated for an App during registration.  This mechanism requires an App to download the public key and deploy along side their integration to verify the signature in the authorization header.   * ST_PADLOCK - App callbacks are signed with a SmartThings certificate which is publicly available at https://key.smartthings.com.  App&#39;s authorize callbacks by fetching the certificate over HTTPS and using it to validate the signature in the authorization header.  When upgrading an App from APP_RSA to ST_PADLOCK, it is recommended to implement both verification methods. This will provide the ability to seamlessly switch between mechanisms if a rollback is needed. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateSignatureTypeRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Object</returns>
        System.Threading.Tasks.Task<Object> UpdateSignatureTypeAsync(string authorization, string appNameOrId, UpdateSignatureTypeRequest updateSignatureTypeRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update an App&#39;s Signature Type
        /// </summary>
        /// <remarks>
        /// Updates the signature type of an App.  Signature options:   * APP_RSA - Legacy signing mechanism comprised of a public / private key generated for an App during registration.  This mechanism requires an App to download the public key and deploy along side their integration to verify the signature in the authorization header.   * ST_PADLOCK - App callbacks are signed with a SmartThings certificate which is publicly available at https://key.smartthings.com.  App&#39;s authorize callbacks by fetching the certificate over HTTPS and using it to validate the signature in the authorization header.  When upgrading an App from APP_RSA to ST_PADLOCK, it is recommended to implement both verification methods. This will provide the ability to seamlessly switch between mechanisms if a rollback is needed. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateSignatureTypeRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Object)</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> UpdateSignatureTypeWithHttpInfoAsync(string authorization, string appNameOrId, UpdateSignatureTypeRequest updateSignatureTypeRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IAppsApi : IAppsApiSync, IAppsApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class AppsApi : IAppsApi
    {
        private SmartThingsNet.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public AppsApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public AppsApi(string basePath)
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
        /// Initializes a new instance of the <see cref="AppsApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public AppsApi(SmartThingsNet.Client.Configuration configuration)
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
        /// Initializes a new instance of the <see cref="AppsApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public AppsApi(SmartThingsNet.Client.ISynchronousClient client, SmartThingsNet.Client.IAsynchronousClient asyncClient, SmartThingsNet.Client.IReadableConfiguration configuration)
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
        /// Create an App A single developer account is allowed to contain a maximum of 100 apps.  Upon hitting that limit, a 422 error response is returned with an error code of LimitError. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="createOrUpdateAppRequest"></param>
        /// <param name="signatureType">The Signature Type of the application. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="requireConfirmation">Override default configuration to use either PING or CONFIRMATION lifecycle. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <returns>CreateAppResponse</returns>
        public CreateAppResponse CreateApp(string authorization, CreateAppRequest createOrUpdateAppRequest, string signatureType = default(string), bool? requireConfirmation = default(bool?))
        {
            SmartThingsNet.Client.ApiResponse<CreateAppResponse> localVarResponse = CreateAppWithHttpInfo(authorization, createOrUpdateAppRequest, signatureType, requireConfirmation);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create an App A single developer account is allowed to contain a maximum of 100 apps.  Upon hitting that limit, a 422 error response is returned with an error code of LimitError. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="createOrUpdateAppRequest"></param>
        /// <param name="signatureType">The Signature Type of the application. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="requireConfirmation">Override default configuration to use either PING or CONFIRMATION lifecycle. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <returns>ApiResponse of CreateAppResponse</returns>
        public SmartThingsNet.Client.ApiResponse<CreateAppResponse> CreateAppWithHttpInfo(string authorization, CreateAppRequest createOrUpdateAppRequest, string signatureType = default(string), bool? requireConfirmation = default(bool?))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->CreateApp");
            }

            // verify the required parameter 'createOrUpdateAppRequest' is set
            if (createOrUpdateAppRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'createOrUpdateAppRequest' when calling AppsApi->CreateApp");
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

            if (signatureType != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "signatureType", signatureType));
            }
            if (requireConfirmation != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "requireConfirmation", requireConfirmation));
            }
            
            localVarRequestOptions.Data = createOrUpdateAppRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<CreateAppResponse>("/apps", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateApp", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create an App A single developer account is allowed to contain a maximum of 100 apps.  Upon hitting that limit, a 422 error response is returned with an error code of LimitError. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="createOrUpdateAppRequest"></param>
        /// <param name="signatureType">The Signature Type of the application. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="requireConfirmation">Override default configuration to use either PING or CONFIRMATION lifecycle. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateAppResponse</returns>
        public async System.Threading.Tasks.Task<CreateAppResponse> CreateAppAsync(string authorization, CreateAppRequest createOrUpdateAppRequest, string signatureType = default(string), bool? requireConfirmation = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<CreateAppResponse> localVarResponse = await CreateAppWithHttpInfoAsync(authorization, createOrUpdateAppRequest, signatureType, requireConfirmation, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create an App A single developer account is allowed to contain a maximum of 100 apps.  Upon hitting that limit, a 422 error response is returned with an error code of LimitError. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="createOrUpdateAppRequest"></param>
        /// <param name="signatureType">The Signature Type of the application. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="requireConfirmation">Override default configuration to use either PING or CONFIRMATION lifecycle. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateAppResponse)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<CreateAppResponse>> CreateAppWithHttpInfoAsync(string authorization, CreateAppRequest createOrUpdateAppRequest, string signatureType = default(string), bool? requireConfirmation = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->CreateApp");
            }

            // verify the required parameter 'createOrUpdateAppRequest' is set
            if (createOrUpdateAppRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'createOrUpdateAppRequest' when calling AppsApi->CreateApp");
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

            if (signatureType != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "signatureType", signatureType));
            }
            if (requireConfirmation != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "requireConfirmation", requireConfirmation));
            }
            
            localVarRequestOptions.Data = createOrUpdateAppRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<CreateAppResponse>("/apps", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateApp", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete an App Delete an app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <returns>Object</returns>
        public Object DeleteApp(string authorization, string appNameOrId)
        {
            SmartThingsNet.Client.ApiResponse<Object> localVarResponse = DeleteAppWithHttpInfo(authorization, appNameOrId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Delete an App Delete an app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <returns>ApiResponse of Object</returns>
        public SmartThingsNet.Client.ApiResponse<Object> DeleteAppWithHttpInfo(string authorization, string appNameOrId)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->DeleteApp");
            }

            // verify the required parameter 'appNameOrId' is set
            if (appNameOrId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'appNameOrId' when calling AppsApi->DeleteApp");
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

            localVarRequestOptions.PathParameters.Add("appNameOrId", SmartThingsNet.Client.ClientUtils.ParameterToString(appNameOrId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/apps/{appNameOrId}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteApp", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete an App Delete an app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Object</returns>
        public async System.Threading.Tasks.Task<Object> DeleteAppAsync(string authorization, string appNameOrId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<Object> localVarResponse = await DeleteAppWithHttpInfoAsync(authorization, appNameOrId, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Delete an App Delete an app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Object)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Object>> DeleteAppWithHttpInfoAsync(string authorization, string appNameOrId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->DeleteApp");
            }

            // verify the required parameter 'appNameOrId' is set
            if (appNameOrId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'appNameOrId' when calling AppsApi->DeleteApp");
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

            localVarRequestOptions.PathParameters.Add("appNameOrId", SmartThingsNet.Client.ClientUtils.ParameterToString(appNameOrId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/apps/{appNameOrId}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteApp", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Generate an App&#39;s OAuth Client/Secret When an app is first created an OAuth client/secret are automatically generated for the integration.  However, there are times when it maybe useful to re-generate a client/secret  such as in cases where a secret becomes compromised. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="generateAppOAuthRequest"></param>
        /// <returns>GenerateAppOAuthResponse</returns>
        public GenerateAppOAuthResponse GenerateAppOauth(string authorization, string appNameOrId, GenerateAppOAuthRequest generateAppOAuthRequest)
        {
            SmartThingsNet.Client.ApiResponse<GenerateAppOAuthResponse> localVarResponse = GenerateAppOauthWithHttpInfo(authorization, appNameOrId, generateAppOAuthRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Generate an App&#39;s OAuth Client/Secret When an app is first created an OAuth client/secret are automatically generated for the integration.  However, there are times when it maybe useful to re-generate a client/secret  such as in cases where a secret becomes compromised. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="generateAppOAuthRequest"></param>
        /// <returns>ApiResponse of GenerateAppOAuthResponse</returns>
        public SmartThingsNet.Client.ApiResponse<GenerateAppOAuthResponse> GenerateAppOauthWithHttpInfo(string authorization, string appNameOrId, GenerateAppOAuthRequest generateAppOAuthRequest)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->GenerateAppOauth");
            }

            // verify the required parameter 'appNameOrId' is set
            if (appNameOrId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'appNameOrId' when calling AppsApi->GenerateAppOauth");
            }

            // verify the required parameter 'generateAppOAuthRequest' is set
            if (generateAppOAuthRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'generateAppOAuthRequest' when calling AppsApi->GenerateAppOauth");
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

            localVarRequestOptions.PathParameters.Add("appNameOrId", SmartThingsNet.Client.ClientUtils.ParameterToString(appNameOrId)); // path parameter
            
            localVarRequestOptions.Data = generateAppOAuthRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<GenerateAppOAuthResponse>("/apps/{appNameOrId}/oauth/generate", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GenerateAppOauth", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Generate an App&#39;s OAuth Client/Secret When an app is first created an OAuth client/secret are automatically generated for the integration.  However, there are times when it maybe useful to re-generate a client/secret  such as in cases where a secret becomes compromised. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="generateAppOAuthRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GenerateAppOAuthResponse</returns>
        public async System.Threading.Tasks.Task<GenerateAppOAuthResponse> GenerateAppOauthAsync(string authorization, string appNameOrId, GenerateAppOAuthRequest generateAppOAuthRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<GenerateAppOAuthResponse> localVarResponse = await GenerateAppOauthWithHttpInfoAsync(authorization, appNameOrId, generateAppOAuthRequest, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Generate an App&#39;s OAuth Client/Secret When an app is first created an OAuth client/secret are automatically generated for the integration.  However, there are times when it maybe useful to re-generate a client/secret  such as in cases where a secret becomes compromised. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="generateAppOAuthRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GenerateAppOAuthResponse)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<GenerateAppOAuthResponse>> GenerateAppOauthWithHttpInfoAsync(string authorization, string appNameOrId, GenerateAppOAuthRequest generateAppOAuthRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->GenerateAppOauth");
            }

            // verify the required parameter 'appNameOrId' is set
            if (appNameOrId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'appNameOrId' when calling AppsApi->GenerateAppOauth");
            }

            // verify the required parameter 'generateAppOAuthRequest' is set
            if (generateAppOAuthRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'generateAppOAuthRequest' when calling AppsApi->GenerateAppOauth");
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

            localVarRequestOptions.PathParameters.Add("appNameOrId", SmartThingsNet.Client.ClientUtils.ParameterToString(appNameOrId)); // path parameter
            
            localVarRequestOptions.Data = generateAppOAuthRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<GenerateAppOAuthResponse>("/apps/{appNameOrId}/oauth/generate", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GenerateAppOauth", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get an App Get a single app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <returns>App</returns>
        public App GetApp(string authorization, string appNameOrId)
        {
            SmartThingsNet.Client.ApiResponse<App> localVarResponse = GetAppWithHttpInfo(authorization, appNameOrId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get an App Get a single app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <returns>ApiResponse of App</returns>
        public SmartThingsNet.Client.ApiResponse<App> GetAppWithHttpInfo(string authorization, string appNameOrId)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->GetApp");
            }

            // verify the required parameter 'appNameOrId' is set
            if (appNameOrId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'appNameOrId' when calling AppsApi->GetApp");
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

            localVarRequestOptions.PathParameters.Add("appNameOrId", SmartThingsNet.Client.ClientUtils.ParameterToString(appNameOrId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<App>("/apps/{appNameOrId}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetApp", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get an App Get a single app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of App</returns>
        public async System.Threading.Tasks.Task<App> GetAppAsync(string authorization, string appNameOrId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<App> localVarResponse = await GetAppWithHttpInfoAsync(authorization, appNameOrId, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get an App Get a single app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (App)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<App>> GetAppWithHttpInfoAsync(string authorization, string appNameOrId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->GetApp");
            }

            // verify the required parameter 'appNameOrId' is set
            if (appNameOrId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'appNameOrId' when calling AppsApi->GetApp");
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

            localVarRequestOptions.PathParameters.Add("appNameOrId", SmartThingsNet.Client.ClientUtils.ParameterToString(appNameOrId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<App>("/apps/{appNameOrId}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetApp", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get an App&#39;s OAuth Settings Retrieve an app&#39;s oauth settings.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <returns>AppOAuth</returns>
        public AppOAuth GetAppOauth(string authorization, string appNameOrId)
        {
            SmartThingsNet.Client.ApiResponse<AppOAuth> localVarResponse = GetAppOauthWithHttpInfo(authorization, appNameOrId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get an App&#39;s OAuth Settings Retrieve an app&#39;s oauth settings.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <returns>ApiResponse of AppOAuth</returns>
        public SmartThingsNet.Client.ApiResponse<AppOAuth> GetAppOauthWithHttpInfo(string authorization, string appNameOrId)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->GetAppOauth");
            }

            // verify the required parameter 'appNameOrId' is set
            if (appNameOrId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'appNameOrId' when calling AppsApi->GetAppOauth");
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

            localVarRequestOptions.PathParameters.Add("appNameOrId", SmartThingsNet.Client.ClientUtils.ParameterToString(appNameOrId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<AppOAuth>("/apps/{appNameOrId}/oauth", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetAppOauth", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get an App&#39;s OAuth Settings Retrieve an app&#39;s oauth settings.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AppOAuth</returns>
        public async System.Threading.Tasks.Task<AppOAuth> GetAppOauthAsync(string authorization, string appNameOrId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<AppOAuth> localVarResponse = await GetAppOauthWithHttpInfoAsync(authorization, appNameOrId, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get an App&#39;s OAuth Settings Retrieve an app&#39;s oauth settings.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AppOAuth)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<AppOAuth>> GetAppOauthWithHttpInfoAsync(string authorization, string appNameOrId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->GetAppOauth");
            }

            // verify the required parameter 'appNameOrId' is set
            if (appNameOrId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'appNameOrId' when calling AppsApi->GetAppOauth");
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

            localVarRequestOptions.PathParameters.Add("appNameOrId", SmartThingsNet.Client.ClientUtils.ParameterToString(appNameOrId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<AppOAuth>("/apps/{appNameOrId}/oauth", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetAppOauth", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get an App&#39;s Settings Retrieve the settings for an app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <returns>GetAppSettingsResponse</returns>
        public GetAppSettingsResponse GetAppSettings(string authorization, string appNameOrId)
        {
            SmartThingsNet.Client.ApiResponse<GetAppSettingsResponse> localVarResponse = GetAppSettingsWithHttpInfo(authorization, appNameOrId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get an App&#39;s Settings Retrieve the settings for an app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <returns>ApiResponse of GetAppSettingsResponse</returns>
        public SmartThingsNet.Client.ApiResponse<GetAppSettingsResponse> GetAppSettingsWithHttpInfo(string authorization, string appNameOrId)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->GetAppSettings");
            }

            // verify the required parameter 'appNameOrId' is set
            if (appNameOrId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'appNameOrId' when calling AppsApi->GetAppSettings");
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

            localVarRequestOptions.PathParameters.Add("appNameOrId", SmartThingsNet.Client.ClientUtils.ParameterToString(appNameOrId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetAppSettingsResponse>("/apps/{appNameOrId}/settings", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetAppSettings", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get an App&#39;s Settings Retrieve the settings for an app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetAppSettingsResponse</returns>
        public async System.Threading.Tasks.Task<GetAppSettingsResponse> GetAppSettingsAsync(string authorization, string appNameOrId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<GetAppSettingsResponse> localVarResponse = await GetAppSettingsWithHttpInfoAsync(authorization, appNameOrId, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get an App&#39;s Settings Retrieve the settings for an app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetAppSettingsResponse)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<GetAppSettingsResponse>> GetAppSettingsWithHttpInfoAsync(string authorization, string appNameOrId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->GetAppSettings");
            }

            // verify the required parameter 'appNameOrId' is set
            if (appNameOrId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'appNameOrId' when calling AppsApi->GetAppSettings");
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

            localVarRequestOptions.PathParameters.Add("appNameOrId", SmartThingsNet.Client.ClientUtils.ParameterToString(appNameOrId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetAppSettingsResponse>("/apps/{appNameOrId}/settings", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetAppSettings", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List All Apps List all apps configured in an account.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appType">The App Type of the application. (optional)</param>
        /// <param name="classification">An App maybe associated to many classifications.  A classification drives how the integration is presented to the user in the SmartThings mobile clients.  These classifications include: * AUTOMATION - Denotes an integration that should display under the \&quot;Automation\&quot; tab in mobile clients. * SERVICE - Denotes an integration that is classified as a \&quot;Service\&quot;. * DEVICE - Denotes an integration that should display under the \&quot;Device\&quot; tab in mobile clients. * CONNECTED_SERVICE - Denotes an integration that should display under the \&quot;Connected Services\&quot; menu in mobile clients.  (optional)</param>
        /// <param name="tag">May be used to filter a resource by it&#39;s assigned user-tags.  Multiple tag query params are automatically joined with OR.  Example usage in query string: &#x60;&#x60;&#x60; ?tag:key_name&#x3D;value1&amp;tag:key_name&#x3D;value2 &#x60;&#x60;&#x60;  (optional)</param>
        /// <returns>PagedApps</returns>
        public PagedApps ListApps(string authorization, string appType = default(string), string classification = default(string), string tag = default(string))
        {
            SmartThingsNet.Client.ApiResponse<PagedApps> localVarResponse = ListAppsWithHttpInfo(authorization, appType, classification, tag);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List All Apps List all apps configured in an account.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appType">The App Type of the application. (optional)</param>
        /// <param name="classification">An App maybe associated to many classifications.  A classification drives how the integration is presented to the user in the SmartThings mobile clients.  These classifications include: * AUTOMATION - Denotes an integration that should display under the \&quot;Automation\&quot; tab in mobile clients. * SERVICE - Denotes an integration that is classified as a \&quot;Service\&quot;. * DEVICE - Denotes an integration that should display under the \&quot;Device\&quot; tab in mobile clients. * CONNECTED_SERVICE - Denotes an integration that should display under the \&quot;Connected Services\&quot; menu in mobile clients.  (optional)</param>
        /// <param name="tag">May be used to filter a resource by it&#39;s assigned user-tags.  Multiple tag query params are automatically joined with OR.  Example usage in query string: &#x60;&#x60;&#x60; ?tag:key_name&#x3D;value1&amp;tag:key_name&#x3D;value2 &#x60;&#x60;&#x60;  (optional)</param>
        /// <returns>ApiResponse of PagedApps</returns>
        public SmartThingsNet.Client.ApiResponse<PagedApps> ListAppsWithHttpInfo(string authorization, string appType = default(string), string classification = default(string), string tag = default(string))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->ListApps");
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

            if (appType != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "appType", appType));
            }
            if (classification != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "classification", classification));
            }
            if (tag != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "tag", tag));
            }
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<PagedApps>("/apps", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListApps", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List All Apps List all apps configured in an account.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appType">The App Type of the application. (optional)</param>
        /// <param name="classification">An App maybe associated to many classifications.  A classification drives how the integration is presented to the user in the SmartThings mobile clients.  These classifications include: * AUTOMATION - Denotes an integration that should display under the \&quot;Automation\&quot; tab in mobile clients. * SERVICE - Denotes an integration that is classified as a \&quot;Service\&quot;. * DEVICE - Denotes an integration that should display under the \&quot;Device\&quot; tab in mobile clients. * CONNECTED_SERVICE - Denotes an integration that should display under the \&quot;Connected Services\&quot; menu in mobile clients.  (optional)</param>
        /// <param name="tag">May be used to filter a resource by it&#39;s assigned user-tags.  Multiple tag query params are automatically joined with OR.  Example usage in query string: &#x60;&#x60;&#x60; ?tag:key_name&#x3D;value1&amp;tag:key_name&#x3D;value2 &#x60;&#x60;&#x60;  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PagedApps</returns>
        public async System.Threading.Tasks.Task<PagedApps> ListAppsAsync(string authorization, string appType = default(string), string classification = default(string), string tag = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<PagedApps> localVarResponse = await ListAppsWithHttpInfoAsync(authorization, appType, classification, tag, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List All Apps List all apps configured in an account.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appType">The App Type of the application. (optional)</param>
        /// <param name="classification">An App maybe associated to many classifications.  A classification drives how the integration is presented to the user in the SmartThings mobile clients.  These classifications include: * AUTOMATION - Denotes an integration that should display under the \&quot;Automation\&quot; tab in mobile clients. * SERVICE - Denotes an integration that is classified as a \&quot;Service\&quot;. * DEVICE - Denotes an integration that should display under the \&quot;Device\&quot; tab in mobile clients. * CONNECTED_SERVICE - Denotes an integration that should display under the \&quot;Connected Services\&quot; menu in mobile clients.  (optional)</param>
        /// <param name="tag">May be used to filter a resource by it&#39;s assigned user-tags.  Multiple tag query params are automatically joined with OR.  Example usage in query string: &#x60;&#x60;&#x60; ?tag:key_name&#x3D;value1&amp;tag:key_name&#x3D;value2 &#x60;&#x60;&#x60;  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PagedApps)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<PagedApps>> ListAppsWithHttpInfoAsync(string authorization, string appType = default(string), string classification = default(string), string tag = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->ListApps");
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

            if (appType != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "appType", appType));
            }
            if (classification != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "classification", classification));
            }
            if (tag != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "tag", tag));
            }
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<PagedApps>("/apps", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListApps", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Send a Confirmation Request to an App Prepare to register an App by sending the endpoint a confirmation message.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="appRegisterRequest"></param>
        /// <returns>Object</returns>
        public Object Register(string authorization, string appNameOrId, Object appRegisterRequest)
        {
            SmartThingsNet.Client.ApiResponse<Object> localVarResponse = RegisterWithHttpInfo(authorization, appNameOrId, appRegisterRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Send a Confirmation Request to an App Prepare to register an App by sending the endpoint a confirmation message.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="appRegisterRequest"></param>
        /// <returns>ApiResponse of Object</returns>
        public SmartThingsNet.Client.ApiResponse<Object> RegisterWithHttpInfo(string authorization, string appNameOrId, Object appRegisterRequest)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->Register");
            }

            // verify the required parameter 'appNameOrId' is set
            if (appNameOrId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'appNameOrId' when calling AppsApi->Register");
            }

            // verify the required parameter 'appRegisterRequest' is set
            if (appRegisterRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'appRegisterRequest' when calling AppsApi->Register");
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

            localVarRequestOptions.PathParameters.Add("appNameOrId", SmartThingsNet.Client.ClientUtils.ParameterToString(appNameOrId)); // path parameter
            
            localVarRequestOptions.Data = appRegisterRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<Object>("/apps/{appNameOrId}/register", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("Register", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Send a Confirmation Request to an App Prepare to register an App by sending the endpoint a confirmation message.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="appRegisterRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Object</returns>
        public async System.Threading.Tasks.Task<Object> RegisterAsync(string authorization, string appNameOrId, Object appRegisterRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<Object> localVarResponse = await RegisterWithHttpInfoAsync(authorization, appNameOrId, appRegisterRequest, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Send a Confirmation Request to an App Prepare to register an App by sending the endpoint a confirmation message.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="appRegisterRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Object)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Object>> RegisterWithHttpInfoAsync(string authorization, string appNameOrId, Object appRegisterRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->Register");
            }

            // verify the required parameter 'appNameOrId' is set
            if (appNameOrId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'appNameOrId' when calling AppsApi->Register");
            }

            // verify the required parameter 'appRegisterRequest' is set
            if (appRegisterRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'appRegisterRequest' when calling AppsApi->Register");
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

            localVarRequestOptions.PathParameters.Add("appNameOrId", SmartThingsNet.Client.ClientUtils.ParameterToString(appNameOrId)); // path parameter
            
            localVarRequestOptions.Data = appRegisterRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<Object>("/apps/{appNameOrId}/register", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("Register", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update an App Update an app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateAppRequest"></param>
        /// <param name="signatureType">The Signature Type of the application. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="requireConfirmation">Override default configuration to use either PING or CONFIRMATION lifecycle. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <returns>App</returns>
        public App UpdateApp(string authorization, string appNameOrId, UpdateAppRequest updateAppRequest, string signatureType = default(string), bool? requireConfirmation = default(bool?))
        {
            SmartThingsNet.Client.ApiResponse<App> localVarResponse = UpdateAppWithHttpInfo(authorization, appNameOrId, updateAppRequest, signatureType, requireConfirmation);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update an App Update an app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateAppRequest"></param>
        /// <param name="signatureType">The Signature Type of the application. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="requireConfirmation">Override default configuration to use either PING or CONFIRMATION lifecycle. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <returns>ApiResponse of App</returns>
        public SmartThingsNet.Client.ApiResponse<App> UpdateAppWithHttpInfo(string authorization, string appNameOrId, UpdateAppRequest updateAppRequest, string signatureType = default(string), bool? requireConfirmation = default(bool?))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->UpdateApp");
            }

            // verify the required parameter 'appNameOrId' is set
            if (appNameOrId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'appNameOrId' when calling AppsApi->UpdateApp");
            }

            // verify the required parameter 'updateAppRequest' is set
            if (updateAppRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'updateAppRequest' when calling AppsApi->UpdateApp");
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

            localVarRequestOptions.PathParameters.Add("appNameOrId", SmartThingsNet.Client.ClientUtils.ParameterToString(appNameOrId)); // path parameter
            if (signatureType != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "signatureType", signatureType));
            }
            if (requireConfirmation != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "requireConfirmation", requireConfirmation));
            }
            
            localVarRequestOptions.Data = updateAppRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<App>("/apps/{appNameOrId}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateApp", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update an App Update an app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateAppRequest"></param>
        /// <param name="signatureType">The Signature Type of the application. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="requireConfirmation">Override default configuration to use either PING or CONFIRMATION lifecycle. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of App</returns>
        public async System.Threading.Tasks.Task<App> UpdateAppAsync(string authorization, string appNameOrId, UpdateAppRequest updateAppRequest, string signatureType = default(string), bool? requireConfirmation = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<App> localVarResponse = await UpdateAppWithHttpInfoAsync(authorization, appNameOrId, updateAppRequest, signatureType, requireConfirmation, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update an App Update an app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateAppRequest"></param>
        /// <param name="signatureType">The Signature Type of the application. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="requireConfirmation">Override default configuration to use either PING or CONFIRMATION lifecycle. For WEBHOOK_SMART_APP only.  (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (App)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<App>> UpdateAppWithHttpInfoAsync(string authorization, string appNameOrId, UpdateAppRequest updateAppRequest, string signatureType = default(string), bool? requireConfirmation = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->UpdateApp");
            }

            // verify the required parameter 'appNameOrId' is set
            if (appNameOrId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'appNameOrId' when calling AppsApi->UpdateApp");
            }

            // verify the required parameter 'updateAppRequest' is set
            if (updateAppRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'updateAppRequest' when calling AppsApi->UpdateApp");
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

            localVarRequestOptions.PathParameters.Add("appNameOrId", SmartThingsNet.Client.ClientUtils.ParameterToString(appNameOrId)); // path parameter
            if (signatureType != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "signatureType", signatureType));
            }
            if (requireConfirmation != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "requireConfirmation", requireConfirmation));
            }
            
            localVarRequestOptions.Data = updateAppRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<App>("/apps/{appNameOrId}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateApp", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update an App&#39;s OAuth Settings Update an app&#39;s oauth settings.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateAppOAuthRequest"></param>
        /// <returns>AppOAuth</returns>
        public AppOAuth UpdateAppOauth(string authorization, string appNameOrId, UpdateAppOAuthRequest updateAppOAuthRequest)
        {
            SmartThingsNet.Client.ApiResponse<AppOAuth> localVarResponse = UpdateAppOauthWithHttpInfo(authorization, appNameOrId, updateAppOAuthRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update an App&#39;s OAuth Settings Update an app&#39;s oauth settings.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateAppOAuthRequest"></param>
        /// <returns>ApiResponse of AppOAuth</returns>
        public SmartThingsNet.Client.ApiResponse<AppOAuth> UpdateAppOauthWithHttpInfo(string authorization, string appNameOrId, UpdateAppOAuthRequest updateAppOAuthRequest)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->UpdateAppOauth");
            }

            // verify the required parameter 'appNameOrId' is set
            if (appNameOrId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'appNameOrId' when calling AppsApi->UpdateAppOauth");
            }

            // verify the required parameter 'updateAppOAuthRequest' is set
            if (updateAppOAuthRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'updateAppOAuthRequest' when calling AppsApi->UpdateAppOauth");
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

            localVarRequestOptions.PathParameters.Add("appNameOrId", SmartThingsNet.Client.ClientUtils.ParameterToString(appNameOrId)); // path parameter
            
            localVarRequestOptions.Data = updateAppOAuthRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<AppOAuth>("/apps/{appNameOrId}/oauth", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateAppOauth", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update an App&#39;s OAuth Settings Update an app&#39;s oauth settings.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateAppOAuthRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AppOAuth</returns>
        public async System.Threading.Tasks.Task<AppOAuth> UpdateAppOauthAsync(string authorization, string appNameOrId, UpdateAppOAuthRequest updateAppOAuthRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<AppOAuth> localVarResponse = await UpdateAppOauthWithHttpInfoAsync(authorization, appNameOrId, updateAppOAuthRequest, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update an App&#39;s OAuth Settings Update an app&#39;s oauth settings.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateAppOAuthRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AppOAuth)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<AppOAuth>> UpdateAppOauthWithHttpInfoAsync(string authorization, string appNameOrId, UpdateAppOAuthRequest updateAppOAuthRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->UpdateAppOauth");
            }

            // verify the required parameter 'appNameOrId' is set
            if (appNameOrId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'appNameOrId' when calling AppsApi->UpdateAppOauth");
            }

            // verify the required parameter 'updateAppOAuthRequest' is set
            if (updateAppOAuthRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'updateAppOAuthRequest' when calling AppsApi->UpdateAppOauth");
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

            localVarRequestOptions.PathParameters.Add("appNameOrId", SmartThingsNet.Client.ClientUtils.ParameterToString(appNameOrId)); // path parameter
            
            localVarRequestOptions.Data = updateAppOAuthRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<AppOAuth>("/apps/{appNameOrId}/oauth", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateAppOauth", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update an App&#39;s Settings Update the settings for an app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId  field of an app.</param>
        /// <param name="updateAppSettingsRequest"></param>
        /// <returns>UpdateAppSettingsResponse</returns>
        public UpdateAppSettingsResponse UpdateAppSettings(string authorization, string appNameOrId, UpdateAppSettingsRequest updateAppSettingsRequest)
        {
            SmartThingsNet.Client.ApiResponse<UpdateAppSettingsResponse> localVarResponse = UpdateAppSettingsWithHttpInfo(authorization, appNameOrId, updateAppSettingsRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update an App&#39;s Settings Update the settings for an app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId  field of an app.</param>
        /// <param name="updateAppSettingsRequest"></param>
        /// <returns>ApiResponse of UpdateAppSettingsResponse</returns>
        public SmartThingsNet.Client.ApiResponse<UpdateAppSettingsResponse> UpdateAppSettingsWithHttpInfo(string authorization, string appNameOrId, UpdateAppSettingsRequest updateAppSettingsRequest)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->UpdateAppSettings");
            }

            // verify the required parameter 'appNameOrId' is set
            if (appNameOrId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'appNameOrId' when calling AppsApi->UpdateAppSettings");
            }

            // verify the required parameter 'updateAppSettingsRequest' is set
            if (updateAppSettingsRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'updateAppSettingsRequest' when calling AppsApi->UpdateAppSettings");
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

            localVarRequestOptions.PathParameters.Add("appNameOrId", SmartThingsNet.Client.ClientUtils.ParameterToString(appNameOrId)); // path parameter
            
            localVarRequestOptions.Data = updateAppSettingsRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<UpdateAppSettingsResponse>("/apps/{appNameOrId}/settings", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateAppSettings", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update an App&#39;s Settings Update the settings for an app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId  field of an app.</param>
        /// <param name="updateAppSettingsRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of UpdateAppSettingsResponse</returns>
        public async System.Threading.Tasks.Task<UpdateAppSettingsResponse> UpdateAppSettingsAsync(string authorization, string appNameOrId, UpdateAppSettingsRequest updateAppSettingsRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<UpdateAppSettingsResponse> localVarResponse = await UpdateAppSettingsWithHttpInfoAsync(authorization, appNameOrId, updateAppSettingsRequest, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update an App&#39;s Settings Update the settings for an app.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId  field of an app.</param>
        /// <param name="updateAppSettingsRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (UpdateAppSettingsResponse)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<UpdateAppSettingsResponse>> UpdateAppSettingsWithHttpInfoAsync(string authorization, string appNameOrId, UpdateAppSettingsRequest updateAppSettingsRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->UpdateAppSettings");
            }

            // verify the required parameter 'appNameOrId' is set
            if (appNameOrId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'appNameOrId' when calling AppsApi->UpdateAppSettings");
            }

            // verify the required parameter 'updateAppSettingsRequest' is set
            if (updateAppSettingsRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'updateAppSettingsRequest' when calling AppsApi->UpdateAppSettings");
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

            localVarRequestOptions.PathParameters.Add("appNameOrId", SmartThingsNet.Client.ClientUtils.ParameterToString(appNameOrId)); // path parameter
            
            localVarRequestOptions.Data = updateAppSettingsRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<UpdateAppSettingsResponse>("/apps/{appNameOrId}/settings", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateAppSettings", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update an App&#39;s Signature Type Updates the signature type of an App.  Signature options:   * APP_RSA - Legacy signing mechanism comprised of a public / private key generated for an App during registration.  This mechanism requires an App to download the public key and deploy along side their integration to verify the signature in the authorization header.   * ST_PADLOCK - App callbacks are signed with a SmartThings certificate which is publicly available at https://key.smartthings.com.  App&#39;s authorize callbacks by fetching the certificate over HTTPS and using it to validate the signature in the authorization header.  When upgrading an App from APP_RSA to ST_PADLOCK, it is recommended to implement both verification methods. This will provide the ability to seamlessly switch between mechanisms if a rollback is needed. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateSignatureTypeRequest"></param>
        /// <returns>Object</returns>
        public Object UpdateSignatureType(string authorization, string appNameOrId, UpdateSignatureTypeRequest updateSignatureTypeRequest)
        {
            SmartThingsNet.Client.ApiResponse<Object> localVarResponse = UpdateSignatureTypeWithHttpInfo(authorization, appNameOrId, updateSignatureTypeRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update an App&#39;s Signature Type Updates the signature type of an App.  Signature options:   * APP_RSA - Legacy signing mechanism comprised of a public / private key generated for an App during registration.  This mechanism requires an App to download the public key and deploy along side their integration to verify the signature in the authorization header.   * ST_PADLOCK - App callbacks are signed with a SmartThings certificate which is publicly available at https://key.smartthings.com.  App&#39;s authorize callbacks by fetching the certificate over HTTPS and using it to validate the signature in the authorization header.  When upgrading an App from APP_RSA to ST_PADLOCK, it is recommended to implement both verification methods. This will provide the ability to seamlessly switch between mechanisms if a rollback is needed. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateSignatureTypeRequest"></param>
        /// <returns>ApiResponse of Object</returns>
        public SmartThingsNet.Client.ApiResponse<Object> UpdateSignatureTypeWithHttpInfo(string authorization, string appNameOrId, UpdateSignatureTypeRequest updateSignatureTypeRequest)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->UpdateSignatureType");
            }

            // verify the required parameter 'appNameOrId' is set
            if (appNameOrId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'appNameOrId' when calling AppsApi->UpdateSignatureType");
            }

            // verify the required parameter 'updateSignatureTypeRequest' is set
            if (updateSignatureTypeRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'updateSignatureTypeRequest' when calling AppsApi->UpdateSignatureType");
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

            localVarRequestOptions.PathParameters.Add("appNameOrId", SmartThingsNet.Client.ClientUtils.ParameterToString(appNameOrId)); // path parameter
            
            localVarRequestOptions.Data = updateSignatureTypeRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<Object>("/apps/{appNameOrId}/signature-type", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateSignatureType", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update an App&#39;s Signature Type Updates the signature type of an App.  Signature options:   * APP_RSA - Legacy signing mechanism comprised of a public / private key generated for an App during registration.  This mechanism requires an App to download the public key and deploy along side their integration to verify the signature in the authorization header.   * ST_PADLOCK - App callbacks are signed with a SmartThings certificate which is publicly available at https://key.smartthings.com.  App&#39;s authorize callbacks by fetching the certificate over HTTPS and using it to validate the signature in the authorization header.  When upgrading an App from APP_RSA to ST_PADLOCK, it is recommended to implement both verification methods. This will provide the ability to seamlessly switch between mechanisms if a rollback is needed. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateSignatureTypeRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Object</returns>
        public async System.Threading.Tasks.Task<Object> UpdateSignatureTypeAsync(string authorization, string appNameOrId, UpdateSignatureTypeRequest updateSignatureTypeRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<Object> localVarResponse = await UpdateSignatureTypeWithHttpInfoAsync(authorization, appNameOrId, updateSignatureTypeRequest, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update an App&#39;s Signature Type Updates the signature type of an App.  Signature options:   * APP_RSA - Legacy signing mechanism comprised of a public / private key generated for an App during registration.  This mechanism requires an App to download the public key and deploy along side their integration to verify the signature in the authorization header.   * ST_PADLOCK - App callbacks are signed with a SmartThings certificate which is publicly available at https://key.smartthings.com.  App&#39;s authorize callbacks by fetching the certificate over HTTPS and using it to validate the signature in the authorization header.  When upgrading an App from APP_RSA to ST_PADLOCK, it is recommended to implement both verification methods. This will provide the ability to seamlessly switch between mechanisms if a rollback is needed. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="appNameOrId">The appName or appId field of an app.</param>
        /// <param name="updateSignatureTypeRequest"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Object)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Object>> UpdateSignatureTypeWithHttpInfoAsync(string authorization, string appNameOrId, UpdateSignatureTypeRequest updateSignatureTypeRequest, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling AppsApi->UpdateSignatureType");
            }

            // verify the required parameter 'appNameOrId' is set
            if (appNameOrId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'appNameOrId' when calling AppsApi->UpdateSignatureType");
            }

            // verify the required parameter 'updateSignatureTypeRequest' is set
            if (updateSignatureTypeRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'updateSignatureTypeRequest' when calling AppsApi->UpdateSignatureType");
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

            localVarRequestOptions.PathParameters.Add("appNameOrId", SmartThingsNet.Client.ClientUtils.ParameterToString(appNameOrId)); // path parameter
            
            localVarRequestOptions.Data = updateSignatureTypeRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<Object>("/apps/{appNameOrId}/signature-type", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateSignatureType", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

    }
}
