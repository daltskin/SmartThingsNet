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
    /// DetailViewItemAllOf
    /// </summary>
    [DataContract]
    public partial class DetailViewItemAllOf :  IEquatable<DetailViewItemAllOf>, IValidatableObject
    {
        /// <summary>
        /// Specify the type of UI component to use to display this action or state. The corresponding field must also be included. For example, if you specify \&quot;switch\&quot; here, you must also include the \&quot;switch\&quot; key and its object definition for this action or state.
        /// </summary>
        /// <value>Specify the type of UI component to use to display this action or state. The corresponding field must also be included. For example, if you specify \&quot;switch\&quot; here, you must also include the \&quot;switch\&quot; key and its object definition for this action or state.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum DisplayTypeEnum
        {
            /// <summary>
            /// Enum ToggleSwitch for value: toggleSwitch
            /// </summary>
            [EnumMember(Value = "toggleSwitch")]
            ToggleSwitch = 1,

            /// <summary>
            /// Enum StandbyPowerSwitch for value: standbyPowerSwitch
            /// </summary>
            [EnumMember(Value = "standbyPowerSwitch")]
            StandbyPowerSwitch = 2,

            /// <summary>
            /// Enum Switch for value: switch
            /// </summary>
            [EnumMember(Value = "switch")]
            Switch = 3,

            /// <summary>
            /// Enum Slider for value: slider
            /// </summary>
            [EnumMember(Value = "slider")]
            Slider = 4,

            /// <summary>
            /// Enum PushButton for value: pushButton
            /// </summary>
            [EnumMember(Value = "pushButton")]
            PushButton = 5,

            /// <summary>
            /// Enum PlayPause for value: playPause
            /// </summary>
            [EnumMember(Value = "playPause")]
            PlayPause = 6,

            /// <summary>
            /// Enum PlayStop for value: playStop
            /// </summary>
            [EnumMember(Value = "playStop")]
            PlayStop = 7,

            /// <summary>
            /// Enum List for value: list
            /// </summary>
            [EnumMember(Value = "list")]
            List = 8,

            /// <summary>
            /// Enum TextField for value: textField
            /// </summary>
            [EnumMember(Value = "textField")]
            TextField = 9,

            /// <summary>
            /// Enum NumberField for value: numberField
            /// </summary>
            [EnumMember(Value = "numberField")]
            NumberField = 10,

            /// <summary>
            /// Enum Stepper for value: stepper
            /// </summary>
            [EnumMember(Value = "stepper")]
            Stepper = 11,

            /// <summary>
            /// Enum State for value: state
            /// </summary>
            [EnumMember(Value = "state")]
            State = 12,

            /// <summary>
            /// Enum MultiArgCommand for value: multiArgCommand
            /// </summary>
            [EnumMember(Value = "multiArgCommand")]
            MultiArgCommand = 13

        }

        /// <summary>
        /// Specify the type of UI component to use to display this action or state. The corresponding field must also be included. For example, if you specify \&quot;switch\&quot; here, you must also include the \&quot;switch\&quot; key and its object definition for this action or state.
        /// </summary>
        /// <value>Specify the type of UI component to use to display this action or state. The corresponding field must also be included. For example, if you specify \&quot;switch\&quot; here, you must also include the \&quot;switch\&quot; key and its object definition for this action or state.</value>
        [DataMember(Name="displayType", EmitDefaultValue=false)]
        public DisplayTypeEnum DisplayType { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="DetailViewItemAllOf" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected DetailViewItemAllOf() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DetailViewItemAllOf" /> class.
        /// </summary>
        /// <param name="label">label (required).</param>
        /// <param name="displayType">Specify the type of UI component to use to display this action or state. The corresponding field must also be included. For example, if you specify \&quot;switch\&quot; here, you must also include the \&quot;switch\&quot; key and its object definition for this action or state. (required).</param>
        /// <param name="toggleSwitch">toggleSwitch.</param>
        /// <param name="standbyPowerSwitch">standbyPowerSwitch.</param>
        /// <param name="_switch">_switch.</param>
        /// <param name="slider">slider.</param>
        /// <param name="pushButton">pushButton.</param>
        /// <param name="playPause">playPause.</param>
        /// <param name="playStop">playStop.</param>
        /// <param name="list">list.</param>
        /// <param name="textField">textField.</param>
        /// <param name="numberField">numberField.</param>
        /// <param name="stepper">stepper.</param>
        /// <param name="state">state.</param>
        /// <param name="multiArgCommand">multiArgCommand.</param>
        public DetailViewItemAllOf(string label = default(string), DisplayTypeEnum displayType = default(DisplayTypeEnum), ToggleSwitch toggleSwitch = default(ToggleSwitch), StandbyPowerSwitch standbyPowerSwitch = default(StandbyPowerSwitch), SwitchObj _switch = default(SwitchObj), Slider slider = default(Slider), PushButton pushButton = default(PushButton), PlayPause playPause = default(PlayPause), PlayStop playStop = default(PlayStop), ListForDetailView list = default(ListForDetailView), TextField textField = default(TextField), NumberField numberField = default(NumberField), Stepper stepper = default(Stepper), State state = default(State), MultiArgCommand multiArgCommand = default(MultiArgCommand))
        {
            // to ensure "label" is required (not null)
            this.Label = label ?? throw new ArgumentNullException("label is a required property for DetailViewItemAllOf and cannot be null");
            this.DisplayType = displayType;
            this.ToggleSwitch = toggleSwitch;
            this.StandbyPowerSwitch = standbyPowerSwitch;
            this.Switch = _switch;
            this.Slider = slider;
            this.PushButton = pushButton;
            this.PlayPause = playPause;
            this.PlayStop = playStop;
            this.List = list;
            this.TextField = textField;
            this.NumberField = numberField;
            this.Stepper = stepper;
            this.State = state;
            this.MultiArgCommand = multiArgCommand;
        }
        
        /// <summary>
        /// Gets or Sets Label
        /// </summary>
        [DataMember(Name="label", EmitDefaultValue=false)]
        public string Label { get; set; }

        /// <summary>
        /// Gets or Sets ToggleSwitch
        /// </summary>
        [DataMember(Name="toggleSwitch", EmitDefaultValue=false)]
        public ToggleSwitch ToggleSwitch { get; set; }

        /// <summary>
        /// Gets or Sets StandbyPowerSwitch
        /// </summary>
        [DataMember(Name="standbyPowerSwitch", EmitDefaultValue=false)]
        public StandbyPowerSwitch StandbyPowerSwitch { get; set; }

        /// <summary>
        /// Gets or Sets Switch
        /// </summary>
        [DataMember(Name="switch", EmitDefaultValue=false)]
        public SwitchObj Switch { get; set; }

        /// <summary>
        /// Gets or Sets Slider
        /// </summary>
        [DataMember(Name="slider", EmitDefaultValue=false)]
        public Slider Slider { get; set; }

        /// <summary>
        /// Gets or Sets PushButton
        /// </summary>
        [DataMember(Name="pushButton", EmitDefaultValue=false)]
        public PushButton PushButton { get; set; }

        /// <summary>
        /// Gets or Sets PlayPause
        /// </summary>
        [DataMember(Name="playPause", EmitDefaultValue=false)]
        public PlayPause PlayPause { get; set; }

        /// <summary>
        /// Gets or Sets PlayStop
        /// </summary>
        [DataMember(Name="playStop", EmitDefaultValue=false)]
        public PlayStop PlayStop { get; set; }

        /// <summary>
        /// Gets or Sets List
        /// </summary>
        [DataMember(Name="list", EmitDefaultValue=false)]
        public ListForDetailView List { get; set; }

        /// <summary>
        /// Gets or Sets TextField
        /// </summary>
        [DataMember(Name="textField", EmitDefaultValue=false)]
        public TextField TextField { get; set; }

        /// <summary>
        /// Gets or Sets NumberField
        /// </summary>
        [DataMember(Name="numberField", EmitDefaultValue=false)]
        public NumberField NumberField { get; set; }

        /// <summary>
        /// Gets or Sets Stepper
        /// </summary>
        [DataMember(Name="stepper", EmitDefaultValue=false)]
        public Stepper Stepper { get; set; }

        /// <summary>
        /// Gets or Sets State
        /// </summary>
        [DataMember(Name="state", EmitDefaultValue=false)]
        public State State { get; set; }

        /// <summary>
        /// Gets or Sets MultiArgCommand
        /// </summary>
        [DataMember(Name="multiArgCommand", EmitDefaultValue=false)]
        public MultiArgCommand MultiArgCommand { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class DetailViewItemAllOf {\n");
            sb.Append("  Label: ").Append(Label).Append("\n");
            sb.Append("  DisplayType: ").Append(DisplayType).Append("\n");
            sb.Append("  ToggleSwitch: ").Append(ToggleSwitch).Append("\n");
            sb.Append("  StandbyPowerSwitch: ").Append(StandbyPowerSwitch).Append("\n");
            sb.Append("  Switch: ").Append(Switch).Append("\n");
            sb.Append("  Slider: ").Append(Slider).Append("\n");
            sb.Append("  PushButton: ").Append(PushButton).Append("\n");
            sb.Append("  PlayPause: ").Append(PlayPause).Append("\n");
            sb.Append("  PlayStop: ").Append(PlayStop).Append("\n");
            sb.Append("  List: ").Append(List).Append("\n");
            sb.Append("  TextField: ").Append(TextField).Append("\n");
            sb.Append("  NumberField: ").Append(NumberField).Append("\n");
            sb.Append("  Stepper: ").Append(Stepper).Append("\n");
            sb.Append("  State: ").Append(State).Append("\n");
            sb.Append("  MultiArgCommand: ").Append(MultiArgCommand).Append("\n");
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
            return this.Equals(input as DetailViewItemAllOf);
        }

        /// <summary>
        /// Returns true if DetailViewItemAllOf instances are equal
        /// </summary>
        /// <param name="input">Instance of DetailViewItemAllOf to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DetailViewItemAllOf input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Label == input.Label ||
                    (this.Label != null &&
                    this.Label.Equals(input.Label))
                ) && 
                (
                    this.DisplayType == input.DisplayType ||
                    this.DisplayType.Equals(input.DisplayType)
                ) && 
                (
                    this.ToggleSwitch == input.ToggleSwitch ||
                    (this.ToggleSwitch != null &&
                    this.ToggleSwitch.Equals(input.ToggleSwitch))
                ) && 
                (
                    this.StandbyPowerSwitch == input.StandbyPowerSwitch ||
                    (this.StandbyPowerSwitch != null &&
                    this.StandbyPowerSwitch.Equals(input.StandbyPowerSwitch))
                ) && 
                (
                    this.Switch == input.Switch ||
                    (this.Switch != null &&
                    this.Switch.Equals(input.Switch))
                ) && 
                (
                    this.Slider == input.Slider ||
                    (this.Slider != null &&
                    this.Slider.Equals(input.Slider))
                ) && 
                (
                    this.PushButton == input.PushButton ||
                    (this.PushButton != null &&
                    this.PushButton.Equals(input.PushButton))
                ) && 
                (
                    this.PlayPause == input.PlayPause ||
                    (this.PlayPause != null &&
                    this.PlayPause.Equals(input.PlayPause))
                ) && 
                (
                    this.PlayStop == input.PlayStop ||
                    (this.PlayStop != null &&
                    this.PlayStop.Equals(input.PlayStop))
                ) && 
                (
                    this.List == input.List ||
                    (this.List != null &&
                    this.List.Equals(input.List))
                ) && 
                (
                    this.TextField == input.TextField ||
                    (this.TextField != null &&
                    this.TextField.Equals(input.TextField))
                ) && 
                (
                    this.NumberField == input.NumberField ||
                    (this.NumberField != null &&
                    this.NumberField.Equals(input.NumberField))
                ) && 
                (
                    this.Stepper == input.Stepper ||
                    (this.Stepper != null &&
                    this.Stepper.Equals(input.Stepper))
                ) && 
                (
                    this.State == input.State ||
                    (this.State != null &&
                    this.State.Equals(input.State))
                ) && 
                (
                    this.MultiArgCommand == input.MultiArgCommand ||
                    (this.MultiArgCommand != null &&
                    this.MultiArgCommand.Equals(input.MultiArgCommand))
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
                if (this.Label != null)
                    hashCode = hashCode * 59 + this.Label.GetHashCode();
                hashCode = hashCode * 59 + this.DisplayType.GetHashCode();
                if (this.ToggleSwitch != null)
                    hashCode = hashCode * 59 + this.ToggleSwitch.GetHashCode();
                if (this.StandbyPowerSwitch != null)
                    hashCode = hashCode * 59 + this.StandbyPowerSwitch.GetHashCode();
                if (this.Switch != null)
                    hashCode = hashCode * 59 + this.Switch.GetHashCode();
                if (this.Slider != null)
                    hashCode = hashCode * 59 + this.Slider.GetHashCode();
                if (this.PushButton != null)
                    hashCode = hashCode * 59 + this.PushButton.GetHashCode();
                if (this.PlayPause != null)
                    hashCode = hashCode * 59 + this.PlayPause.GetHashCode();
                if (this.PlayStop != null)
                    hashCode = hashCode * 59 + this.PlayStop.GetHashCode();
                if (this.List != null)
                    hashCode = hashCode * 59 + this.List.GetHashCode();
                if (this.TextField != null)
                    hashCode = hashCode * 59 + this.TextField.GetHashCode();
                if (this.NumberField != null)
                    hashCode = hashCode * 59 + this.NumberField.GetHashCode();
                if (this.Stepper != null)
                    hashCode = hashCode * 59 + this.Stepper.GetHashCode();
                if (this.State != null)
                    hashCode = hashCode * 59 + this.State.GetHashCode();
                if (this.MultiArgCommand != null)
                    hashCode = hashCode * 59 + this.MultiArgCommand.GetHashCode();
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
            yield break;
        }
    }

}
