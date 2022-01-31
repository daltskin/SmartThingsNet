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
    /// PostEndpointApp
    /// </summary>
    [DataContract(Name = "PostEndpointApp")]
    public partial class PostEndpointApp : IEquatable<PostEndpointApp>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostEndpointApp" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected PostEndpointApp() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="PostEndpointApp" /> class.
        /// </summary>
        /// <param name="appName">The name of the SmartThings Schema App.</param>
        /// <param name="partnerName">The name of the partner/brand (required).</param>
        /// <param name="oAuthAuthorizationUrl">oAuth authorization url of the partner (required).</param>
        /// <param name="lambdaArn">lambda arn of the partner for US region (default).</param>
        /// <param name="lambdaArnEU">lambda arn of the partner for EU region.</param>
        /// <param name="lambdaArnAP">lambda arn of the partner for AP region.</param>
        /// <param name="lambdaArnCN">lambda arn of the partner for CN region.</param>
        /// <param name="icon">url of partner icon.</param>
        /// <param name="icon2x">url of partner icon in 2x dimensions.</param>
        /// <param name="icon3x">url of partner icon in 3x dimensions.</param>
        /// <param name="endpointAppId">SmartThings Schema App id for the partner.</param>
        /// <param name="oAuthClientId">Client id for the partner oAuth (required).</param>
        /// <param name="oAuthClientSecret">Client secret for the partner oAuth (required).</param>
        /// <param name="oAuthTokenUrl">oAuth token refresh url of the partner (required).</param>
        /// <param name="oAuthScope">oAuth scope for the partner. Example \&quot;remote_control:all\&quot; for Lifx.</param>
        /// <param name="userId">user id for the partner.</param>
        /// <param name="hostingType">Possible values - \&quot;lambda\&quot; or \&quot;webhook\&quot; (required).</param>
        /// <param name="schemaType">Possible values - \&quot;alexa-schema\&quot;, \&quot;st-schema\&quot;, \&quot;google-schema\&quot;.</param>
        /// <param name="webhookUrl">webhook url for the partner.</param>
        /// <param name="certificationStatus">Possible values - \&quot;\&quot;, \&quot;cst\&quot;, \&quot;wwst\&quot;, \&quot;review\&quot;.</param>
        /// <param name="userEmail">Email for the partner.</param>
        /// <param name="viperAppLinks">viperAppLinks.</param>
        public PostEndpointApp(string appName = default(string), string partnerName = default(string), string oAuthAuthorizationUrl = default(string), string lambdaArn = default(string), string lambdaArnEU = default(string), string lambdaArnAP = default(string), string lambdaArnCN = default(string), string icon = default(string), string icon2x = default(string), string icon3x = default(string), string endpointAppId = default(string), string oAuthClientId = default(string), string oAuthClientSecret = default(string), string oAuthTokenUrl = default(string), string oAuthScope = default(string), string userId = default(string), string hostingType = default(string), string schemaType = default(string), string webhookUrl = default(string), string certificationStatus = default(string), string userEmail = default(string), ViperAppLinks viperAppLinks = default(ViperAppLinks))
        {
            // to ensure "partnerName" is required (not null)
            if (partnerName == null) {
                throw new ArgumentNullException("partnerName is a required property for PostEndpointApp and cannot be null");
            }
            this.PartnerName = partnerName;
            // to ensure "oAuthAuthorizationUrl" is required (not null)
            if (oAuthAuthorizationUrl == null) {
                throw new ArgumentNullException("oAuthAuthorizationUrl is a required property for PostEndpointApp and cannot be null");
            }
            this.OAuthAuthorizationUrl = oAuthAuthorizationUrl;
            // to ensure "oAuthClientId" is required (not null)
            if (oAuthClientId == null) {
                throw new ArgumentNullException("oAuthClientId is a required property for PostEndpointApp and cannot be null");
            }
            this.OAuthClientId = oAuthClientId;
            // to ensure "oAuthClientSecret" is required (not null)
            if (oAuthClientSecret == null) {
                throw new ArgumentNullException("oAuthClientSecret is a required property for PostEndpointApp and cannot be null");
            }
            this.OAuthClientSecret = oAuthClientSecret;
            // to ensure "oAuthTokenUrl" is required (not null)
            if (oAuthTokenUrl == null) {
                throw new ArgumentNullException("oAuthTokenUrl is a required property for PostEndpointApp and cannot be null");
            }
            this.OAuthTokenUrl = oAuthTokenUrl;
            // to ensure "hostingType" is required (not null)
            if (hostingType == null) {
                throw new ArgumentNullException("hostingType is a required property for PostEndpointApp and cannot be null");
            }
            this.HostingType = hostingType;
            this.AppName = appName;
            this.LambdaArn = lambdaArn;
            this.LambdaArnEU = lambdaArnEU;
            this.LambdaArnAP = lambdaArnAP;
            this.LambdaArnCN = lambdaArnCN;
            this.Icon = icon;
            this.Icon2x = icon2x;
            this.Icon3x = icon3x;
            this.EndpointAppId = endpointAppId;
            this.OAuthScope = oAuthScope;
            this.UserId = userId;
            this.SchemaType = schemaType;
            this.WebhookUrl = webhookUrl;
            this.CertificationStatus = certificationStatus;
            this.UserEmail = userEmail;
            this.ViperAppLinks = viperAppLinks;
        }

        /// <summary>
        /// The name of the SmartThings Schema App
        /// </summary>
        /// <value>The name of the SmartThings Schema App</value>
        [DataMember(Name = "appName", EmitDefaultValue = false)]
        public string AppName { get; set; }

        /// <summary>
        /// The name of the partner/brand
        /// </summary>
        /// <value>The name of the partner/brand</value>
        [DataMember(Name = "partnerName", IsRequired = true, EmitDefaultValue = false)]
        public string PartnerName { get; set; }

        /// <summary>
        /// oAuth authorization url of the partner
        /// </summary>
        /// <value>oAuth authorization url of the partner</value>
        [DataMember(Name = "oAuthAuthorizationUrl", IsRequired = true, EmitDefaultValue = false)]
        public string OAuthAuthorizationUrl { get; set; }

        /// <summary>
        /// lambda arn of the partner for US region (default)
        /// </summary>
        /// <value>lambda arn of the partner for US region (default)</value>
        [DataMember(Name = "lambdaArn", EmitDefaultValue = false)]
        public string LambdaArn { get; set; }

        /// <summary>
        /// lambda arn of the partner for EU region
        /// </summary>
        /// <value>lambda arn of the partner for EU region</value>
        [DataMember(Name = "lambdaArnEU", EmitDefaultValue = false)]
        public string LambdaArnEU { get; set; }

        /// <summary>
        /// lambda arn of the partner for AP region
        /// </summary>
        /// <value>lambda arn of the partner for AP region</value>
        [DataMember(Name = "lambdaArnAP", EmitDefaultValue = false)]
        public string LambdaArnAP { get; set; }

        /// <summary>
        /// lambda arn of the partner for CN region
        /// </summary>
        /// <value>lambda arn of the partner for CN region</value>
        [DataMember(Name = "lambdaArnCN", EmitDefaultValue = false)]
        public string LambdaArnCN { get; set; }

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
        /// SmartThings Schema App id for the partner
        /// </summary>
        /// <value>SmartThings Schema App id for the partner</value>
        [DataMember(Name = "endpointAppId", EmitDefaultValue = false)]
        public string EndpointAppId { get; set; }

        /// <summary>
        /// Client id for the partner oAuth
        /// </summary>
        /// <value>Client id for the partner oAuth</value>
        [DataMember(Name = "oAuthClientId", IsRequired = true, EmitDefaultValue = false)]
        public string OAuthClientId { get; set; }

        /// <summary>
        /// Client secret for the partner oAuth
        /// </summary>
        /// <value>Client secret for the partner oAuth</value>
        [DataMember(Name = "oAuthClientSecret", IsRequired = true, EmitDefaultValue = false)]
        public string OAuthClientSecret { get; set; }

        /// <summary>
        /// oAuth token refresh url of the partner
        /// </summary>
        /// <value>oAuth token refresh url of the partner</value>
        [DataMember(Name = "oAuthTokenUrl", IsRequired = true, EmitDefaultValue = false)]
        public string OAuthTokenUrl { get; set; }

        /// <summary>
        /// oAuth scope for the partner. Example \&quot;remote_control:all\&quot; for Lifx
        /// </summary>
        /// <value>oAuth scope for the partner. Example \&quot;remote_control:all\&quot; for Lifx</value>
        [DataMember(Name = "oAuthScope", EmitDefaultValue = false)]
        public string OAuthScope { get; set; }

        /// <summary>
        /// user id for the partner
        /// </summary>
        /// <value>user id for the partner</value>
        [DataMember(Name = "userId", EmitDefaultValue = false)]
        public string UserId { get; set; }

        /// <summary>
        /// Possible values - \&quot;lambda\&quot; or \&quot;webhook\&quot;
        /// </summary>
        /// <value>Possible values - \&quot;lambda\&quot; or \&quot;webhook\&quot;</value>
        [DataMember(Name = "hostingType", IsRequired = true, EmitDefaultValue = false)]
        public string HostingType { get; set; }

        /// <summary>
        /// Possible values - \&quot;alexa-schema\&quot;, \&quot;st-schema\&quot;, \&quot;google-schema\&quot;
        /// </summary>
        /// <value>Possible values - \&quot;alexa-schema\&quot;, \&quot;st-schema\&quot;, \&quot;google-schema\&quot;</value>
        [DataMember(Name = "schemaType", EmitDefaultValue = false)]
        public string SchemaType { get; set; }

        /// <summary>
        /// webhook url for the partner
        /// </summary>
        /// <value>webhook url for the partner</value>
        [DataMember(Name = "webhookUrl", EmitDefaultValue = false)]
        public string WebhookUrl { get; set; }

        /// <summary>
        /// Possible values - \&quot;\&quot;, \&quot;cst\&quot;, \&quot;wwst\&quot;, \&quot;review\&quot;
        /// </summary>
        /// <value>Possible values - \&quot;\&quot;, \&quot;cst\&quot;, \&quot;wwst\&quot;, \&quot;review\&quot;</value>
        [DataMember(Name = "certificationStatus", EmitDefaultValue = false)]
        public string CertificationStatus { get; set; }

        /// <summary>
        /// Email for the partner
        /// </summary>
        /// <value>Email for the partner</value>
        [DataMember(Name = "userEmail", EmitDefaultValue = false)]
        public string UserEmail { get; set; }

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
            sb.Append("class PostEndpointApp {\n");
            sb.Append("  AppName: ").Append(AppName).Append("\n");
            sb.Append("  PartnerName: ").Append(PartnerName).Append("\n");
            sb.Append("  OAuthAuthorizationUrl: ").Append(OAuthAuthorizationUrl).Append("\n");
            sb.Append("  LambdaArn: ").Append(LambdaArn).Append("\n");
            sb.Append("  LambdaArnEU: ").Append(LambdaArnEU).Append("\n");
            sb.Append("  LambdaArnAP: ").Append(LambdaArnAP).Append("\n");
            sb.Append("  LambdaArnCN: ").Append(LambdaArnCN).Append("\n");
            sb.Append("  Icon: ").Append(Icon).Append("\n");
            sb.Append("  Icon2x: ").Append(Icon2x).Append("\n");
            sb.Append("  Icon3x: ").Append(Icon3x).Append("\n");
            sb.Append("  EndpointAppId: ").Append(EndpointAppId).Append("\n");
            sb.Append("  OAuthClientId: ").Append(OAuthClientId).Append("\n");
            sb.Append("  OAuthClientSecret: ").Append(OAuthClientSecret).Append("\n");
            sb.Append("  OAuthTokenUrl: ").Append(OAuthTokenUrl).Append("\n");
            sb.Append("  OAuthScope: ").Append(OAuthScope).Append("\n");
            sb.Append("  UserId: ").Append(UserId).Append("\n");
            sb.Append("  HostingType: ").Append(HostingType).Append("\n");
            sb.Append("  SchemaType: ").Append(SchemaType).Append("\n");
            sb.Append("  WebhookUrl: ").Append(WebhookUrl).Append("\n");
            sb.Append("  CertificationStatus: ").Append(CertificationStatus).Append("\n");
            sb.Append("  UserEmail: ").Append(UserEmail).Append("\n");
            sb.Append("  ViperAppLinks: ").Append(ViperAppLinks).Append("\n");
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
            return this.Equals(input as PostEndpointApp);
        }

        /// <summary>
        /// Returns true if PostEndpointApp instances are equal
        /// </summary>
        /// <param name="input">Instance of PostEndpointApp to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PostEndpointApp input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.AppName == input.AppName ||
                    (this.AppName != null &&
                    this.AppName.Equals(input.AppName))
                ) && 
                (
                    this.PartnerName == input.PartnerName ||
                    (this.PartnerName != null &&
                    this.PartnerName.Equals(input.PartnerName))
                ) && 
                (
                    this.OAuthAuthorizationUrl == input.OAuthAuthorizationUrl ||
                    (this.OAuthAuthorizationUrl != null &&
                    this.OAuthAuthorizationUrl.Equals(input.OAuthAuthorizationUrl))
                ) && 
                (
                    this.LambdaArn == input.LambdaArn ||
                    (this.LambdaArn != null &&
                    this.LambdaArn.Equals(input.LambdaArn))
                ) && 
                (
                    this.LambdaArnEU == input.LambdaArnEU ||
                    (this.LambdaArnEU != null &&
                    this.LambdaArnEU.Equals(input.LambdaArnEU))
                ) && 
                (
                    this.LambdaArnAP == input.LambdaArnAP ||
                    (this.LambdaArnAP != null &&
                    this.LambdaArnAP.Equals(input.LambdaArnAP))
                ) && 
                (
                    this.LambdaArnCN == input.LambdaArnCN ||
                    (this.LambdaArnCN != null &&
                    this.LambdaArnCN.Equals(input.LambdaArnCN))
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
                    this.EndpointAppId == input.EndpointAppId ||
                    (this.EndpointAppId != null &&
                    this.EndpointAppId.Equals(input.EndpointAppId))
                ) && 
                (
                    this.OAuthClientId == input.OAuthClientId ||
                    (this.OAuthClientId != null &&
                    this.OAuthClientId.Equals(input.OAuthClientId))
                ) && 
                (
                    this.OAuthClientSecret == input.OAuthClientSecret ||
                    (this.OAuthClientSecret != null &&
                    this.OAuthClientSecret.Equals(input.OAuthClientSecret))
                ) && 
                (
                    this.OAuthTokenUrl == input.OAuthTokenUrl ||
                    (this.OAuthTokenUrl != null &&
                    this.OAuthTokenUrl.Equals(input.OAuthTokenUrl))
                ) && 
                (
                    this.OAuthScope == input.OAuthScope ||
                    (this.OAuthScope != null &&
                    this.OAuthScope.Equals(input.OAuthScope))
                ) && 
                (
                    this.UserId == input.UserId ||
                    (this.UserId != null &&
                    this.UserId.Equals(input.UserId))
                ) && 
                (
                    this.HostingType == input.HostingType ||
                    (this.HostingType != null &&
                    this.HostingType.Equals(input.HostingType))
                ) && 
                (
                    this.SchemaType == input.SchemaType ||
                    (this.SchemaType != null &&
                    this.SchemaType.Equals(input.SchemaType))
                ) && 
                (
                    this.WebhookUrl == input.WebhookUrl ||
                    (this.WebhookUrl != null &&
                    this.WebhookUrl.Equals(input.WebhookUrl))
                ) && 
                (
                    this.CertificationStatus == input.CertificationStatus ||
                    (this.CertificationStatus != null &&
                    this.CertificationStatus.Equals(input.CertificationStatus))
                ) && 
                (
                    this.UserEmail == input.UserEmail ||
                    (this.UserEmail != null &&
                    this.UserEmail.Equals(input.UserEmail))
                ) && 
                (
                    this.ViperAppLinks == input.ViperAppLinks ||
                    (this.ViperAppLinks != null &&
                    this.ViperAppLinks.Equals(input.ViperAppLinks))
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
                if (this.AppName != null)
                {
                    hashCode = (hashCode * 59) + this.AppName.GetHashCode();
                }
                if (this.PartnerName != null)
                {
                    hashCode = (hashCode * 59) + this.PartnerName.GetHashCode();
                }
                if (this.OAuthAuthorizationUrl != null)
                {
                    hashCode = (hashCode * 59) + this.OAuthAuthorizationUrl.GetHashCode();
                }
                if (this.LambdaArn != null)
                {
                    hashCode = (hashCode * 59) + this.LambdaArn.GetHashCode();
                }
                if (this.LambdaArnEU != null)
                {
                    hashCode = (hashCode * 59) + this.LambdaArnEU.GetHashCode();
                }
                if (this.LambdaArnAP != null)
                {
                    hashCode = (hashCode * 59) + this.LambdaArnAP.GetHashCode();
                }
                if (this.LambdaArnCN != null)
                {
                    hashCode = (hashCode * 59) + this.LambdaArnCN.GetHashCode();
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
                if (this.EndpointAppId != null)
                {
                    hashCode = (hashCode * 59) + this.EndpointAppId.GetHashCode();
                }
                if (this.OAuthClientId != null)
                {
                    hashCode = (hashCode * 59) + this.OAuthClientId.GetHashCode();
                }
                if (this.OAuthClientSecret != null)
                {
                    hashCode = (hashCode * 59) + this.OAuthClientSecret.GetHashCode();
                }
                if (this.OAuthTokenUrl != null)
                {
                    hashCode = (hashCode * 59) + this.OAuthTokenUrl.GetHashCode();
                }
                if (this.OAuthScope != null)
                {
                    hashCode = (hashCode * 59) + this.OAuthScope.GetHashCode();
                }
                if (this.UserId != null)
                {
                    hashCode = (hashCode * 59) + this.UserId.GetHashCode();
                }
                if (this.HostingType != null)
                {
                    hashCode = (hashCode * 59) + this.HostingType.GetHashCode();
                }
                if (this.SchemaType != null)
                {
                    hashCode = (hashCode * 59) + this.SchemaType.GetHashCode();
                }
                if (this.WebhookUrl != null)
                {
                    hashCode = (hashCode * 59) + this.WebhookUrl.GetHashCode();
                }
                if (this.CertificationStatus != null)
                {
                    hashCode = (hashCode * 59) + this.CertificationStatus.GetHashCode();
                }
                if (this.UserEmail != null)
                {
                    hashCode = (hashCode * 59) + this.UserEmail.GetHashCode();
                }
                if (this.ViperAppLinks != null)
                {
                    hashCode = (hashCode * 59) + this.ViperAppLinks.GetHashCode();
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