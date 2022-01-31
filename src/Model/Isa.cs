/*
 * SmartThings API
 *
 * # Overview  This is the reference documentation for the SmartThings API.  The SmartThings API, a RESTful API, provides a method for your integration to communicate with the SmartThings Platform. The API is the core of the platform. It is used to control devices, create Automations, manage Locations, retrieve user and device information; if you want to communicate with the SmartThings platform, you’ll be using the SmartThings API. All responses are sent as [JSON](http://www.json.org/).  The SmartThings API consists of several endpoints, including Rules, Locations, Devices, and more. Even though each of these endpoints are not standalone APIs, you may hear them referred to as such. For example, the Rules API is used to build Automations.  # Authentication  Before using the SmartThings API, you’ll need to obtain an Authorization Token. All SmartThings resources are protected with [OAuth 2.0 Bearer Tokens](https://tools.ietf.org/html/rfc6750#section-2.1) sent on the request as an `Authorization: Bearer <TOKEN>` header. Operations require specific OAuth scopes that specify the exact permissions authorized by the user.  ## Authorization token types  There are two types of tokens:   * SmartApp tokens   * Personal access tokens (PAT).  ### SmartApp tokens  SmartApp tokens are used to communicate between third-party integrations, or SmartApps, and the SmartThings API. When a SmartApp is called by the SmartThings platform, it is sent an authorization token that can be used to interact with the SmartThings API.  ### Personal access tokens  Personal access tokens are used to authorize interaction with the API for non-SmartApp use cases. When creating personal access tokens, you can specifiy the permissions granted to the token. These permissions define the OAuth2 scopes for the personal access token.  Personal access tokesn can be created and managed on the [personal access tokens page](https://account.smartthings.com/tokens).  ## OAuth2 scopes  Operations may be protected by one or more OAuth security schemes, which specify the required permissions. Each scope for a given scheme is required. If multiple schemes are specified (uncommon), you may use either scheme.  SmartApp token scopes are derived from the permissions requested by the SmartApp and granted by the end-user during installation. Personal access token scopes are associated with the specific permissions authorized when the token is created.  Scopes are generally in the form `permission:entity-type:entity-id`.  **An `*` used for the `entity-id` specifies that the permission may be applied to all entities that the token type has access to, or may be replaced with a specific ID.**  For more information about authrization and permissions, visit the [Authorization section](https://developer-preview.smartthings.com/docs/advanced/authorization-and-permissions) in the SmartThings documentation.  <!- - ReDoc-Inject: <security-definitions> - ->  # Errors  The SmartThings API uses conventional HTTP response codes to indicate the success or failure of a request.  In general:  * A `2XX` response code indicates success * A `4XX` response code indicates an error given the inputs for the request * A `5XX` response code indicates a failure on the SmartThings platform  API errors will contain a JSON response body with more information about the error:  ```json {   \"requestId\": \"031fec1a-f19f-470a-a7da-710569082846\"   \"error\": {     \"code\": \"ConstraintViolationError\",     \"message\": \"Validation errors occurred while process your request.\",     \"details\": [       { \"code\": \"PatternError\", \"target\": \"latitude\", \"message\": \"Invalid format.\" },       { \"code\": \"SizeError\", \"target\": \"name\", \"message\": \"Too small.\" },       { \"code\": \"SizeError\", \"target\": \"description\", \"message\": \"Too big.\" }     ]   } } ```  ## Error Response Body  The error response attributes are:  | Property | Type | Required | Description | | - -- | - -- | - -- | - -- | | requestId | String | No | A request identifier that can be used to correlate an error to additional logging on the SmartThings servers. | error | Error | **Yes** | The Error object, documented below.  ## Error Object  The Error object contains the following attributes:  | Property | Type | Required | Description | | - -- | - -- | - -- | - -- | | code | String | **Yes** | A SmartThings-defined error code that serves as a more specific indicator of the error than the HTTP error code specified in the response. See [SmartThings Error Codes](#section/Errors/SmartThings-Error-Codes) for more information. | message | String | **Yes** | A description of the error, intended to aid debugging of error responses. | target | String | No | The target of the error. For example, this could be the name of the property that caused the error. | details | Error[] | No | An array of Error objects that typically represent distinct, related errors that occurred during the request. As an optional attribute, this may be null or an empty array.  ## Standard HTTP Error Codes  The following table lists the most common HTTP error responses:  | Code | Name | Description | | - -- | - -- | - -- | | 400 | Bad Request | The client has issued an invalid request. This is commonly used to specify validation errors in a request payload. | 401 | Unauthorized | Authorization for the API is required, but the request has not been authenticated. | 403 | Forbidden | The request has been authenticated but does not have appropriate permissions, or a requested resource is not found. | 404 | Not Found | The requested path does not exist. | 406 | Not Acceptable | The client has requested a MIME type via the Accept header for a value not supported by the server. | 415 | Unsupported Media Type | The client has defined a contentType header that is not supported by the server. | 422 | Unprocessable Entity | The client has made a valid request, but the server cannot process it. This is often used for APIs for which certain limits have been exceeded. | 429 | Too Many Requests | The client has exceeded the number of requests allowed for a given time window. | 500 | Internal Server Error | An unexpected error on the SmartThings servers has occurred. These errors are generally rare. | 501 | Not Implemented | The client request was valid and understood by the server, but the requested feature has yet to be implemented. These errors are generally rare.  ## SmartThings Error Codes  SmartThings specifies several standard custom error codes. These provide more information than the standard HTTP error response codes. The following table lists the standard SmartThings error codes and their descriptions:  | Code | Typical HTTP Status Codes | Description | | - -- | - -- | - -- | | PatternError | 400, 422 | The client has provided input that does not match the expected pattern. | ConstraintViolationError | 422 | The client has provided input that has violated one or more constraints. | NotNullError | 422 | The client has provided a null input for a field that is required to be non-null. | NullError | 422 | The client has provided an input for a field that is required to be null. | NotEmptyError | 422 | The client has provided an empty input for a field that is required to be non-empty. | SizeError | 400, 422 | The client has provided a value that does not meet size restrictions. | Unexpected Error | 500 | A non-recoverable error condition has occurred. A problem occurred on the SmartThings server that is no fault of the client. | UnprocessableEntityError | 422 | The client has sent a malformed request body. | TooManyRequestError | 429 | The client issued too many requests too quickly. | LimitError | 422 | The client has exceeded certain limits an API enforces. | UnsupportedOperationError | 400, 422 | The client has issued a request to a feature that currently isn't supported by the SmartThings platform. These errors are generally rare.  ## Custom Error Codes  An API may define its own error codes where appropriate. Custom error codes are documented in each API endpoint's documentation section.  # Warnings The SmartThings API issues warning messages via standard HTTP Warning headers. These messages do not represent a request failure, but provide additional information that the requester might want to act upon. For example, a warning will be issued if you are using an old API version.  # API Versions  The SmartThings API supports both path and header-based versioning. The following are equivalent:  - https://api.smartthings.com/v1/locations - https://api.smartthings.com/locations with header `Accept: application/vnd.smartthings+json;v=1`  Currently, only version 1 is available.  # Paging  Operations that return a list of objects return a paginated response. The `_links` object contains the items returned, and links to the next and previous result page, if applicable.  ```json {   \"items\": [     {       \"locationId\": \"6b3d1909-1e1c-43ec-adc2-5f941de4fbf9\",       \"name\": \"Home\"     },     {       \"locationId\": \"6b3d1909-1e1c-43ec-adc2-5f94d6g4fbf9\",       \"name\": \"Work\"     }     ....   ],   \"_links\": {     \"next\": {       \"href\": \"https://api.smartthings.com/v1/locations?page=3\"     },     \"previous\": {       \"href\": \"https://api.smartthings.com/v1/locations?page=1\"     }   } } ```  # Localization  Some SmartThings APIs support localization. Specific information regarding localization endpoints are documented in the API itself. However, the following applies to all endpoints that support localization.  ## Fallback Patterns  When making a request with the `Accept-Language` header, the following fallback pattern is observed: 1. Response will be translated with exact locale tag. 2. If a translation does not exist for the requested language and region, the translation for the language will be returned. 3. If a translation does not exist for the language, English (en) will be returned. 4. Finally, an untranslated response will be returned in the absense of the above translations.  ## Accept-Language Header The format of the `Accept-Language` header follows what is defined in [RFC 7231, section 5.3.5](https://tools.ietf.org/html/rfc7231#section-5.3.5)  ## Content-Language The `Content-Language` header should be set on the response from the server to indicate which translation was given back to the client. The absense of the header indicates that the server did not recieve a request with the `Accept-Language` header set. 
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
    /// Isa
    /// </summary>
    [DataContract(Name = "Isa")]
    public partial class Isa : IEquatable<Isa>, IValidatableObject
    {
        /// <summary>
        /// connection status between partner and ST platform
        /// </summary>
        /// <value>connection status between partner and ST platform</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum PartnerSTConnectionEnum
        {
            /// <summary>
            /// Enum Connected for value: connected
            /// </summary>
            [EnumMember(Value = "connected")]
            Connected = 1,

            /// <summary>
            /// Enum Disconnected for value: disconnected
            /// </summary>
            [EnumMember(Value = "disconnected")]
            Disconnected = 2

        }


        /// <summary>
        /// connection status between partner and ST platform
        /// </summary>
        /// <value>connection status between partner and ST platform</value>
        [DataMember(Name = "partnerSTConnection", EmitDefaultValue = false)]
        public PartnerSTConnectionEnum? PartnerSTConnection { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Isa" /> class.
        /// </summary>
        /// <param name="pageType">Possible values - __requiresLogin__ or __loggedIn__. These two values determine what fields are returned in this response. If value is \&quot;requiresLogin\&quot;, only \&quot;oAuthLink\&quot; is returned in the response. If value is \&quot;loggedIn\&quot;, only isaId, partnerName, appName, devices and icons are returned..</param>
        /// <param name="isaId">isaId (Installed App Id).</param>
        /// <param name="endpointAppId">endpoint app id of the installed smart app.</param>
        /// <param name="partnerName">partner or brand name eg LIFX Inc..</param>
        /// <param name="appName">Connector name. eg Lifx (Connect).</param>
        /// <param name="icon">url of partner icon.</param>
        /// <param name="icon2x">url of partner icon in 2x dimensions.</param>
        /// <param name="icon3x">url of partner icon in 3x dimensions.</param>
        /// <param name="locationId">location of the installed smart app.</param>
        /// <param name="devices">devices.</param>
        /// <param name="oAuthLink">generated oAuth link for the user to login to partner server. This will only be returned when the user is not logged in..</param>
        /// <param name="viperAppLinks">viperAppLinks.</param>
        /// <param name="partnerSTConnection">connection status between partner and ST platform.</param>
        public Isa(string pageType = default(string), string isaId = default(string), string endpointAppId = default(string), string partnerName = default(string), string appName = default(string), string icon = default(string), string icon2x = default(string), string icon3x = default(string), string locationId = default(string), List<DeviceResults> devices = default(List<DeviceResults>), string oAuthLink = default(string), ViperAppLinks viperAppLinks = default(ViperAppLinks), PartnerSTConnectionEnum? partnerSTConnection = default(PartnerSTConnectionEnum?))
        {
            this.PageType = pageType;
            this.IsaId = isaId;
            this.EndpointAppId = endpointAppId;
            this.PartnerName = partnerName;
            this.AppName = appName;
            this.Icon = icon;
            this.Icon2x = icon2x;
            this.Icon3x = icon3x;
            this.LocationId = locationId;
            this.Devices = devices;
            this.OAuthLink = oAuthLink;
            this.ViperAppLinks = viperAppLinks;
            this.PartnerSTConnection = partnerSTConnection;
        }

        /// <summary>
        /// Possible values - __requiresLogin__ or __loggedIn__. These two values determine what fields are returned in this response. If value is \&quot;requiresLogin\&quot;, only \&quot;oAuthLink\&quot; is returned in the response. If value is \&quot;loggedIn\&quot;, only isaId, partnerName, appName, devices and icons are returned.
        /// </summary>
        /// <value>Possible values - __requiresLogin__ or __loggedIn__. These two values determine what fields are returned in this response. If value is \&quot;requiresLogin\&quot;, only \&quot;oAuthLink\&quot; is returned in the response. If value is \&quot;loggedIn\&quot;, only isaId, partnerName, appName, devices and icons are returned.</value>
        [DataMember(Name = "pageType", EmitDefaultValue = false)]
        public string PageType { get; set; }

        /// <summary>
        /// isaId (Installed App Id)
        /// </summary>
        /// <value>isaId (Installed App Id)</value>
        [DataMember(Name = "isaId", EmitDefaultValue = false)]
        public string IsaId { get; set; }

        /// <summary>
        /// endpoint app id of the installed smart app
        /// </summary>
        /// <value>endpoint app id of the installed smart app</value>
        [DataMember(Name = "endpointAppId", EmitDefaultValue = false)]
        public string EndpointAppId { get; set; }

        /// <summary>
        /// partner or brand name eg LIFX Inc.
        /// </summary>
        /// <value>partner or brand name eg LIFX Inc.</value>
        [DataMember(Name = "partnerName", EmitDefaultValue = false)]
        public string PartnerName { get; set; }

        /// <summary>
        /// Connector name. eg Lifx (Connect)
        /// </summary>
        /// <value>Connector name. eg Lifx (Connect)</value>
        [DataMember(Name = "appName", EmitDefaultValue = false)]
        public string AppName { get; set; }

        /// <summary>
        /// url of partner icon
        /// </summary>
        /// <value>url of partner icon</value>
        [DataMember(Name = "icon", EmitDefaultValue = false)]
        public string Icon { get; set; }

        /// <summary>
        /// url of partner icon in 2x dimensions
        /// </summary>
        /// <value>url of partner icon in 2x dimensions</value>
        [DataMember(Name = "icon2x", EmitDefaultValue = false)]
        public string Icon2x { get; set; }

        /// <summary>
        /// url of partner icon in 3x dimensions
        /// </summary>
        /// <value>url of partner icon in 3x dimensions</value>
        [DataMember(Name = "icon3x", EmitDefaultValue = false)]
        public string Icon3x { get; set; }

        /// <summary>
        /// location of the installed smart app
        /// </summary>
        /// <value>location of the installed smart app</value>
        [DataMember(Name = "locationId", EmitDefaultValue = false)]
        public string LocationId { get; set; }

        /// <summary>
        /// Gets or Sets Devices
        /// </summary>
        [DataMember(Name = "devices", EmitDefaultValue = false)]
        public List<DeviceResults> Devices { get; set; }

        /// <summary>
        /// generated oAuth link for the user to login to partner server. This will only be returned when the user is not logged in.
        /// </summary>
        /// <value>generated oAuth link for the user to login to partner server. This will only be returned when the user is not logged in.</value>
        [DataMember(Name = "oAuthLink", EmitDefaultValue = false)]
        public string OAuthLink { get; set; }

        /// <summary>
        /// Gets or Sets ViperAppLinks
        /// </summary>
        [DataMember(Name = "viperAppLinks", EmitDefaultValue = false)]
        public ViperAppLinks ViperAppLinks { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Isa {\n");
            sb.Append("  PageType: ").Append(PageType).Append("\n");
            sb.Append("  IsaId: ").Append(IsaId).Append("\n");
            sb.Append("  EndpointAppId: ").Append(EndpointAppId).Append("\n");
            sb.Append("  PartnerName: ").Append(PartnerName).Append("\n");
            sb.Append("  AppName: ").Append(AppName).Append("\n");
            sb.Append("  Icon: ").Append(Icon).Append("\n");
            sb.Append("  Icon2x: ").Append(Icon2x).Append("\n");
            sb.Append("  Icon3x: ").Append(Icon3x).Append("\n");
            sb.Append("  LocationId: ").Append(LocationId).Append("\n");
            sb.Append("  Devices: ").Append(Devices).Append("\n");
            sb.Append("  OAuthLink: ").Append(OAuthLink).Append("\n");
            sb.Append("  ViperAppLinks: ").Append(ViperAppLinks).Append("\n");
            sb.Append("  PartnerSTConnection: ").Append(PartnerSTConnection).Append("\n");
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
            return this.Equals(input as Isa);
        }

        /// <summary>
        /// Returns true if Isa instances are equal
        /// </summary>
        /// <param name="input">Instance of Isa to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Isa input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.PageType == input.PageType ||
                    (this.PageType != null &&
                    this.PageType.Equals(input.PageType))
                ) && 
                (
                    this.IsaId == input.IsaId ||
                    (this.IsaId != null &&
                    this.IsaId.Equals(input.IsaId))
                ) && 
                (
                    this.EndpointAppId == input.EndpointAppId ||
                    (this.EndpointAppId != null &&
                    this.EndpointAppId.Equals(input.EndpointAppId))
                ) && 
                (
                    this.PartnerName == input.PartnerName ||
                    (this.PartnerName != null &&
                    this.PartnerName.Equals(input.PartnerName))
                ) && 
                (
                    this.AppName == input.AppName ||
                    (this.AppName != null &&
                    this.AppName.Equals(input.AppName))
                ) && 
                (
                    this.Icon == input.Icon ||
                    (this.Icon != null &&
                    this.Icon.Equals(input.Icon))
                ) && 
                (
                    this.Icon2x == input.Icon2x ||
                    (this.Icon2x != null &&
                    this.Icon2x.Equals(input.Icon2x))
                ) && 
                (
                    this.Icon3x == input.Icon3x ||
                    (this.Icon3x != null &&
                    this.Icon3x.Equals(input.Icon3x))
                ) && 
                (
                    this.LocationId == input.LocationId ||
                    (this.LocationId != null &&
                    this.LocationId.Equals(input.LocationId))
                ) && 
                (
                    this.Devices == input.Devices ||
                    this.Devices != null &&
                    input.Devices != null &&
                    this.Devices.SequenceEqual(input.Devices)
                ) && 
                (
                    this.OAuthLink == input.OAuthLink ||
                    (this.OAuthLink != null &&
                    this.OAuthLink.Equals(input.OAuthLink))
                ) && 
                (
                    this.ViperAppLinks == input.ViperAppLinks ||
                    (this.ViperAppLinks != null &&
                    this.ViperAppLinks.Equals(input.ViperAppLinks))
                ) && 
                (
                    this.PartnerSTConnection == input.PartnerSTConnection ||
                    this.PartnerSTConnection.Equals(input.PartnerSTConnection)
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
                if (this.PageType != null)
                {
                    hashCode = (hashCode * 59) + this.PageType.GetHashCode();
                }
                if (this.IsaId != null)
                {
                    hashCode = (hashCode * 59) + this.IsaId.GetHashCode();
                }
                if (this.EndpointAppId != null)
                {
                    hashCode = (hashCode * 59) + this.EndpointAppId.GetHashCode();
                }
                if (this.PartnerName != null)
                {
                    hashCode = (hashCode * 59) + this.PartnerName.GetHashCode();
                }
                if (this.AppName != null)
                {
                    hashCode = (hashCode * 59) + this.AppName.GetHashCode();
                }
                if (this.Icon != null)
                {
                    hashCode = (hashCode * 59) + this.Icon.GetHashCode();
                }
                if (this.Icon2x != null)
                {
                    hashCode = (hashCode * 59) + this.Icon2x.GetHashCode();
                }
                if (this.Icon3x != null)
                {
                    hashCode = (hashCode * 59) + this.Icon3x.GetHashCode();
                }
                if (this.LocationId != null)
                {
                    hashCode = (hashCode * 59) + this.LocationId.GetHashCode();
                }
                if (this.Devices != null)
                {
                    hashCode = (hashCode * 59) + this.Devices.GetHashCode();
                }
                if (this.OAuthLink != null)
                {
                    hashCode = (hashCode * 59) + this.OAuthLink.GetHashCode();
                }
                if (this.ViperAppLinks != null)
                {
                    hashCode = (hashCode * 59) + this.ViperAppLinks.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.PartnerSTConnection.GetHashCode();
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
