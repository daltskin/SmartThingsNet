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
    /// Device
    /// </summary>
    [DataContract(Name = "Device")]
    public partial class Device : IEquatable<Device>, IValidatableObject
    {

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", IsRequired = false, EmitDefaultValue = false)]
        public DeviceIntegrationType Type { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Device" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected Device() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Device" /> class.
        /// </summary>
        /// <param name="deviceId">The identifier for the Device instance. (required).</param>
        /// <param name="name">The name that the Device integration (Device Handler or SmartApp) defines for the Device..</param>
        /// <param name="label">The name that a user chooses for the Device. This defaults to the same value as name..</param>
        /// <param name="manufacturerName">The Device manufacturer name. (required).</param>
        /// <param name="presentationId">A non-unique id that is used to help drive UI information. (required).</param>
        /// <param name="deviceManufacturerCode">The Device manufacturer code..</param>
        /// <param name="locationId">The ID of the Location with which the Device is associated..</param>
        /// <param name="ownerId">The identifier for the owner of the Device instance..</param>
        /// <param name="roomId">The ID of the Room with which the Device is associated. If the Device is not associated with any room, this field will be null..</param>
        /// <param name="deviceTypeId">Deprecated please look under \&quot;dth\&quot;..</param>
        /// <param name="deviceTypeName">Deprecated please look under \&quot;dth\&quot;..</param>
        /// <param name="deviceNetworkType">Deprecated please look under \&quot;dth\&quot;..</param>
        /// <param name="components">The IDs of all compenents on the Device..</param>
        /// <param name="createTime">The time when the device was created at..</param>
        /// <param name="parentDeviceId">The id of the Parent device..</param>
        /// <param name="childDevices">Device details for all child devices of the Device..</param>
        /// <param name="profile">profile.</param>
        /// <param name="app">app.</param>
        /// <param name="ble">ble.</param>
        /// <param name="bleD2D">bleD2D.</param>
        /// <param name="dth">dth.</param>
        /// <param name="lan">lan.</param>
        /// <param name="zigbee">zigbee.</param>
        /// <param name="zwave">zwave.</param>
        /// <param name="matter">matter.</param>
        /// <param name="ir">ir.</param>
        /// <param name="irOcf">irOcf.</param>
        /// <param name="ocf">ocf.</param>
        /// <param name="viper">viper.</param>
        /// <param name="type">type (required).</param>
        /// <param name="restrictionTier">Restriction tier of the device, if any. (required).</param>
        /// <param name="allowed">List of client-facing action identifiers that are currently permitted for the user. If the value of this property is not null, then any action not included in the list value of the property is currently prohibited for the user.   &lt;ul&gt;   &lt;li&gt; w:devices - the user can change device details   &lt;li&gt; w:devices:locationId - the user can move the device from a location   &lt;li&gt; w:devices:roomId - the user can move or remove the device from a room   &lt;li&gt; x:devices - the user can execute commands on the device   &lt;li&gt; d:devices - the user can uninstall the device   &lt;/ul&gt; .</param>
        public Device(string deviceId = default(string), string name = default(string), string label = default(string), string manufacturerName = default(string), string presentationId = default(string), string deviceManufacturerCode = default(string), string locationId = default(string), string ownerId = default(string), string roomId = default(string), string deviceTypeId = default(string), string deviceTypeName = default(string), string deviceNetworkType = default(string), List<DeviceComponent> components = default(List<DeviceComponent>), string createTime = default(string), string parentDeviceId = default(string), List<Device> childDevices = default(List<Device>), DeviceProfileReference profile = default(DeviceProfileReference), AppDeviceDetails app = default(AppDeviceDetails), Object ble = default(Object), BleD2DDeviceDetails bleD2D = default(BleD2DDeviceDetails), DthDeviceDetails dth = default(DthDeviceDetails), LanDeviceDetails lan = default(LanDeviceDetails), ZigbeeDeviceDetails zigbee = default(ZigbeeDeviceDetails), ZwaveDeviceDetails zwave = default(ZwaveDeviceDetails), MatterDeviceDetails matter = default(MatterDeviceDetails), IrDeviceDetails ir = default(IrDeviceDetails), IrDeviceDetails irOcf = default(IrDeviceDetails), OcfDeviceDetails ocf = default(OcfDeviceDetails), ViperDeviceDetails viper = default(ViperDeviceDetails), DeviceIntegrationType type = default(DeviceIntegrationType), int restrictionTier = default(int), List<string> allowed = default(List<string>))
        {
            // to ensure "deviceId" is required (not null)
            if (deviceId == null) {
                throw new ArgumentNullException("deviceId is a required property for Device and cannot be null");
            }
            this.DeviceId = deviceId;
            // to ensure "manufacturerName" is required (not null)
            if (manufacturerName == null) {
                throw new ArgumentNullException("manufacturerName is a required property for Device and cannot be null");
            }
            this.ManufacturerName = manufacturerName;
            // to ensure "presentationId" is required (not null)
            if (presentationId == null) {
                throw new ArgumentNullException("presentationId is a required property for Device and cannot be null");
            }
            this.PresentationId = presentationId;
            this.Type = type;
            this.RestrictionTier = restrictionTier;
            this.Name = name;
            this.Label = label;
            this.DeviceManufacturerCode = deviceManufacturerCode;
            this.LocationId = locationId;
            this.OwnerId = ownerId;
            this.RoomId = roomId;
            this.DeviceTypeId = deviceTypeId;
            this.DeviceTypeName = deviceTypeName;
            this.DeviceNetworkType = deviceNetworkType;
            this.Components = components;
            this.CreateTime = createTime;
            this.ParentDeviceId = parentDeviceId;
            this.ChildDevices = childDevices;
            this.Profile = profile;
            this.App = app;
            this.Ble = ble;
            this.BleD2D = bleD2D;
            this.Dth = dth;
            this.Lan = lan;
            this.Zigbee = zigbee;
            this.Zwave = zwave;
            this.Matter = matter;
            this.Ir = ir;
            this.IrOcf = irOcf;
            this.Ocf = ocf;
            this.Viper = viper;
            this.Allowed = allowed;
        }

        /// <summary>
        /// The identifier for the Device instance.
        /// </summary>
        /// <value>The identifier for the Device instance.</value>
        [DataMember(Name = "deviceId", IsRequired = true, EmitDefaultValue = false)]
        public string DeviceId { get; set; }

        /// <summary>
        /// The name that the Device integration (Device Handler or SmartApp) defines for the Device.
        /// </summary>
        /// <value>The name that the Device integration (Device Handler or SmartApp) defines for the Device.</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// The name that a user chooses for the Device. This defaults to the same value as name.
        /// </summary>
        /// <value>The name that a user chooses for the Device. This defaults to the same value as name.</value>
        [DataMember(Name = "label", EmitDefaultValue = false)]
        public string Label { get; set; }

        /// <summary>
        /// The Device manufacturer name.
        /// </summary>
        /// <value>The Device manufacturer name.</value>
        [DataMember(Name = "manufacturerName", IsRequired = false, EmitDefaultValue = false)]
        public string ManufacturerName { get; set; }

        /// <summary>
        /// A non-unique id that is used to help drive UI information.
        /// </summary>
        /// <value>A non-unique id that is used to help drive UI information.</value>
        [DataMember(Name = "presentationId", IsRequired = false, EmitDefaultValue = false)]
        public string PresentationId { get; set; }

        /// <summary>
        /// The Device manufacturer code.
        /// </summary>
        /// <value>The Device manufacturer code.</value>
        [DataMember(Name = "deviceManufacturerCode", EmitDefaultValue = false)]
        public string DeviceManufacturerCode { get; set; }

        /// <summary>
        /// The ID of the Location with which the Device is associated.
        /// </summary>
        /// <value>The ID of the Location with which the Device is associated.</value>
        [DataMember(Name = "locationId", EmitDefaultValue = false)]
        public string LocationId { get; set; }

        /// <summary>
        /// The identifier for the owner of the Device instance.
        /// </summary>
        /// <value>The identifier for the owner of the Device instance.</value>
        [DataMember(Name = "ownerId", EmitDefaultValue = false)]
        public string OwnerId { get; set; }

        /// <summary>
        /// The ID of the Room with which the Device is associated. If the Device is not associated with any room, this field will be null.
        /// </summary>
        /// <value>The ID of the Room with which the Device is associated. If the Device is not associated with any room, this field will be null.</value>
        [DataMember(Name = "roomId", EmitDefaultValue = false)]
        public string RoomId { get; set; }

        /// <summary>
        /// Deprecated please look under \&quot;dth\&quot;.
        /// </summary>
        /// <value>Deprecated please look under \&quot;dth\&quot;.</value>
        [DataMember(Name = "deviceTypeId", EmitDefaultValue = false)]
        public string DeviceTypeId { get; set; }

        /// <summary>
        /// Deprecated please look under \&quot;dth\&quot;.
        /// </summary>
        /// <value>Deprecated please look under \&quot;dth\&quot;.</value>
        [DataMember(Name = "deviceTypeName", EmitDefaultValue = false)]
        public string DeviceTypeName { get; set; }

        /// <summary>
        /// Deprecated please look under \&quot;dth\&quot;.
        /// </summary>
        /// <value>Deprecated please look under \&quot;dth\&quot;.</value>
        [DataMember(Name = "deviceNetworkType", EmitDefaultValue = false)]
        public string DeviceNetworkType { get; set; }

        /// <summary>
        /// The IDs of all compenents on the Device.
        /// </summary>
        /// <value>The IDs of all compenents on the Device.</value>
        [DataMember(Name = "components", EmitDefaultValue = false)]
        public List<DeviceComponent> Components { get; set; }

        /// <summary>
        /// The time when the device was created at.
        /// </summary>
        /// <value>The time when the device was created at.</value>
        [DataMember(Name = "createTime", EmitDefaultValue = false)]
        public string CreateTime { get; set; }

        /// <summary>
        /// The id of the Parent device.
        /// </summary>
        /// <value>The id of the Parent device.</value>
        [DataMember(Name = "parentDeviceId", EmitDefaultValue = false)]
        public string ParentDeviceId { get; set; }

        /// <summary>
        /// Device details for all child devices of the Device.
        /// </summary>
        /// <value>Device details for all child devices of the Device.</value>
        [DataMember(Name = "childDevices", EmitDefaultValue = false)]
        public List<Device> ChildDevices { get; set; }

        /// <summary>
        /// Gets or Sets Profile
        /// </summary>
        [DataMember(Name = "profile", EmitDefaultValue = false)]
        public DeviceProfileReference Profile { get; set; }

        /// <summary>
        /// Gets or Sets App
        /// </summary>
        [DataMember(Name = "app", EmitDefaultValue = false)]
        public AppDeviceDetails App { get; set; }

        /// <summary>
        /// Gets or Sets Ble
        /// </summary>
        [DataMember(Name = "ble", EmitDefaultValue = false)]
        public Object Ble { get; set; }

        /// <summary>
        /// Gets or Sets BleD2D
        /// </summary>
        [DataMember(Name = "bleD2D", EmitDefaultValue = false)]
        public BleD2DDeviceDetails BleD2D { get; set; }

        /// <summary>
        /// Gets or Sets Dth
        /// </summary>
        [DataMember(Name = "dth", EmitDefaultValue = false)]
        public DthDeviceDetails Dth { get; set; }

        /// <summary>
        /// Gets or Sets Lan
        /// </summary>
        [DataMember(Name = "lan", EmitDefaultValue = false)]
        public LanDeviceDetails Lan { get; set; }

        /// <summary>
        /// Gets or Sets Zigbee
        /// </summary>
        [DataMember(Name = "zigbee", EmitDefaultValue = false)]
        public ZigbeeDeviceDetails Zigbee { get; set; }

        /// <summary>
        /// Gets or Sets Zwave
        /// </summary>
        [DataMember(Name = "zwave", EmitDefaultValue = false)]
        public ZwaveDeviceDetails Zwave { get; set; }

        /// <summary>
        /// Gets or Sets Matter
        /// </summary>
        [DataMember(Name = "matter", EmitDefaultValue = false)]
        public MatterDeviceDetails Matter { get; set; }

        /// <summary>
        /// Gets or Sets Ir
        /// </summary>
        [DataMember(Name = "ir", EmitDefaultValue = false)]
        public IrDeviceDetails Ir { get; set; }

        /// <summary>
        /// Gets or Sets IrOcf
        /// </summary>
        [DataMember(Name = "irOcf", EmitDefaultValue = false)]
        public IrDeviceDetails IrOcf { get; set; }

        /// <summary>
        /// Gets or Sets Ocf
        /// </summary>
        [DataMember(Name = "ocf", EmitDefaultValue = false)]
        public OcfDeviceDetails Ocf { get; set; }

        /// <summary>
        /// Gets or Sets Viper
        /// </summary>
        [DataMember(Name = "viper", EmitDefaultValue = false)]
        public ViperDeviceDetails Viper { get; set; }

        /// <summary>
        /// Restriction tier of the device, if any.
        /// </summary>
        /// <value>Restriction tier of the device, if any.</value>
        [DataMember(Name = "restrictionTier", IsRequired = false, EmitDefaultValue = false)]
        public int RestrictionTier { get; set; }

        /// <summary>
        /// List of client-facing action identifiers that are currently permitted for the user. If the value of this property is not null, then any action not included in the list value of the property is currently prohibited for the user.   &lt;ul&gt;   &lt;li&gt; w:devices - the user can change device details   &lt;li&gt; w:devices:locationId - the user can move the device from a location   &lt;li&gt; w:devices:roomId - the user can move or remove the device from a room   &lt;li&gt; x:devices - the user can execute commands on the device   &lt;li&gt; d:devices - the user can uninstall the device   &lt;/ul&gt; 
        /// </summary>
        /// <value>List of client-facing action identifiers that are currently permitted for the user. If the value of this property is not null, then any action not included in the list value of the property is currently prohibited for the user.   &lt;ul&gt;   &lt;li&gt; w:devices - the user can change device details   &lt;li&gt; w:devices:locationId - the user can move the device from a location   &lt;li&gt; w:devices:roomId - the user can move or remove the device from a room   &lt;li&gt; x:devices - the user can execute commands on the device   &lt;li&gt; d:devices - the user can uninstall the device   &lt;/ul&gt; </value>
        [DataMember(Name = "allowed", EmitDefaultValue = false)]
        public List<string> Allowed { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Device {\n");
            sb.Append("  DeviceId: ").Append(DeviceId).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Label: ").Append(Label).Append("\n");
            sb.Append("  ManufacturerName: ").Append(ManufacturerName).Append("\n");
            sb.Append("  PresentationId: ").Append(PresentationId).Append("\n");
            sb.Append("  DeviceManufacturerCode: ").Append(DeviceManufacturerCode).Append("\n");
            sb.Append("  LocationId: ").Append(LocationId).Append("\n");
            sb.Append("  OwnerId: ").Append(OwnerId).Append("\n");
            sb.Append("  RoomId: ").Append(RoomId).Append("\n");
            sb.Append("  DeviceTypeId: ").Append(DeviceTypeId).Append("\n");
            sb.Append("  DeviceTypeName: ").Append(DeviceTypeName).Append("\n");
            sb.Append("  DeviceNetworkType: ").Append(DeviceNetworkType).Append("\n");
            sb.Append("  Components: ").Append(Components).Append("\n");
            sb.Append("  CreateTime: ").Append(CreateTime).Append("\n");
            sb.Append("  ParentDeviceId: ").Append(ParentDeviceId).Append("\n");
            sb.Append("  ChildDevices: ").Append(ChildDevices).Append("\n");
            sb.Append("  Profile: ").Append(Profile).Append("\n");
            sb.Append("  App: ").Append(App).Append("\n");
            sb.Append("  Ble: ").Append(Ble).Append("\n");
            sb.Append("  BleD2D: ").Append(BleD2D).Append("\n");
            sb.Append("  Dth: ").Append(Dth).Append("\n");
            sb.Append("  Lan: ").Append(Lan).Append("\n");
            sb.Append("  Zigbee: ").Append(Zigbee).Append("\n");
            sb.Append("  Zwave: ").Append(Zwave).Append("\n");
            sb.Append("  Matter: ").Append(Matter).Append("\n");
            sb.Append("  Ir: ").Append(Ir).Append("\n");
            sb.Append("  IrOcf: ").Append(IrOcf).Append("\n");
            sb.Append("  Ocf: ").Append(Ocf).Append("\n");
            sb.Append("  Viper: ").Append(Viper).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  RestrictionTier: ").Append(RestrictionTier).Append("\n");
            sb.Append("  Allowed: ").Append(Allowed).Append("\n");
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
            {
                return false;
            }
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
                    this.ManufacturerName == input.ManufacturerName ||
                    (this.ManufacturerName != null &&
                    this.ManufacturerName.Equals(input.ManufacturerName))
                ) && 
                (
                    this.PresentationId == input.PresentationId ||
                    (this.PresentationId != null &&
                    this.PresentationId.Equals(input.PresentationId))
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
                    this.OwnerId == input.OwnerId ||
                    (this.OwnerId != null &&
                    this.OwnerId.Equals(input.OwnerId))
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
                    this.CreateTime == input.CreateTime ||
                    (this.CreateTime != null &&
                    this.CreateTime.Equals(input.CreateTime))
                ) && 
                (
                    this.ParentDeviceId == input.ParentDeviceId ||
                    (this.ParentDeviceId != null &&
                    this.ParentDeviceId.Equals(input.ParentDeviceId))
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
                    this.Ble == input.Ble ||
                    (this.Ble != null &&
                    this.Ble.Equals(input.Ble))
                ) && 
                (
                    this.BleD2D == input.BleD2D ||
                    (this.BleD2D != null &&
                    this.BleD2D.Equals(input.BleD2D))
                ) && 
                (
                    this.Dth == input.Dth ||
                    (this.Dth != null &&
                    this.Dth.Equals(input.Dth))
                ) && 
                (
                    this.Lan == input.Lan ||
                    (this.Lan != null &&
                    this.Lan.Equals(input.Lan))
                ) && 
                (
                    this.Zigbee == input.Zigbee ||
                    (this.Zigbee != null &&
                    this.Zigbee.Equals(input.Zigbee))
                ) && 
                (
                    this.Zwave == input.Zwave ||
                    (this.Zwave != null &&
                    this.Zwave.Equals(input.Zwave))
                ) && 
                (
                    this.Matter == input.Matter ||
                    (this.Matter != null &&
                    this.Matter.Equals(input.Matter))
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
                    this.Ocf == input.Ocf ||
                    (this.Ocf != null &&
                    this.Ocf.Equals(input.Ocf))
                ) && 
                (
                    this.Viper == input.Viper ||
                    (this.Viper != null &&
                    this.Viper.Equals(input.Viper))
                ) && 
                (
                    this.Type == input.Type ||
                    this.Type.Equals(input.Type)
                ) && 
                (
                    this.RestrictionTier == input.RestrictionTier ||
                    this.RestrictionTier.Equals(input.RestrictionTier)
                ) && 
                (
                    this.Allowed == input.Allowed ||
                    this.Allowed != null &&
                    input.Allowed != null &&
                    this.Allowed.SequenceEqual(input.Allowed)
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
                {
                    hashCode = (hashCode * 59) + this.DeviceId.GetHashCode();
                }
                if (this.Name != null)
                {
                    hashCode = (hashCode * 59) + this.Name.GetHashCode();
                }
                if (this.Label != null)
                {
                    hashCode = (hashCode * 59) + this.Label.GetHashCode();
                }
                if (this.ManufacturerName != null)
                {
                    hashCode = (hashCode * 59) + this.ManufacturerName.GetHashCode();
                }
                if (this.PresentationId != null)
                {
                    hashCode = (hashCode * 59) + this.PresentationId.GetHashCode();
                }
                if (this.DeviceManufacturerCode != null)
                {
                    hashCode = (hashCode * 59) + this.DeviceManufacturerCode.GetHashCode();
                }
                if (this.LocationId != null)
                {
                    hashCode = (hashCode * 59) + this.LocationId.GetHashCode();
                }
                if (this.OwnerId != null)
                {
                    hashCode = (hashCode * 59) + this.OwnerId.GetHashCode();
                }
                if (this.RoomId != null)
                {
                    hashCode = (hashCode * 59) + this.RoomId.GetHashCode();
                }
                if (this.DeviceTypeId != null)
                {
                    hashCode = (hashCode * 59) + this.DeviceTypeId.GetHashCode();
                }
                if (this.DeviceTypeName != null)
                {
                    hashCode = (hashCode * 59) + this.DeviceTypeName.GetHashCode();
                }
                if (this.DeviceNetworkType != null)
                {
                    hashCode = (hashCode * 59) + this.DeviceNetworkType.GetHashCode();
                }
                if (this.Components != null)
                {
                    hashCode = (hashCode * 59) + this.Components.GetHashCode();
                }
                if (this.CreateTime != null)
                {
                    hashCode = (hashCode * 59) + this.CreateTime.GetHashCode();
                }
                if (this.ParentDeviceId != null)
                {
                    hashCode = (hashCode * 59) + this.ParentDeviceId.GetHashCode();
                }
                if (this.ChildDevices != null)
                {
                    hashCode = (hashCode * 59) + this.ChildDevices.GetHashCode();
                }
                if (this.Profile != null)
                {
                    hashCode = (hashCode * 59) + this.Profile.GetHashCode();
                }
                if (this.App != null)
                {
                    hashCode = (hashCode * 59) + this.App.GetHashCode();
                }
                if (this.Ble != null)
                {
                    hashCode = (hashCode * 59) + this.Ble.GetHashCode();
                }
                if (this.BleD2D != null)
                {
                    hashCode = (hashCode * 59) + this.BleD2D.GetHashCode();
                }
                if (this.Dth != null)
                {
                    hashCode = (hashCode * 59) + this.Dth.GetHashCode();
                }
                if (this.Lan != null)
                {
                    hashCode = (hashCode * 59) + this.Lan.GetHashCode();
                }
                if (this.Zigbee != null)
                {
                    hashCode = (hashCode * 59) + this.Zigbee.GetHashCode();
                }
                if (this.Zwave != null)
                {
                    hashCode = (hashCode * 59) + this.Zwave.GetHashCode();
                }
                if (this.Matter != null)
                {
                    hashCode = (hashCode * 59) + this.Matter.GetHashCode();
                }
                if (this.Ir != null)
                {
                    hashCode = (hashCode * 59) + this.Ir.GetHashCode();
                }
                if (this.IrOcf != null)
                {
                    hashCode = (hashCode * 59) + this.IrOcf.GetHashCode();
                }
                if (this.Ocf != null)
                {
                    hashCode = (hashCode * 59) + this.Ocf.GetHashCode();
                }
                if (this.Viper != null)
                {
                    hashCode = (hashCode * 59) + this.Viper.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.Type.GetHashCode();
                hashCode = (hashCode * 59) + this.RestrictionTier.GetHashCode();
                if (this.Allowed != null)
                {
                    hashCode = (hashCode * 59) + this.Allowed.GetHashCode();
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
