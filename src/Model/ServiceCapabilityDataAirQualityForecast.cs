/*
 * SmartThings API
 *
 * # Overview  This is the reference documentation for the SmartThings API.  The SmartThings API, a RESTful API, provides a method for your integration to communicate with the SmartThings Platform. The API is the core of the platform. It is used to control devices, create Automations, manage Locations, retrieve user and device information; if you want to communicate with the SmartThings platform, you’ll be using the SmartThings API. All responses are sent as [JSON](http://www.json.org/).  The SmartThings API consists of several endpoints, including Rules, Locations, Devices, and more. Even though each of these endpoints are not standalone APIs, you may hear them referred to as such. For example, the Rules API is used to build Automations.  # Authentication  Before using the SmartThings API, you’ll need to obtain an Authorization Token. All SmartThings resources are protected with [OAuth 2.0 Bearer Tokens](https://tools.ietf.org/html/rfc6750#section-2.1) sent on the request as an `Authorization: Bearer <TOKEN>` header. Operations require specific OAuth scopes that specify the exact permissions authorized by the user.  ## Authorization token types  There are two types of tokens:   * SmartApp tokens   * Personal access tokens (PAT).  ### SmartApp tokens  SmartApp tokens are used to communicate between third-party integrations, or SmartApps, and the SmartThings API. When a SmartApp is called by the SmartThings platform, it is sent an authorization token that can be used to interact with the SmartThings API.  ### Personal access tokens  Personal access tokens are used to authorize interaction with the API for non-SmartApp use cases. When creating personal access tokens, you can specifiy the permissions granted to the token. These permissions define the OAuth2 scopes for the personal access token.  Personal access tokesn can be created and managed on the [personal access tokens page](https://account.smartthings.com/tokens).  ## OAuth2 scopes  Operations may be protected by one or more OAuth security schemes, which specify the required permissions. Each scope for a given scheme is required. If multiple schemes are specified (uncommon), you may use either scheme.  SmartApp token scopes are derived from the permissions requested by the SmartApp and granted by the end-user during installation. Personal access token scopes are associated with the specific permissions authorized when the token is created.  Scopes are generally in the form `permission:entity-type:entity-id`.  **An `*` used for the `entity-id` specifies that the permission may be applied to all entities that the token type has access to, or may be replaced with a specific ID.**  For more information about authrization and permissions, visit the [Authorization section](https://developer-preview.smartthings.com/docs/advanced/authorization-and-permissions) in the SmartThings documentation.  <!- - ReDoc-Inject: <security-definitions> - ->  # Errors  The SmartThings API uses conventional HTTP response codes to indicate the success or failure of a request.  In general:  * A `2XX` response code indicates success * A `4XX` response code indicates an error given the inputs for the request * A `5XX` response code indicates a failure on the SmartThings platform  API errors will contain a JSON response body with more information about the error:  ```json {   \"requestId\": \"031fec1a-f19f-470a-a7da-710569082846\"   \"error\": {     \"code\": \"ConstraintViolationError\",     \"message\": \"Validation errors occurred while process your request.\",     \"details\": [       { \"code\": \"PatternError\", \"target\": \"latitude\", \"message\": \"Invalid format.\" },       { \"code\": \"SizeError\", \"target\": \"name\", \"message\": \"Too small.\" },       { \"code\": \"SizeError\", \"target\": \"description\", \"message\": \"Too big.\" }     ]   } } ```  ## Error Response Body  The error response attributes are:  | Property | Type | Required | Description | | - -- | - -- | - -- | - -- | | requestId | String | No | A request identifier that can be used to correlate an error to additional logging on the SmartThings servers. | error | Error | **Yes** | The Error object, documented below.  ## Error Object  The Error object contains the following attributes:  | Property | Type | Required | Description | | - -- | - -- | - -- | - -- | | code | String | **Yes** | A SmartThings-defined error code that serves as a more specific indicator of the error than the HTTP error code specified in the response. See [SmartThings Error Codes](#section/Errors/SmartThings-Error-Codes) for more information. | message | String | **Yes** | A description of the error, intended to aid debugging of error responses. | target | String | No | The target of the error. For example, this could be the name of the property that caused the error. | details | Error[] | No | An array of Error objects that typically represent distinct, related errors that occurred during the request. As an optional attribute, this may be null or an empty array.  ## Standard HTTP Error Codes  The following table lists the most common HTTP error responses:  | Code | Name | Description | | - -- | - -- | - -- | | 400 | Bad Request | The client has issued an invalid request. This is commonly used to specify validation errors in a request payload. | 401 | Unauthorized | Authorization for the API is required, but the request has not been authenticated. | 403 | Forbidden | The request has been authenticated but does not have appropriate permissions, or a requested resource is not found. | 404 | Not Found | The requested path does not exist. | 406 | Not Acceptable | The client has requested a MIME type via the Accept header for a value not supported by the server. | 415 | Unsupported Media Type | The client has defined a contentType header that is not supported by the server. | 422 | Unprocessable Entity | The client has made a valid request, but the server cannot process it. This is often used for APIs for which certain limits have been exceeded. | 429 | Too Many Requests | The client has exceeded the number of requests allowed for a given time window. | 500 | Internal Server Error | An unexpected error on the SmartThings servers has occurred. These errors are generally rare. | 501 | Not Implemented | The client request was valid and understood by the server, but the requested feature has yet to be implemented. These errors are generally rare.  ## SmartThings Error Codes  SmartThings specifies several standard custom error codes. These provide more information than the standard HTTP error response codes. The following table lists the standard SmartThings error codes and their descriptions:  | Code | Typical HTTP Status Codes | Description | | - -- | - -- | - -- | | PatternError | 400, 422 | The client has provided input that does not match the expected pattern. | ConstraintViolationError | 422 | The client has provided input that has violated one or more constraints. | NotNullError | 422 | The client has provided a null input for a field that is required to be non-null. | NullError | 422 | The client has provided an input for a field that is required to be null. | NotEmptyError | 422 | The client has provided an empty input for a field that is required to be non-empty. | SizeError | 400, 422 | The client has provided a value that does not meet size restrictions. | Unexpected Error | 500 | A non-recoverable error condition has occurred. A problem occurred on the SmartThings server that is no fault of the client. | UnprocessableEntityError | 422 | The client has sent a malformed request body. | TooManyRequestError | 429 | The client issued too many requests too quickly. | LimitError | 422 | The client has exceeded certain limits an API enforces. | UnsupportedOperationError | 400, 422 | The client has issued a request to a feature that currently isn't supported by the SmartThings platform. These errors are generally rare.  ## Custom Error Codes  An API may define its own error codes where appropriate. Custom error codes are documented in each API endpoint's documentation section.  # Warnings The SmartThings API issues warning messages via standard HTTP Warning headers. These messages do not represent a request failure, but provide additional information that the requester might want to act upon. For example, a warning will be issued if you are using an old API version.  # API Versions  The SmartThings API supports both path and header-based versioning. The following are equivalent:  - https://api.smartthings.com/v1/locations - https://api.smartthings.com/locations with header `Accept: application/vnd.smartthings+json;v=1`  Currently, only version 1 is available.  # Paging  Operations that return a list of objects return a paginated response. The `_links` object contains the items returned, and links to the next and previous result page, if applicable.  ```json {   \"items\": [     {       \"locationId\": \"6b3d1909-1e1c-43ec-adc2-5f941de4fbf9\",       \"name\": \"Home\"     },     {       \"locationId\": \"6b3d1909-1e1c-43ec-adc2-5f94d6g4fbf9\",       \"name\": \"Work\"     }     ....   ],   \"_links\": {     \"next\": {       \"href\": \"https://api.smartthings.com/v1/locations?page=3\"     },     \"previous\": {       \"href\": \"https://api.smartthings.com/v1/locations?page=1\"     }   } } ```  # Allowed Permissions  The response payload to a request for a SmartThings entity (e.g. a location or a device) may contain an `allowed` list property.  This list contains strings called **action identifiers** (such as `w:locations`) that provide information about what actions are permitted by the user's token on that entity at the time the request was processed.  The action identifiers are defined in the API documentation for the 200 response of the particular endpoint queried. For each documented action identifier (e.g. `w:locations`), you will find a description of the user action (e.g. \"edit the name of the location\") associated with that identifier. The endpoint documentation will contain a complete list of the action identifiers that may appear in the allowed list property.  If the `allowed` list property is present in the response payload: * the user action documented for an action identifier present in the list is permitted for the user on the entity at the time the request is processed * the user action documented for an action identifier **not** present in the list is **not** permitted to the user on the entity at the time the request is processed  If the `allowed` list property is **not** present or has a `null` value in the response payload, then the response provides **no information** about any user actions being permissible except that the user has permission to view the returned entity.  The response provides **no information** about the permissibility of user actions that are not specifically mentioned in the documentation for the particular endpoint.  The table below is a high-level guide to interpreting action identifiers.  It does not indicate that any given endpoint will document or return any of the action identifiers listed below. Remember that the endpoint API documentation is the final source of truth for interpreting action identifiers in a response payload.  | Action Identifier Format | Examples | Meaning | | - -- | - -- | - -- | | `w:grant:`\\<grant type> | `w:grant:share` on a location payload | User may **bestow** the grant type on the entity to another user (e.g. through an invitation.) | | `r:`\\<child type> | `r:devices` on a location payload | User may **list and view** child type entities of the returned entity.  NOTE: there may be finer-grained controls on the child type entities. | | `l:`\\<child type> | `l:devices` on a location payload | User may **list and summarize** child type entities of the returned entity.  NOTE: there may be finer-grained controls on the child type entities.  This is weaker than `r:`\\<child type> and rarely used. | | `w:`\\<child type> | `w:devices` on a location payload | User may **create** entities of the child type as children of the returned entity. | | `x:`\\<child type> | `x:devices` on a location payload | User may **execute commands** on child type entities of the returned entity.  NOTE: there may be finer-grained controls on the child type entities. | | `r:`\\<entity type> | `r:locations` on a location payload | This will only be returned in a list/summary response and only in a case when the list/summary is designed not to show all the details of the entity. | | `w:`\\<entity type> | `w:locations` on a location payload<br/>`w:devices` on a device payload | User may **edit** specificly-documented properties of the returned entity. | | `x:`\\<entity type> | `x:devices` on a device payload | user may **execute commands** on the returned entity.  NOTE: there may be finer-grained controls on the children of the entity. | | `d:`\\<entity type> | `d:locations` on a location payload | User may **delete** the returned entity. | | `r:`\\<entity type>`:`\\<attribute group> | `r:locations:currentMode` on a location payload | User may **view** the specified (and clearly-documented) attribute or attribute group of the returned entity. | | `w:`\\<entity type>`:`\\<attribute group> | `w:locations:geo` on a location payload | User may **edit** the specified (and clearly-documented) attribute or attribute group of the returned entity. | | `x:`\\<entity type>`:`\\<attribute group> | `x:devices:switch` on a device payload | User may **execute commands** on the specified (and clearly-documented) attribute or attribute group of the returned entity. |  # Localization  Some SmartThings APIs support localization. Specific information regarding localization endpoints are documented in the API itself. However, the following applies to all endpoints that support localization.  ## Fallback Patterns  When making a request with the `Accept-Language` header, the following fallback pattern is observed: 1. Response will be translated with exact locale tag. 2. If a translation does not exist for the requested language and region, the translation for the language will be returned. 3. If a translation does not exist for the language, English (en) will be returned. 4. Finally, an untranslated response will be returned in the absense of the above translations.  ## Accept-Language Header The format of the `Accept-Language` header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5)  ## Content-Language The `Content-Language` header should be set on the response from the server to indicate which translation was given back to the client. The absense of the header indicates that the server did not recieve a request with the `Accept-Language` header set. 
 *
 * The version of the OpenAPI document: 1.0-PREVIEW
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = SmartThingsNet.Client.OpenAPIDateConverter;

namespace SmartThingsNet.Model
{
    /// <summary>
    /// ServiceCapabilityDataAirQualityForecast
    /// </summary>
    [DataContract(Name = "ServiceCapabilityData_airQualityForecast")]
    public partial class ServiceCapabilityDataAirQualityForecast : IEquatable<ServiceCapabilityDataAirQualityForecast>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceCapabilityDataAirQualityForecast" /> class.
        /// </summary>
        /// <param name="lastUpdateTime">lastUpdateTime.</param>
        /// <param name="vendor">vendor.</param>
        /// <param name="version">version.</param>
        /// <param name="pm10Index1Hour">pm10Index1Hour.</param>
        /// <param name="pm10Index2Hour">pm10Index2Hour.</param>
        /// <param name="pm10Index3Hour">pm10Index3Hour.</param>
        /// <param name="pm10Index4Hour">pm10Index4Hour.</param>
        /// <param name="pm10Index5Hour">pm10Index5Hour.</param>
        /// <param name="pm10Index6Hour">pm10Index6Hour.</param>
        /// <param name="pm10Index7Hour">pm10Index7Hour.</param>
        /// <param name="pm10Index8Hour">pm10Index8Hour.</param>
        /// <param name="pm10Index9Hour">pm10Index9Hour.</param>
        /// <param name="pm10Index10Hour">pm10Index10Hour.</param>
        /// <param name="pm10Index11Hour">pm10Index11Hour.</param>
        /// <param name="pm10Index12Hour">pm10Index12Hour.</param>
        /// <param name="pm10Amount1Hour">pm10Amount1Hour.</param>
        /// <param name="pm10Amount2Hour">pm10Amount2Hour.</param>
        /// <param name="pm10Amount3Hour">pm10Amount3Hour.</param>
        /// <param name="pm10Amount4Hour">pm10Amount4Hour.</param>
        /// <param name="pm10Amount5Hour">pm10Amount5Hour.</param>
        /// <param name="pm10Amount6Hour">pm10Amount6Hour.</param>
        /// <param name="pm10Amount7Hour">pm10Amount7Hour.</param>
        /// <param name="pm10Amount8Hour">pm10Amount8Hour.</param>
        /// <param name="pm10Amount9Hour">pm10Amount9Hour.</param>
        /// <param name="pm10Amount10Hour">pm10Amount10Hour.</param>
        /// <param name="pm10Amount11Hour">pm10Amount11Hour.</param>
        /// <param name="pm10Amount12Hour">pm10Amount12Hour.</param>
        /// <param name="pm25Index1Hour">pm25Index1Hour.</param>
        /// <param name="pm25Index2Hour">pm25Index2Hour.</param>
        /// <param name="pm25Index3Hour">pm25Index3Hour.</param>
        /// <param name="pm25Index4Hour">pm25Index4Hour.</param>
        /// <param name="pm25Index5Hour">pm25Index5Hour.</param>
        /// <param name="pm25Index6Hour">pm25Index6Hour.</param>
        /// <param name="pm25Index7Hour">pm25Index7Hour.</param>
        /// <param name="pm25Index8Hour">pm25Index8Hour.</param>
        /// <param name="pm25Index9Hour">pm25Index9Hour.</param>
        /// <param name="pm25Index10Hour">pm25Index10Hour.</param>
        /// <param name="pm25Index11Hour">pm25Index11Hour.</param>
        /// <param name="pm25Index12Hour">pm25Index12Hour.</param>
        /// <param name="pm25Amount1Hour">pm25Amount1Hour.</param>
        /// <param name="pm25Amount2Hour">pm25Amount2Hour.</param>
        /// <param name="pm25Amount3Hour">pm25Amount3Hour.</param>
        /// <param name="pm25Amount4Hour">pm25Amount4Hour.</param>
        /// <param name="pm25Amount5Hour">pm25Amount5Hour.</param>
        /// <param name="pm25Amount6Hour">pm25Amount6Hour.</param>
        /// <param name="pm25Amount7Hour">pm25Amount7Hour.</param>
        /// <param name="pm25Amount8Hour">pm25Amount8Hour.</param>
        /// <param name="pm25Amount9Hour">pm25Amount9Hour.</param>
        /// <param name="pm25Amount10Hour">pm25Amount10Hour.</param>
        /// <param name="pm25Amount11Hour">pm25Amount11Hour.</param>
        /// <param name="pm25Amount12Hour">pm25Amount12Hour.</param>
        public ServiceCapabilityDataAirQualityForecast(ServiceCapabilityDataLastUpdateTime lastUpdateTime = default(ServiceCapabilityDataLastUpdateTime), ServiceCapabilityDataAirQualityVendor vendor = default(ServiceCapabilityDataAirQualityVendor), ServiceCapabilityDataAirQualityVersion version = default(ServiceCapabilityDataAirQualityVersion), ServiceCapabilityDataAirQualityForecastPm10Index1Hour pm10Index1Hour = default(ServiceCapabilityDataAirQualityForecastPm10Index1Hour), ServiceCapabilityDataAirQualityForecastPm10Index2Hour pm10Index2Hour = default(ServiceCapabilityDataAirQualityForecastPm10Index2Hour), ServiceCapabilityDataAirQualityForecastPm10Index3Hour pm10Index3Hour = default(ServiceCapabilityDataAirQualityForecastPm10Index3Hour), ServiceCapabilityDataAirQualityForecastPm10Index4Hour pm10Index4Hour = default(ServiceCapabilityDataAirQualityForecastPm10Index4Hour), ServiceCapabilityDataAirQualityForecastPm10Index5Hour pm10Index5Hour = default(ServiceCapabilityDataAirQualityForecastPm10Index5Hour), ServiceCapabilityDataAirQualityForecastPm10Index6Hour pm10Index6Hour = default(ServiceCapabilityDataAirQualityForecastPm10Index6Hour), ServiceCapabilityDataAirQualityForecastPm10Index7Hour pm10Index7Hour = default(ServiceCapabilityDataAirQualityForecastPm10Index7Hour), ServiceCapabilityDataAirQualityForecastPm10Index8Hour pm10Index8Hour = default(ServiceCapabilityDataAirQualityForecastPm10Index8Hour), ServiceCapabilityDataAirQualityForecastPm10Index9Hour pm10Index9Hour = default(ServiceCapabilityDataAirQualityForecastPm10Index9Hour), ServiceCapabilityDataAirQualityForecastPm10Index10Hour pm10Index10Hour = default(ServiceCapabilityDataAirQualityForecastPm10Index10Hour), ServiceCapabilityDataAirQualityForecastPm10Index11Hour pm10Index11Hour = default(ServiceCapabilityDataAirQualityForecastPm10Index11Hour), ServiceCapabilityDataAirQualityForecastPm10Index12Hour pm10Index12Hour = default(ServiceCapabilityDataAirQualityForecastPm10Index12Hour), ServiceCapabilityDataAirQualityForecastPm10Amount1Hour pm10Amount1Hour = default(ServiceCapabilityDataAirQualityForecastPm10Amount1Hour), ServiceCapabilityDataAirQualityForecastPm10Amount2Hour pm10Amount2Hour = default(ServiceCapabilityDataAirQualityForecastPm10Amount2Hour), ServiceCapabilityDataAirQualityForecastPm10Amount3Hour pm10Amount3Hour = default(ServiceCapabilityDataAirQualityForecastPm10Amount3Hour), ServiceCapabilityDataAirQualityForecastPm10Amount4Hour pm10Amount4Hour = default(ServiceCapabilityDataAirQualityForecastPm10Amount4Hour), ServiceCapabilityDataAirQualityForecastPm10Amount5Hour pm10Amount5Hour = default(ServiceCapabilityDataAirQualityForecastPm10Amount5Hour), ServiceCapabilityDataAirQualityForecastPm10Amount6Hour pm10Amount6Hour = default(ServiceCapabilityDataAirQualityForecastPm10Amount6Hour), ServiceCapabilityDataAirQualityForecastPm10Amount7Hour pm10Amount7Hour = default(ServiceCapabilityDataAirQualityForecastPm10Amount7Hour), ServiceCapabilityDataAirQualityForecastPm10Amount8Hour pm10Amount8Hour = default(ServiceCapabilityDataAirQualityForecastPm10Amount8Hour), ServiceCapabilityDataAirQualityForecastPm10Amount9Hour pm10Amount9Hour = default(ServiceCapabilityDataAirQualityForecastPm10Amount9Hour), ServiceCapabilityDataAirQualityForecastPm10Amount10Hour pm10Amount10Hour = default(ServiceCapabilityDataAirQualityForecastPm10Amount10Hour), ServiceCapabilityDataAirQualityForecastPm10Amount11Hour pm10Amount11Hour = default(ServiceCapabilityDataAirQualityForecastPm10Amount11Hour), ServiceCapabilityDataAirQualityForecastPm10Amount12Hour pm10Amount12Hour = default(ServiceCapabilityDataAirQualityForecastPm10Amount12Hour), ServiceCapabilityDataAirQualityForecastPm25Index1Hour pm25Index1Hour = default(ServiceCapabilityDataAirQualityForecastPm25Index1Hour), ServiceCapabilityDataAirQualityForecastPm25Index2Hour pm25Index2Hour = default(ServiceCapabilityDataAirQualityForecastPm25Index2Hour), ServiceCapabilityDataAirQualityForecastPm25Index3Hour pm25Index3Hour = default(ServiceCapabilityDataAirQualityForecastPm25Index3Hour), ServiceCapabilityDataAirQualityForecastPm25Index4Hour pm25Index4Hour = default(ServiceCapabilityDataAirQualityForecastPm25Index4Hour), ServiceCapabilityDataAirQualityForecastPm25Index5Hour pm25Index5Hour = default(ServiceCapabilityDataAirQualityForecastPm25Index5Hour), ServiceCapabilityDataAirQualityForecastPm25Index6Hour pm25Index6Hour = default(ServiceCapabilityDataAirQualityForecastPm25Index6Hour), ServiceCapabilityDataAirQualityForecastPm25Index7Hour pm25Index7Hour = default(ServiceCapabilityDataAirQualityForecastPm25Index7Hour), ServiceCapabilityDataAirQualityForecastPm25Index8Hour pm25Index8Hour = default(ServiceCapabilityDataAirQualityForecastPm25Index8Hour), ServiceCapabilityDataAirQualityForecastPm25Index9Hour pm25Index9Hour = default(ServiceCapabilityDataAirQualityForecastPm25Index9Hour), ServiceCapabilityDataAirQualityForecastPm25Index10Hour pm25Index10Hour = default(ServiceCapabilityDataAirQualityForecastPm25Index10Hour), ServiceCapabilityDataAirQualityForecastPm25Index11Hour pm25Index11Hour = default(ServiceCapabilityDataAirQualityForecastPm25Index11Hour), ServiceCapabilityDataAirQualityForecastPm25Index12Hour pm25Index12Hour = default(ServiceCapabilityDataAirQualityForecastPm25Index12Hour), ServiceCapabilityDataAirQualityForecastPm25Amount1Hour pm25Amount1Hour = default(ServiceCapabilityDataAirQualityForecastPm25Amount1Hour), ServiceCapabilityDataAirQualityForecastPm25Amount2Hour pm25Amount2Hour = default(ServiceCapabilityDataAirQualityForecastPm25Amount2Hour), ServiceCapabilityDataAirQualityForecastPm25Amount3Hour pm25Amount3Hour = default(ServiceCapabilityDataAirQualityForecastPm25Amount3Hour), ServiceCapabilityDataAirQualityForecastPm25Amount4Hour pm25Amount4Hour = default(ServiceCapabilityDataAirQualityForecastPm25Amount4Hour), ServiceCapabilityDataAirQualityForecastPm25Amount5Hour pm25Amount5Hour = default(ServiceCapabilityDataAirQualityForecastPm25Amount5Hour), ServiceCapabilityDataAirQualityForecastPm25Amount6Hour pm25Amount6Hour = default(ServiceCapabilityDataAirQualityForecastPm25Amount6Hour), ServiceCapabilityDataAirQualityForecastPm25Amount7Hour pm25Amount7Hour = default(ServiceCapabilityDataAirQualityForecastPm25Amount7Hour), ServiceCapabilityDataAirQualityForecastPm25Amount8Hour pm25Amount8Hour = default(ServiceCapabilityDataAirQualityForecastPm25Amount8Hour), ServiceCapabilityDataAirQualityForecastPm25Amount9Hour pm25Amount9Hour = default(ServiceCapabilityDataAirQualityForecastPm25Amount9Hour), ServiceCapabilityDataAirQualityForecastPm25Amount10Hour pm25Amount10Hour = default(ServiceCapabilityDataAirQualityForecastPm25Amount10Hour), ServiceCapabilityDataAirQualityForecastPm25Amount11Hour pm25Amount11Hour = default(ServiceCapabilityDataAirQualityForecastPm25Amount11Hour), ServiceCapabilityDataAirQualityForecastPm25Amount12Hour pm25Amount12Hour = default(ServiceCapabilityDataAirQualityForecastPm25Amount12Hour))
        {
            this.LastUpdateTime = lastUpdateTime;
            this.Vendor = vendor;
            this._Version = version;
            this.Pm10Index1Hour = pm10Index1Hour;
            this.Pm10Index2Hour = pm10Index2Hour;
            this.Pm10Index3Hour = pm10Index3Hour;
            this.Pm10Index4Hour = pm10Index4Hour;
            this.Pm10Index5Hour = pm10Index5Hour;
            this.Pm10Index6Hour = pm10Index6Hour;
            this.Pm10Index7Hour = pm10Index7Hour;
            this.Pm10Index8Hour = pm10Index8Hour;
            this.Pm10Index9Hour = pm10Index9Hour;
            this.Pm10Index10Hour = pm10Index10Hour;
            this.Pm10Index11Hour = pm10Index11Hour;
            this.Pm10Index12Hour = pm10Index12Hour;
            this.Pm10Amount1Hour = pm10Amount1Hour;
            this.Pm10Amount2Hour = pm10Amount2Hour;
            this.Pm10Amount3Hour = pm10Amount3Hour;
            this.Pm10Amount4Hour = pm10Amount4Hour;
            this.Pm10Amount5Hour = pm10Amount5Hour;
            this.Pm10Amount6Hour = pm10Amount6Hour;
            this.Pm10Amount7Hour = pm10Amount7Hour;
            this.Pm10Amount8Hour = pm10Amount8Hour;
            this.Pm10Amount9Hour = pm10Amount9Hour;
            this.Pm10Amount10Hour = pm10Amount10Hour;
            this.Pm10Amount11Hour = pm10Amount11Hour;
            this.Pm10Amount12Hour = pm10Amount12Hour;
            this.Pm25Index1Hour = pm25Index1Hour;
            this.Pm25Index2Hour = pm25Index2Hour;
            this.Pm25Index3Hour = pm25Index3Hour;
            this.Pm25Index4Hour = pm25Index4Hour;
            this.Pm25Index5Hour = pm25Index5Hour;
            this.Pm25Index6Hour = pm25Index6Hour;
            this.Pm25Index7Hour = pm25Index7Hour;
            this.Pm25Index8Hour = pm25Index8Hour;
            this.Pm25Index9Hour = pm25Index9Hour;
            this.Pm25Index10Hour = pm25Index10Hour;
            this.Pm25Index11Hour = pm25Index11Hour;
            this.Pm25Index12Hour = pm25Index12Hour;
            this.Pm25Amount1Hour = pm25Amount1Hour;
            this.Pm25Amount2Hour = pm25Amount2Hour;
            this.Pm25Amount3Hour = pm25Amount3Hour;
            this.Pm25Amount4Hour = pm25Amount4Hour;
            this.Pm25Amount5Hour = pm25Amount5Hour;
            this.Pm25Amount6Hour = pm25Amount6Hour;
            this.Pm25Amount7Hour = pm25Amount7Hour;
            this.Pm25Amount8Hour = pm25Amount8Hour;
            this.Pm25Amount9Hour = pm25Amount9Hour;
            this.Pm25Amount10Hour = pm25Amount10Hour;
            this.Pm25Amount11Hour = pm25Amount11Hour;
            this.Pm25Amount12Hour = pm25Amount12Hour;
        }

        /// <summary>
        /// Gets or Sets LastUpdateTime
        /// </summary>
        [DataMember(Name = "lastUpdateTime", EmitDefaultValue = false)]
        public ServiceCapabilityDataLastUpdateTime LastUpdateTime { get; set; }

        /// <summary>
        /// Gets or Sets Vendor
        /// </summary>
        [DataMember(Name = "vendor", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityVendor Vendor { get; set; }

        /// <summary>
        /// Gets or Sets _Version
        /// </summary>
        [DataMember(Name = "version", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityVersion _Version { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Index1Hour
        /// </summary>
        [DataMember(Name = "pm10Index1Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Index1Hour Pm10Index1Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Index2Hour
        /// </summary>
        [DataMember(Name = "pm10Index2Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Index2Hour Pm10Index2Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Index3Hour
        /// </summary>
        [DataMember(Name = "pm10Index3Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Index3Hour Pm10Index3Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Index4Hour
        /// </summary>
        [DataMember(Name = "pm10Index4Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Index4Hour Pm10Index4Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Index5Hour
        /// </summary>
        [DataMember(Name = "pm10Index5Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Index5Hour Pm10Index5Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Index6Hour
        /// </summary>
        [DataMember(Name = "pm10Index6Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Index6Hour Pm10Index6Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Index7Hour
        /// </summary>
        [DataMember(Name = "pm10Index7Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Index7Hour Pm10Index7Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Index8Hour
        /// </summary>
        [DataMember(Name = "pm10Index8Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Index8Hour Pm10Index8Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Index9Hour
        /// </summary>
        [DataMember(Name = "pm10Index9Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Index9Hour Pm10Index9Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Index10Hour
        /// </summary>
        [DataMember(Name = "pm10Index10Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Index10Hour Pm10Index10Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Index11Hour
        /// </summary>
        [DataMember(Name = "pm10Index11Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Index11Hour Pm10Index11Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Index12Hour
        /// </summary>
        [DataMember(Name = "pm10Index12Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Index12Hour Pm10Index12Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Amount1Hour
        /// </summary>
        [DataMember(Name = "pm10Amount1Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Amount1Hour Pm10Amount1Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Amount2Hour
        /// </summary>
        [DataMember(Name = "pm10Amount2Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Amount2Hour Pm10Amount2Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Amount3Hour
        /// </summary>
        [DataMember(Name = "pm10Amount3Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Amount3Hour Pm10Amount3Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Amount4Hour
        /// </summary>
        [DataMember(Name = "pm10Amount4Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Amount4Hour Pm10Amount4Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Amount5Hour
        /// </summary>
        [DataMember(Name = "pm10Amount5Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Amount5Hour Pm10Amount5Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Amount6Hour
        /// </summary>
        [DataMember(Name = "pm10Amount6Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Amount6Hour Pm10Amount6Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Amount7Hour
        /// </summary>
        [DataMember(Name = "pm10Amount7Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Amount7Hour Pm10Amount7Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Amount8Hour
        /// </summary>
        [DataMember(Name = "pm10Amount8Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Amount8Hour Pm10Amount8Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Amount9Hour
        /// </summary>
        [DataMember(Name = "pm10Amount9Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Amount9Hour Pm10Amount9Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Amount10Hour
        /// </summary>
        [DataMember(Name = "pm10Amount10Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Amount10Hour Pm10Amount10Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Amount11Hour
        /// </summary>
        [DataMember(Name = "pm10Amount11Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Amount11Hour Pm10Amount11Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm10Amount12Hour
        /// </summary>
        [DataMember(Name = "pm10Amount12Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm10Amount12Hour Pm10Amount12Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Index1Hour
        /// </summary>
        [DataMember(Name = "pm25Index1Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Index1Hour Pm25Index1Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Index2Hour
        /// </summary>
        [DataMember(Name = "pm25Index2Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Index2Hour Pm25Index2Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Index3Hour
        /// </summary>
        [DataMember(Name = "pm25Index3Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Index3Hour Pm25Index3Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Index4Hour
        /// </summary>
        [DataMember(Name = "pm25Index4Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Index4Hour Pm25Index4Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Index5Hour
        /// </summary>
        [DataMember(Name = "pm25Index5Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Index5Hour Pm25Index5Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Index6Hour
        /// </summary>
        [DataMember(Name = "pm25Index6Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Index6Hour Pm25Index6Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Index7Hour
        /// </summary>
        [DataMember(Name = "pm25Index7Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Index7Hour Pm25Index7Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Index8Hour
        /// </summary>
        [DataMember(Name = "pm25Index8Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Index8Hour Pm25Index8Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Index9Hour
        /// </summary>
        [DataMember(Name = "pm25Index9Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Index9Hour Pm25Index9Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Index10Hour
        /// </summary>
        [DataMember(Name = "pm25Index10Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Index10Hour Pm25Index10Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Index11Hour
        /// </summary>
        [DataMember(Name = "pm25Index11Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Index11Hour Pm25Index11Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Index12Hour
        /// </summary>
        [DataMember(Name = "pm25Index12Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Index12Hour Pm25Index12Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Amount1Hour
        /// </summary>
        [DataMember(Name = "pm25Amount1Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Amount1Hour Pm25Amount1Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Amount2Hour
        /// </summary>
        [DataMember(Name = "pm25Amount2Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Amount2Hour Pm25Amount2Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Amount3Hour
        /// </summary>
        [DataMember(Name = "pm25Amount3Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Amount3Hour Pm25Amount3Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Amount4Hour
        /// </summary>
        [DataMember(Name = "pm25Amount4Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Amount4Hour Pm25Amount4Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Amount5Hour
        /// </summary>
        [DataMember(Name = "pm25Amount5Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Amount5Hour Pm25Amount5Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Amount6Hour
        /// </summary>
        [DataMember(Name = "pm25Amount6Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Amount6Hour Pm25Amount6Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Amount7Hour
        /// </summary>
        [DataMember(Name = "pm25Amount7Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Amount7Hour Pm25Amount7Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Amount8Hour
        /// </summary>
        [DataMember(Name = "pm25Amount8Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Amount8Hour Pm25Amount8Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Amount9Hour
        /// </summary>
        [DataMember(Name = "pm25Amount9Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Amount9Hour Pm25Amount9Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Amount10Hour
        /// </summary>
        [DataMember(Name = "pm25Amount10Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Amount10Hour Pm25Amount10Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Amount11Hour
        /// </summary>
        [DataMember(Name = "pm25Amount11Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Amount11Hour Pm25Amount11Hour { get; set; }

        /// <summary>
        /// Gets or Sets Pm25Amount12Hour
        /// </summary>
        [DataMember(Name = "pm25Amount12Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataAirQualityForecastPm25Amount12Hour Pm25Amount12Hour { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class ServiceCapabilityDataAirQualityForecast {\n");
            sb.Append("  LastUpdateTime: ").Append(LastUpdateTime).Append("\n");
            sb.Append("  Vendor: ").Append(Vendor).Append("\n");
            sb.Append("  _Version: ").Append(_Version).Append("\n");
            sb.Append("  Pm10Index1Hour: ").Append(Pm10Index1Hour).Append("\n");
            sb.Append("  Pm10Index2Hour: ").Append(Pm10Index2Hour).Append("\n");
            sb.Append("  Pm10Index3Hour: ").Append(Pm10Index3Hour).Append("\n");
            sb.Append("  Pm10Index4Hour: ").Append(Pm10Index4Hour).Append("\n");
            sb.Append("  Pm10Index5Hour: ").Append(Pm10Index5Hour).Append("\n");
            sb.Append("  Pm10Index6Hour: ").Append(Pm10Index6Hour).Append("\n");
            sb.Append("  Pm10Index7Hour: ").Append(Pm10Index7Hour).Append("\n");
            sb.Append("  Pm10Index8Hour: ").Append(Pm10Index8Hour).Append("\n");
            sb.Append("  Pm10Index9Hour: ").Append(Pm10Index9Hour).Append("\n");
            sb.Append("  Pm10Index10Hour: ").Append(Pm10Index10Hour).Append("\n");
            sb.Append("  Pm10Index11Hour: ").Append(Pm10Index11Hour).Append("\n");
            sb.Append("  Pm10Index12Hour: ").Append(Pm10Index12Hour).Append("\n");
            sb.Append("  Pm10Amount1Hour: ").Append(Pm10Amount1Hour).Append("\n");
            sb.Append("  Pm10Amount2Hour: ").Append(Pm10Amount2Hour).Append("\n");
            sb.Append("  Pm10Amount3Hour: ").Append(Pm10Amount3Hour).Append("\n");
            sb.Append("  Pm10Amount4Hour: ").Append(Pm10Amount4Hour).Append("\n");
            sb.Append("  Pm10Amount5Hour: ").Append(Pm10Amount5Hour).Append("\n");
            sb.Append("  Pm10Amount6Hour: ").Append(Pm10Amount6Hour).Append("\n");
            sb.Append("  Pm10Amount7Hour: ").Append(Pm10Amount7Hour).Append("\n");
            sb.Append("  Pm10Amount8Hour: ").Append(Pm10Amount8Hour).Append("\n");
            sb.Append("  Pm10Amount9Hour: ").Append(Pm10Amount9Hour).Append("\n");
            sb.Append("  Pm10Amount10Hour: ").Append(Pm10Amount10Hour).Append("\n");
            sb.Append("  Pm10Amount11Hour: ").Append(Pm10Amount11Hour).Append("\n");
            sb.Append("  Pm10Amount12Hour: ").Append(Pm10Amount12Hour).Append("\n");
            sb.Append("  Pm25Index1Hour: ").Append(Pm25Index1Hour).Append("\n");
            sb.Append("  Pm25Index2Hour: ").Append(Pm25Index2Hour).Append("\n");
            sb.Append("  Pm25Index3Hour: ").Append(Pm25Index3Hour).Append("\n");
            sb.Append("  Pm25Index4Hour: ").Append(Pm25Index4Hour).Append("\n");
            sb.Append("  Pm25Index5Hour: ").Append(Pm25Index5Hour).Append("\n");
            sb.Append("  Pm25Index6Hour: ").Append(Pm25Index6Hour).Append("\n");
            sb.Append("  Pm25Index7Hour: ").Append(Pm25Index7Hour).Append("\n");
            sb.Append("  Pm25Index8Hour: ").Append(Pm25Index8Hour).Append("\n");
            sb.Append("  Pm25Index9Hour: ").Append(Pm25Index9Hour).Append("\n");
            sb.Append("  Pm25Index10Hour: ").Append(Pm25Index10Hour).Append("\n");
            sb.Append("  Pm25Index11Hour: ").Append(Pm25Index11Hour).Append("\n");
            sb.Append("  Pm25Index12Hour: ").Append(Pm25Index12Hour).Append("\n");
            sb.Append("  Pm25Amount1Hour: ").Append(Pm25Amount1Hour).Append("\n");
            sb.Append("  Pm25Amount2Hour: ").Append(Pm25Amount2Hour).Append("\n");
            sb.Append("  Pm25Amount3Hour: ").Append(Pm25Amount3Hour).Append("\n");
            sb.Append("  Pm25Amount4Hour: ").Append(Pm25Amount4Hour).Append("\n");
            sb.Append("  Pm25Amount5Hour: ").Append(Pm25Amount5Hour).Append("\n");
            sb.Append("  Pm25Amount6Hour: ").Append(Pm25Amount6Hour).Append("\n");
            sb.Append("  Pm25Amount7Hour: ").Append(Pm25Amount7Hour).Append("\n");
            sb.Append("  Pm25Amount8Hour: ").Append(Pm25Amount8Hour).Append("\n");
            sb.Append("  Pm25Amount9Hour: ").Append(Pm25Amount9Hour).Append("\n");
            sb.Append("  Pm25Amount10Hour: ").Append(Pm25Amount10Hour).Append("\n");
            sb.Append("  Pm25Amount11Hour: ").Append(Pm25Amount11Hour).Append("\n");
            sb.Append("  Pm25Amount12Hour: ").Append(Pm25Amount12Hour).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as ServiceCapabilityDataAirQualityForecast);
        }

        /// <summary>
        /// Returns true if ServiceCapabilityDataAirQualityForecast instances are equal
        /// </summary>
        /// <param name="input">Instance of ServiceCapabilityDataAirQualityForecast to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ServiceCapabilityDataAirQualityForecast input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.LastUpdateTime == input.LastUpdateTime ||
                    (this.LastUpdateTime != null &&
                    this.LastUpdateTime.Equals(input.LastUpdateTime))
                ) && 
                (
                    this.Vendor == input.Vendor ||
                    (this.Vendor != null &&
                    this.Vendor.Equals(input.Vendor))
                ) && 
                (
                    this._Version == input._Version ||
                    (this._Version != null &&
                    this._Version.Equals(input._Version))
                ) && 
                (
                    this.Pm10Index1Hour == input.Pm10Index1Hour ||
                    (this.Pm10Index1Hour != null &&
                    this.Pm10Index1Hour.Equals(input.Pm10Index1Hour))
                ) && 
                (
                    this.Pm10Index2Hour == input.Pm10Index2Hour ||
                    (this.Pm10Index2Hour != null &&
                    this.Pm10Index2Hour.Equals(input.Pm10Index2Hour))
                ) && 
                (
                    this.Pm10Index3Hour == input.Pm10Index3Hour ||
                    (this.Pm10Index3Hour != null &&
                    this.Pm10Index3Hour.Equals(input.Pm10Index3Hour))
                ) && 
                (
                    this.Pm10Index4Hour == input.Pm10Index4Hour ||
                    (this.Pm10Index4Hour != null &&
                    this.Pm10Index4Hour.Equals(input.Pm10Index4Hour))
                ) && 
                (
                    this.Pm10Index5Hour == input.Pm10Index5Hour ||
                    (this.Pm10Index5Hour != null &&
                    this.Pm10Index5Hour.Equals(input.Pm10Index5Hour))
                ) && 
                (
                    this.Pm10Index6Hour == input.Pm10Index6Hour ||
                    (this.Pm10Index6Hour != null &&
                    this.Pm10Index6Hour.Equals(input.Pm10Index6Hour))
                ) && 
                (
                    this.Pm10Index7Hour == input.Pm10Index7Hour ||
                    (this.Pm10Index7Hour != null &&
                    this.Pm10Index7Hour.Equals(input.Pm10Index7Hour))
                ) && 
                (
                    this.Pm10Index8Hour == input.Pm10Index8Hour ||
                    (this.Pm10Index8Hour != null &&
                    this.Pm10Index8Hour.Equals(input.Pm10Index8Hour))
                ) && 
                (
                    this.Pm10Index9Hour == input.Pm10Index9Hour ||
                    (this.Pm10Index9Hour != null &&
                    this.Pm10Index9Hour.Equals(input.Pm10Index9Hour))
                ) && 
                (
                    this.Pm10Index10Hour == input.Pm10Index10Hour ||
                    (this.Pm10Index10Hour != null &&
                    this.Pm10Index10Hour.Equals(input.Pm10Index10Hour))
                ) && 
                (
                    this.Pm10Index11Hour == input.Pm10Index11Hour ||
                    (this.Pm10Index11Hour != null &&
                    this.Pm10Index11Hour.Equals(input.Pm10Index11Hour))
                ) && 
                (
                    this.Pm10Index12Hour == input.Pm10Index12Hour ||
                    (this.Pm10Index12Hour != null &&
                    this.Pm10Index12Hour.Equals(input.Pm10Index12Hour))
                ) && 
                (
                    this.Pm10Amount1Hour == input.Pm10Amount1Hour ||
                    (this.Pm10Amount1Hour != null &&
                    this.Pm10Amount1Hour.Equals(input.Pm10Amount1Hour))
                ) && 
                (
                    this.Pm10Amount2Hour == input.Pm10Amount2Hour ||
                    (this.Pm10Amount2Hour != null &&
                    this.Pm10Amount2Hour.Equals(input.Pm10Amount2Hour))
                ) && 
                (
                    this.Pm10Amount3Hour == input.Pm10Amount3Hour ||
                    (this.Pm10Amount3Hour != null &&
                    this.Pm10Amount3Hour.Equals(input.Pm10Amount3Hour))
                ) && 
                (
                    this.Pm10Amount4Hour == input.Pm10Amount4Hour ||
                    (this.Pm10Amount4Hour != null &&
                    this.Pm10Amount4Hour.Equals(input.Pm10Amount4Hour))
                ) && 
                (
                    this.Pm10Amount5Hour == input.Pm10Amount5Hour ||
                    (this.Pm10Amount5Hour != null &&
                    this.Pm10Amount5Hour.Equals(input.Pm10Amount5Hour))
                ) && 
                (
                    this.Pm10Amount6Hour == input.Pm10Amount6Hour ||
                    (this.Pm10Amount6Hour != null &&
                    this.Pm10Amount6Hour.Equals(input.Pm10Amount6Hour))
                ) && 
                (
                    this.Pm10Amount7Hour == input.Pm10Amount7Hour ||
                    (this.Pm10Amount7Hour != null &&
                    this.Pm10Amount7Hour.Equals(input.Pm10Amount7Hour))
                ) && 
                (
                    this.Pm10Amount8Hour == input.Pm10Amount8Hour ||
                    (this.Pm10Amount8Hour != null &&
                    this.Pm10Amount8Hour.Equals(input.Pm10Amount8Hour))
                ) && 
                (
                    this.Pm10Amount9Hour == input.Pm10Amount9Hour ||
                    (this.Pm10Amount9Hour != null &&
                    this.Pm10Amount9Hour.Equals(input.Pm10Amount9Hour))
                ) && 
                (
                    this.Pm10Amount10Hour == input.Pm10Amount10Hour ||
                    (this.Pm10Amount10Hour != null &&
                    this.Pm10Amount10Hour.Equals(input.Pm10Amount10Hour))
                ) && 
                (
                    this.Pm10Amount11Hour == input.Pm10Amount11Hour ||
                    (this.Pm10Amount11Hour != null &&
                    this.Pm10Amount11Hour.Equals(input.Pm10Amount11Hour))
                ) && 
                (
                    this.Pm10Amount12Hour == input.Pm10Amount12Hour ||
                    (this.Pm10Amount12Hour != null &&
                    this.Pm10Amount12Hour.Equals(input.Pm10Amount12Hour))
                ) && 
                (
                    this.Pm25Index1Hour == input.Pm25Index1Hour ||
                    (this.Pm25Index1Hour != null &&
                    this.Pm25Index1Hour.Equals(input.Pm25Index1Hour))
                ) && 
                (
                    this.Pm25Index2Hour == input.Pm25Index2Hour ||
                    (this.Pm25Index2Hour != null &&
                    this.Pm25Index2Hour.Equals(input.Pm25Index2Hour))
                ) && 
                (
                    this.Pm25Index3Hour == input.Pm25Index3Hour ||
                    (this.Pm25Index3Hour != null &&
                    this.Pm25Index3Hour.Equals(input.Pm25Index3Hour))
                ) && 
                (
                    this.Pm25Index4Hour == input.Pm25Index4Hour ||
                    (this.Pm25Index4Hour != null &&
                    this.Pm25Index4Hour.Equals(input.Pm25Index4Hour))
                ) && 
                (
                    this.Pm25Index5Hour == input.Pm25Index5Hour ||
                    (this.Pm25Index5Hour != null &&
                    this.Pm25Index5Hour.Equals(input.Pm25Index5Hour))
                ) && 
                (
                    this.Pm25Index6Hour == input.Pm25Index6Hour ||
                    (this.Pm25Index6Hour != null &&
                    this.Pm25Index6Hour.Equals(input.Pm25Index6Hour))
                ) && 
                (
                    this.Pm25Index7Hour == input.Pm25Index7Hour ||
                    (this.Pm25Index7Hour != null &&
                    this.Pm25Index7Hour.Equals(input.Pm25Index7Hour))
                ) && 
                (
                    this.Pm25Index8Hour == input.Pm25Index8Hour ||
                    (this.Pm25Index8Hour != null &&
                    this.Pm25Index8Hour.Equals(input.Pm25Index8Hour))
                ) && 
                (
                    this.Pm25Index9Hour == input.Pm25Index9Hour ||
                    (this.Pm25Index9Hour != null &&
                    this.Pm25Index9Hour.Equals(input.Pm25Index9Hour))
                ) && 
                (
                    this.Pm25Index10Hour == input.Pm25Index10Hour ||
                    (this.Pm25Index10Hour != null &&
                    this.Pm25Index10Hour.Equals(input.Pm25Index10Hour))
                ) && 
                (
                    this.Pm25Index11Hour == input.Pm25Index11Hour ||
                    (this.Pm25Index11Hour != null &&
                    this.Pm25Index11Hour.Equals(input.Pm25Index11Hour))
                ) && 
                (
                    this.Pm25Index12Hour == input.Pm25Index12Hour ||
                    (this.Pm25Index12Hour != null &&
                    this.Pm25Index12Hour.Equals(input.Pm25Index12Hour))
                ) && 
                (
                    this.Pm25Amount1Hour == input.Pm25Amount1Hour ||
                    (this.Pm25Amount1Hour != null &&
                    this.Pm25Amount1Hour.Equals(input.Pm25Amount1Hour))
                ) && 
                (
                    this.Pm25Amount2Hour == input.Pm25Amount2Hour ||
                    (this.Pm25Amount2Hour != null &&
                    this.Pm25Amount2Hour.Equals(input.Pm25Amount2Hour))
                ) && 
                (
                    this.Pm25Amount3Hour == input.Pm25Amount3Hour ||
                    (this.Pm25Amount3Hour != null &&
                    this.Pm25Amount3Hour.Equals(input.Pm25Amount3Hour))
                ) && 
                (
                    this.Pm25Amount4Hour == input.Pm25Amount4Hour ||
                    (this.Pm25Amount4Hour != null &&
                    this.Pm25Amount4Hour.Equals(input.Pm25Amount4Hour))
                ) && 
                (
                    this.Pm25Amount5Hour == input.Pm25Amount5Hour ||
                    (this.Pm25Amount5Hour != null &&
                    this.Pm25Amount5Hour.Equals(input.Pm25Amount5Hour))
                ) && 
                (
                    this.Pm25Amount6Hour == input.Pm25Amount6Hour ||
                    (this.Pm25Amount6Hour != null &&
                    this.Pm25Amount6Hour.Equals(input.Pm25Amount6Hour))
                ) && 
                (
                    this.Pm25Amount7Hour == input.Pm25Amount7Hour ||
                    (this.Pm25Amount7Hour != null &&
                    this.Pm25Amount7Hour.Equals(input.Pm25Amount7Hour))
                ) && 
                (
                    this.Pm25Amount8Hour == input.Pm25Amount8Hour ||
                    (this.Pm25Amount8Hour != null &&
                    this.Pm25Amount8Hour.Equals(input.Pm25Amount8Hour))
                ) && 
                (
                    this.Pm25Amount9Hour == input.Pm25Amount9Hour ||
                    (this.Pm25Amount9Hour != null &&
                    this.Pm25Amount9Hour.Equals(input.Pm25Amount9Hour))
                ) && 
                (
                    this.Pm25Amount10Hour == input.Pm25Amount10Hour ||
                    (this.Pm25Amount10Hour != null &&
                    this.Pm25Amount10Hour.Equals(input.Pm25Amount10Hour))
                ) && 
                (
                    this.Pm25Amount11Hour == input.Pm25Amount11Hour ||
                    (this.Pm25Amount11Hour != null &&
                    this.Pm25Amount11Hour.Equals(input.Pm25Amount11Hour))
                ) && 
                (
                    this.Pm25Amount12Hour == input.Pm25Amount12Hour ||
                    (this.Pm25Amount12Hour != null &&
                    this.Pm25Amount12Hour.Equals(input.Pm25Amount12Hour))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.LastUpdateTime != null)
                {
                    hashCode = (hashCode * 59) + this.LastUpdateTime.GetHashCode();
                }
                if (this.Vendor != null)
                {
                    hashCode = (hashCode * 59) + this.Vendor.GetHashCode();
                }
                if (this._Version != null)
                {
                    hashCode = (hashCode * 59) + this._Version.GetHashCode();
                }
                if (this.Pm10Index1Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Index1Hour.GetHashCode();
                }
                if (this.Pm10Index2Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Index2Hour.GetHashCode();
                }
                if (this.Pm10Index3Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Index3Hour.GetHashCode();
                }
                if (this.Pm10Index4Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Index4Hour.GetHashCode();
                }
                if (this.Pm10Index5Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Index5Hour.GetHashCode();
                }
                if (this.Pm10Index6Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Index6Hour.GetHashCode();
                }
                if (this.Pm10Index7Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Index7Hour.GetHashCode();
                }
                if (this.Pm10Index8Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Index8Hour.GetHashCode();
                }
                if (this.Pm10Index9Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Index9Hour.GetHashCode();
                }
                if (this.Pm10Index10Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Index10Hour.GetHashCode();
                }
                if (this.Pm10Index11Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Index11Hour.GetHashCode();
                }
                if (this.Pm10Index12Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Index12Hour.GetHashCode();
                }
                if (this.Pm10Amount1Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Amount1Hour.GetHashCode();
                }
                if (this.Pm10Amount2Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Amount2Hour.GetHashCode();
                }
                if (this.Pm10Amount3Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Amount3Hour.GetHashCode();
                }
                if (this.Pm10Amount4Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Amount4Hour.GetHashCode();
                }
                if (this.Pm10Amount5Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Amount5Hour.GetHashCode();
                }
                if (this.Pm10Amount6Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Amount6Hour.GetHashCode();
                }
                if (this.Pm10Amount7Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Amount7Hour.GetHashCode();
                }
                if (this.Pm10Amount8Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Amount8Hour.GetHashCode();
                }
                if (this.Pm10Amount9Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Amount9Hour.GetHashCode();
                }
                if (this.Pm10Amount10Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Amount10Hour.GetHashCode();
                }
                if (this.Pm10Amount11Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Amount11Hour.GetHashCode();
                }
                if (this.Pm10Amount12Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm10Amount12Hour.GetHashCode();
                }
                if (this.Pm25Index1Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Index1Hour.GetHashCode();
                }
                if (this.Pm25Index2Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Index2Hour.GetHashCode();
                }
                if (this.Pm25Index3Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Index3Hour.GetHashCode();
                }
                if (this.Pm25Index4Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Index4Hour.GetHashCode();
                }
                if (this.Pm25Index5Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Index5Hour.GetHashCode();
                }
                if (this.Pm25Index6Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Index6Hour.GetHashCode();
                }
                if (this.Pm25Index7Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Index7Hour.GetHashCode();
                }
                if (this.Pm25Index8Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Index8Hour.GetHashCode();
                }
                if (this.Pm25Index9Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Index9Hour.GetHashCode();
                }
                if (this.Pm25Index10Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Index10Hour.GetHashCode();
                }
                if (this.Pm25Index11Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Index11Hour.GetHashCode();
                }
                if (this.Pm25Index12Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Index12Hour.GetHashCode();
                }
                if (this.Pm25Amount1Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Amount1Hour.GetHashCode();
                }
                if (this.Pm25Amount2Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Amount2Hour.GetHashCode();
                }
                if (this.Pm25Amount3Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Amount3Hour.GetHashCode();
                }
                if (this.Pm25Amount4Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Amount4Hour.GetHashCode();
                }
                if (this.Pm25Amount5Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Amount5Hour.GetHashCode();
                }
                if (this.Pm25Amount6Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Amount6Hour.GetHashCode();
                }
                if (this.Pm25Amount7Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Amount7Hour.GetHashCode();
                }
                if (this.Pm25Amount8Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Amount8Hour.GetHashCode();
                }
                if (this.Pm25Amount9Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Amount9Hour.GetHashCode();
                }
                if (this.Pm25Amount10Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Amount10Hour.GetHashCode();
                }
                if (this.Pm25Amount11Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Amount11Hour.GetHashCode();
                }
                if (this.Pm25Amount12Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Pm25Amount12Hour.GetHashCode();
                }
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        public IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
