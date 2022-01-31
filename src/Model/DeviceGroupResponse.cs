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
    /// A group
    /// </summary>
    [DataContract(Name = "DeviceGroupResponse")]
    public partial class DeviceGroupResponse : IEquatable<DeviceGroupResponse>, IValidatableObject
    {

        /// <summary>
        /// Gets or Sets GroupType
        /// </summary>
        [DataMember(Name = "groupType", IsRequired = true, EmitDefaultValue = false)]
        public DeviceGroupType GroupType { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceGroupResponse" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected DeviceGroupResponse() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceGroupResponse" /> class.
        /// </summary>
        /// <param name="groupName">Name of the group created (required).</param>
        /// <param name="groupType">groupType (required).</param>
        /// <param name="locationId">Location where this group is.</param>
        /// <param name="restrictionTier">restriction tier of the device group.</param>
        /// <param name="devices">An array of devices in the group (required).</param>
        /// <param name="deviceGroupId">a GUID that identifies a group (required).</param>
        /// <param name="deviceGroupCapabilities">Lowest common denominator capability of all the devices in the group (required).</param>
        /// <param name="roomId">roomId.</param>
        public DeviceGroupResponse(string groupName = default(string), DeviceGroupType groupType = default(DeviceGroupType), string locationId = default(string), int restrictionTier = default(int), List<DeviceGroupResponseDevices> devices = default(List<DeviceGroupResponseDevices>), string deviceGroupId = default(string), List<DeviceGroupCapability> deviceGroupCapabilities = default(List<DeviceGroupCapability>), string roomId = default(string))
        {
            // to ensure "groupName" is required (not null)
            if (groupName == null) {
                throw new ArgumentNullException("groupName is a required property for DeviceGroupResponse and cannot be null");
            }
            this.GroupName = groupName;
            this.GroupType = groupType;
            // to ensure "devices" is required (not null)
            if (devices == null) {
                throw new ArgumentNullException("devices is a required property for DeviceGroupResponse and cannot be null");
            }
            this.Devices = devices;
            // to ensure "deviceGroupId" is required (not null)
            if (deviceGroupId == null) {
                throw new ArgumentNullException("deviceGroupId is a required property for DeviceGroupResponse and cannot be null");
            }
            this.DeviceGroupId = deviceGroupId;
            // to ensure "deviceGroupCapabilities" is required (not null)
            if (deviceGroupCapabilities == null) {
                throw new ArgumentNullException("deviceGroupCapabilities is a required property for DeviceGroupResponse and cannot be null");
            }
            this.DeviceGroupCapabilities = deviceGroupCapabilities;
            this.LocationId = locationId;
            this.RestrictionTier = restrictionTier;
            this.RoomId = roomId;
        }

        /// <summary>
        /// Name of the group created
        /// </summary>
        /// <value>Name of the group created</value>
        [DataMember(Name = "groupName", IsRequired = true, EmitDefaultValue = false)]
        public string GroupName { get; set; }

        /// <summary>
        /// Location where this group is
        /// </summary>
        /// <value>Location where this group is</value>
        [DataMember(Name = "locationId", EmitDefaultValue = false)]
        public string LocationId { get; set; }

        /// <summary>
        /// restriction tier of the device group
        /// </summary>
        /// <value>restriction tier of the device group</value>
        [DataMember(Name = "restrictionTier", EmitDefaultValue = false)]
        public int RestrictionTier { get; set; }

        /// <summary>
        /// An array of devices in the group
        /// </summary>
        /// <value>An array of devices in the group</value>
        [DataMember(Name = "devices", IsRequired = true, EmitDefaultValue = false)]
        public List<DeviceGroupResponseDevices> Devices { get; set; }

        /// <summary>
        /// a GUID that identifies a group
        /// </summary>
        /// <value>a GUID that identifies a group</value>
        [DataMember(Name = "deviceGroupId", IsRequired = true, EmitDefaultValue = false)]
        public string DeviceGroupId { get; set; }

        /// <summary>
        /// Lowest common denominator capability of all the devices in the group
        /// </summary>
        /// <value>Lowest common denominator capability of all the devices in the group</value>
        [DataMember(Name = "deviceGroupCapabilities", IsRequired = true, EmitDefaultValue = false)]
        public List<DeviceGroupCapability> DeviceGroupCapabilities { get; set; }

        /// <summary>
        /// Gets or Sets RoomId
        /// </summary>
        [DataMember(Name = "roomId", EmitDefaultValue = false)]
        public string RoomId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class DeviceGroupResponse {\n");
            sb.Append("  GroupName: ").Append(GroupName).Append("\n");
            sb.Append("  GroupType: ").Append(GroupType).Append("\n");
            sb.Append("  LocationId: ").Append(LocationId).Append("\n");
            sb.Append("  RestrictionTier: ").Append(RestrictionTier).Append("\n");
            sb.Append("  Devices: ").Append(Devices).Append("\n");
            sb.Append("  DeviceGroupId: ").Append(DeviceGroupId).Append("\n");
            sb.Append("  DeviceGroupCapabilities: ").Append(DeviceGroupCapabilities).Append("\n");
            sb.Append("  RoomId: ").Append(RoomId).Append("\n");
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
            return this.Equals(input as DeviceGroupResponse);
        }

        /// <summary>
        /// Returns true if DeviceGroupResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of DeviceGroupResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DeviceGroupResponse input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.GroupName == input.GroupName ||
                    (this.GroupName != null &&
                    this.GroupName.Equals(input.GroupName))
                ) && 
                (
                    this.GroupType == input.GroupType ||
                    this.GroupType.Equals(input.GroupType)
                ) && 
                (
                    this.LocationId == input.LocationId ||
                    (this.LocationId != null &&
                    this.LocationId.Equals(input.LocationId))
                ) && 
                (
                    this.RestrictionTier == input.RestrictionTier ||
                    this.RestrictionTier.Equals(input.RestrictionTier)
                ) && 
                (
                    this.Devices == input.Devices ||
                    this.Devices != null &&
                    input.Devices != null &&
                    this.Devices.SequenceEqual(input.Devices)
                ) && 
                (
                    this.DeviceGroupId == input.DeviceGroupId ||
                    (this.DeviceGroupId != null &&
                    this.DeviceGroupId.Equals(input.DeviceGroupId))
                ) && 
                (
                    this.DeviceGroupCapabilities == input.DeviceGroupCapabilities ||
                    this.DeviceGroupCapabilities != null &&
                    input.DeviceGroupCapabilities != null &&
                    this.DeviceGroupCapabilities.SequenceEqual(input.DeviceGroupCapabilities)
                ) && 
                (
                    this.RoomId == input.RoomId ||
                    (this.RoomId != null &&
                    this.RoomId.Equals(input.RoomId))
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
                if (this.GroupName != null)
                {
                    hashCode = (hashCode * 59) + this.GroupName.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.GroupType.GetHashCode();
                if (this.LocationId != null)
                {
                    hashCode = (hashCode * 59) + this.LocationId.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.RestrictionTier.GetHashCode();
                if (this.Devices != null)
                {
                    hashCode = (hashCode * 59) + this.Devices.GetHashCode();
                }
                if (this.DeviceGroupId != null)
                {
                    hashCode = (hashCode * 59) + this.DeviceGroupId.GetHashCode();
                }
                if (this.DeviceGroupCapabilities != null)
                {
                    hashCode = (hashCode * 59) + this.DeviceGroupCapabilities.GetHashCode();
                }
                if (this.RoomId != null)
                {
                    hashCode = (hashCode * 59) + this.RoomId.GetHashCode();
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