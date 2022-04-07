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
    public interface ICapabilitiesApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Create a Capability
        /// </summary>
        /// <remarks>
        /// Create a Capability.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityRequest"></param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="_namespace">The namespace of the Capability (optional)</param>
        /// <returns>Capability</returns>
        Capability CreateCapability(string authorization, CreateCapabilityRequest capabilityRequest, string xSTOrganization = default(string), string _namespace = default(string));

        /// <summary>
        /// Create a Capability
        /// </summary>
        /// <remarks>
        /// Create a Capability.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityRequest"></param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="_namespace">The namespace of the Capability (optional)</param>
        /// <returns>ApiResponse of Capability</returns>
        ApiResponse<Capability> CreateCapabilityWithHttpInfo(string authorization, CreateCapabilityRequest capabilityRequest, string xSTOrganization = default(string), string _namespace = default(string));
        /// <summary>
        /// Create a Capability Localization for a Locale
        /// </summary>
        /// <remarks>
        /// Add translated values in a desired language (i.e. locale) for a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be stored. Translated values can include: capability name labels, attribute name labels and attribute value labels. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="capabilityLocalization"></param>
        /// <returns>CapabilityLocalization</returns>
        CapabilityLocalization CreateCapabilityLocalization(string authorization, string capabilityId, int capabilityVersion, CapabilityLocalization capabilityLocalization);

        /// <summary>
        /// Create a Capability Localization for a Locale
        /// </summary>
        /// <remarks>
        /// Add translated values in a desired language (i.e. locale) for a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be stored. Translated values can include: capability name labels, attribute name labels and attribute value labels. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="capabilityLocalization"></param>
        /// <returns>ApiResponse of CapabilityLocalization</returns>
        ApiResponse<CapabilityLocalization> CreateCapabilityLocalizationWithHttpInfo(string authorization, string capabilityId, int capabilityVersion, CapabilityLocalization capabilityLocalization);
        /// <summary>
        /// Delete a Capability by ID and Version
        /// </summary>
        /// <remarks>
        /// Delete a Capability with a given ID and version.  The Capability must be in &#x60;proposed&#x60; status. Once a Capability is updated to &#x60;live&#x60; status, it cannot be deleted and must be transitioned to &#x60;deprecated&#x60;. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <returns>Object</returns>
        Object DeleteCapability(string authorization, string capabilityId, int capabilityVersion, string xSTOrganization = default(string));

        /// <summary>
        /// Delete a Capability by ID and Version
        /// </summary>
        /// <remarks>
        /// Delete a Capability with a given ID and version.  The Capability must be in &#x60;proposed&#x60; status. Once a Capability is updated to &#x60;live&#x60; status, it cannot be deleted and must be transitioned to &#x60;deprecated&#x60;. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <returns>ApiResponse of Object</returns>
        ApiResponse<Object> DeleteCapabilityWithHttpInfo(string authorization, string capabilityId, int capabilityVersion, string xSTOrganization = default(string));
        /// <summary>
        /// Get a Capability by ID and Version
        /// </summary>
        /// <remarks>
        /// Get a Capability with a given ID and version.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <returns>Capability</returns>
        Capability GetCapability(string authorization, string capabilityId, int capabilityVersion);

        /// <summary>
        /// Get a Capability by ID and Version
        /// </summary>
        /// <remarks>
        /// Get a Capability with a given ID and version.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <returns>ApiResponse of Capability</returns>
        ApiResponse<Capability> GetCapabilityWithHttpInfo(string authorization, string capabilityId, int capabilityVersion);
        /// <summary>
        /// Get a Capability Localization by Locale
        /// </summary>
        /// <remarks>
        /// Retrieve translated values in a desired language (i.e. locale) for a Capability. A &#x60;locale&#x60; is the abbreviated, internationalization code for the requested language. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <returns>CapabilityLocalization</returns>
        CapabilityLocalization GetCapabilityLocalization(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, string manufacturerName = default(string));

        /// <summary>
        /// Get a Capability Localization by Locale
        /// </summary>
        /// <remarks>
        /// Retrieve translated values in a desired language (i.e. locale) for a Capability. A &#x60;locale&#x60; is the abbreviated, internationalization code for the requested language. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <returns>ApiResponse of CapabilityLocalization</returns>
        ApiResponse<CapabilityLocalization> GetCapabilityLocalizationWithHttpInfo(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, string manufacturerName = default(string));
        /// <summary>
        /// List all Capabilities
        /// </summary>
        /// <remarks>
        /// List all standard Capabilities.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <returns>PagedCapabilities</returns>
        PagedCapabilities ListCapabilities(string authorization);

        /// <summary>
        /// List all Capabilities
        /// </summary>
        /// <remarks>
        /// List all standard Capabilities.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <returns>ApiResponse of PagedCapabilities</returns>
        ApiResponse<PagedCapabilities> ListCapabilitiesWithHttpInfo(string authorization);
        /// <summary>
        /// List a Capability&#39;s Localizations
        /// </summary>
        /// <remarks>
        /// List all languages (i.e. locales) for which a Capability has translated values. A &#x60;locale&#x60; is the abbreviated, internationalization code for the associated language. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <returns>CapabilityLocaleLocalizations</returns>
        CapabilityLocaleLocalizations ListCapabilityLocalizations(string authorization, string capabilityId, int capabilityVersion);

        /// <summary>
        /// List a Capability&#39;s Localizations
        /// </summary>
        /// <remarks>
        /// List all languages (i.e. locales) for which a Capability has translated values. A &#x60;locale&#x60; is the abbreviated, internationalization code for the associated language. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <returns>ApiResponse of CapabilityLocaleLocalizations</returns>
        ApiResponse<CapabilityLocaleLocalizations> ListCapabilityLocalizationsWithHttpInfo(string authorization, string capabilityId, int capabilityVersion);
        /// <summary>
        /// List a Capability&#39;s Versions
        /// </summary>
        /// <remarks>
        /// List a Capability&#39;s versions.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <returns>PagedCapabilities</returns>
        PagedCapabilities ListCapabilityVersions(string authorization, string capabilityId);

        /// <summary>
        /// List a Capability&#39;s Versions
        /// </summary>
        /// <remarks>
        /// List a Capability&#39;s versions.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <returns>ApiResponse of PagedCapabilities</returns>
        ApiResponse<PagedCapabilities> ListCapabilityVersionsWithHttpInfo(string authorization, string capabilityId);
        /// <summary>
        /// List Capabilities by Namespace
        /// </summary>
        /// <remarks>
        /// List Capabilities by namespace.  Namespaces are used to organize a user&#39;s Capabilities and provide a way to uniquely identify them. A user can retrieve all of the Capabilities under their assigned namespace by referencing it.  The namespace is recognizable as the first part of a capabilityId in the format &#x60;namespace.capabilityName&#x60;. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="_namespace">The namespace of the Capability</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <returns>PagedCapabilities</returns>
        PagedCapabilities ListNamespacedCapabilities(string authorization, string _namespace, string xSTOrganization = default(string));

        /// <summary>
        /// List Capabilities by Namespace
        /// </summary>
        /// <remarks>
        /// List Capabilities by namespace.  Namespaces are used to organize a user&#39;s Capabilities and provide a way to uniquely identify them. A user can retrieve all of the Capabilities under their assigned namespace by referencing it.  The namespace is recognizable as the first part of a capabilityId in the format &#x60;namespace.capabilityName&#x60;. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="_namespace">The namespace of the Capability</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <returns>ApiResponse of PagedCapabilities</returns>
        ApiResponse<PagedCapabilities> ListNamespacedCapabilitiesWithHttpInfo(string authorization, string _namespace, string xSTOrganization = default(string));
        /// <summary>
        /// Patch a Capability Localization by Locale
        /// </summary>
        /// <remarks>
        /// Modify specific values of existing language (i.e. locale) translations for a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be modified.&lt;br /&gt;&lt;br /&gt; All fields in the request body are optional. Any fields populated will replace existing localization values. Any fields left blank will fall back to existing values. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="capabilityLocalization"></param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <returns>CapabilityLocalization</returns>
        CapabilityLocalization PatchCapabilityLocalization(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, CapabilityLocalization capabilityLocalization, string manufacturerName = default(string));

        /// <summary>
        /// Patch a Capability Localization by Locale
        /// </summary>
        /// <remarks>
        /// Modify specific values of existing language (i.e. locale) translations for a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be modified.&lt;br /&gt;&lt;br /&gt; All fields in the request body are optional. Any fields populated will replace existing localization values. Any fields left blank will fall back to existing values. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="capabilityLocalization"></param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <returns>ApiResponse of CapabilityLocalization</returns>
        ApiResponse<CapabilityLocalization> PatchCapabilityLocalizationWithHttpInfo(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, CapabilityLocalization capabilityLocalization, string manufacturerName = default(string));
        /// <summary>
        /// Update a Capability
        /// </summary>
        /// <remarks>
        /// Update a Capability.  Capabilities with a &#x60;proposed&#x60; status can be updated at will.  Capabilities with a &#x60;live&#x60;, &#x60;deprecated&#x60;, or &#x60;dead&#x60; status are immutable and can&#39;t be updated.  Capability names cannot be changed. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="capabilityRequest"></param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <returns>Capability</returns>
        Capability UpdateCapability(string authorization, string capabilityId, int capabilityVersion, UpdateCapabilityRequest capabilityRequest, string xSTOrganization = default(string));

        /// <summary>
        /// Update a Capability
        /// </summary>
        /// <remarks>
        /// Update a Capability.  Capabilities with a &#x60;proposed&#x60; status can be updated at will.  Capabilities with a &#x60;live&#x60;, &#x60;deprecated&#x60;, or &#x60;dead&#x60; status are immutable and can&#39;t be updated.  Capability names cannot be changed. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="capabilityRequest"></param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <returns>ApiResponse of Capability</returns>
        ApiResponse<Capability> UpdateCapabilityWithHttpInfo(string authorization, string capabilityId, int capabilityVersion, UpdateCapabilityRequest capabilityRequest, string xSTOrganization = default(string));
        /// <summary>
        /// Update a Capability Localization by Locale
        /// </summary>
        /// <remarks>
        /// Update existing translated values for a given language (i.e. locale) associated with a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be updated.&lt;br /&gt;&lt;br /&gt; If the &#x60;tag&#x60; argument is provided in the request body, it will be ignored. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="capabilityLocalization"></param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <returns>CapabilityLocalization</returns>
        CapabilityLocalization UpdateCapabilityLocalization(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, CapabilityLocalization capabilityLocalization, string manufacturerName = default(string));

        /// <summary>
        /// Update a Capability Localization by Locale
        /// </summary>
        /// <remarks>
        /// Update existing translated values for a given language (i.e. locale) associated with a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be updated.&lt;br /&gt;&lt;br /&gt; If the &#x60;tag&#x60; argument is provided in the request body, it will be ignored. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="capabilityLocalization"></param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <returns>ApiResponse of CapabilityLocalization</returns>
        ApiResponse<CapabilityLocalization> UpdateCapabilityLocalizationWithHttpInfo(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, CapabilityLocalization capabilityLocalization, string manufacturerName = default(string));
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface ICapabilitiesApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Create a Capability
        /// </summary>
        /// <remarks>
        /// Create a Capability.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityRequest"></param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="_namespace">The namespace of the Capability (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Capability</returns>
        System.Threading.Tasks.Task<Capability> CreateCapabilityAsync(string authorization, CreateCapabilityRequest capabilityRequest, string xSTOrganization = default(string), string _namespace = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create a Capability
        /// </summary>
        /// <remarks>
        /// Create a Capability.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityRequest"></param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="_namespace">The namespace of the Capability (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Capability)</returns>
        System.Threading.Tasks.Task<ApiResponse<Capability>> CreateCapabilityWithHttpInfoAsync(string authorization, CreateCapabilityRequest capabilityRequest, string xSTOrganization = default(string), string _namespace = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Create a Capability Localization for a Locale
        /// </summary>
        /// <remarks>
        /// Add translated values in a desired language (i.e. locale) for a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be stored. Translated values can include: capability name labels, attribute name labels and attribute value labels. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="capabilityLocalization"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CapabilityLocalization</returns>
        System.Threading.Tasks.Task<CapabilityLocalization> CreateCapabilityLocalizationAsync(string authorization, string capabilityId, int capabilityVersion, CapabilityLocalization capabilityLocalization, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create a Capability Localization for a Locale
        /// </summary>
        /// <remarks>
        /// Add translated values in a desired language (i.e. locale) for a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be stored. Translated values can include: capability name labels, attribute name labels and attribute value labels. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="capabilityLocalization"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CapabilityLocalization)</returns>
        System.Threading.Tasks.Task<ApiResponse<CapabilityLocalization>> CreateCapabilityLocalizationWithHttpInfoAsync(string authorization, string capabilityId, int capabilityVersion, CapabilityLocalization capabilityLocalization, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete a Capability by ID and Version
        /// </summary>
        /// <remarks>
        /// Delete a Capability with a given ID and version.  The Capability must be in &#x60;proposed&#x60; status. Once a Capability is updated to &#x60;live&#x60; status, it cannot be deleted and must be transitioned to &#x60;deprecated&#x60;. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Object</returns>
        System.Threading.Tasks.Task<Object> DeleteCapabilityAsync(string authorization, string capabilityId, int capabilityVersion, string xSTOrganization = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete a Capability by ID and Version
        /// </summary>
        /// <remarks>
        /// Delete a Capability with a given ID and version.  The Capability must be in &#x60;proposed&#x60; status. Once a Capability is updated to &#x60;live&#x60; status, it cannot be deleted and must be transitioned to &#x60;deprecated&#x60;. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Object)</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteCapabilityWithHttpInfoAsync(string authorization, string capabilityId, int capabilityVersion, string xSTOrganization = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get a Capability by ID and Version
        /// </summary>
        /// <remarks>
        /// Get a Capability with a given ID and version.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Capability</returns>
        System.Threading.Tasks.Task<Capability> GetCapabilityAsync(string authorization, string capabilityId, int capabilityVersion, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get a Capability by ID and Version
        /// </summary>
        /// <remarks>
        /// Get a Capability with a given ID and version.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Capability)</returns>
        System.Threading.Tasks.Task<ApiResponse<Capability>> GetCapabilityWithHttpInfoAsync(string authorization, string capabilityId, int capabilityVersion, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get a Capability Localization by Locale
        /// </summary>
        /// <remarks>
        /// Retrieve translated values in a desired language (i.e. locale) for a Capability. A &#x60;locale&#x60; is the abbreviated, internationalization code for the requested language. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CapabilityLocalization</returns>
        System.Threading.Tasks.Task<CapabilityLocalization> GetCapabilityLocalizationAsync(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, string manufacturerName = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get a Capability Localization by Locale
        /// </summary>
        /// <remarks>
        /// Retrieve translated values in a desired language (i.e. locale) for a Capability. A &#x60;locale&#x60; is the abbreviated, internationalization code for the requested language. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CapabilityLocalization)</returns>
        System.Threading.Tasks.Task<ApiResponse<CapabilityLocalization>> GetCapabilityLocalizationWithHttpInfoAsync(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, string manufacturerName = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List all Capabilities
        /// </summary>
        /// <remarks>
        /// List all standard Capabilities.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PagedCapabilities</returns>
        System.Threading.Tasks.Task<PagedCapabilities> ListCapabilitiesAsync(string authorization, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List all Capabilities
        /// </summary>
        /// <remarks>
        /// List all standard Capabilities.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PagedCapabilities)</returns>
        System.Threading.Tasks.Task<ApiResponse<PagedCapabilities>> ListCapabilitiesWithHttpInfoAsync(string authorization, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List a Capability&#39;s Localizations
        /// </summary>
        /// <remarks>
        /// List all languages (i.e. locales) for which a Capability has translated values. A &#x60;locale&#x60; is the abbreviated, internationalization code for the associated language. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CapabilityLocaleLocalizations</returns>
        System.Threading.Tasks.Task<CapabilityLocaleLocalizations> ListCapabilityLocalizationsAsync(string authorization, string capabilityId, int capabilityVersion, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List a Capability&#39;s Localizations
        /// </summary>
        /// <remarks>
        /// List all languages (i.e. locales) for which a Capability has translated values. A &#x60;locale&#x60; is the abbreviated, internationalization code for the associated language. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CapabilityLocaleLocalizations)</returns>
        System.Threading.Tasks.Task<ApiResponse<CapabilityLocaleLocalizations>> ListCapabilityLocalizationsWithHttpInfoAsync(string authorization, string capabilityId, int capabilityVersion, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List a Capability&#39;s Versions
        /// </summary>
        /// <remarks>
        /// List a Capability&#39;s versions.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PagedCapabilities</returns>
        System.Threading.Tasks.Task<PagedCapabilities> ListCapabilityVersionsAsync(string authorization, string capabilityId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List a Capability&#39;s Versions
        /// </summary>
        /// <remarks>
        /// List a Capability&#39;s versions.
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PagedCapabilities)</returns>
        System.Threading.Tasks.Task<ApiResponse<PagedCapabilities>> ListCapabilityVersionsWithHttpInfoAsync(string authorization, string capabilityId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Capabilities by Namespace
        /// </summary>
        /// <remarks>
        /// List Capabilities by namespace.  Namespaces are used to organize a user&#39;s Capabilities and provide a way to uniquely identify them. A user can retrieve all of the Capabilities under their assigned namespace by referencing it.  The namespace is recognizable as the first part of a capabilityId in the format &#x60;namespace.capabilityName&#x60;. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="_namespace">The namespace of the Capability</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PagedCapabilities</returns>
        System.Threading.Tasks.Task<PagedCapabilities> ListNamespacedCapabilitiesAsync(string authorization, string _namespace, string xSTOrganization = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Capabilities by Namespace
        /// </summary>
        /// <remarks>
        /// List Capabilities by namespace.  Namespaces are used to organize a user&#39;s Capabilities and provide a way to uniquely identify them. A user can retrieve all of the Capabilities under their assigned namespace by referencing it.  The namespace is recognizable as the first part of a capabilityId in the format &#x60;namespace.capabilityName&#x60;. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="_namespace">The namespace of the Capability</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PagedCapabilities)</returns>
        System.Threading.Tasks.Task<ApiResponse<PagedCapabilities>> ListNamespacedCapabilitiesWithHttpInfoAsync(string authorization, string _namespace, string xSTOrganization = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Patch a Capability Localization by Locale
        /// </summary>
        /// <remarks>
        /// Modify specific values of existing language (i.e. locale) translations for a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be modified.&lt;br /&gt;&lt;br /&gt; All fields in the request body are optional. Any fields populated will replace existing localization values. Any fields left blank will fall back to existing values. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="capabilityLocalization"></param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CapabilityLocalization</returns>
        System.Threading.Tasks.Task<CapabilityLocalization> PatchCapabilityLocalizationAsync(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, CapabilityLocalization capabilityLocalization, string manufacturerName = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Patch a Capability Localization by Locale
        /// </summary>
        /// <remarks>
        /// Modify specific values of existing language (i.e. locale) translations for a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be modified.&lt;br /&gt;&lt;br /&gt; All fields in the request body are optional. Any fields populated will replace existing localization values. Any fields left blank will fall back to existing values. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="capabilityLocalization"></param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CapabilityLocalization)</returns>
        System.Threading.Tasks.Task<ApiResponse<CapabilityLocalization>> PatchCapabilityLocalizationWithHttpInfoAsync(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, CapabilityLocalization capabilityLocalization, string manufacturerName = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update a Capability
        /// </summary>
        /// <remarks>
        /// Update a Capability.  Capabilities with a &#x60;proposed&#x60; status can be updated at will.  Capabilities with a &#x60;live&#x60;, &#x60;deprecated&#x60;, or &#x60;dead&#x60; status are immutable and can&#39;t be updated.  Capability names cannot be changed. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="capabilityRequest"></param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Capability</returns>
        System.Threading.Tasks.Task<Capability> UpdateCapabilityAsync(string authorization, string capabilityId, int capabilityVersion, UpdateCapabilityRequest capabilityRequest, string xSTOrganization = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update a Capability
        /// </summary>
        /// <remarks>
        /// Update a Capability.  Capabilities with a &#x60;proposed&#x60; status can be updated at will.  Capabilities with a &#x60;live&#x60;, &#x60;deprecated&#x60;, or &#x60;dead&#x60; status are immutable and can&#39;t be updated.  Capability names cannot be changed. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="capabilityRequest"></param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Capability)</returns>
        System.Threading.Tasks.Task<ApiResponse<Capability>> UpdateCapabilityWithHttpInfoAsync(string authorization, string capabilityId, int capabilityVersion, UpdateCapabilityRequest capabilityRequest, string xSTOrganization = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update a Capability Localization by Locale
        /// </summary>
        /// <remarks>
        /// Update existing translated values for a given language (i.e. locale) associated with a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be updated.&lt;br /&gt;&lt;br /&gt; If the &#x60;tag&#x60; argument is provided in the request body, it will be ignored. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="capabilityLocalization"></param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CapabilityLocalization</returns>
        System.Threading.Tasks.Task<CapabilityLocalization> UpdateCapabilityLocalizationAsync(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, CapabilityLocalization capabilityLocalization, string manufacturerName = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update a Capability Localization by Locale
        /// </summary>
        /// <remarks>
        /// Update existing translated values for a given language (i.e. locale) associated with a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be updated.&lt;br /&gt;&lt;br /&gt; If the &#x60;tag&#x60; argument is provided in the request body, it will be ignored. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="capabilityLocalization"></param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CapabilityLocalization)</returns>
        System.Threading.Tasks.Task<ApiResponse<CapabilityLocalization>> UpdateCapabilityLocalizationWithHttpInfoAsync(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, CapabilityLocalization capabilityLocalization, string manufacturerName = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface ICapabilitiesApi : ICapabilitiesApiSync, ICapabilitiesApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class CapabilitiesApi : ICapabilitiesApi
    {
        private SmartThingsNet.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CapabilitiesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public CapabilitiesApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CapabilitiesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public CapabilitiesApi(string basePath)
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
        /// Initializes a new instance of the <see cref="CapabilitiesApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public CapabilitiesApi(SmartThingsNet.Client.Configuration configuration)
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
        /// Initializes a new instance of the <see cref="CapabilitiesApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public CapabilitiesApi(SmartThingsNet.Client.ISynchronousClient client, SmartThingsNet.Client.IAsynchronousClient asyncClient, SmartThingsNet.Client.IReadableConfiguration configuration)
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
        /// Create a Capability Create a Capability.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityRequest"></param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="_namespace">The namespace of the Capability (optional)</param>
        /// <returns>Capability</returns>
        public Capability CreateCapability(string authorization, CreateCapabilityRequest capabilityRequest, string xSTOrganization = default(string), string _namespace = default(string))
        {
            SmartThingsNet.Client.ApiResponse<Capability> localVarResponse = CreateCapabilityWithHttpInfo(authorization, capabilityRequest, xSTOrganization, _namespace);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create a Capability Create a Capability.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityRequest"></param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="_namespace">The namespace of the Capability (optional)</param>
        /// <returns>ApiResponse of Capability</returns>
        public SmartThingsNet.Client.ApiResponse<Capability> CreateCapabilityWithHttpInfo(string authorization, CreateCapabilityRequest capabilityRequest, string xSTOrganization = default(string), string _namespace = default(string))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->CreateCapability");
            }

            // verify the required parameter 'capabilityRequest' is set
            if (capabilityRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityRequest' when calling CapabilitiesApi->CreateCapability");
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

            if (_namespace != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "namespace", _namespace));
            }
            
            if (xSTOrganization != null)
            {
                localVarRequestOptions.HeaderParameters.Add("X-ST-Organization", SmartThingsNet.Client.ClientUtils.ParameterToString(xSTOrganization)); // header parameter
            }
            localVarRequestOptions.Data = capabilityRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Capability>("/capabilities", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateCapability", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create a Capability Create a Capability.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityRequest"></param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="_namespace">The namespace of the Capability (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Capability</returns>
        public async System.Threading.Tasks.Task<Capability> CreateCapabilityAsync(string authorization, CreateCapabilityRequest capabilityRequest, string xSTOrganization = default(string), string _namespace = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<Capability> localVarResponse = await CreateCapabilityWithHttpInfoAsync(authorization, capabilityRequest, xSTOrganization, _namespace, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create a Capability Create a Capability.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityRequest"></param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="_namespace">The namespace of the Capability (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Capability)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Capability>> CreateCapabilityWithHttpInfoAsync(string authorization, CreateCapabilityRequest capabilityRequest, string xSTOrganization = default(string), string _namespace = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->CreateCapability");
            }

            // verify the required parameter 'capabilityRequest' is set
            if (capabilityRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityRequest' when calling CapabilitiesApi->CreateCapability");
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

            if (_namespace != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "namespace", _namespace));
            }
            
            if (xSTOrganization != null)
            {
                localVarRequestOptions.HeaderParameters.Add("X-ST-Organization", SmartThingsNet.Client.ClientUtils.ParameterToString(xSTOrganization)); // header parameter
            }
            localVarRequestOptions.Data = capabilityRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Capability>("/capabilities", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateCapability", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create a Capability Localization for a Locale Add translated values in a desired language (i.e. locale) for a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be stored. Translated values can include: capability name labels, attribute name labels and attribute value labels. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="capabilityLocalization"></param>
        /// <returns>CapabilityLocalization</returns>
        public CapabilityLocalization CreateCapabilityLocalization(string authorization, string capabilityId, int capabilityVersion, CapabilityLocalization capabilityLocalization)
        {
            SmartThingsNet.Client.ApiResponse<CapabilityLocalization> localVarResponse = CreateCapabilityLocalizationWithHttpInfo(authorization, capabilityId, capabilityVersion, capabilityLocalization);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create a Capability Localization for a Locale Add translated values in a desired language (i.e. locale) for a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be stored. Translated values can include: capability name labels, attribute name labels and attribute value labels. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="capabilityLocalization"></param>
        /// <returns>ApiResponse of CapabilityLocalization</returns>
        public SmartThingsNet.Client.ApiResponse<CapabilityLocalization> CreateCapabilityLocalizationWithHttpInfo(string authorization, string capabilityId, int capabilityVersion, CapabilityLocalization capabilityLocalization)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->CreateCapabilityLocalization");
            }

            // verify the required parameter 'capabilityId' is set
            if (capabilityId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityId' when calling CapabilitiesApi->CreateCapabilityLocalization");
            }

            // verify the required parameter 'capabilityLocalization' is set
            if (capabilityLocalization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityLocalization' when calling CapabilitiesApi->CreateCapabilityLocalization");
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

            localVarRequestOptions.PathParameters.Add("capabilityId", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityId)); // path parameter
            localVarRequestOptions.PathParameters.Add("capabilityVersion", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityVersion)); // path parameter
            
            localVarRequestOptions.Data = capabilityLocalization;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<CapabilityLocalization>("/capabilities/{capabilityId}/{capabilityVersion}/i18n", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateCapabilityLocalization", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create a Capability Localization for a Locale Add translated values in a desired language (i.e. locale) for a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be stored. Translated values can include: capability name labels, attribute name labels and attribute value labels. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="capabilityLocalization"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CapabilityLocalization</returns>
        public async System.Threading.Tasks.Task<CapabilityLocalization> CreateCapabilityLocalizationAsync(string authorization, string capabilityId, int capabilityVersion, CapabilityLocalization capabilityLocalization, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<CapabilityLocalization> localVarResponse = await CreateCapabilityLocalizationWithHttpInfoAsync(authorization, capabilityId, capabilityVersion, capabilityLocalization, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create a Capability Localization for a Locale Add translated values in a desired language (i.e. locale) for a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be stored. Translated values can include: capability name labels, attribute name labels and attribute value labels. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="capabilityLocalization"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CapabilityLocalization)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<CapabilityLocalization>> CreateCapabilityLocalizationWithHttpInfoAsync(string authorization, string capabilityId, int capabilityVersion, CapabilityLocalization capabilityLocalization, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->CreateCapabilityLocalization");
            }

            // verify the required parameter 'capabilityId' is set
            if (capabilityId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityId' when calling CapabilitiesApi->CreateCapabilityLocalization");
            }

            // verify the required parameter 'capabilityLocalization' is set
            if (capabilityLocalization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityLocalization' when calling CapabilitiesApi->CreateCapabilityLocalization");
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

            localVarRequestOptions.PathParameters.Add("capabilityId", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityId)); // path parameter
            localVarRequestOptions.PathParameters.Add("capabilityVersion", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityVersion)); // path parameter
            
            localVarRequestOptions.Data = capabilityLocalization;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<CapabilityLocalization>("/capabilities/{capabilityId}/{capabilityVersion}/i18n", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateCapabilityLocalization", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete a Capability by ID and Version Delete a Capability with a given ID and version.  The Capability must be in &#x60;proposed&#x60; status. Once a Capability is updated to &#x60;live&#x60; status, it cannot be deleted and must be transitioned to &#x60;deprecated&#x60;. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <returns>Object</returns>
        public Object DeleteCapability(string authorization, string capabilityId, int capabilityVersion, string xSTOrganization = default(string))
        {
            SmartThingsNet.Client.ApiResponse<Object> localVarResponse = DeleteCapabilityWithHttpInfo(authorization, capabilityId, capabilityVersion, xSTOrganization);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Delete a Capability by ID and Version Delete a Capability with a given ID and version.  The Capability must be in &#x60;proposed&#x60; status. Once a Capability is updated to &#x60;live&#x60; status, it cannot be deleted and must be transitioned to &#x60;deprecated&#x60;. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <returns>ApiResponse of Object</returns>
        public SmartThingsNet.Client.ApiResponse<Object> DeleteCapabilityWithHttpInfo(string authorization, string capabilityId, int capabilityVersion, string xSTOrganization = default(string))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->DeleteCapability");
            }

            // verify the required parameter 'capabilityId' is set
            if (capabilityId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityId' when calling CapabilitiesApi->DeleteCapability");
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

            localVarRequestOptions.PathParameters.Add("capabilityId", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityId)); // path parameter
            localVarRequestOptions.PathParameters.Add("capabilityVersion", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityVersion)); // path parameter
            
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
            var localVarResponse = this.Client.Delete<Object>("/capabilities/{capabilityId}/{capabilityVersion}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteCapability", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete a Capability by ID and Version Delete a Capability with a given ID and version.  The Capability must be in &#x60;proposed&#x60; status. Once a Capability is updated to &#x60;live&#x60; status, it cannot be deleted and must be transitioned to &#x60;deprecated&#x60;. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Object</returns>
        public async System.Threading.Tasks.Task<Object> DeleteCapabilityAsync(string authorization, string capabilityId, int capabilityVersion, string xSTOrganization = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<Object> localVarResponse = await DeleteCapabilityWithHttpInfoAsync(authorization, capabilityId, capabilityVersion, xSTOrganization, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Delete a Capability by ID and Version Delete a Capability with a given ID and version.  The Capability must be in &#x60;proposed&#x60; status. Once a Capability is updated to &#x60;live&#x60; status, it cannot be deleted and must be transitioned to &#x60;deprecated&#x60;. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Object)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Object>> DeleteCapabilityWithHttpInfoAsync(string authorization, string capabilityId, int capabilityVersion, string xSTOrganization = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->DeleteCapability");
            }

            // verify the required parameter 'capabilityId' is set
            if (capabilityId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityId' when calling CapabilitiesApi->DeleteCapability");
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

            localVarRequestOptions.PathParameters.Add("capabilityId", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityId)); // path parameter
            localVarRequestOptions.PathParameters.Add("capabilityVersion", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityVersion)); // path parameter
            
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
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/capabilities/{capabilityId}/{capabilityVersion}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteCapability", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get a Capability by ID and Version Get a Capability with a given ID and version.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <returns>Capability</returns>
        public Capability GetCapability(string authorization, string capabilityId, int capabilityVersion)
        {
            SmartThingsNet.Client.ApiResponse<Capability> localVarResponse = GetCapabilityWithHttpInfo(authorization, capabilityId, capabilityVersion);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get a Capability by ID and Version Get a Capability with a given ID and version.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <returns>ApiResponse of Capability</returns>
        public SmartThingsNet.Client.ApiResponse<Capability> GetCapabilityWithHttpInfo(string authorization, string capabilityId, int capabilityVersion)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->GetCapability");
            }

            // verify the required parameter 'capabilityId' is set
            if (capabilityId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityId' when calling CapabilitiesApi->GetCapability");
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

            localVarRequestOptions.PathParameters.Add("capabilityId", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityId)); // path parameter
            localVarRequestOptions.PathParameters.Add("capabilityVersion", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityVersion)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<Capability>("/capabilities/{capabilityId}/{capabilityVersion}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetCapability", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get a Capability by ID and Version Get a Capability with a given ID and version.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Capability</returns>
        public async System.Threading.Tasks.Task<Capability> GetCapabilityAsync(string authorization, string capabilityId, int capabilityVersion, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<Capability> localVarResponse = await GetCapabilityWithHttpInfoAsync(authorization, capabilityId, capabilityVersion, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get a Capability by ID and Version Get a Capability with a given ID and version.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Capability)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Capability>> GetCapabilityWithHttpInfoAsync(string authorization, string capabilityId, int capabilityVersion, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->GetCapability");
            }

            // verify the required parameter 'capabilityId' is set
            if (capabilityId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityId' when calling CapabilitiesApi->GetCapability");
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

            localVarRequestOptions.PathParameters.Add("capabilityId", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityId)); // path parameter
            localVarRequestOptions.PathParameters.Add("capabilityVersion", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityVersion)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<Capability>("/capabilities/{capabilityId}/{capabilityVersion}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetCapability", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get a Capability Localization by Locale Retrieve translated values in a desired language (i.e. locale) for a Capability. A &#x60;locale&#x60; is the abbreviated, internationalization code for the requested language. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <returns>CapabilityLocalization</returns>
        public CapabilityLocalization GetCapabilityLocalization(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, string manufacturerName = default(string))
        {
            SmartThingsNet.Client.ApiResponse<CapabilityLocalization> localVarResponse = GetCapabilityLocalizationWithHttpInfo(authorization, capabilityId, capabilityVersion, locale, presentationId, manufacturerName);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get a Capability Localization by Locale Retrieve translated values in a desired language (i.e. locale) for a Capability. A &#x60;locale&#x60; is the abbreviated, internationalization code for the requested language. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <returns>ApiResponse of CapabilityLocalization</returns>
        public SmartThingsNet.Client.ApiResponse<CapabilityLocalization> GetCapabilityLocalizationWithHttpInfo(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, string manufacturerName = default(string))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->GetCapabilityLocalization");
            }

            // verify the required parameter 'capabilityId' is set
            if (capabilityId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityId' when calling CapabilitiesApi->GetCapabilityLocalization");
            }

            // verify the required parameter 'locale' is set
            if (locale == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'locale' when calling CapabilitiesApi->GetCapabilityLocalization");
            }

            // verify the required parameter 'presentationId' is set
            if (presentationId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'presentationId' when calling CapabilitiesApi->GetCapabilityLocalization");
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

            localVarRequestOptions.PathParameters.Add("capabilityId", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityId)); // path parameter
            localVarRequestOptions.PathParameters.Add("capabilityVersion", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityVersion)); // path parameter
            localVarRequestOptions.PathParameters.Add("locale", SmartThingsNet.Client.ClientUtils.ParameterToString(locale)); // path parameter
            if (manufacturerName != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "manufacturerName", manufacturerName));
            }
            localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "presentationId", presentationId));
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<CapabilityLocalization>("/capabilities/{capabilityId}/{capabilityVersion}/i18n/{locale}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetCapabilityLocalization", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get a Capability Localization by Locale Retrieve translated values in a desired language (i.e. locale) for a Capability. A &#x60;locale&#x60; is the abbreviated, internationalization code for the requested language. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CapabilityLocalization</returns>
        public async System.Threading.Tasks.Task<CapabilityLocalization> GetCapabilityLocalizationAsync(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, string manufacturerName = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<CapabilityLocalization> localVarResponse = await GetCapabilityLocalizationWithHttpInfoAsync(authorization, capabilityId, capabilityVersion, locale, presentationId, manufacturerName, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get a Capability Localization by Locale Retrieve translated values in a desired language (i.e. locale) for a Capability. A &#x60;locale&#x60; is the abbreviated, internationalization code for the requested language. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CapabilityLocalization)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<CapabilityLocalization>> GetCapabilityLocalizationWithHttpInfoAsync(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, string manufacturerName = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->GetCapabilityLocalization");
            }

            // verify the required parameter 'capabilityId' is set
            if (capabilityId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityId' when calling CapabilitiesApi->GetCapabilityLocalization");
            }

            // verify the required parameter 'locale' is set
            if (locale == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'locale' when calling CapabilitiesApi->GetCapabilityLocalization");
            }

            // verify the required parameter 'presentationId' is set
            if (presentationId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'presentationId' when calling CapabilitiesApi->GetCapabilityLocalization");
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

            localVarRequestOptions.PathParameters.Add("capabilityId", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityId)); // path parameter
            localVarRequestOptions.PathParameters.Add("capabilityVersion", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityVersion)); // path parameter
            localVarRequestOptions.PathParameters.Add("locale", SmartThingsNet.Client.ClientUtils.ParameterToString(locale)); // path parameter
            if (manufacturerName != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "manufacturerName", manufacturerName));
            }
            localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "presentationId", presentationId));
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<CapabilityLocalization>("/capabilities/{capabilityId}/{capabilityVersion}/i18n/{locale}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetCapabilityLocalization", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List all Capabilities List all standard Capabilities.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <returns>PagedCapabilities</returns>
        public PagedCapabilities ListCapabilities(string authorization)
        {
            SmartThingsNet.Client.ApiResponse<PagedCapabilities> localVarResponse = ListCapabilitiesWithHttpInfo(authorization);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List all Capabilities List all standard Capabilities.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <returns>ApiResponse of PagedCapabilities</returns>
        public SmartThingsNet.Client.ApiResponse<PagedCapabilities> ListCapabilitiesWithHttpInfo(string authorization)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->ListCapabilities");
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

            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<PagedCapabilities>("/capabilities", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListCapabilities", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List all Capabilities List all standard Capabilities.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PagedCapabilities</returns>
        public async System.Threading.Tasks.Task<PagedCapabilities> ListCapabilitiesAsync(string authorization, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<PagedCapabilities> localVarResponse = await ListCapabilitiesWithHttpInfoAsync(authorization, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List all Capabilities List all standard Capabilities.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PagedCapabilities)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<PagedCapabilities>> ListCapabilitiesWithHttpInfoAsync(string authorization, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->ListCapabilities");
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

            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<PagedCapabilities>("/capabilities", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListCapabilities", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List a Capability&#39;s Localizations List all languages (i.e. locales) for which a Capability has translated values. A &#x60;locale&#x60; is the abbreviated, internationalization code for the associated language. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <returns>CapabilityLocaleLocalizations</returns>
        public CapabilityLocaleLocalizations ListCapabilityLocalizations(string authorization, string capabilityId, int capabilityVersion)
        {
            SmartThingsNet.Client.ApiResponse<CapabilityLocaleLocalizations> localVarResponse = ListCapabilityLocalizationsWithHttpInfo(authorization, capabilityId, capabilityVersion);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List a Capability&#39;s Localizations List all languages (i.e. locales) for which a Capability has translated values. A &#x60;locale&#x60; is the abbreviated, internationalization code for the associated language. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <returns>ApiResponse of CapabilityLocaleLocalizations</returns>
        public SmartThingsNet.Client.ApiResponse<CapabilityLocaleLocalizations> ListCapabilityLocalizationsWithHttpInfo(string authorization, string capabilityId, int capabilityVersion)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->ListCapabilityLocalizations");
            }

            // verify the required parameter 'capabilityId' is set
            if (capabilityId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityId' when calling CapabilitiesApi->ListCapabilityLocalizations");
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

            localVarRequestOptions.PathParameters.Add("capabilityId", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityId)); // path parameter
            localVarRequestOptions.PathParameters.Add("capabilityVersion", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityVersion)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<CapabilityLocaleLocalizations>("/capabilities/{capabilityId}/{capabilityVersion}/i18n", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListCapabilityLocalizations", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List a Capability&#39;s Localizations List all languages (i.e. locales) for which a Capability has translated values. A &#x60;locale&#x60; is the abbreviated, internationalization code for the associated language. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CapabilityLocaleLocalizations</returns>
        public async System.Threading.Tasks.Task<CapabilityLocaleLocalizations> ListCapabilityLocalizationsAsync(string authorization, string capabilityId, int capabilityVersion, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<CapabilityLocaleLocalizations> localVarResponse = await ListCapabilityLocalizationsWithHttpInfoAsync(authorization, capabilityId, capabilityVersion, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List a Capability&#39;s Localizations List all languages (i.e. locales) for which a Capability has translated values. A &#x60;locale&#x60; is the abbreviated, internationalization code for the associated language. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CapabilityLocaleLocalizations)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<CapabilityLocaleLocalizations>> ListCapabilityLocalizationsWithHttpInfoAsync(string authorization, string capabilityId, int capabilityVersion, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->ListCapabilityLocalizations");
            }

            // verify the required parameter 'capabilityId' is set
            if (capabilityId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityId' when calling CapabilitiesApi->ListCapabilityLocalizations");
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

            localVarRequestOptions.PathParameters.Add("capabilityId", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityId)); // path parameter
            localVarRequestOptions.PathParameters.Add("capabilityVersion", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityVersion)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<CapabilityLocaleLocalizations>("/capabilities/{capabilityId}/{capabilityVersion}/i18n", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListCapabilityLocalizations", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List a Capability&#39;s Versions List a Capability&#39;s versions.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <returns>PagedCapabilities</returns>
        public PagedCapabilities ListCapabilityVersions(string authorization, string capabilityId)
        {
            SmartThingsNet.Client.ApiResponse<PagedCapabilities> localVarResponse = ListCapabilityVersionsWithHttpInfo(authorization, capabilityId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List a Capability&#39;s Versions List a Capability&#39;s versions.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <returns>ApiResponse of PagedCapabilities</returns>
        public SmartThingsNet.Client.ApiResponse<PagedCapabilities> ListCapabilityVersionsWithHttpInfo(string authorization, string capabilityId)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->ListCapabilityVersions");
            }

            // verify the required parameter 'capabilityId' is set
            if (capabilityId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityId' when calling CapabilitiesApi->ListCapabilityVersions");
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

            localVarRequestOptions.PathParameters.Add("capabilityId", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<PagedCapabilities>("/capabilities/{capabilityId}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListCapabilityVersions", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List a Capability&#39;s Versions List a Capability&#39;s versions.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PagedCapabilities</returns>
        public async System.Threading.Tasks.Task<PagedCapabilities> ListCapabilityVersionsAsync(string authorization, string capabilityId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<PagedCapabilities> localVarResponse = await ListCapabilityVersionsWithHttpInfoAsync(authorization, capabilityId, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List a Capability&#39;s Versions List a Capability&#39;s versions.
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PagedCapabilities)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<PagedCapabilities>> ListCapabilityVersionsWithHttpInfoAsync(string authorization, string capabilityId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->ListCapabilityVersions");
            }

            // verify the required parameter 'capabilityId' is set
            if (capabilityId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityId' when calling CapabilitiesApi->ListCapabilityVersions");
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

            localVarRequestOptions.PathParameters.Add("capabilityId", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityId)); // path parameter
            

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<PagedCapabilities>("/capabilities/{capabilityId}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListCapabilityVersions", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Capabilities by Namespace List Capabilities by namespace.  Namespaces are used to organize a user&#39;s Capabilities and provide a way to uniquely identify them. A user can retrieve all of the Capabilities under their assigned namespace by referencing it.  The namespace is recognizable as the first part of a capabilityId in the format &#x60;namespace.capabilityName&#x60;. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="_namespace">The namespace of the Capability</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <returns>PagedCapabilities</returns>
        public PagedCapabilities ListNamespacedCapabilities(string authorization, string _namespace, string xSTOrganization = default(string))
        {
            SmartThingsNet.Client.ApiResponse<PagedCapabilities> localVarResponse = ListNamespacedCapabilitiesWithHttpInfo(authorization, _namespace, xSTOrganization);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Capabilities by Namespace List Capabilities by namespace.  Namespaces are used to organize a user&#39;s Capabilities and provide a way to uniquely identify them. A user can retrieve all of the Capabilities under their assigned namespace by referencing it.  The namespace is recognizable as the first part of a capabilityId in the format &#x60;namespace.capabilityName&#x60;. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="_namespace">The namespace of the Capability</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <returns>ApiResponse of PagedCapabilities</returns>
        public SmartThingsNet.Client.ApiResponse<PagedCapabilities> ListNamespacedCapabilitiesWithHttpInfo(string authorization, string _namespace, string xSTOrganization = default(string))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->ListNamespacedCapabilities");
            }

            // verify the required parameter '_namespace' is set
            if (_namespace == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter '_namespace' when calling CapabilitiesApi->ListNamespacedCapabilities");
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

            localVarRequestOptions.PathParameters.Add("namespace", SmartThingsNet.Client.ClientUtils.ParameterToString(_namespace)); // path parameter
            
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
            var localVarResponse = this.Client.Get<PagedCapabilities>("/capabilities/namespaces/{namespace}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListNamespacedCapabilities", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Capabilities by Namespace List Capabilities by namespace.  Namespaces are used to organize a user&#39;s Capabilities and provide a way to uniquely identify them. A user can retrieve all of the Capabilities under their assigned namespace by referencing it.  The namespace is recognizable as the first part of a capabilityId in the format &#x60;namespace.capabilityName&#x60;. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="_namespace">The namespace of the Capability</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PagedCapabilities</returns>
        public async System.Threading.Tasks.Task<PagedCapabilities> ListNamespacedCapabilitiesAsync(string authorization, string _namespace, string xSTOrganization = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<PagedCapabilities> localVarResponse = await ListNamespacedCapabilitiesWithHttpInfoAsync(authorization, _namespace, xSTOrganization, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Capabilities by Namespace List Capabilities by namespace.  Namespaces are used to organize a user&#39;s Capabilities and provide a way to uniquely identify them. A user can retrieve all of the Capabilities under their assigned namespace by referencing it.  The namespace is recognizable as the first part of a capabilityId in the format &#x60;namespace.capabilityName&#x60;. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="_namespace">The namespace of the Capability</param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PagedCapabilities)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<PagedCapabilities>> ListNamespacedCapabilitiesWithHttpInfoAsync(string authorization, string _namespace, string xSTOrganization = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->ListNamespacedCapabilities");
            }

            // verify the required parameter '_namespace' is set
            if (_namespace == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter '_namespace' when calling CapabilitiesApi->ListNamespacedCapabilities");
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

            localVarRequestOptions.PathParameters.Add("namespace", SmartThingsNet.Client.ClientUtils.ParameterToString(_namespace)); // path parameter
            
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
            var localVarResponse = await this.AsynchronousClient.GetAsync<PagedCapabilities>("/capabilities/namespaces/{namespace}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListNamespacedCapabilities", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Patch a Capability Localization by Locale Modify specific values of existing language (i.e. locale) translations for a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be modified.&lt;br /&gt;&lt;br /&gt; All fields in the request body are optional. Any fields populated will replace existing localization values. Any fields left blank will fall back to existing values. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="capabilityLocalization"></param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <returns>CapabilityLocalization</returns>
        public CapabilityLocalization PatchCapabilityLocalization(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, CapabilityLocalization capabilityLocalization, string manufacturerName = default(string))
        {
            SmartThingsNet.Client.ApiResponse<CapabilityLocalization> localVarResponse = PatchCapabilityLocalizationWithHttpInfo(authorization, capabilityId, capabilityVersion, locale, presentationId, capabilityLocalization, manufacturerName);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Patch a Capability Localization by Locale Modify specific values of existing language (i.e. locale) translations for a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be modified.&lt;br /&gt;&lt;br /&gt; All fields in the request body are optional. Any fields populated will replace existing localization values. Any fields left blank will fall back to existing values. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="capabilityLocalization"></param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <returns>ApiResponse of CapabilityLocalization</returns>
        public SmartThingsNet.Client.ApiResponse<CapabilityLocalization> PatchCapabilityLocalizationWithHttpInfo(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, CapabilityLocalization capabilityLocalization, string manufacturerName = default(string))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->PatchCapabilityLocalization");
            }

            // verify the required parameter 'capabilityId' is set
            if (capabilityId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityId' when calling CapabilitiesApi->PatchCapabilityLocalization");
            }

            // verify the required parameter 'locale' is set
            if (locale == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'locale' when calling CapabilitiesApi->PatchCapabilityLocalization");
            }

            // verify the required parameter 'presentationId' is set
            if (presentationId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'presentationId' when calling CapabilitiesApi->PatchCapabilityLocalization");
            }

            // verify the required parameter 'capabilityLocalization' is set
            if (capabilityLocalization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityLocalization' when calling CapabilitiesApi->PatchCapabilityLocalization");
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

            localVarRequestOptions.PathParameters.Add("capabilityId", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityId)); // path parameter
            localVarRequestOptions.PathParameters.Add("capabilityVersion", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityVersion)); // path parameter
            localVarRequestOptions.PathParameters.Add("locale", SmartThingsNet.Client.ClientUtils.ParameterToString(locale)); // path parameter
            if (manufacturerName != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "manufacturerName", manufacturerName));
            }
            localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "presentationId", presentationId));
            
            localVarRequestOptions.Data = capabilityLocalization;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Patch<CapabilityLocalization>("/capabilities/{capabilityId}/{capabilityVersion}/i18n/{locale}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PatchCapabilityLocalization", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Patch a Capability Localization by Locale Modify specific values of existing language (i.e. locale) translations for a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be modified.&lt;br /&gt;&lt;br /&gt; All fields in the request body are optional. Any fields populated will replace existing localization values. Any fields left blank will fall back to existing values. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="capabilityLocalization"></param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CapabilityLocalization</returns>
        public async System.Threading.Tasks.Task<CapabilityLocalization> PatchCapabilityLocalizationAsync(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, CapabilityLocalization capabilityLocalization, string manufacturerName = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<CapabilityLocalization> localVarResponse = await PatchCapabilityLocalizationWithHttpInfoAsync(authorization, capabilityId, capabilityVersion, locale, presentationId, capabilityLocalization, manufacturerName, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Patch a Capability Localization by Locale Modify specific values of existing language (i.e. locale) translations for a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be modified.&lt;br /&gt;&lt;br /&gt; All fields in the request body are optional. Any fields populated will replace existing localization values. Any fields left blank will fall back to existing values. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="capabilityLocalization"></param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CapabilityLocalization)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<CapabilityLocalization>> PatchCapabilityLocalizationWithHttpInfoAsync(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, CapabilityLocalization capabilityLocalization, string manufacturerName = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->PatchCapabilityLocalization");
            }

            // verify the required parameter 'capabilityId' is set
            if (capabilityId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityId' when calling CapabilitiesApi->PatchCapabilityLocalization");
            }

            // verify the required parameter 'locale' is set
            if (locale == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'locale' when calling CapabilitiesApi->PatchCapabilityLocalization");
            }

            // verify the required parameter 'presentationId' is set
            if (presentationId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'presentationId' when calling CapabilitiesApi->PatchCapabilityLocalization");
            }

            // verify the required parameter 'capabilityLocalization' is set
            if (capabilityLocalization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityLocalization' when calling CapabilitiesApi->PatchCapabilityLocalization");
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

            localVarRequestOptions.PathParameters.Add("capabilityId", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityId)); // path parameter
            localVarRequestOptions.PathParameters.Add("capabilityVersion", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityVersion)); // path parameter
            localVarRequestOptions.PathParameters.Add("locale", SmartThingsNet.Client.ClientUtils.ParameterToString(locale)); // path parameter
            if (manufacturerName != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "manufacturerName", manufacturerName));
            }
            localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "presentationId", presentationId));
            
            localVarRequestOptions.Data = capabilityLocalization;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PatchAsync<CapabilityLocalization>("/capabilities/{capabilityId}/{capabilityVersion}/i18n/{locale}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PatchCapabilityLocalization", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update a Capability Update a Capability.  Capabilities with a &#x60;proposed&#x60; status can be updated at will.  Capabilities with a &#x60;live&#x60;, &#x60;deprecated&#x60;, or &#x60;dead&#x60; status are immutable and can&#39;t be updated.  Capability names cannot be changed. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="capabilityRequest"></param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <returns>Capability</returns>
        public Capability UpdateCapability(string authorization, string capabilityId, int capabilityVersion, UpdateCapabilityRequest capabilityRequest, string xSTOrganization = default(string))
        {
            SmartThingsNet.Client.ApiResponse<Capability> localVarResponse = UpdateCapabilityWithHttpInfo(authorization, capabilityId, capabilityVersion, capabilityRequest, xSTOrganization);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update a Capability Update a Capability.  Capabilities with a &#x60;proposed&#x60; status can be updated at will.  Capabilities with a &#x60;live&#x60;, &#x60;deprecated&#x60;, or &#x60;dead&#x60; status are immutable and can&#39;t be updated.  Capability names cannot be changed. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="capabilityRequest"></param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <returns>ApiResponse of Capability</returns>
        public SmartThingsNet.Client.ApiResponse<Capability> UpdateCapabilityWithHttpInfo(string authorization, string capabilityId, int capabilityVersion, UpdateCapabilityRequest capabilityRequest, string xSTOrganization = default(string))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->UpdateCapability");
            }

            // verify the required parameter 'capabilityId' is set
            if (capabilityId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityId' when calling CapabilitiesApi->UpdateCapability");
            }

            // verify the required parameter 'capabilityRequest' is set
            if (capabilityRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityRequest' when calling CapabilitiesApi->UpdateCapability");
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

            localVarRequestOptions.PathParameters.Add("capabilityId", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityId)); // path parameter
            localVarRequestOptions.PathParameters.Add("capabilityVersion", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityVersion)); // path parameter
            
            if (xSTOrganization != null)
            {
                localVarRequestOptions.HeaderParameters.Add("X-ST-Organization", SmartThingsNet.Client.ClientUtils.ParameterToString(xSTOrganization)); // header parameter
            }
            localVarRequestOptions.Data = capabilityRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<Capability>("/capabilities/{capabilityId}/{capabilityVersion}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateCapability", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update a Capability Update a Capability.  Capabilities with a &#x60;proposed&#x60; status can be updated at will.  Capabilities with a &#x60;live&#x60;, &#x60;deprecated&#x60;, or &#x60;dead&#x60; status are immutable and can&#39;t be updated.  Capability names cannot be changed. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="capabilityRequest"></param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Capability</returns>
        public async System.Threading.Tasks.Task<Capability> UpdateCapabilityAsync(string authorization, string capabilityId, int capabilityVersion, UpdateCapabilityRequest capabilityRequest, string xSTOrganization = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<Capability> localVarResponse = await UpdateCapabilityWithHttpInfoAsync(authorization, capabilityId, capabilityVersion, capabilityRequest, xSTOrganization, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update a Capability Update a Capability.  Capabilities with a &#x60;proposed&#x60; status can be updated at will.  Capabilities with a &#x60;live&#x60;, &#x60;deprecated&#x60;, or &#x60;dead&#x60; status are immutable and can&#39;t be updated.  Capability names cannot be changed. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="capabilityRequest"></param>
        /// <param name="xSTOrganization">Organization id. If not provided, default user organization will be used. (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Capability)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Capability>> UpdateCapabilityWithHttpInfoAsync(string authorization, string capabilityId, int capabilityVersion, UpdateCapabilityRequest capabilityRequest, string xSTOrganization = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->UpdateCapability");
            }

            // verify the required parameter 'capabilityId' is set
            if (capabilityId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityId' when calling CapabilitiesApi->UpdateCapability");
            }

            // verify the required parameter 'capabilityRequest' is set
            if (capabilityRequest == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityRequest' when calling CapabilitiesApi->UpdateCapability");
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

            localVarRequestOptions.PathParameters.Add("capabilityId", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityId)); // path parameter
            localVarRequestOptions.PathParameters.Add("capabilityVersion", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityVersion)); // path parameter
            
            if (xSTOrganization != null)
            {
                localVarRequestOptions.HeaderParameters.Add("X-ST-Organization", SmartThingsNet.Client.ClientUtils.ParameterToString(xSTOrganization)); // header parameter
            }
            localVarRequestOptions.Data = capabilityRequest;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<Capability>("/capabilities/{capabilityId}/{capabilityVersion}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateCapability", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update a Capability Localization by Locale Update existing translated values for a given language (i.e. locale) associated with a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be updated.&lt;br /&gt;&lt;br /&gt; If the &#x60;tag&#x60; argument is provided in the request body, it will be ignored. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="capabilityLocalization"></param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <returns>CapabilityLocalization</returns>
        public CapabilityLocalization UpdateCapabilityLocalization(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, CapabilityLocalization capabilityLocalization, string manufacturerName = default(string))
        {
            SmartThingsNet.Client.ApiResponse<CapabilityLocalization> localVarResponse = UpdateCapabilityLocalizationWithHttpInfo(authorization, capabilityId, capabilityVersion, locale, presentationId, capabilityLocalization, manufacturerName);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update a Capability Localization by Locale Update existing translated values for a given language (i.e. locale) associated with a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be updated.&lt;br /&gt;&lt;br /&gt; If the &#x60;tag&#x60; argument is provided in the request body, it will be ignored. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="capabilityLocalization"></param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <returns>ApiResponse of CapabilityLocalization</returns>
        public SmartThingsNet.Client.ApiResponse<CapabilityLocalization> UpdateCapabilityLocalizationWithHttpInfo(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, CapabilityLocalization capabilityLocalization, string manufacturerName = default(string))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->UpdateCapabilityLocalization");
            }

            // verify the required parameter 'capabilityId' is set
            if (capabilityId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityId' when calling CapabilitiesApi->UpdateCapabilityLocalization");
            }

            // verify the required parameter 'locale' is set
            if (locale == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'locale' when calling CapabilitiesApi->UpdateCapabilityLocalization");
            }

            // verify the required parameter 'presentationId' is set
            if (presentationId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'presentationId' when calling CapabilitiesApi->UpdateCapabilityLocalization");
            }

            // verify the required parameter 'capabilityLocalization' is set
            if (capabilityLocalization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityLocalization' when calling CapabilitiesApi->UpdateCapabilityLocalization");
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

            localVarRequestOptions.PathParameters.Add("capabilityId", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityId)); // path parameter
            localVarRequestOptions.PathParameters.Add("capabilityVersion", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityVersion)); // path parameter
            localVarRequestOptions.PathParameters.Add("locale", SmartThingsNet.Client.ClientUtils.ParameterToString(locale)); // path parameter
            if (manufacturerName != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "manufacturerName", manufacturerName));
            }
            localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "presentationId", presentationId));
            
            localVarRequestOptions.Data = capabilityLocalization;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<CapabilityLocalization>("/capabilities/{capabilityId}/{capabilityVersion}/i18n/{locale}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateCapabilityLocalization", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update a Capability Localization by Locale Update existing translated values for a given language (i.e. locale) associated with a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be updated.&lt;br /&gt;&lt;br /&gt; If the &#x60;tag&#x60; argument is provided in the request body, it will be ignored. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="capabilityLocalization"></param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CapabilityLocalization</returns>
        public async System.Threading.Tasks.Task<CapabilityLocalization> UpdateCapabilityLocalizationAsync(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, CapabilityLocalization capabilityLocalization, string manufacturerName = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            SmartThingsNet.Client.ApiResponse<CapabilityLocalization> localVarResponse = await UpdateCapabilityLocalizationWithHttpInfoAsync(authorization, capabilityId, capabilityVersion, locale, presentationId, capabilityLocalization, manufacturerName, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update a Capability Localization by Locale Update existing translated values for a given language (i.e. locale) associated with a Capability. The &#x60;locale&#x60; is the abbreviated, internationalization code associated with the language for which values will be updated.&lt;br /&gt;&lt;br /&gt; If the &#x60;tag&#x60; argument is provided in the request body, it will be ignored. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">OAuth token</param>
        /// <param name="capabilityId">The ID of the capability.</param>
        /// <param name="capabilityVersion">The version of the Capability</param>
        /// <param name="locale">The tag of the locale as defined in [RFC bcp47](http://www.rfc-editor.org/rfc/bcp/bcp47.txt).</param>
        /// <param name="presentationId">System generated identifier that corresponds to a device presentation (formerly &#x60;vid&#x60;)</param>
        /// <param name="capabilityLocalization"></param>
        /// <param name="manufacturerName">Secondary namespacing key for grouping presentations (formerly &#x60;mnmn&#x60;) (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CapabilityLocalization)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<CapabilityLocalization>> UpdateCapabilityLocalizationWithHttpInfoAsync(string authorization, string capabilityId, int capabilityVersion, string locale, string presentationId, CapabilityLocalization capabilityLocalization, string manufacturerName = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'authorization' when calling CapabilitiesApi->UpdateCapabilityLocalization");
            }

            // verify the required parameter 'capabilityId' is set
            if (capabilityId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityId' when calling CapabilitiesApi->UpdateCapabilityLocalization");
            }

            // verify the required parameter 'locale' is set
            if (locale == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'locale' when calling CapabilitiesApi->UpdateCapabilityLocalization");
            }

            // verify the required parameter 'presentationId' is set
            if (presentationId == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'presentationId' when calling CapabilitiesApi->UpdateCapabilityLocalization");
            }

            // verify the required parameter 'capabilityLocalization' is set
            if (capabilityLocalization == null)
            {
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'capabilityLocalization' when calling CapabilitiesApi->UpdateCapabilityLocalization");
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

            localVarRequestOptions.PathParameters.Add("capabilityId", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityId)); // path parameter
            localVarRequestOptions.PathParameters.Add("capabilityVersion", SmartThingsNet.Client.ClientUtils.ParameterToString(capabilityVersion)); // path parameter
            localVarRequestOptions.PathParameters.Add("locale", SmartThingsNet.Client.ClientUtils.ParameterToString(locale)); // path parameter
            if (manufacturerName != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "manufacturerName", manufacturerName));
            }
            localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "presentationId", presentationId));
            
            localVarRequestOptions.Data = capabilityLocalization;

            // authentication (Bearer) required
            // oauth required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<CapabilityLocalization>("/capabilities/{capabilityId}/{capabilityVersion}/i18n/{locale}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateCapabilityLocalization", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

    }
}
