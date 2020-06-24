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
    /// Device
    /// </summary>
    [DataContract]
    public partial class Device :  IEquatable<Device>, IValidatableObject
    {
        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name="type", EmitDefaultValue=false)]
        public DeviceIntegrationType? Type { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Device" /> class.
        /// </summary>
        /// <param name="deviceId">The identifier for the device instance..</param>
        /// <param name="name">The name that the device integration (Device Handler or SmartApp) defines for the device..</param>
        /// <param name="label">The name that a user chooses for the device. This defaults to the same value as name..</param>
        /// <param name="deviceManufacturerCode">The device manufacturer code..</param>
        /// <param name="locationId">The ID of the Location with which the device is associated..</param>
        /// <param name="roomId">The ID of the Room with which the device is associated. If the device is not associated with any room, then this field will be null..</param>
        /// <param name="deviceTypeId">Deprecated please look under \&quot;dth\&quot;..</param>
        /// <param name="deviceTypeName">Deprecated please look under \&quot;dth\&quot;..</param>
        /// <param name="deviceNetworkType">Deprecated please look under \&quot;dth\&quot;..</param>
        /// <param name="components">The IDs of all compenents on the device..</param>
        /// <param name="childDevices">Device details for all child devices of the device..</param>
        /// <param name="profile">profile.</param>
        /// <param name="app">app.</param>
        /// <param name="dth">dth.</param>
        /// <param name="ir">ir.</param>
        /// <param name="irOcf">irOcf.</param>
        /// <param name="viper">viper.</param>
        /// <param name="type">type.</param>
        public Device(string deviceId = default(string), string name = default(string), string label = default(string), string deviceManufacturerCode = default(string), string locationId = default(string), string roomId = default(string), string deviceTypeId = default(string), string deviceTypeName = default(string), string deviceNetworkType = default(string), List<DeviceComponent> components = default(List<DeviceComponent>), List<Device> childDevices = default(List<Device>), DeviceProfileReference profile = default(DeviceProfileReference), AppDeviceDetails app = default(AppDeviceDetails), DthDeviceDetails dth = default(DthDeviceDetails), IrDeviceDetails ir = default(IrDeviceDetails), IrDeviceDetails irOcf = default(IrDeviceDetails), ViperDeviceDetails viper = default(ViperDeviceDetails), DeviceIntegrationType? type = default(DeviceIntegrationType?))
        {
            this.DeviceId = deviceId;
            this.Name = name;
            this.Label = label;
            this.DeviceManufacturerCode = deviceManufacturerCode;
            this.LocationId = locationId;
            this.RoomId = roomId;
            this.DeviceTypeId = deviceTypeId;
            this.DeviceTypeName = deviceTypeName;
            this.DeviceNetworkType = deviceNetworkType;
            this.Components = components;
            this.ChildDevices = childDevices;
            this.Profile = profile;
            this.App = app;
            this.Dth = dth;
            this.Ir = ir;
            this.IrOcf = irOcf;
            this.Viper = viper;
            this.Type = type;
        }
        
        /// <summary>
        /// The identifier for the device instance.
        /// </summary>
        /// <value>The identifier for the device instance.</value>
        [DataMember(Name="deviceId", EmitDefaultValue=false)]
        public string DeviceId { get; set; }

        /// <summary>
        /// The name that the device integration (Device Handler or SmartApp) defines for the device.
        /// </summary>
        /// <value>The name that the device integration (Device Handler or SmartApp) defines for the device.</value>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// The name that a user chooses for the device. This defaults to the same value as name.
        /// </summary>
        /// <value>The name that a user chooses for the device. This defaults to the same value as name.</value>
        [DataMember(Name="label", EmitDefaultValue=false)]
        public string Label { get; set; }

        /// <summary>
        /// The device manufacturer code.
        /// </summary>
        /// <value>The device manufacturer code.</value>
        [DataMember(Name="deviceManufacturerCode", EmitDefaultValue=false)]
        public string DeviceManufacturerCode { get; set; }

        /// <summary>
        /// The ID of the Location with which the device is associated.
        /// </summary>
        /// <value>The ID of the Location with which the device is associated.</value>
        [DataMember(Name="locationId", EmitDefaultValue=false)]
        public string LocationId { get; set; }

        /// <summary>
        /// The ID of the Room with which the device is associated. If the device is not associated with any room, then this field will be null.
        /// </summary>
        /// <value>The ID of the Room with which the device is associated. If the device is not associated with any room, then this field will be null.</value>
        [DataMember(Name="roomId", EmitDefaultValue=false)]
        public string RoomId { get; set; }

        /// <summary>
        /// Deprecated please look under \&quot;dth\&quot;.
        /// </summary>
        /// <value>Deprecated please look under \&quot;dth\&quot;.</value>
        [DataMember(Name="deviceTypeId", EmitDefaultValue=false)]
        public string DeviceTypeId { get; set; }

        /// <summary>
        /// Deprecated please look under \&quot;dth\&quot;.
        /// </summary>
        /// <value>Deprecated please look under \&quot;dth\&quot;.</value>
        [DataMember(Name="deviceTypeName", EmitDefaultValue=false)]
        public string DeviceTypeName { get; set; }

        /// <summary>
        /// Deprecated please look under \&quot;dth\&quot;.
        /// </summary>
        /// <value>Deprecated please look under \&quot;dth\&quot;.</value>
        [DataMember(Name="deviceNetworkType", EmitDefaultValue=false)]
        public string DeviceNetworkType { get; set; }

        /// <summary>
        /// The IDs of all compenents on the device.
        /// </summary>
        /// <value>The IDs of all compenents on the device.</value>
        [DataMember(Name="components", EmitDefaultValue=false)]
        public List<DeviceComponent> Components { get; set; }

        /// <summary>
        /// Device details for all child devices of the device.
        /// </summary>
        /// <value>Device details for all child devices of the device.</value>
        [DataMember(Name="childDevices", EmitDefaultValue=false)]
        public List<Device> ChildDevices { get; set; }

        /// <summary>
        /// Gets or Sets Profile
        /// </summary>
        [DataMember(Name="profile", EmitDefaultValue=false)]
        public DeviceProfileReference Profile { get; set; }

        /// <summary>
        /// Gets or Sets App
        /// </summary>
        [DataMember(Name="app", EmitDefaultValue=false)]
        public AppDeviceDetails App { get; set; }

        /// <summary>
        /// Gets or Sets Dth
        /// </summary>
        [DataMember(Name="dth", EmitDefaultValue=false)]
        public DthDeviceDetails Dth { get; set; }

        /// <summary>
        /// Gets or Sets Ir
        /// </summary>
        [DataMember(Name="ir", EmitDefaultValue=false)]
        public IrDeviceDetails Ir { get; set; }

        /// <summary>
        /// Gets or Sets IrOcf
        /// </summary>
        [DataMember(Name="irOcf", EmitDefaultValue=false)]
        public IrDeviceDetails IrOcf { get; set; }

        /// <summary>
        /// Gets or Sets Viper
        /// </summary>
        [DataMember(Name="viper", EmitDefaultValue=false)]
        public ViperDeviceDetails Viper { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Device {\n");
            sb.Append("  DeviceId: ").Append(DeviceId).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Label: ").Append(Label).Append("\n");
            sb.Append("  DeviceManufacturerCode: ").Append(DeviceManufacturerCode).Append("\n");
            sb.Append("  LocationId: ").Append(LocationId).Append("\n");
            sb.Append("  RoomId: ").Append(RoomId).Append("\n");
            sb.Append("  DeviceTypeId: ").Append(DeviceTypeId).Append("\n");
            sb.Append("  DeviceTypeName: ").Append(DeviceTypeName).Append("\n");
            sb.Append("  DeviceNetworkType: ").Append(DeviceNetworkType).Append("\n");
            sb.Append("  Components: ").Append(Components).Append("\n");
            sb.Append("  ChildDevices: ").Append(ChildDevices).Append("\n");
            sb.Append("  Profile: ").Append(Profile).Append("\n");
            sb.Append("  App: ").Append(App).Append("\n");
            sb.Append("  Dth: ").Append(Dth).Append("\n");
            sb.Append("  Ir: ").Append(Ir).Append("\n");
            sb.Append("  IrOcf: ").Append(IrOcf).Append("\n");
            sb.Append("  Viper: ").Append(Viper).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
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
            return this.Equals(input as Device);
        }

        /// <summary>
        /// Returns true if Device instances are equal
        /// </summary>
        /// <param name="input">Instance of Device to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Device input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.DeviceId == input.DeviceId ||
                    (this.DeviceId != null &&
                    this.DeviceId.Equals(input.DeviceId))
                ) && 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.Label == input.Label ||
                    (this.Label != null &&
                    this.Label.Equals(input.Label))
                ) && 
                (
                    this.DeviceManufacturerCode == input.DeviceManufacturerCode ||
                    (this.DeviceManufacturerCode != null &&
                    this.DeviceManufacturerCode.Equals(input.DeviceManufacturerCode))
                ) && 
                (
                    this.LocationId == input.LocationId ||
                    (this.LocationId != null &&
                    this.LocationId.Equals(input.LocationId))
                ) && 
                (
                    this.RoomId == input.RoomId ||
                    (this.RoomId != null &&
                    this.RoomId.Equals(input.RoomId))
                ) && 
                (
                    this.DeviceTypeId == input.DeviceTypeId ||
                    (this.DeviceTypeId != null &&
                    this.DeviceTypeId.Equals(input.DeviceTypeId))
                ) && 
                (
                    this.DeviceTypeName == input.DeviceTypeName ||
                    (this.DeviceTypeName != null &&
                    this.DeviceTypeName.Equals(input.DeviceTypeName))
                ) && 
                (
                    this.DeviceNetworkType == input.DeviceNetworkType ||
                    (this.DeviceNetworkType != null &&
                    this.DeviceNetworkType.Equals(input.DeviceNetworkType))
                ) && 
                (
                    this.Components == input.Components ||
                    this.Components != null &&
                    input.Components != null &&
                    this.Components.SequenceEqual(input.Components)
                ) && 
                (
                    this.ChildDevices == input.ChildDevices ||
                    this.ChildDevices != null &&
                    input.ChildDevices != null &&
                    this.ChildDevices.SequenceEqual(input.ChildDevices)
                ) && 
                (
                    this.Profile == input.Profile ||
                    (this.Profile != null &&
                    this.Profile.Equals(input.Profile))
                ) && 
                (
                    this.App == input.App ||
                    (this.App != null &&
                    this.App.Equals(input.App))
                ) && 
                (
                    this.Dth == input.Dth ||
                    (this.Dth != null &&
                    this.Dth.Equals(input.Dth))
                ) && 
                (
                    this.Ir == input.Ir ||
                    (this.Ir != null &&
                    this.Ir.Equals(input.Ir))
                ) && 
                (
                    this.IrOcf == input.IrOcf ||
                    (this.IrOcf != null &&
                    this.IrOcf.Equals(input.IrOcf))
                ) && 
                (
                    this.Viper == input.Viper ||
                    (this.Viper != null &&
                    this.Viper.Equals(input.Viper))
                ) && 
                (
                    this.Type == input.Type ||
                    this.Type.Equals(input.Type)
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
                if (this.DeviceId != null)
                    hashCode = hashCode * 59 + this.DeviceId.GetHashCode();
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                if (this.Label != null)
                    hashCode = hashCode * 59 + this.Label.GetHashCode();
                if (this.DeviceManufacturerCode != null)
                    hashCode = hashCode * 59 + this.DeviceManufacturerCode.GetHashCode();
                if (this.LocationId != null)
                    hashCode = hashCode * 59 + this.LocationId.GetHashCode();
                if (this.RoomId != null)
                    hashCode = hashCode * 59 + this.RoomId.GetHashCode();
                if (this.DeviceTypeId != null)
                    hashCode = hashCode * 59 + this.DeviceTypeId.GetHashCode();
                if (this.DeviceTypeName != null)
                    hashCode = hashCode * 59 + this.DeviceTypeName.GetHashCode();
                if (this.DeviceNetworkType != null)
                    hashCode = hashCode * 59 + this.DeviceNetworkType.GetHashCode();
                if (this.Components != null)
                    hashCode = hashCode * 59 + this.Components.GetHashCode();
                if (this.ChildDevices != null)
                    hashCode = hashCode * 59 + this.ChildDevices.GetHashCode();
                if (this.Profile != null)
                    hashCode = hashCode * 59 + this.Profile.GetHashCode();
                if (this.App != null)
                    hashCode = hashCode * 59 + this.App.GetHashCode();
                if (this.Dth != null)
                    hashCode = hashCode * 59 + this.Dth.GetHashCode();
                if (this.Ir != null)
                    hashCode = hashCode * 59 + this.Ir.GetHashCode();
                if (this.IrOcf != null)
                    hashCode = hashCode * 59 + this.IrOcf.GetHashCode();
                if (this.Viper != null)
                    hashCode = hashCode * 59 + this.Viper.GetHashCode();
                hashCode = hashCode * 59 + this.Type.GetHashCode();
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
