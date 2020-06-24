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
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = SmartThingsNet.Client.OpenAPIDateConverter;

namespace SmartThingsNet.Model
{
    /// <summary>
    /// CreateAppRequest
    /// </summary>
    [DataContract]
    public partial class CreateAppRequest :  IEquatable<CreateAppRequest>, IValidatableObject
    {
        /// <summary>
        /// Gets or Sets AppType
        /// </summary>
        [DataMember(Name="appType", EmitDefaultValue=false)]
        public AppType AppType { get; set; }
        /// <summary>
        /// Gets or Sets PrincipalType
        /// </summary>
        [DataMember(Name="principalType", EmitDefaultValue=false)]
        public PrincipalType? PrincipalType { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAppRequest" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected CreateAppRequest() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAppRequest" /> class.
        /// </summary>
        /// <param name="appName">A globally unique, developer-defined identifier for an app. It is alpha-numeric, may contain dashes, underscores, periods, and must be less then 250 characters long.  (required).</param>
        /// <param name="displayName">A default display name for an app.  (required).</param>
        /// <param name="description">A default description for an app.  (required).</param>
        /// <param name="singleInstance">Inform the installation systems that a particular app can only be installed once within a user&#39;s account.  (default to false).</param>
        /// <param name="iconImage">iconImage.</param>
        /// <param name="appType">appType (required).</param>
        /// <param name="principalType">principalType.</param>
        /// <param name="classifications">An App maybe associated to many classifications.  A classification drives how the integration is presented to the user in the SmartThings mobile clients.  These classifications include: * AUTOMATION - Denotes an integration that should display under the \&quot;Automation\&quot; tab in mobile clients. * SERVICE - Denotes an integration that is classified as a \&quot;Service\&quot;. * DEVICE - Denotes an integration that should display under the \&quot;Device\&quot; tab in mobile clients. * CONNECTED_SERVICE - Denotes an integration that should display under the \&quot;Connected Services\&quot; menu in mobile clients. * HIDDEN - Denotes an integration that should not display in mobile clients  (required).</param>
        /// <param name="lambdaSmartApp">lambdaSmartApp.</param>
        /// <param name="webhookSmartApp">webhookSmartApp.</param>
        /// <param name="oauth">oauth.</param>
        /// <param name="ui">ui.</param>
        public CreateAppRequest(string appName = default(string), string displayName = default(string), string description = default(string), bool singleInstance = false, IconImage iconImage = default(IconImage), AppType appType = default(AppType), PrincipalType? principalType = default(PrincipalType?), List<AppClassification> classifications = default(List<AppClassification>), CreateOrUpdateLambdaSmartAppRequest lambdaSmartApp = default(CreateOrUpdateLambdaSmartAppRequest), CreateOrUpdateWebhookSmartAppRequest webhookSmartApp = default(CreateOrUpdateWebhookSmartAppRequest), AppOAuth oauth = default(AppOAuth), AppUISettings ui = default(AppUISettings))
        {
            // to ensure "appName" is required (not null)
            this.AppName = appName ?? throw new ArgumentNullException("appName is a required property for CreateAppRequest and cannot be null");
            // to ensure "displayName" is required (not null)
            this.DisplayName = displayName ?? throw new ArgumentNullException("displayName is a required property for CreateAppRequest and cannot be null");
            // to ensure "description" is required (not null)
            this.Description = description ?? throw new ArgumentNullException("description is a required property for CreateAppRequest and cannot be null");
            this.AppType = appType;
            // to ensure "classifications" is required (not null)
            this.Classifications = classifications ?? throw new ArgumentNullException("classifications is a required property for CreateAppRequest and cannot be null");
            this.SingleInstance = singleInstance;
            this.IconImage = iconImage;
            this.PrincipalType = principalType;
            this.LambdaSmartApp = lambdaSmartApp;
            this.WebhookSmartApp = webhookSmartApp;
            this.Oauth = oauth;
            this.Ui = ui;
        }
        
        /// <summary>
        /// A globally unique, developer-defined identifier for an app. It is alpha-numeric, may contain dashes, underscores, periods, and must be less then 250 characters long. 
        /// </summary>
        /// <value>A globally unique, developer-defined identifier for an app. It is alpha-numeric, may contain dashes, underscores, periods, and must be less then 250 characters long. </value>
        [DataMember(Name="appName", EmitDefaultValue=false)]
        public string AppName { get; set; }

        /// <summary>
        /// A default display name for an app. 
        /// </summary>
        /// <value>A default display name for an app. </value>
        [DataMember(Name="displayName", EmitDefaultValue=false)]
        public string DisplayName { get; set; }

        /// <summary>
        /// A default description for an app. 
        /// </summary>
        /// <value>A default description for an app. </value>
        [DataMember(Name="description", EmitDefaultValue=false)]
        public string Description { get; set; }

        /// <summary>
        /// Inform the installation systems that a particular app can only be installed once within a user&#39;s account. 
        /// </summary>
        /// <value>Inform the installation systems that a particular app can only be installed once within a user&#39;s account. </value>
        [DataMember(Name="singleInstance", EmitDefaultValue=false)]
        public bool SingleInstance { get; set; }

        /// <summary>
        /// Gets or Sets IconImage
        /// </summary>
        [DataMember(Name="iconImage", EmitDefaultValue=false)]
        public IconImage IconImage { get; set; }

        /// <summary>
        /// An App maybe associated to many classifications.  A classification drives how the integration is presented to the user in the SmartThings mobile clients.  These classifications include: * AUTOMATION - Denotes an integration that should display under the \&quot;Automation\&quot; tab in mobile clients. * SERVICE - Denotes an integration that is classified as a \&quot;Service\&quot;. * DEVICE - Denotes an integration that should display under the \&quot;Device\&quot; tab in mobile clients. * CONNECTED_SERVICE - Denotes an integration that should display under the \&quot;Connected Services\&quot; menu in mobile clients. * HIDDEN - Denotes an integration that should not display in mobile clients 
        /// </summary>
        /// <value>An App maybe associated to many classifications.  A classification drives how the integration is presented to the user in the SmartThings mobile clients.  These classifications include: * AUTOMATION - Denotes an integration that should display under the \&quot;Automation\&quot; tab in mobile clients. * SERVICE - Denotes an integration that is classified as a \&quot;Service\&quot;. * DEVICE - Denotes an integration that should display under the \&quot;Device\&quot; tab in mobile clients. * CONNECTED_SERVICE - Denotes an integration that should display under the \&quot;Connected Services\&quot; menu in mobile clients. * HIDDEN - Denotes an integration that should not display in mobile clients </value>
        [DataMember(Name="classifications", EmitDefaultValue=false)]
        public List<AppClassification> Classifications { get; set; }

        /// <summary>
        /// Gets or Sets LambdaSmartApp
        /// </summary>
        [DataMember(Name="lambdaSmartApp", EmitDefaultValue=false)]
        public CreateOrUpdateLambdaSmartAppRequest LambdaSmartApp { get; set; }

        /// <summary>
        /// Gets or Sets WebhookSmartApp
        /// </summary>
        [DataMember(Name="webhookSmartApp", EmitDefaultValue=false)]
        public CreateOrUpdateWebhookSmartAppRequest WebhookSmartApp { get; set; }

        /// <summary>
        /// Gets or Sets Oauth
        /// </summary>
        [DataMember(Name="oauth", EmitDefaultValue=false)]
        public AppOAuth Oauth { get; set; }

        /// <summary>
        /// Gets or Sets Ui
        /// </summary>
        [DataMember(Name="ui", EmitDefaultValue=false)]
        public AppUISettings Ui { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CreateAppRequest {\n");
            sb.Append("  AppName: ").Append(AppName).Append("\n");
            sb.Append("  DisplayName: ").Append(DisplayName).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  SingleInstance: ").Append(SingleInstance).Append("\n");
            sb.Append("  IconImage: ").Append(IconImage).Append("\n");
            sb.Append("  AppType: ").Append(AppType).Append("\n");
            sb.Append("  PrincipalType: ").Append(PrincipalType).Append("\n");
            sb.Append("  Classifications: ").Append(Classifications).Append("\n");
            sb.Append("  LambdaSmartApp: ").Append(LambdaSmartApp).Append("\n");
            sb.Append("  WebhookSmartApp: ").Append(WebhookSmartApp).Append("\n");
            sb.Append("  Oauth: ").Append(Oauth).Append("\n");
            sb.Append("  Ui: ").Append(Ui).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as CreateAppRequest);
        }

        /// <summary>
        /// Returns true if CreateAppRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of CreateAppRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CreateAppRequest input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.AppName == input.AppName ||
                    (this.AppName != null &&
                    this.AppName.Equals(input.AppName))
                ) && 
                (
                    this.DisplayName == input.DisplayName ||
                    (this.DisplayName != null &&
                    this.DisplayName.Equals(input.DisplayName))
                ) && 
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description))
                ) && 
                (
                    this.SingleInstance == input.SingleInstance ||
                    this.SingleInstance.Equals(input.SingleInstance)
                ) && 
                (
                    this.IconImage == input.IconImage ||
                    (this.IconImage != null &&
                    this.IconImage.Equals(input.IconImage))
                ) && 
                (
                    this.AppType == input.AppType ||
                    this.AppType.Equals(input.AppType)
                ) && 
                (
                    this.PrincipalType == input.PrincipalType ||
                    this.PrincipalType.Equals(input.PrincipalType)
                ) && 
                (
                    this.Classifications == input.Classifications ||
                    this.Classifications != null &&
                    input.Classifications != null &&
                    this.Classifications.SequenceEqual(input.Classifications)
                ) && 
                (
                    this.LambdaSmartApp == input.LambdaSmartApp ||
                    (this.LambdaSmartApp != null &&
                    this.LambdaSmartApp.Equals(input.LambdaSmartApp))
                ) && 
                (
                    this.WebhookSmartApp == input.WebhookSmartApp ||
                    (this.WebhookSmartApp != null &&
                    this.WebhookSmartApp.Equals(input.WebhookSmartApp))
                ) && 
                (
                    this.Oauth == input.Oauth ||
                    (this.Oauth != null &&
                    this.Oauth.Equals(input.Oauth))
                ) && 
                (
                    this.Ui == input.Ui ||
                    (this.Ui != null &&
                    this.Ui.Equals(input.Ui))
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
                    hashCode = hashCode * 59 + this.AppName.GetHashCode();
                if (this.DisplayName != null)
                    hashCode = hashCode * 59 + this.DisplayName.GetHashCode();
                if (this.Description != null)
                    hashCode = hashCode * 59 + this.Description.GetHashCode();
                hashCode = hashCode * 59 + this.SingleInstance.GetHashCode();
                if (this.IconImage != null)
                    hashCode = hashCode * 59 + this.IconImage.GetHashCode();
                hashCode = hashCode * 59 + this.AppType.GetHashCode();
                hashCode = hashCode * 59 + this.PrincipalType.GetHashCode();
                if (this.Classifications != null)
                    hashCode = hashCode * 59 + this.Classifications.GetHashCode();
                if (this.LambdaSmartApp != null)
                    hashCode = hashCode * 59 + this.LambdaSmartApp.GetHashCode();
                if (this.WebhookSmartApp != null)
                    hashCode = hashCode * 59 + this.WebhookSmartApp.GetHashCode();
                if (this.Oauth != null)
                    hashCode = hashCode * 59 + this.Oauth.GetHashCode();
                if (this.Ui != null)
                    hashCode = hashCode * 59 + this.Ui.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            // DisplayName (string) maxLength
            if(this.DisplayName != null && this.DisplayName.Length > 75)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for DisplayName, length must be less than 75.", new [] { "DisplayName" });
            }

            // Description (string) maxLength
            if(this.Description != null && this.Description.Length > 250)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Description, length must be less than 250.", new [] { "Description" });
            }

            yield break;
        }
    }

}
