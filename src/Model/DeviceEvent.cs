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
    /// An event on a device that matched a subscription for this app.
    /// </summary>
    [DataContract(Name = "DeviceEvent")]
    public partial class DeviceEvent : IEquatable<DeviceEvent>, IValidatableObject
    {

        /// <summary>
        /// Gets or Sets OwnerType
        /// </summary>
        [DataMember(Name = "ownerType", EmitDefaultValue = false)]
        public EventOwnerType? OwnerType { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceEvent" /> class.
        /// </summary>
        /// <param name="eventId">The ID of the event..</param>
        /// <param name="locationId">The id of the location in which the event was triggered.  This field is not used or populated for user-level events.  Location id may also be sent in &#x60;ownerId&#x60; with &#x60;ownerType&#x60; &#x3D; &#x60;LOCATION&#x60;. .</param>
        /// <param name="ownerId">ID for what owns the device event. Works in tandem with &#x60;ownerType&#x60; as a composite identifier..</param>
        /// <param name="ownerType">ownerType.</param>
        /// <param name="deviceId">The ID of the device associated with the DEVICE_EVENT..</param>
        /// <param name="componentId">The name of the component on the device that the event is associated with..</param>
        /// <param name="capability">The name of the capability associated with the DEVICE_EVENT..</param>
        /// <param name="attribute">The name of the DEVICE_EVENT. This typically corresponds to an attribute name of the device-handler’s capabilities..</param>
        /// <param name="value">The value of the event. The type of the value is dependent on the capability&#39;s attribute type. .</param>
        /// <param name="valueType">The root level data type of the value field. The data types are representitive of standard JSON data types. .</param>
        /// <param name="unit">Unit of the value field..</param>
        /// <param name="stateChange">Whether or not the state of the device has changed as a result of the DEVICE_EVENT..</param>
        /// <param name="data">json map as defined by capability data schema.</param>
        /// <param name="subscriptionName">The name of subscription that caused delivery..</param>
        public DeviceEvent(string eventId = default(string), string locationId = default(string), string ownerId = default(string), EventOwnerType? ownerType = default(EventOwnerType?), string deviceId = default(string), string componentId = default(string), string capability = default(string), string attribute = default(string), Object value = default(Object), string valueType = default(string), string unit = default(string), bool stateChange = default(bool), Object data = default(Object), string subscriptionName = default(string))
        {
            this.EventId = eventId;
            this.LocationId = locationId;
            this.OwnerId = ownerId;
            this.OwnerType = ownerType;
            this.DeviceId = deviceId;
            this.ComponentId = componentId;
            this.Capability = capability;
            this.Attribute = attribute;
            this.Value = value;
            this.ValueType = valueType;
            this.Unit = unit;
            this.StateChange = stateChange;
            this.Data = data;
            this.SubscriptionName = subscriptionName;
        }

        /// <summary>
        /// The ID of the event.
        /// </summary>
        /// <value>The ID of the event.</value>
        [DataMember(Name = "eventId", EmitDefaultValue = false)]
        public string EventId { get; set; }

        /// <summary>
        /// The id of the location in which the event was triggered.  This field is not used or populated for user-level events.  Location id may also be sent in &#x60;ownerId&#x60; with &#x60;ownerType&#x60; &#x3D; &#x60;LOCATION&#x60;. 
        /// </summary>
        /// <value>The id of the location in which the event was triggered.  This field is not used or populated for user-level events.  Location id may also be sent in &#x60;ownerId&#x60; with &#x60;ownerType&#x60; &#x3D; &#x60;LOCATION&#x60;. </value>
        [DataMember(Name = "locationId", EmitDefaultValue = false)]
        public string LocationId { get; set; }

        /// <summary>
        /// ID for what owns the device event. Works in tandem with &#x60;ownerType&#x60; as a composite identifier.
        /// </summary>
        /// <value>ID for what owns the device event. Works in tandem with &#x60;ownerType&#x60; as a composite identifier.</value>
        [DataMember(Name = "ownerId", EmitDefaultValue = false)]
        public string OwnerId { get; set; }

        /// <summary>
        /// The ID of the device associated with the DEVICE_EVENT.
        /// </summary>
        /// <value>The ID of the device associated with the DEVICE_EVENT.</value>
        [DataMember(Name = "deviceId", EmitDefaultValue = false)]
        public string DeviceId { get; set; }

        /// <summary>
        /// The name of the component on the device that the event is associated with.
        /// </summary>
        /// <value>The name of the component on the device that the event is associated with.</value>
        [DataMember(Name = "componentId", EmitDefaultValue = false)]
        public string ComponentId { get; set; }

        /// <summary>
        /// The name of the capability associated with the DEVICE_EVENT.
        /// </summary>
        /// <value>The name of the capability associated with the DEVICE_EVENT.</value>
        [DataMember(Name = "capability", EmitDefaultValue = false)]
        public string Capability { get; set; }

        /// <summary>
        /// The name of the DEVICE_EVENT. This typically corresponds to an attribute name of the device-handler’s capabilities.
        /// </summary>
        /// <value>The name of the DEVICE_EVENT. This typically corresponds to an attribute name of the device-handler’s capabilities.</value>
        [DataMember(Name = "attribute", EmitDefaultValue = false)]
        public string Attribute { get; set; }

        /// <summary>
        /// The value of the event. The type of the value is dependent on the capability&#39;s attribute type. 
        /// </summary>
        /// <value>The value of the event. The type of the value is dependent on the capability&#39;s attribute type. </value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public Object Value { get; set; }

        /// <summary>
        /// The root level data type of the value field. The data types are representitive of standard JSON data types. 
        /// </summary>
        /// <value>The root level data type of the value field. The data types are representitive of standard JSON data types. </value>
        [DataMember(Name = "valueType", EmitDefaultValue = false)]
        public string ValueType { get; set; }

        /// <summary>
        /// Unit of the value field.
        /// </summary>
        /// <value>Unit of the value field.</value>
        [DataMember(Name = "unit", EmitDefaultValue = false)]
        public string Unit { get; set; }

        /// <summary>
        /// Whether or not the state of the device has changed as a result of the DEVICE_EVENT.
        /// </summary>
        /// <value>Whether or not the state of the device has changed as a result of the DEVICE_EVENT.</value>
        [DataMember(Name = "stateChange", EmitDefaultValue = true)]
        public bool StateChange { get; set; }

        /// <summary>
        /// json map as defined by capability data schema
        /// </summary>
        /// <value>json map as defined by capability data schema</value>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public Object Data { get; set; }

        /// <summary>
        /// The name of subscription that caused delivery.
        /// </summary>
        /// <value>The name of subscription that caused delivery.</value>
        [DataMember(Name = "subscriptionName", EmitDefaultValue = false)]
        public string SubscriptionName { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class DeviceEvent {\n");
            sb.Append("  EventId: ").Append(EventId).Append("\n");
            sb.Append("  LocationId: ").Append(LocationId).Append("\n");
            sb.Append("  OwnerId: ").Append(OwnerId).Append("\n");
            sb.Append("  OwnerType: ").Append(OwnerType).Append("\n");
            sb.Append("  DeviceId: ").Append(DeviceId).Append("\n");
            sb.Append("  ComponentId: ").Append(ComponentId).Append("\n");
            sb.Append("  Capability: ").Append(Capability).Append("\n");
            sb.Append("  Attribute: ").Append(Attribute).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
            sb.Append("  ValueType: ").Append(ValueType).Append("\n");
            sb.Append("  Unit: ").Append(Unit).Append("\n");
            sb.Append("  StateChange: ").Append(StateChange).Append("\n");
            sb.Append("  Data: ").Append(Data).Append("\n");
            sb.Append("  SubscriptionName: ").Append(SubscriptionName).Append("\n");
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
            return this.Equals(input as DeviceEvent);
        }

        /// <summary>
        /// Returns true if DeviceEvent instances are equal
        /// </summary>
        /// <param name="input">Instance of DeviceEvent to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(DeviceEvent input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.EventId == input.EventId ||
                    (this.EventId != null &&
                    this.EventId.Equals(input.EventId))
                ) && 
                (
                    this.LocationId == input.LocationId ||
                    (this.LocationId != null &&
                    this.LocationId.Equals(input.LocationId))
                ) && 
                (
                    this.OwnerId == input.OwnerId ||
                    (this.OwnerId != null &&
                    this.OwnerId.Equals(input.OwnerId))
                ) && 
                (
                    this.OwnerType == input.OwnerType ||
                    this.OwnerType.Equals(input.OwnerType)
                ) && 
                (
                    this.DeviceId == input.DeviceId ||
                    (this.DeviceId != null &&
                    this.DeviceId.Equals(input.DeviceId))
                ) && 
                (
                    this.ComponentId == input.ComponentId ||
                    (this.ComponentId != null &&
                    this.ComponentId.Equals(input.ComponentId))
                ) && 
                (
                    this.Capability == input.Capability ||
                    (this.Capability != null &&
                    this.Capability.Equals(input.Capability))
                ) && 
                (
                    this.Attribute == input.Attribute ||
                    (this.Attribute != null &&
                    this.Attribute.Equals(input.Attribute))
                ) && 
                (
                    this.Value == input.Value ||
                    (this.Value != null &&
                    this.Value.Equals(input.Value))
                ) && 
                (
                    this.ValueType == input.ValueType ||
                    (this.ValueType != null &&
                    this.ValueType.Equals(input.ValueType))
                ) && 
                (
                    this.Unit == input.Unit ||
                    (this.Unit != null &&
                    this.Unit.Equals(input.Unit))
                ) && 
                (
                    this.StateChange == input.StateChange ||
                    this.StateChange.Equals(input.StateChange)
                ) && 
                (
                    this.Data == input.Data ||
                    (this.Data != null &&
                    this.Data.Equals(input.Data))
                ) && 
                (
                    this.SubscriptionName == input.SubscriptionName ||
                    (this.SubscriptionName != null &&
                    this.SubscriptionName.Equals(input.SubscriptionName))
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
                if (this.EventId != null)
                {
                    hashCode = (hashCode * 59) + this.EventId.GetHashCode();
                }
                if (this.LocationId != null)
                {
                    hashCode = (hashCode * 59) + this.LocationId.GetHashCode();
                }
                if (this.OwnerId != null)
                {
                    hashCode = (hashCode * 59) + this.OwnerId.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.OwnerType.GetHashCode();
                if (this.DeviceId != null)
                {
                    hashCode = (hashCode * 59) + this.DeviceId.GetHashCode();
                }
                if (this.ComponentId != null)
                {
                    hashCode = (hashCode * 59) + this.ComponentId.GetHashCode();
                }
                if (this.Capability != null)
                {
                    hashCode = (hashCode * 59) + this.Capability.GetHashCode();
                }
                if (this.Attribute != null)
                {
                    hashCode = (hashCode * 59) + this.Attribute.GetHashCode();
                }
                if (this.Value != null)
                {
                    hashCode = (hashCode * 59) + this.Value.GetHashCode();
                }
                if (this.ValueType != null)
                {
                    hashCode = (hashCode * 59) + this.ValueType.GetHashCode();
                }
                if (this.Unit != null)
                {
                    hashCode = (hashCode * 59) + this.Unit.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.StateChange.GetHashCode();
                if (this.Data != null)
                {
                    hashCode = (hashCode * 59) + this.Data.GetHashCode();
                }
                if (this.SubscriptionName != null)
                {
                    hashCode = (hashCode * 59) + this.SubscriptionName.GetHashCode();
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
