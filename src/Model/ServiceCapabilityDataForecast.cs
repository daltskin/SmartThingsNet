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
    /// ServiceCapabilityDataForecast
    /// </summary>
    [DataContract(Name = "ServiceCapabilityData_forecast")]
    public partial class ServiceCapabilityDataForecast : IEquatable<ServiceCapabilityDataForecast>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceCapabilityDataForecast" /> class.
        /// </summary>
        /// <param name="lastUpdateTime">lastUpdateTime.</param>
        /// <param name="vendor">vendor.</param>
        /// <param name="version">version.</param>
        /// <param name="precip1Hour">precip1Hour.</param>
        /// <param name="precipMin1Hour">precipMin1Hour.</param>
        /// <param name="precipMax1Hour">precipMax1Hour.</param>
        /// <param name="precip2Hour">precip2Hour.</param>
        /// <param name="precipMin2Hour">precipMin2Hour.</param>
        /// <param name="precipMax2Hour">precipMax2Hour.</param>
        /// <param name="precip3Hour">precip3Hour.</param>
        /// <param name="precipMin3Hour">precipMin3Hour.</param>
        /// <param name="precipMax3Hour">precipMax3Hour.</param>
        /// <param name="precip4Hour">precip4Hour.</param>
        /// <param name="precipMin4Hour">precipMin4Hour.</param>
        /// <param name="precipMax4Hour">precipMax4Hour.</param>
        /// <param name="precip5Hour">precip5Hour.</param>
        /// <param name="precipMin5Hour">precipMin5Hour.</param>
        /// <param name="precipMax5Hour">precipMax5Hour.</param>
        /// <param name="precip6Hour">precip6Hour.</param>
        /// <param name="precipMin6Hour">precipMin6Hour.</param>
        /// <param name="precipMax6Hour">precipMax6Hour.</param>
        /// <param name="precip7Hour">precip7Hour.</param>
        /// <param name="precipMin7Hour">precipMin7Hour.</param>
        /// <param name="precipMax7Hour">precipMax7Hour.</param>
        /// <param name="precip8Hour">precip8Hour.</param>
        /// <param name="precipMin8Hour">precipMin8Hour.</param>
        /// <param name="precipMax8Hour">precipMax8Hour.</param>
        /// <param name="precip9Hour">precip9Hour.</param>
        /// <param name="precipMin9Hour">precipMin9Hour.</param>
        /// <param name="precipMax9Hour">precipMax9Hour.</param>
        /// <param name="precip10Hour">precip10Hour.</param>
        /// <param name="precipMin10Hour">precipMin10Hour.</param>
        /// <param name="precipMax10Hour">precipMax10Hour.</param>
        /// <param name="precip11Hour">precip11Hour.</param>
        /// <param name="precipMin11Hour">precipMin11Hour.</param>
        /// <param name="precipMax11Hour">precipMax11Hour.</param>
        /// <param name="precip12Hour">precip12Hour.</param>
        /// <param name="precipMin12Hour">precipMin12Hour.</param>
        /// <param name="precipMax12Hour">precipMax12Hour.</param>
        /// <param name="precip24Hour">precip24Hour.</param>
        /// <param name="snow1Hour">snow1Hour.</param>
        /// <param name="snowMin1Hour">snowMin1Hour.</param>
        /// <param name="snowMax1Hour">snowMax1Hour.</param>
        /// <param name="snow2Hour">snow2Hour.</param>
        /// <param name="snowMin2Hour">snowMin2Hour.</param>
        /// <param name="snowMax2Hour">snowMax2Hour.</param>
        /// <param name="snow3Hour">snow3Hour.</param>
        /// <param name="snowMin3Hour">snowMin3Hour.</param>
        /// <param name="snowMax3Hour">snowMax3Hour.</param>
        /// <param name="snow4Hour">snow4Hour.</param>
        /// <param name="snowMin4Hour">snowMin4Hour.</param>
        /// <param name="snowMax4Hour">snowMax4Hour.</param>
        /// <param name="snow5Hour">snow5Hour.</param>
        /// <param name="snowMin5Hour">snowMin5Hour.</param>
        /// <param name="snowMax5Hour">snowMax5Hour.</param>
        /// <param name="snow6Hour">snow6Hour.</param>
        /// <param name="snowMin6Hour">snowMin6Hour.</param>
        /// <param name="snowMax6Hour">snowMax6Hour.</param>
        /// <param name="snow7Hour">snow7Hour.</param>
        /// <param name="snowMin7Hour">snowMin7Hour.</param>
        /// <param name="snowMax7Hour">snowMax7Hour.</param>
        /// <param name="snow8Hour">snow8Hour.</param>
        /// <param name="snowMin8Hour">snowMin8Hour.</param>
        /// <param name="snowMax8Hour">snowMax8Hour.</param>
        /// <param name="snow9Hour">snow9Hour.</param>
        /// <param name="snowMin9Hour">snowMin9Hour.</param>
        /// <param name="snowMax9Hour">snowMax9Hour.</param>
        /// <param name="snow10Hour">snow10Hour.</param>
        /// <param name="snowMin10Hour">snowMin10Hour.</param>
        /// <param name="snowMax10Hour">snowMax10Hour.</param>
        /// <param name="snow11Hour">snow11Hour.</param>
        /// <param name="snowMin11Hour">snowMin11Hour.</param>
        /// <param name="snowMax11Hour">snowMax11Hour.</param>
        /// <param name="snow12Hour">snow12Hour.</param>
        /// <param name="snowMin12Hour">snowMin12Hour.</param>
        /// <param name="snowMax12Hour">snowMax12Hour.</param>
        /// <param name="snow24Hour">snow24Hour.</param>
        /// <param name="temperature1Hour">temperature1Hour.</param>
        /// <param name="temperature2Hour">temperature2Hour.</param>
        /// <param name="temperature3Hour">temperature3Hour.</param>
        /// <param name="temperature4Hour">temperature4Hour.</param>
        /// <param name="temperature5Hour">temperature5Hour.</param>
        /// <param name="temperature6Hour">temperature6Hour.</param>
        /// <param name="temperature7Hour">temperature7Hour.</param>
        /// <param name="temperature8Hour">temperature8Hour.</param>
        /// <param name="temperature9Hour">temperature9Hour.</param>
        /// <param name="temperature10Hour">temperature10Hour.</param>
        /// <param name="temperature11Hour">temperature11Hour.</param>
        /// <param name="temperature12Hour">temperature12Hour.</param>
        public ServiceCapabilityDataForecast(ServiceCapabilityDataLastUpdateTime lastUpdateTime = default(ServiceCapabilityDataLastUpdateTime), ServiceCapabilityDataAirQualityVendor vendor = default(ServiceCapabilityDataAirQualityVendor), ServiceCapabilityDataAirQualityVersion version = default(ServiceCapabilityDataAirQualityVersion), ServiceCapabilityDataForecastPrecip1Hour precip1Hour = default(ServiceCapabilityDataForecastPrecip1Hour), ServiceCapabilityDataForecastPrecipMin1Hour precipMin1Hour = default(ServiceCapabilityDataForecastPrecipMin1Hour), ServiceCapabilityDataForecastPrecipMax1Hour precipMax1Hour = default(ServiceCapabilityDataForecastPrecipMax1Hour), ServiceCapabilityDataForecastPrecip2Hour precip2Hour = default(ServiceCapabilityDataForecastPrecip2Hour), ServiceCapabilityDataForecastPrecipMin2Hour precipMin2Hour = default(ServiceCapabilityDataForecastPrecipMin2Hour), ServiceCapabilityDataForecastPrecipMin2Hour precipMax2Hour = default(ServiceCapabilityDataForecastPrecipMin2Hour), ServiceCapabilityDataForecastPrecip3Hour precip3Hour = default(ServiceCapabilityDataForecastPrecip3Hour), ServiceCapabilityDataForecastPrecipMin3Hour precipMin3Hour = default(ServiceCapabilityDataForecastPrecipMin3Hour), ServiceCapabilityDataForecastPrecipMin3Hour precipMax3Hour = default(ServiceCapabilityDataForecastPrecipMin3Hour), ServiceCapabilityDataForecastPrecip4Hour precip4Hour = default(ServiceCapabilityDataForecastPrecip4Hour), ServiceCapabilityDataForecastPrecipMin4Hour precipMin4Hour = default(ServiceCapabilityDataForecastPrecipMin4Hour), ServiceCapabilityDataForecastPrecipMax4Hour precipMax4Hour = default(ServiceCapabilityDataForecastPrecipMax4Hour), ServiceCapabilityDataForecastPrecip5Hour precip5Hour = default(ServiceCapabilityDataForecastPrecip5Hour), ServiceCapabilityDataForecastPrecipMin5Hour precipMin5Hour = default(ServiceCapabilityDataForecastPrecipMin5Hour), ServiceCapabilityDataForecastPrecipMin5Hour precipMax5Hour = default(ServiceCapabilityDataForecastPrecipMin5Hour), ServiceCapabilityDataForecastPrecip6Hour precip6Hour = default(ServiceCapabilityDataForecastPrecip6Hour), ServiceCapabilityDataForecastPrecipMin6Hour precipMin6Hour = default(ServiceCapabilityDataForecastPrecipMin6Hour), ServiceCapabilityDataForecastPrecipMin6Hour precipMax6Hour = default(ServiceCapabilityDataForecastPrecipMin6Hour), ServiceCapabilityDataForecastPrecip7Hour precip7Hour = default(ServiceCapabilityDataForecastPrecip7Hour), ServiceCapabilityDataForecastPrecipMin7Hour precipMin7Hour = default(ServiceCapabilityDataForecastPrecipMin7Hour), ServiceCapabilityDataForecastPrecipMin7Hour precipMax7Hour = default(ServiceCapabilityDataForecastPrecipMin7Hour), ServiceCapabilityDataForecastPrecip8Hour precip8Hour = default(ServiceCapabilityDataForecastPrecip8Hour), ServiceCapabilityDataForecastPrecipMin8Hour precipMin8Hour = default(ServiceCapabilityDataForecastPrecipMin8Hour), ServiceCapabilityDataForecastPrecipMin8Hour precipMax8Hour = default(ServiceCapabilityDataForecastPrecipMin8Hour), ServiceCapabilityDataForecastPrecip9Hour precip9Hour = default(ServiceCapabilityDataForecastPrecip9Hour), ServiceCapabilityDataForecastPrecipMin9Hour precipMin9Hour = default(ServiceCapabilityDataForecastPrecipMin9Hour), ServiceCapabilityDataForecastPrecipMin9Hour precipMax9Hour = default(ServiceCapabilityDataForecastPrecipMin9Hour), ServiceCapabilityDataForecastPrecip10Hour precip10Hour = default(ServiceCapabilityDataForecastPrecip10Hour), ServiceCapabilityDataForecastPrecipMin10Hour precipMin10Hour = default(ServiceCapabilityDataForecastPrecipMin10Hour), ServiceCapabilityDataForecastPrecipMin10Hour precipMax10Hour = default(ServiceCapabilityDataForecastPrecipMin10Hour), ServiceCapabilityDataForecastPrecip11Hour precip11Hour = default(ServiceCapabilityDataForecastPrecip11Hour), ServiceCapabilityDataForecastPrecipMin11Hour precipMin11Hour = default(ServiceCapabilityDataForecastPrecipMin11Hour), ServiceCapabilityDataForecastPrecipMin11Hour precipMax11Hour = default(ServiceCapabilityDataForecastPrecipMin11Hour), ServiceCapabilityDataForecastPrecip12Hour precip12Hour = default(ServiceCapabilityDataForecastPrecip12Hour), ServiceCapabilityDataForecastPrecipMin12Hour precipMin12Hour = default(ServiceCapabilityDataForecastPrecipMin12Hour), ServiceCapabilityDataForecastPrecipMin12Hour precipMax12Hour = default(ServiceCapabilityDataForecastPrecipMin12Hour), ServiceCapabilityDataForecastPrecip24Hour precip24Hour = default(ServiceCapabilityDataForecastPrecip24Hour), ServiceCapabilityDataForecastSnow1Hour snow1Hour = default(ServiceCapabilityDataForecastSnow1Hour), ServiceCapabilityDataForecastSnowMin1Hour snowMin1Hour = default(ServiceCapabilityDataForecastSnowMin1Hour), ServiceCapabilityDataForecastSnowMax1Hour snowMax1Hour = default(ServiceCapabilityDataForecastSnowMax1Hour), ServiceCapabilityDataForecastSnow2Hour snow2Hour = default(ServiceCapabilityDataForecastSnow2Hour), ServiceCapabilityDataForecastSnowMin2Hour snowMin2Hour = default(ServiceCapabilityDataForecastSnowMin2Hour), ServiceCapabilityDataForecastSnowMax2Hour snowMax2Hour = default(ServiceCapabilityDataForecastSnowMax2Hour), ServiceCapabilityDataForecastSnow3Hour snow3Hour = default(ServiceCapabilityDataForecastSnow3Hour), ServiceCapabilityDataForecastSnowMin3Hour snowMin3Hour = default(ServiceCapabilityDataForecastSnowMin3Hour), ServiceCapabilityDataForecastSnowMax3Hour snowMax3Hour = default(ServiceCapabilityDataForecastSnowMax3Hour), ServiceCapabilityDataForecastSnow4Hour snow4Hour = default(ServiceCapabilityDataForecastSnow4Hour), ServiceCapabilityDataForecastSnowMin4Hour snowMin4Hour = default(ServiceCapabilityDataForecastSnowMin4Hour), ServiceCapabilityDataForecastSnowMax4Hour snowMax4Hour = default(ServiceCapabilityDataForecastSnowMax4Hour), ServiceCapabilityDataForecastSnow5Hour snow5Hour = default(ServiceCapabilityDataForecastSnow5Hour), ServiceCapabilityDataForecastSnowMin5Hour snowMin5Hour = default(ServiceCapabilityDataForecastSnowMin5Hour), ServiceCapabilityDataForecastSnowMax5Hour snowMax5Hour = default(ServiceCapabilityDataForecastSnowMax5Hour), ServiceCapabilityDataForecastSnow6Hour snow6Hour = default(ServiceCapabilityDataForecastSnow6Hour), ServiceCapabilityDataForecastSnowMin6Hour snowMin6Hour = default(ServiceCapabilityDataForecastSnowMin6Hour), ServiceCapabilityDataForecastSnowMax6Hour snowMax6Hour = default(ServiceCapabilityDataForecastSnowMax6Hour), ServiceCapabilityDataForecastSnow7Hour snow7Hour = default(ServiceCapabilityDataForecastSnow7Hour), ServiceCapabilityDataForecastSnowMin7Hour snowMin7Hour = default(ServiceCapabilityDataForecastSnowMin7Hour), ServiceCapabilityDataForecastSnowMax7Hour snowMax7Hour = default(ServiceCapabilityDataForecastSnowMax7Hour), ServiceCapabilityDataForecastSnow8Hour snow8Hour = default(ServiceCapabilityDataForecastSnow8Hour), ServiceCapabilityDataForecastSnowMin8Hour snowMin8Hour = default(ServiceCapabilityDataForecastSnowMin8Hour), ServiceCapabilityDataForecastSnowMax8Hour snowMax8Hour = default(ServiceCapabilityDataForecastSnowMax8Hour), ServiceCapabilityDataForecastSnow9Hour snow9Hour = default(ServiceCapabilityDataForecastSnow9Hour), ServiceCapabilityDataForecastSnowMin9Hour snowMin9Hour = default(ServiceCapabilityDataForecastSnowMin9Hour), ServiceCapabilityDataForecastSnowMax9Hour snowMax9Hour = default(ServiceCapabilityDataForecastSnowMax9Hour), ServiceCapabilityDataForecastSnow10Hour snow10Hour = default(ServiceCapabilityDataForecastSnow10Hour), ServiceCapabilityDataForecastSnowMin10Hour snowMin10Hour = default(ServiceCapabilityDataForecastSnowMin10Hour), ServiceCapabilityDataForecastSnowMax10Hour snowMax10Hour = default(ServiceCapabilityDataForecastSnowMax10Hour), ServiceCapabilityDataForecastSnow11Hour snow11Hour = default(ServiceCapabilityDataForecastSnow11Hour), ServiceCapabilityDataForecastSnowMin11Hour snowMin11Hour = default(ServiceCapabilityDataForecastSnowMin11Hour), ServiceCapabilityDataForecastSnowMax11Hour snowMax11Hour = default(ServiceCapabilityDataForecastSnowMax11Hour), ServiceCapabilityDataForecastSnow12Hour snow12Hour = default(ServiceCapabilityDataForecastSnow12Hour), ServiceCapabilityDataForecastSnowMin12Hour snowMin12Hour = default(ServiceCapabilityDataForecastSnowMin12Hour), ServiceCapabilityDataForecastSnowMax12Hour snowMax12Hour = default(ServiceCapabilityDataForecastSnowMax12Hour), ServiceCapabilityDataForecastSnow24Hour snow24Hour = default(ServiceCapabilityDataForecastSnow24Hour), ServiceCapabilityDataForecastTemperature1Hour temperature1Hour = default(ServiceCapabilityDataForecastTemperature1Hour), ServiceCapabilityDataForecastTemperature1Hour temperature2Hour = default(ServiceCapabilityDataForecastTemperature1Hour), ServiceCapabilityDataForecastTemperature1Hour temperature3Hour = default(ServiceCapabilityDataForecastTemperature1Hour), ServiceCapabilityDataForecastTemperature1Hour temperature4Hour = default(ServiceCapabilityDataForecastTemperature1Hour), ServiceCapabilityDataForecastTemperature1Hour temperature5Hour = default(ServiceCapabilityDataForecastTemperature1Hour), ServiceCapabilityDataForecastTemperature1Hour temperature6Hour = default(ServiceCapabilityDataForecastTemperature1Hour), ServiceCapabilityDataForecastTemperature1Hour temperature7Hour = default(ServiceCapabilityDataForecastTemperature1Hour), ServiceCapabilityDataForecastTemperature1Hour temperature8Hour = default(ServiceCapabilityDataForecastTemperature1Hour), ServiceCapabilityDataForecastTemperature1Hour temperature9Hour = default(ServiceCapabilityDataForecastTemperature1Hour), ServiceCapabilityDataForecastTemperature1Hour temperature10Hour = default(ServiceCapabilityDataForecastTemperature1Hour), ServiceCapabilityDataForecastTemperature1Hour temperature11Hour = default(ServiceCapabilityDataForecastTemperature1Hour), ServiceCapabilityDataForecastTemperature1Hour temperature12Hour = default(ServiceCapabilityDataForecastTemperature1Hour))
        {
            this.LastUpdateTime = lastUpdateTime;
            this.Vendor = vendor;
            this._Version = version;
            this.Precip1Hour = precip1Hour;
            this.PrecipMin1Hour = precipMin1Hour;
            this.PrecipMax1Hour = precipMax1Hour;
            this.Precip2Hour = precip2Hour;
            this.PrecipMin2Hour = precipMin2Hour;
            this.PrecipMax2Hour = precipMax2Hour;
            this.Precip3Hour = precip3Hour;
            this.PrecipMin3Hour = precipMin3Hour;
            this.PrecipMax3Hour = precipMax3Hour;
            this.Precip4Hour = precip4Hour;
            this.PrecipMin4Hour = precipMin4Hour;
            this.PrecipMax4Hour = precipMax4Hour;
            this.Precip5Hour = precip5Hour;
            this.PrecipMin5Hour = precipMin5Hour;
            this.PrecipMax5Hour = precipMax5Hour;
            this.Precip6Hour = precip6Hour;
            this.PrecipMin6Hour = precipMin6Hour;
            this.PrecipMax6Hour = precipMax6Hour;
            this.Precip7Hour = precip7Hour;
            this.PrecipMin7Hour = precipMin7Hour;
            this.PrecipMax7Hour = precipMax7Hour;
            this.Precip8Hour = precip8Hour;
            this.PrecipMin8Hour = precipMin8Hour;
            this.PrecipMax8Hour = precipMax8Hour;
            this.Precip9Hour = precip9Hour;
            this.PrecipMin9Hour = precipMin9Hour;
            this.PrecipMax9Hour = precipMax9Hour;
            this.Precip10Hour = precip10Hour;
            this.PrecipMin10Hour = precipMin10Hour;
            this.PrecipMax10Hour = precipMax10Hour;
            this.Precip11Hour = precip11Hour;
            this.PrecipMin11Hour = precipMin11Hour;
            this.PrecipMax11Hour = precipMax11Hour;
            this.Precip12Hour = precip12Hour;
            this.PrecipMin12Hour = precipMin12Hour;
            this.PrecipMax12Hour = precipMax12Hour;
            this.Precip24Hour = precip24Hour;
            this.Snow1Hour = snow1Hour;
            this.SnowMin1Hour = snowMin1Hour;
            this.SnowMax1Hour = snowMax1Hour;
            this.Snow2Hour = snow2Hour;
            this.SnowMin2Hour = snowMin2Hour;
            this.SnowMax2Hour = snowMax2Hour;
            this.Snow3Hour = snow3Hour;
            this.SnowMin3Hour = snowMin3Hour;
            this.SnowMax3Hour = snowMax3Hour;
            this.Snow4Hour = snow4Hour;
            this.SnowMin4Hour = snowMin4Hour;
            this.SnowMax4Hour = snowMax4Hour;
            this.Snow5Hour = snow5Hour;
            this.SnowMin5Hour = snowMin5Hour;
            this.SnowMax5Hour = snowMax5Hour;
            this.Snow6Hour = snow6Hour;
            this.SnowMin6Hour = snowMin6Hour;
            this.SnowMax6Hour = snowMax6Hour;
            this.Snow7Hour = snow7Hour;
            this.SnowMin7Hour = snowMin7Hour;
            this.SnowMax7Hour = snowMax7Hour;
            this.Snow8Hour = snow8Hour;
            this.SnowMin8Hour = snowMin8Hour;
            this.SnowMax8Hour = snowMax8Hour;
            this.Snow9Hour = snow9Hour;
            this.SnowMin9Hour = snowMin9Hour;
            this.SnowMax9Hour = snowMax9Hour;
            this.Snow10Hour = snow10Hour;
            this.SnowMin10Hour = snowMin10Hour;
            this.SnowMax10Hour = snowMax10Hour;
            this.Snow11Hour = snow11Hour;
            this.SnowMin11Hour = snowMin11Hour;
            this.SnowMax11Hour = snowMax11Hour;
            this.Snow12Hour = snow12Hour;
            this.SnowMin12Hour = snowMin12Hour;
            this.SnowMax12Hour = snowMax12Hour;
            this.Snow24Hour = snow24Hour;
            this.Temperature1Hour = temperature1Hour;
            this.Temperature2Hour = temperature2Hour;
            this.Temperature3Hour = temperature3Hour;
            this.Temperature4Hour = temperature4Hour;
            this.Temperature5Hour = temperature5Hour;
            this.Temperature6Hour = temperature6Hour;
            this.Temperature7Hour = temperature7Hour;
            this.Temperature8Hour = temperature8Hour;
            this.Temperature9Hour = temperature9Hour;
            this.Temperature10Hour = temperature10Hour;
            this.Temperature11Hour = temperature11Hour;
            this.Temperature12Hour = temperature12Hour;
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
        /// Gets or Sets Precip1Hour
        /// </summary>
        [DataMember(Name = "precip1Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecip1Hour Precip1Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMin1Hour
        /// </summary>
        [DataMember(Name = "precipMin1Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMin1Hour PrecipMin1Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMax1Hour
        /// </summary>
        [DataMember(Name = "precipMax1Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMax1Hour PrecipMax1Hour { get; set; }

        /// <summary>
        /// Gets or Sets Precip2Hour
        /// </summary>
        [DataMember(Name = "precip2Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecip2Hour Precip2Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMin2Hour
        /// </summary>
        [DataMember(Name = "precipMin2Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMin2Hour PrecipMin2Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMax2Hour
        /// </summary>
        [DataMember(Name = "precipMax2Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMin2Hour PrecipMax2Hour { get; set; }

        /// <summary>
        /// Gets or Sets Precip3Hour
        /// </summary>
        [DataMember(Name = "precip3Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecip3Hour Precip3Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMin3Hour
        /// </summary>
        [DataMember(Name = "precipMin3Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMin3Hour PrecipMin3Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMax3Hour
        /// </summary>
        [DataMember(Name = "precipMax3Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMin3Hour PrecipMax3Hour { get; set; }

        /// <summary>
        /// Gets or Sets Precip4Hour
        /// </summary>
        [DataMember(Name = "precip4Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecip4Hour Precip4Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMin4Hour
        /// </summary>
        [DataMember(Name = "precipMin4Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMin4Hour PrecipMin4Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMax4Hour
        /// </summary>
        [DataMember(Name = "precipMax4Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMax4Hour PrecipMax4Hour { get; set; }

        /// <summary>
        /// Gets or Sets Precip5Hour
        /// </summary>
        [DataMember(Name = "precip5Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecip5Hour Precip5Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMin5Hour
        /// </summary>
        [DataMember(Name = "precipMin5Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMin5Hour PrecipMin5Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMax5Hour
        /// </summary>
        [DataMember(Name = "precipMax5Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMin5Hour PrecipMax5Hour { get; set; }

        /// <summary>
        /// Gets or Sets Precip6Hour
        /// </summary>
        [DataMember(Name = "precip6Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecip6Hour Precip6Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMin6Hour
        /// </summary>
        [DataMember(Name = "precipMin6Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMin6Hour PrecipMin6Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMax6Hour
        /// </summary>
        [DataMember(Name = "precipMax6Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMin6Hour PrecipMax6Hour { get; set; }

        /// <summary>
        /// Gets or Sets Precip7Hour
        /// </summary>
        [DataMember(Name = "precip7Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecip7Hour Precip7Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMin7Hour
        /// </summary>
        [DataMember(Name = "precipMin7Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMin7Hour PrecipMin7Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMax7Hour
        /// </summary>
        [DataMember(Name = "precipMax7Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMin7Hour PrecipMax7Hour { get; set; }

        /// <summary>
        /// Gets or Sets Precip8Hour
        /// </summary>
        [DataMember(Name = "precip8Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecip8Hour Precip8Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMin8Hour
        /// </summary>
        [DataMember(Name = "precipMin8Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMin8Hour PrecipMin8Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMax8Hour
        /// </summary>
        [DataMember(Name = "precipMax8Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMin8Hour PrecipMax8Hour { get; set; }

        /// <summary>
        /// Gets or Sets Precip9Hour
        /// </summary>
        [DataMember(Name = "precip9Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecip9Hour Precip9Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMin9Hour
        /// </summary>
        [DataMember(Name = "precipMin9Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMin9Hour PrecipMin9Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMax9Hour
        /// </summary>
        [DataMember(Name = "precipMax9Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMin9Hour PrecipMax9Hour { get; set; }

        /// <summary>
        /// Gets or Sets Precip10Hour
        /// </summary>
        [DataMember(Name = "precip10Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecip10Hour Precip10Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMin10Hour
        /// </summary>
        [DataMember(Name = "precipMin10Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMin10Hour PrecipMin10Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMax10Hour
        /// </summary>
        [DataMember(Name = "precipMax10Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMin10Hour PrecipMax10Hour { get; set; }

        /// <summary>
        /// Gets or Sets Precip11Hour
        /// </summary>
        [DataMember(Name = "precip11Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecip11Hour Precip11Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMin11Hour
        /// </summary>
        [DataMember(Name = "precipMin11Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMin11Hour PrecipMin11Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMax11Hour
        /// </summary>
        [DataMember(Name = "precipMax11Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMin11Hour PrecipMax11Hour { get; set; }

        /// <summary>
        /// Gets or Sets Precip12Hour
        /// </summary>
        [DataMember(Name = "precip12Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecip12Hour Precip12Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMin12Hour
        /// </summary>
        [DataMember(Name = "precipMin12Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMin12Hour PrecipMin12Hour { get; set; }

        /// <summary>
        /// Gets or Sets PrecipMax12Hour
        /// </summary>
        [DataMember(Name = "precipMax12Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecipMin12Hour PrecipMax12Hour { get; set; }

        /// <summary>
        /// Gets or Sets Precip24Hour
        /// </summary>
        [DataMember(Name = "precip24Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastPrecip24Hour Precip24Hour { get; set; }

        /// <summary>
        /// Gets or Sets Snow1Hour
        /// </summary>
        [DataMember(Name = "snow1Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnow1Hour Snow1Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMin1Hour
        /// </summary>
        [DataMember(Name = "snowMin1Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMin1Hour SnowMin1Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMax1Hour
        /// </summary>
        [DataMember(Name = "snowMax1Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMax1Hour SnowMax1Hour { get; set; }

        /// <summary>
        /// Gets or Sets Snow2Hour
        /// </summary>
        [DataMember(Name = "snow2Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnow2Hour Snow2Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMin2Hour
        /// </summary>
        [DataMember(Name = "snowMin2Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMin2Hour SnowMin2Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMax2Hour
        /// </summary>
        [DataMember(Name = "snowMax2Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMax2Hour SnowMax2Hour { get; set; }

        /// <summary>
        /// Gets or Sets Snow3Hour
        /// </summary>
        [DataMember(Name = "snow3Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnow3Hour Snow3Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMin3Hour
        /// </summary>
        [DataMember(Name = "snowMin3Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMin3Hour SnowMin3Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMax3Hour
        /// </summary>
        [DataMember(Name = "snowMax3Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMax3Hour SnowMax3Hour { get; set; }

        /// <summary>
        /// Gets or Sets Snow4Hour
        /// </summary>
        [DataMember(Name = "snow4Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnow4Hour Snow4Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMin4Hour
        /// </summary>
        [DataMember(Name = "snowMin4Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMin4Hour SnowMin4Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMax4Hour
        /// </summary>
        [DataMember(Name = "snowMax4Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMax4Hour SnowMax4Hour { get; set; }

        /// <summary>
        /// Gets or Sets Snow5Hour
        /// </summary>
        [DataMember(Name = "snow5Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnow5Hour Snow5Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMin5Hour
        /// </summary>
        [DataMember(Name = "snowMin5Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMin5Hour SnowMin5Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMax5Hour
        /// </summary>
        [DataMember(Name = "snowMax5Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMax5Hour SnowMax5Hour { get; set; }

        /// <summary>
        /// Gets or Sets Snow6Hour
        /// </summary>
        [DataMember(Name = "snow6Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnow6Hour Snow6Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMin6Hour
        /// </summary>
        [DataMember(Name = "snowMin6Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMin6Hour SnowMin6Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMax6Hour
        /// </summary>
        [DataMember(Name = "snowMax6Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMax6Hour SnowMax6Hour { get; set; }

        /// <summary>
        /// Gets or Sets Snow7Hour
        /// </summary>
        [DataMember(Name = "snow7Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnow7Hour Snow7Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMin7Hour
        /// </summary>
        [DataMember(Name = "snowMin7Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMin7Hour SnowMin7Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMax7Hour
        /// </summary>
        [DataMember(Name = "snowMax7Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMax7Hour SnowMax7Hour { get; set; }

        /// <summary>
        /// Gets or Sets Snow8Hour
        /// </summary>
        [DataMember(Name = "snow8Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnow8Hour Snow8Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMin8Hour
        /// </summary>
        [DataMember(Name = "snowMin8Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMin8Hour SnowMin8Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMax8Hour
        /// </summary>
        [DataMember(Name = "snowMax8Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMax8Hour SnowMax8Hour { get; set; }

        /// <summary>
        /// Gets or Sets Snow9Hour
        /// </summary>
        [DataMember(Name = "snow9Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnow9Hour Snow9Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMin9Hour
        /// </summary>
        [DataMember(Name = "snowMin9Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMin9Hour SnowMin9Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMax9Hour
        /// </summary>
        [DataMember(Name = "snowMax9Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMax9Hour SnowMax9Hour { get; set; }

        /// <summary>
        /// Gets or Sets Snow10Hour
        /// </summary>
        [DataMember(Name = "snow10Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnow10Hour Snow10Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMin10Hour
        /// </summary>
        [DataMember(Name = "snowMin10Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMin10Hour SnowMin10Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMax10Hour
        /// </summary>
        [DataMember(Name = "snowMax10Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMax10Hour SnowMax10Hour { get; set; }

        /// <summary>
        /// Gets or Sets Snow11Hour
        /// </summary>
        [DataMember(Name = "snow11Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnow11Hour Snow11Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMin11Hour
        /// </summary>
        [DataMember(Name = "snowMin11Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMin11Hour SnowMin11Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMax11Hour
        /// </summary>
        [DataMember(Name = "snowMax11Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMax11Hour SnowMax11Hour { get; set; }

        /// <summary>
        /// Gets or Sets Snow12Hour
        /// </summary>
        [DataMember(Name = "snow12Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnow12Hour Snow12Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMin12Hour
        /// </summary>
        [DataMember(Name = "snowMin12Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMin12Hour SnowMin12Hour { get; set; }

        /// <summary>
        /// Gets or Sets SnowMax12Hour
        /// </summary>
        [DataMember(Name = "snowMax12Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnowMax12Hour SnowMax12Hour { get; set; }

        /// <summary>
        /// Gets or Sets Snow24Hour
        /// </summary>
        [DataMember(Name = "snow24Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastSnow24Hour Snow24Hour { get; set; }

        /// <summary>
        /// Gets or Sets Temperature1Hour
        /// </summary>
        [DataMember(Name = "temperature1Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastTemperature1Hour Temperature1Hour { get; set; }

        /// <summary>
        /// Gets or Sets Temperature2Hour
        /// </summary>
        [DataMember(Name = "temperature2Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastTemperature1Hour Temperature2Hour { get; set; }

        /// <summary>
        /// Gets or Sets Temperature3Hour
        /// </summary>
        [DataMember(Name = "temperature3Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastTemperature1Hour Temperature3Hour { get; set; }

        /// <summary>
        /// Gets or Sets Temperature4Hour
        /// </summary>
        [DataMember(Name = "temperature4Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastTemperature1Hour Temperature4Hour { get; set; }

        /// <summary>
        /// Gets or Sets Temperature5Hour
        /// </summary>
        [DataMember(Name = "temperature5Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastTemperature1Hour Temperature5Hour { get; set; }

        /// <summary>
        /// Gets or Sets Temperature6Hour
        /// </summary>
        [DataMember(Name = "temperature6Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastTemperature1Hour Temperature6Hour { get; set; }

        /// <summary>
        /// Gets or Sets Temperature7Hour
        /// </summary>
        [DataMember(Name = "temperature7Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastTemperature1Hour Temperature7Hour { get; set; }

        /// <summary>
        /// Gets or Sets Temperature8Hour
        /// </summary>
        [DataMember(Name = "temperature8Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastTemperature1Hour Temperature8Hour { get; set; }

        /// <summary>
        /// Gets or Sets Temperature9Hour
        /// </summary>
        [DataMember(Name = "temperature9Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastTemperature1Hour Temperature9Hour { get; set; }

        /// <summary>
        /// Gets or Sets Temperature10Hour
        /// </summary>
        [DataMember(Name = "temperature10Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastTemperature1Hour Temperature10Hour { get; set; }

        /// <summary>
        /// Gets or Sets Temperature11Hour
        /// </summary>
        [DataMember(Name = "temperature11Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastTemperature1Hour Temperature11Hour { get; set; }

        /// <summary>
        /// Gets or Sets Temperature12Hour
        /// </summary>
        [DataMember(Name = "temperature12Hour", EmitDefaultValue = false)]
        public ServiceCapabilityDataForecastTemperature1Hour Temperature12Hour { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class ServiceCapabilityDataForecast {\n");
            sb.Append("  LastUpdateTime: ").Append(LastUpdateTime).Append("\n");
            sb.Append("  Vendor: ").Append(Vendor).Append("\n");
            sb.Append("  _Version: ").Append(_Version).Append("\n");
            sb.Append("  Precip1Hour: ").Append(Precip1Hour).Append("\n");
            sb.Append("  PrecipMin1Hour: ").Append(PrecipMin1Hour).Append("\n");
            sb.Append("  PrecipMax1Hour: ").Append(PrecipMax1Hour).Append("\n");
            sb.Append("  Precip2Hour: ").Append(Precip2Hour).Append("\n");
            sb.Append("  PrecipMin2Hour: ").Append(PrecipMin2Hour).Append("\n");
            sb.Append("  PrecipMax2Hour: ").Append(PrecipMax2Hour).Append("\n");
            sb.Append("  Precip3Hour: ").Append(Precip3Hour).Append("\n");
            sb.Append("  PrecipMin3Hour: ").Append(PrecipMin3Hour).Append("\n");
            sb.Append("  PrecipMax3Hour: ").Append(PrecipMax3Hour).Append("\n");
            sb.Append("  Precip4Hour: ").Append(Precip4Hour).Append("\n");
            sb.Append("  PrecipMin4Hour: ").Append(PrecipMin4Hour).Append("\n");
            sb.Append("  PrecipMax4Hour: ").Append(PrecipMax4Hour).Append("\n");
            sb.Append("  Precip5Hour: ").Append(Precip5Hour).Append("\n");
            sb.Append("  PrecipMin5Hour: ").Append(PrecipMin5Hour).Append("\n");
            sb.Append("  PrecipMax5Hour: ").Append(PrecipMax5Hour).Append("\n");
            sb.Append("  Precip6Hour: ").Append(Precip6Hour).Append("\n");
            sb.Append("  PrecipMin6Hour: ").Append(PrecipMin6Hour).Append("\n");
            sb.Append("  PrecipMax6Hour: ").Append(PrecipMax6Hour).Append("\n");
            sb.Append("  Precip7Hour: ").Append(Precip7Hour).Append("\n");
            sb.Append("  PrecipMin7Hour: ").Append(PrecipMin7Hour).Append("\n");
            sb.Append("  PrecipMax7Hour: ").Append(PrecipMax7Hour).Append("\n");
            sb.Append("  Precip8Hour: ").Append(Precip8Hour).Append("\n");
            sb.Append("  PrecipMin8Hour: ").Append(PrecipMin8Hour).Append("\n");
            sb.Append("  PrecipMax8Hour: ").Append(PrecipMax8Hour).Append("\n");
            sb.Append("  Precip9Hour: ").Append(Precip9Hour).Append("\n");
            sb.Append("  PrecipMin9Hour: ").Append(PrecipMin9Hour).Append("\n");
            sb.Append("  PrecipMax9Hour: ").Append(PrecipMax9Hour).Append("\n");
            sb.Append("  Precip10Hour: ").Append(Precip10Hour).Append("\n");
            sb.Append("  PrecipMin10Hour: ").Append(PrecipMin10Hour).Append("\n");
            sb.Append("  PrecipMax10Hour: ").Append(PrecipMax10Hour).Append("\n");
            sb.Append("  Precip11Hour: ").Append(Precip11Hour).Append("\n");
            sb.Append("  PrecipMin11Hour: ").Append(PrecipMin11Hour).Append("\n");
            sb.Append("  PrecipMax11Hour: ").Append(PrecipMax11Hour).Append("\n");
            sb.Append("  Precip12Hour: ").Append(Precip12Hour).Append("\n");
            sb.Append("  PrecipMin12Hour: ").Append(PrecipMin12Hour).Append("\n");
            sb.Append("  PrecipMax12Hour: ").Append(PrecipMax12Hour).Append("\n");
            sb.Append("  Precip24Hour: ").Append(Precip24Hour).Append("\n");
            sb.Append("  Snow1Hour: ").Append(Snow1Hour).Append("\n");
            sb.Append("  SnowMin1Hour: ").Append(SnowMin1Hour).Append("\n");
            sb.Append("  SnowMax1Hour: ").Append(SnowMax1Hour).Append("\n");
            sb.Append("  Snow2Hour: ").Append(Snow2Hour).Append("\n");
            sb.Append("  SnowMin2Hour: ").Append(SnowMin2Hour).Append("\n");
            sb.Append("  SnowMax2Hour: ").Append(SnowMax2Hour).Append("\n");
            sb.Append("  Snow3Hour: ").Append(Snow3Hour).Append("\n");
            sb.Append("  SnowMin3Hour: ").Append(SnowMin3Hour).Append("\n");
            sb.Append("  SnowMax3Hour: ").Append(SnowMax3Hour).Append("\n");
            sb.Append("  Snow4Hour: ").Append(Snow4Hour).Append("\n");
            sb.Append("  SnowMin4Hour: ").Append(SnowMin4Hour).Append("\n");
            sb.Append("  SnowMax4Hour: ").Append(SnowMax4Hour).Append("\n");
            sb.Append("  Snow5Hour: ").Append(Snow5Hour).Append("\n");
            sb.Append("  SnowMin5Hour: ").Append(SnowMin5Hour).Append("\n");
            sb.Append("  SnowMax5Hour: ").Append(SnowMax5Hour).Append("\n");
            sb.Append("  Snow6Hour: ").Append(Snow6Hour).Append("\n");
            sb.Append("  SnowMin6Hour: ").Append(SnowMin6Hour).Append("\n");
            sb.Append("  SnowMax6Hour: ").Append(SnowMax6Hour).Append("\n");
            sb.Append("  Snow7Hour: ").Append(Snow7Hour).Append("\n");
            sb.Append("  SnowMin7Hour: ").Append(SnowMin7Hour).Append("\n");
            sb.Append("  SnowMax7Hour: ").Append(SnowMax7Hour).Append("\n");
            sb.Append("  Snow8Hour: ").Append(Snow8Hour).Append("\n");
            sb.Append("  SnowMin8Hour: ").Append(SnowMin8Hour).Append("\n");
            sb.Append("  SnowMax8Hour: ").Append(SnowMax8Hour).Append("\n");
            sb.Append("  Snow9Hour: ").Append(Snow9Hour).Append("\n");
            sb.Append("  SnowMin9Hour: ").Append(SnowMin9Hour).Append("\n");
            sb.Append("  SnowMax9Hour: ").Append(SnowMax9Hour).Append("\n");
            sb.Append("  Snow10Hour: ").Append(Snow10Hour).Append("\n");
            sb.Append("  SnowMin10Hour: ").Append(SnowMin10Hour).Append("\n");
            sb.Append("  SnowMax10Hour: ").Append(SnowMax10Hour).Append("\n");
            sb.Append("  Snow11Hour: ").Append(Snow11Hour).Append("\n");
            sb.Append("  SnowMin11Hour: ").Append(SnowMin11Hour).Append("\n");
            sb.Append("  SnowMax11Hour: ").Append(SnowMax11Hour).Append("\n");
            sb.Append("  Snow12Hour: ").Append(Snow12Hour).Append("\n");
            sb.Append("  SnowMin12Hour: ").Append(SnowMin12Hour).Append("\n");
            sb.Append("  SnowMax12Hour: ").Append(SnowMax12Hour).Append("\n");
            sb.Append("  Snow24Hour: ").Append(Snow24Hour).Append("\n");
            sb.Append("  Temperature1Hour: ").Append(Temperature1Hour).Append("\n");
            sb.Append("  Temperature2Hour: ").Append(Temperature2Hour).Append("\n");
            sb.Append("  Temperature3Hour: ").Append(Temperature3Hour).Append("\n");
            sb.Append("  Temperature4Hour: ").Append(Temperature4Hour).Append("\n");
            sb.Append("  Temperature5Hour: ").Append(Temperature5Hour).Append("\n");
            sb.Append("  Temperature6Hour: ").Append(Temperature6Hour).Append("\n");
            sb.Append("  Temperature7Hour: ").Append(Temperature7Hour).Append("\n");
            sb.Append("  Temperature8Hour: ").Append(Temperature8Hour).Append("\n");
            sb.Append("  Temperature9Hour: ").Append(Temperature9Hour).Append("\n");
            sb.Append("  Temperature10Hour: ").Append(Temperature10Hour).Append("\n");
            sb.Append("  Temperature11Hour: ").Append(Temperature11Hour).Append("\n");
            sb.Append("  Temperature12Hour: ").Append(Temperature12Hour).Append("\n");
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
            return this.Equals(input as ServiceCapabilityDataForecast);
        }

        /// <summary>
        /// Returns true if ServiceCapabilityDataForecast instances are equal
        /// </summary>
        /// <param name="input">Instance of ServiceCapabilityDataForecast to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ServiceCapabilityDataForecast input)
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
                    this.Precip1Hour == input.Precip1Hour ||
                    (this.Precip1Hour != null &&
                    this.Precip1Hour.Equals(input.Precip1Hour))
                ) && 
                (
                    this.PrecipMin1Hour == input.PrecipMin1Hour ||
                    (this.PrecipMin1Hour != null &&
                    this.PrecipMin1Hour.Equals(input.PrecipMin1Hour))
                ) && 
                (
                    this.PrecipMax1Hour == input.PrecipMax1Hour ||
                    (this.PrecipMax1Hour != null &&
                    this.PrecipMax1Hour.Equals(input.PrecipMax1Hour))
                ) && 
                (
                    this.Precip2Hour == input.Precip2Hour ||
                    (this.Precip2Hour != null &&
                    this.Precip2Hour.Equals(input.Precip2Hour))
                ) && 
                (
                    this.PrecipMin2Hour == input.PrecipMin2Hour ||
                    (this.PrecipMin2Hour != null &&
                    this.PrecipMin2Hour.Equals(input.PrecipMin2Hour))
                ) && 
                (
                    this.PrecipMax2Hour == input.PrecipMax2Hour ||
                    (this.PrecipMax2Hour != null &&
                    this.PrecipMax2Hour.Equals(input.PrecipMax2Hour))
                ) && 
                (
                    this.Precip3Hour == input.Precip3Hour ||
                    (this.Precip3Hour != null &&
                    this.Precip3Hour.Equals(input.Precip3Hour))
                ) && 
                (
                    this.PrecipMin3Hour == input.PrecipMin3Hour ||
                    (this.PrecipMin3Hour != null &&
                    this.PrecipMin3Hour.Equals(input.PrecipMin3Hour))
                ) && 
                (
                    this.PrecipMax3Hour == input.PrecipMax3Hour ||
                    (this.PrecipMax3Hour != null &&
                    this.PrecipMax3Hour.Equals(input.PrecipMax3Hour))
                ) && 
                (
                    this.Precip4Hour == input.Precip4Hour ||
                    (this.Precip4Hour != null &&
                    this.Precip4Hour.Equals(input.Precip4Hour))
                ) && 
                (
                    this.PrecipMin4Hour == input.PrecipMin4Hour ||
                    (this.PrecipMin4Hour != null &&
                    this.PrecipMin4Hour.Equals(input.PrecipMin4Hour))
                ) && 
                (
                    this.PrecipMax4Hour == input.PrecipMax4Hour ||
                    (this.PrecipMax4Hour != null &&
                    this.PrecipMax4Hour.Equals(input.PrecipMax4Hour))
                ) && 
                (
                    this.Precip5Hour == input.Precip5Hour ||
                    (this.Precip5Hour != null &&
                    this.Precip5Hour.Equals(input.Precip5Hour))
                ) && 
                (
                    this.PrecipMin5Hour == input.PrecipMin5Hour ||
                    (this.PrecipMin5Hour != null &&
                    this.PrecipMin5Hour.Equals(input.PrecipMin5Hour))
                ) && 
                (
                    this.PrecipMax5Hour == input.PrecipMax5Hour ||
                    (this.PrecipMax5Hour != null &&
                    this.PrecipMax5Hour.Equals(input.PrecipMax5Hour))
                ) && 
                (
                    this.Precip6Hour == input.Precip6Hour ||
                    (this.Precip6Hour != null &&
                    this.Precip6Hour.Equals(input.Precip6Hour))
                ) && 
                (
                    this.PrecipMin6Hour == input.PrecipMin6Hour ||
                    (this.PrecipMin6Hour != null &&
                    this.PrecipMin6Hour.Equals(input.PrecipMin6Hour))
                ) && 
                (
                    this.PrecipMax6Hour == input.PrecipMax6Hour ||
                    (this.PrecipMax6Hour != null &&
                    this.PrecipMax6Hour.Equals(input.PrecipMax6Hour))
                ) && 
                (
                    this.Precip7Hour == input.Precip7Hour ||
                    (this.Precip7Hour != null &&
                    this.Precip7Hour.Equals(input.Precip7Hour))
                ) && 
                (
                    this.PrecipMin7Hour == input.PrecipMin7Hour ||
                    (this.PrecipMin7Hour != null &&
                    this.PrecipMin7Hour.Equals(input.PrecipMin7Hour))
                ) && 
                (
                    this.PrecipMax7Hour == input.PrecipMax7Hour ||
                    (this.PrecipMax7Hour != null &&
                    this.PrecipMax7Hour.Equals(input.PrecipMax7Hour))
                ) && 
                (
                    this.Precip8Hour == input.Precip8Hour ||
                    (this.Precip8Hour != null &&
                    this.Precip8Hour.Equals(input.Precip8Hour))
                ) && 
                (
                    this.PrecipMin8Hour == input.PrecipMin8Hour ||
                    (this.PrecipMin8Hour != null &&
                    this.PrecipMin8Hour.Equals(input.PrecipMin8Hour))
                ) && 
                (
                    this.PrecipMax8Hour == input.PrecipMax8Hour ||
                    (this.PrecipMax8Hour != null &&
                    this.PrecipMax8Hour.Equals(input.PrecipMax8Hour))
                ) && 
                (
                    this.Precip9Hour == input.Precip9Hour ||
                    (this.Precip9Hour != null &&
                    this.Precip9Hour.Equals(input.Precip9Hour))
                ) && 
                (
                    this.PrecipMin9Hour == input.PrecipMin9Hour ||
                    (this.PrecipMin9Hour != null &&
                    this.PrecipMin9Hour.Equals(input.PrecipMin9Hour))
                ) && 
                (
                    this.PrecipMax9Hour == input.PrecipMax9Hour ||
                    (this.PrecipMax9Hour != null &&
                    this.PrecipMax9Hour.Equals(input.PrecipMax9Hour))
                ) && 
                (
                    this.Precip10Hour == input.Precip10Hour ||
                    (this.Precip10Hour != null &&
                    this.Precip10Hour.Equals(input.Precip10Hour))
                ) && 
                (
                    this.PrecipMin10Hour == input.PrecipMin10Hour ||
                    (this.PrecipMin10Hour != null &&
                    this.PrecipMin10Hour.Equals(input.PrecipMin10Hour))
                ) && 
                (
                    this.PrecipMax10Hour == input.PrecipMax10Hour ||
                    (this.PrecipMax10Hour != null &&
                    this.PrecipMax10Hour.Equals(input.PrecipMax10Hour))
                ) && 
                (
                    this.Precip11Hour == input.Precip11Hour ||
                    (this.Precip11Hour != null &&
                    this.Precip11Hour.Equals(input.Precip11Hour))
                ) && 
                (
                    this.PrecipMin11Hour == input.PrecipMin11Hour ||
                    (this.PrecipMin11Hour != null &&
                    this.PrecipMin11Hour.Equals(input.PrecipMin11Hour))
                ) && 
                (
                    this.PrecipMax11Hour == input.PrecipMax11Hour ||
                    (this.PrecipMax11Hour != null &&
                    this.PrecipMax11Hour.Equals(input.PrecipMax11Hour))
                ) && 
                (
                    this.Precip12Hour == input.Precip12Hour ||
                    (this.Precip12Hour != null &&
                    this.Precip12Hour.Equals(input.Precip12Hour))
                ) && 
                (
                    this.PrecipMin12Hour == input.PrecipMin12Hour ||
                    (this.PrecipMin12Hour != null &&
                    this.PrecipMin12Hour.Equals(input.PrecipMin12Hour))
                ) && 
                (
                    this.PrecipMax12Hour == input.PrecipMax12Hour ||
                    (this.PrecipMax12Hour != null &&
                    this.PrecipMax12Hour.Equals(input.PrecipMax12Hour))
                ) && 
                (
                    this.Precip24Hour == input.Precip24Hour ||
                    (this.Precip24Hour != null &&
                    this.Precip24Hour.Equals(input.Precip24Hour))
                ) && 
                (
                    this.Snow1Hour == input.Snow1Hour ||
                    (this.Snow1Hour != null &&
                    this.Snow1Hour.Equals(input.Snow1Hour))
                ) && 
                (
                    this.SnowMin1Hour == input.SnowMin1Hour ||
                    (this.SnowMin1Hour != null &&
                    this.SnowMin1Hour.Equals(input.SnowMin1Hour))
                ) && 
                (
                    this.SnowMax1Hour == input.SnowMax1Hour ||
                    (this.SnowMax1Hour != null &&
                    this.SnowMax1Hour.Equals(input.SnowMax1Hour))
                ) && 
                (
                    this.Snow2Hour == input.Snow2Hour ||
                    (this.Snow2Hour != null &&
                    this.Snow2Hour.Equals(input.Snow2Hour))
                ) && 
                (
                    this.SnowMin2Hour == input.SnowMin2Hour ||
                    (this.SnowMin2Hour != null &&
                    this.SnowMin2Hour.Equals(input.SnowMin2Hour))
                ) && 
                (
                    this.SnowMax2Hour == input.SnowMax2Hour ||
                    (this.SnowMax2Hour != null &&
                    this.SnowMax2Hour.Equals(input.SnowMax2Hour))
                ) && 
                (
                    this.Snow3Hour == input.Snow3Hour ||
                    (this.Snow3Hour != null &&
                    this.Snow3Hour.Equals(input.Snow3Hour))
                ) && 
                (
                    this.SnowMin3Hour == input.SnowMin3Hour ||
                    (this.SnowMin3Hour != null &&
                    this.SnowMin3Hour.Equals(input.SnowMin3Hour))
                ) && 
                (
                    this.SnowMax3Hour == input.SnowMax3Hour ||
                    (this.SnowMax3Hour != null &&
                    this.SnowMax3Hour.Equals(input.SnowMax3Hour))
                ) && 
                (
                    this.Snow4Hour == input.Snow4Hour ||
                    (this.Snow4Hour != null &&
                    this.Snow4Hour.Equals(input.Snow4Hour))
                ) && 
                (
                    this.SnowMin4Hour == input.SnowMin4Hour ||
                    (this.SnowMin4Hour != null &&
                    this.SnowMin4Hour.Equals(input.SnowMin4Hour))
                ) && 
                (
                    this.SnowMax4Hour == input.SnowMax4Hour ||
                    (this.SnowMax4Hour != null &&
                    this.SnowMax4Hour.Equals(input.SnowMax4Hour))
                ) && 
                (
                    this.Snow5Hour == input.Snow5Hour ||
                    (this.Snow5Hour != null &&
                    this.Snow5Hour.Equals(input.Snow5Hour))
                ) && 
                (
                    this.SnowMin5Hour == input.SnowMin5Hour ||
                    (this.SnowMin5Hour != null &&
                    this.SnowMin5Hour.Equals(input.SnowMin5Hour))
                ) && 
                (
                    this.SnowMax5Hour == input.SnowMax5Hour ||
                    (this.SnowMax5Hour != null &&
                    this.SnowMax5Hour.Equals(input.SnowMax5Hour))
                ) && 
                (
                    this.Snow6Hour == input.Snow6Hour ||
                    (this.Snow6Hour != null &&
                    this.Snow6Hour.Equals(input.Snow6Hour))
                ) && 
                (
                    this.SnowMin6Hour == input.SnowMin6Hour ||
                    (this.SnowMin6Hour != null &&
                    this.SnowMin6Hour.Equals(input.SnowMin6Hour))
                ) && 
                (
                    this.SnowMax6Hour == input.SnowMax6Hour ||
                    (this.SnowMax6Hour != null &&
                    this.SnowMax6Hour.Equals(input.SnowMax6Hour))
                ) && 
                (
                    this.Snow7Hour == input.Snow7Hour ||
                    (this.Snow7Hour != null &&
                    this.Snow7Hour.Equals(input.Snow7Hour))
                ) && 
                (
                    this.SnowMin7Hour == input.SnowMin7Hour ||
                    (this.SnowMin7Hour != null &&
                    this.SnowMin7Hour.Equals(input.SnowMin7Hour))
                ) && 
                (
                    this.SnowMax7Hour == input.SnowMax7Hour ||
                    (this.SnowMax7Hour != null &&
                    this.SnowMax7Hour.Equals(input.SnowMax7Hour))
                ) && 
                (
                    this.Snow8Hour == input.Snow8Hour ||
                    (this.Snow8Hour != null &&
                    this.Snow8Hour.Equals(input.Snow8Hour))
                ) && 
                (
                    this.SnowMin8Hour == input.SnowMin8Hour ||
                    (this.SnowMin8Hour != null &&
                    this.SnowMin8Hour.Equals(input.SnowMin8Hour))
                ) && 
                (
                    this.SnowMax8Hour == input.SnowMax8Hour ||
                    (this.SnowMax8Hour != null &&
                    this.SnowMax8Hour.Equals(input.SnowMax8Hour))
                ) && 
                (
                    this.Snow9Hour == input.Snow9Hour ||
                    (this.Snow9Hour != null &&
                    this.Snow9Hour.Equals(input.Snow9Hour))
                ) && 
                (
                    this.SnowMin9Hour == input.SnowMin9Hour ||
                    (this.SnowMin9Hour != null &&
                    this.SnowMin9Hour.Equals(input.SnowMin9Hour))
                ) && 
                (
                    this.SnowMax9Hour == input.SnowMax9Hour ||
                    (this.SnowMax9Hour != null &&
                    this.SnowMax9Hour.Equals(input.SnowMax9Hour))
                ) && 
                (
                    this.Snow10Hour == input.Snow10Hour ||
                    (this.Snow10Hour != null &&
                    this.Snow10Hour.Equals(input.Snow10Hour))
                ) && 
                (
                    this.SnowMin10Hour == input.SnowMin10Hour ||
                    (this.SnowMin10Hour != null &&
                    this.SnowMin10Hour.Equals(input.SnowMin10Hour))
                ) && 
                (
                    this.SnowMax10Hour == input.SnowMax10Hour ||
                    (this.SnowMax10Hour != null &&
                    this.SnowMax10Hour.Equals(input.SnowMax10Hour))
                ) && 
                (
                    this.Snow11Hour == input.Snow11Hour ||
                    (this.Snow11Hour != null &&
                    this.Snow11Hour.Equals(input.Snow11Hour))
                ) && 
                (
                    this.SnowMin11Hour == input.SnowMin11Hour ||
                    (this.SnowMin11Hour != null &&
                    this.SnowMin11Hour.Equals(input.SnowMin11Hour))
                ) && 
                (
                    this.SnowMax11Hour == input.SnowMax11Hour ||
                    (this.SnowMax11Hour != null &&
                    this.SnowMax11Hour.Equals(input.SnowMax11Hour))
                ) && 
                (
                    this.Snow12Hour == input.Snow12Hour ||
                    (this.Snow12Hour != null &&
                    this.Snow12Hour.Equals(input.Snow12Hour))
                ) && 
                (
                    this.SnowMin12Hour == input.SnowMin12Hour ||
                    (this.SnowMin12Hour != null &&
                    this.SnowMin12Hour.Equals(input.SnowMin12Hour))
                ) && 
                (
                    this.SnowMax12Hour == input.SnowMax12Hour ||
                    (this.SnowMax12Hour != null &&
                    this.SnowMax12Hour.Equals(input.SnowMax12Hour))
                ) && 
                (
                    this.Snow24Hour == input.Snow24Hour ||
                    (this.Snow24Hour != null &&
                    this.Snow24Hour.Equals(input.Snow24Hour))
                ) && 
                (
                    this.Temperature1Hour == input.Temperature1Hour ||
                    (this.Temperature1Hour != null &&
                    this.Temperature1Hour.Equals(input.Temperature1Hour))
                ) && 
                (
                    this.Temperature2Hour == input.Temperature2Hour ||
                    (this.Temperature2Hour != null &&
                    this.Temperature2Hour.Equals(input.Temperature2Hour))
                ) && 
                (
                    this.Temperature3Hour == input.Temperature3Hour ||
                    (this.Temperature3Hour != null &&
                    this.Temperature3Hour.Equals(input.Temperature3Hour))
                ) && 
                (
                    this.Temperature4Hour == input.Temperature4Hour ||
                    (this.Temperature4Hour != null &&
                    this.Temperature4Hour.Equals(input.Temperature4Hour))
                ) && 
                (
                    this.Temperature5Hour == input.Temperature5Hour ||
                    (this.Temperature5Hour != null &&
                    this.Temperature5Hour.Equals(input.Temperature5Hour))
                ) && 
                (
                    this.Temperature6Hour == input.Temperature6Hour ||
                    (this.Temperature6Hour != null &&
                    this.Temperature6Hour.Equals(input.Temperature6Hour))
                ) && 
                (
                    this.Temperature7Hour == input.Temperature7Hour ||
                    (this.Temperature7Hour != null &&
                    this.Temperature7Hour.Equals(input.Temperature7Hour))
                ) && 
                (
                    this.Temperature8Hour == input.Temperature8Hour ||
                    (this.Temperature8Hour != null &&
                    this.Temperature8Hour.Equals(input.Temperature8Hour))
                ) && 
                (
                    this.Temperature9Hour == input.Temperature9Hour ||
                    (this.Temperature9Hour != null &&
                    this.Temperature9Hour.Equals(input.Temperature9Hour))
                ) && 
                (
                    this.Temperature10Hour == input.Temperature10Hour ||
                    (this.Temperature10Hour != null &&
                    this.Temperature10Hour.Equals(input.Temperature10Hour))
                ) && 
                (
                    this.Temperature11Hour == input.Temperature11Hour ||
                    (this.Temperature11Hour != null &&
                    this.Temperature11Hour.Equals(input.Temperature11Hour))
                ) && 
                (
                    this.Temperature12Hour == input.Temperature12Hour ||
                    (this.Temperature12Hour != null &&
                    this.Temperature12Hour.Equals(input.Temperature12Hour))
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
                if (this.Precip1Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Precip1Hour.GetHashCode();
                }
                if (this.PrecipMin1Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMin1Hour.GetHashCode();
                }
                if (this.PrecipMax1Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMax1Hour.GetHashCode();
                }
                if (this.Precip2Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Precip2Hour.GetHashCode();
                }
                if (this.PrecipMin2Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMin2Hour.GetHashCode();
                }
                if (this.PrecipMax2Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMax2Hour.GetHashCode();
                }
                if (this.Precip3Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Precip3Hour.GetHashCode();
                }
                if (this.PrecipMin3Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMin3Hour.GetHashCode();
                }
                if (this.PrecipMax3Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMax3Hour.GetHashCode();
                }
                if (this.Precip4Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Precip4Hour.GetHashCode();
                }
                if (this.PrecipMin4Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMin4Hour.GetHashCode();
                }
                if (this.PrecipMax4Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMax4Hour.GetHashCode();
                }
                if (this.Precip5Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Precip5Hour.GetHashCode();
                }
                if (this.PrecipMin5Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMin5Hour.GetHashCode();
                }
                if (this.PrecipMax5Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMax5Hour.GetHashCode();
                }
                if (this.Precip6Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Precip6Hour.GetHashCode();
                }
                if (this.PrecipMin6Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMin6Hour.GetHashCode();
                }
                if (this.PrecipMax6Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMax6Hour.GetHashCode();
                }
                if (this.Precip7Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Precip7Hour.GetHashCode();
                }
                if (this.PrecipMin7Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMin7Hour.GetHashCode();
                }
                if (this.PrecipMax7Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMax7Hour.GetHashCode();
                }
                if (this.Precip8Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Precip8Hour.GetHashCode();
                }
                if (this.PrecipMin8Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMin8Hour.GetHashCode();
                }
                if (this.PrecipMax8Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMax8Hour.GetHashCode();
                }
                if (this.Precip9Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Precip9Hour.GetHashCode();
                }
                if (this.PrecipMin9Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMin9Hour.GetHashCode();
                }
                if (this.PrecipMax9Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMax9Hour.GetHashCode();
                }
                if (this.Precip10Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Precip10Hour.GetHashCode();
                }
                if (this.PrecipMin10Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMin10Hour.GetHashCode();
                }
                if (this.PrecipMax10Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMax10Hour.GetHashCode();
                }
                if (this.Precip11Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Precip11Hour.GetHashCode();
                }
                if (this.PrecipMin11Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMin11Hour.GetHashCode();
                }
                if (this.PrecipMax11Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMax11Hour.GetHashCode();
                }
                if (this.Precip12Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Precip12Hour.GetHashCode();
                }
                if (this.PrecipMin12Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMin12Hour.GetHashCode();
                }
                if (this.PrecipMax12Hour != null)
                {
                    hashCode = (hashCode * 59) + this.PrecipMax12Hour.GetHashCode();
                }
                if (this.Precip24Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Precip24Hour.GetHashCode();
                }
                if (this.Snow1Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Snow1Hour.GetHashCode();
                }
                if (this.SnowMin1Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMin1Hour.GetHashCode();
                }
                if (this.SnowMax1Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMax1Hour.GetHashCode();
                }
                if (this.Snow2Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Snow2Hour.GetHashCode();
                }
                if (this.SnowMin2Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMin2Hour.GetHashCode();
                }
                if (this.SnowMax2Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMax2Hour.GetHashCode();
                }
                if (this.Snow3Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Snow3Hour.GetHashCode();
                }
                if (this.SnowMin3Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMin3Hour.GetHashCode();
                }
                if (this.SnowMax3Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMax3Hour.GetHashCode();
                }
                if (this.Snow4Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Snow4Hour.GetHashCode();
                }
                if (this.SnowMin4Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMin4Hour.GetHashCode();
                }
                if (this.SnowMax4Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMax4Hour.GetHashCode();
                }
                if (this.Snow5Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Snow5Hour.GetHashCode();
                }
                if (this.SnowMin5Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMin5Hour.GetHashCode();
                }
                if (this.SnowMax5Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMax5Hour.GetHashCode();
                }
                if (this.Snow6Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Snow6Hour.GetHashCode();
                }
                if (this.SnowMin6Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMin6Hour.GetHashCode();
                }
                if (this.SnowMax6Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMax6Hour.GetHashCode();
                }
                if (this.Snow7Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Snow7Hour.GetHashCode();
                }
                if (this.SnowMin7Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMin7Hour.GetHashCode();
                }
                if (this.SnowMax7Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMax7Hour.GetHashCode();
                }
                if (this.Snow8Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Snow8Hour.GetHashCode();
                }
                if (this.SnowMin8Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMin8Hour.GetHashCode();
                }
                if (this.SnowMax8Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMax8Hour.GetHashCode();
                }
                if (this.Snow9Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Snow9Hour.GetHashCode();
                }
                if (this.SnowMin9Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMin9Hour.GetHashCode();
                }
                if (this.SnowMax9Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMax9Hour.GetHashCode();
                }
                if (this.Snow10Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Snow10Hour.GetHashCode();
                }
                if (this.SnowMin10Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMin10Hour.GetHashCode();
                }
                if (this.SnowMax10Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMax10Hour.GetHashCode();
                }
                if (this.Snow11Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Snow11Hour.GetHashCode();
                }
                if (this.SnowMin11Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMin11Hour.GetHashCode();
                }
                if (this.SnowMax11Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMax11Hour.GetHashCode();
                }
                if (this.Snow12Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Snow12Hour.GetHashCode();
                }
                if (this.SnowMin12Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMin12Hour.GetHashCode();
                }
                if (this.SnowMax12Hour != null)
                {
                    hashCode = (hashCode * 59) + this.SnowMax12Hour.GetHashCode();
                }
                if (this.Snow24Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Snow24Hour.GetHashCode();
                }
                if (this.Temperature1Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Temperature1Hour.GetHashCode();
                }
                if (this.Temperature2Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Temperature2Hour.GetHashCode();
                }
                if (this.Temperature3Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Temperature3Hour.GetHashCode();
                }
                if (this.Temperature4Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Temperature4Hour.GetHashCode();
                }
                if (this.Temperature5Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Temperature5Hour.GetHashCode();
                }
                if (this.Temperature6Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Temperature6Hour.GetHashCode();
                }
                if (this.Temperature7Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Temperature7Hour.GetHashCode();
                }
                if (this.Temperature8Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Temperature8Hour.GetHashCode();
                }
                if (this.Temperature9Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Temperature9Hour.GetHashCode();
                }
                if (this.Temperature10Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Temperature10Hour.GetHashCode();
                }
                if (this.Temperature11Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Temperature11Hour.GetHashCode();
                }
                if (this.Temperature12Hour != null)
                {
                    hashCode = (hashCode * 59) + this.Temperature12Hour.GetHashCode();
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
