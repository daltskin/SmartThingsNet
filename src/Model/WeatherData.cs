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
    /// Weather data 
    /// </summary>
    [DataContract(Name = "WeatherData")]
    public partial class WeatherData : IEquatable<WeatherData>, IValidatableObject
    {
        /// <summary>
        /// Defines ConditionState
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ConditionStateEnum
        {
            /// <summary>
            /// Enum UNKNOWN for value: UNKNOWN
            /// </summary>
            [EnumMember(Value = "UNKNOWN")]
            UNKNOWN = 1,

            /// <summary>
            /// Enum CLEAR for value: CLEAR
            /// </summary>
            [EnumMember(Value = "CLEAR")]
            CLEAR = 2,

            /// <summary>
            /// Enum SNOW for value: SNOW
            /// </summary>
            [EnumMember(Value = "SNOW")]
            SNOW = 3,

            /// <summary>
            /// Enum RAIN for value: RAIN
            /// </summary>
            [EnumMember(Value = "RAIN")]
            RAIN = 4,

            /// <summary>
            /// Enum CLOUDY for value: CLOUDY
            /// </summary>
            [EnumMember(Value = "CLOUDY")]
            CLOUDY = 5,

            /// <summary>
            /// Enum STORM for value: STORM
            /// </summary>
            [EnumMember(Value = "STORM")]
            STORM = 6,

            /// <summary>
            /// Enum DUSTY for value: DUSTY
            /// </summary>
            [EnumMember(Value = "DUSTY")]
            DUSTY = 7,

            /// <summary>
            /// Enum FOGGY for value: FOGGY
            /// </summary>
            [EnumMember(Value = "FOGGY")]
            FOGGY = 8

        }


        /// <summary>
        /// Gets or Sets ConditionState
        /// </summary>
        [DataMember(Name = "conditionState", EmitDefaultValue = false)]
        public ConditionStateEnum? ConditionState { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherData" /> class.
        /// </summary>
        /// <param name="cloudCeilingInM">Cloud ceiling in meters.</param>
        /// <param name="cloudCoverPhrase">The phrase of the cloud cover.</param>
        /// <param name="relativeHumidityInPercent">Relative Humidity Percentage.</param>
        /// <param name="sunriseDate">sunriseDate.</param>
        /// <param name="sunsetDate">sunsetDate.</param>
        /// <param name="temperatureInC">Temperature in degrees celsius.</param>
        /// <param name="temperatureFeelsLikeInC">Feels-like temperature in degrees celsius.</param>
        /// <param name="uvDescription">uvDescription.</param>
        /// <param name="uvIndex">uvIndex.</param>
        /// <param name="visibilityInKm">visibilityInKm.</param>
        /// <param name="windDirectionInDegrees">windDirectionInDegrees.</param>
        /// <param name="windDirectionCardinal">windDirectionCardinal.</param>
        /// <param name="windGustInKmph">windGustInKmph.</param>
        /// <param name="windSpeedInKmph">windSpeedInKmph.</param>
        /// <param name="conditionPhraseLong">conditionPhraseLong.</param>
        /// <param name="conditionState">conditionState.</param>
        public WeatherData(int cloudCeilingInM = default(int), string cloudCoverPhrase = default(string), int relativeHumidityInPercent = default(int), string sunriseDate = default(string), string sunsetDate = default(string), double temperatureInC = default(double), double temperatureFeelsLikeInC = default(double), string uvDescription = default(string), int uvIndex = default(int), double visibilityInKm = default(double), int windDirectionInDegrees = default(int), string windDirectionCardinal = default(string), int windGustInKmph = default(int), int windSpeedInKmph = default(int), string conditionPhraseLong = default(string), ConditionStateEnum? conditionState = default(ConditionStateEnum?))
        {
            this.CloudCeilingInM = cloudCeilingInM;
            this.CloudCoverPhrase = cloudCoverPhrase;
            this.RelativeHumidityInPercent = relativeHumidityInPercent;
            this.SunriseDate = sunriseDate;
            this.SunsetDate = sunsetDate;
            this.TemperatureInC = temperatureInC;
            this.TemperatureFeelsLikeInC = temperatureFeelsLikeInC;
            this.UvDescription = uvDescription;
            this.UvIndex = uvIndex;
            this.VisibilityInKm = visibilityInKm;
            this.WindDirectionInDegrees = windDirectionInDegrees;
            this.WindDirectionCardinal = windDirectionCardinal;
            this.WindGustInKmph = windGustInKmph;
            this.WindSpeedInKmph = windSpeedInKmph;
            this.ConditionPhraseLong = conditionPhraseLong;
            this.ConditionState = conditionState;
        }

        /// <summary>
        /// Cloud ceiling in meters
        /// </summary>
        /// <value>Cloud ceiling in meters</value>
        [DataMember(Name = "cloudCeilingInM", EmitDefaultValue = false)]
        public int CloudCeilingInM { get; set; }

        /// <summary>
        /// The phrase of the cloud cover
        /// </summary>
        /// <value>The phrase of the cloud cover</value>
        [DataMember(Name = "cloudCoverPhrase", EmitDefaultValue = false)]
        public string CloudCoverPhrase { get; set; }

        /// <summary>
        /// Relative Humidity Percentage
        /// </summary>
        /// <value>Relative Humidity Percentage</value>
        [DataMember(Name = "relativeHumidityInPercent", EmitDefaultValue = false)]
        public int RelativeHumidityInPercent { get; set; }

        /// <summary>
        /// Gets or Sets SunriseDate
        /// </summary>
        [DataMember(Name = "sunriseDate", EmitDefaultValue = false)]
        public string SunriseDate { get; set; }

        /// <summary>
        /// Gets or Sets SunsetDate
        /// </summary>
        [DataMember(Name = "sunsetDate", EmitDefaultValue = false)]
        public string SunsetDate { get; set; }

        /// <summary>
        /// Temperature in degrees celsius
        /// </summary>
        /// <value>Temperature in degrees celsius</value>
        [DataMember(Name = "temperatureInC", EmitDefaultValue = false)]
        public double TemperatureInC { get; set; }

        /// <summary>
        /// Feels-like temperature in degrees celsius
        /// </summary>
        /// <value>Feels-like temperature in degrees celsius</value>
        [DataMember(Name = "temperatureFeelsLikeInC", EmitDefaultValue = false)]
        public double TemperatureFeelsLikeInC { get; set; }

        /// <summary>
        /// Gets or Sets UvDescription
        /// </summary>
        [DataMember(Name = "uvDescription", EmitDefaultValue = false)]
        public string UvDescription { get; set; }

        /// <summary>
        /// Gets or Sets UvIndex
        /// </summary>
        [DataMember(Name = "uvIndex", EmitDefaultValue = false)]
        public int UvIndex { get; set; }

        /// <summary>
        /// Gets or Sets VisibilityInKm
        /// </summary>
        [DataMember(Name = "visibilityInKm", EmitDefaultValue = false)]
        public double VisibilityInKm { get; set; }

        /// <summary>
        /// Gets or Sets WindDirectionInDegrees
        /// </summary>
        [DataMember(Name = "windDirectionInDegrees", EmitDefaultValue = false)]
        public int WindDirectionInDegrees { get; set; }

        /// <summary>
        /// Gets or Sets WindDirectionCardinal
        /// </summary>
        [DataMember(Name = "windDirectionCardinal", EmitDefaultValue = false)]
        public string WindDirectionCardinal { get; set; }

        /// <summary>
        /// Gets or Sets WindGustInKmph
        /// </summary>
        [DataMember(Name = "windGustInKmph", EmitDefaultValue = false)]
        public int WindGustInKmph { get; set; }

        /// <summary>
        /// Gets or Sets WindSpeedInKmph
        /// </summary>
        [DataMember(Name = "windSpeedInKmph", EmitDefaultValue = false)]
        public int WindSpeedInKmph { get; set; }

        /// <summary>
        /// Gets or Sets ConditionPhraseLong
        /// </summary>
        [DataMember(Name = "conditionPhraseLong", EmitDefaultValue = false)]
        public string ConditionPhraseLong { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class WeatherData {\n");
            sb.Append("  CloudCeilingInM: ").Append(CloudCeilingInM).Append("\n");
            sb.Append("  CloudCoverPhrase: ").Append(CloudCoverPhrase).Append("\n");
            sb.Append("  RelativeHumidityInPercent: ").Append(RelativeHumidityInPercent).Append("\n");
            sb.Append("  SunriseDate: ").Append(SunriseDate).Append("\n");
            sb.Append("  SunsetDate: ").Append(SunsetDate).Append("\n");
            sb.Append("  TemperatureInC: ").Append(TemperatureInC).Append("\n");
            sb.Append("  TemperatureFeelsLikeInC: ").Append(TemperatureFeelsLikeInC).Append("\n");
            sb.Append("  UvDescription: ").Append(UvDescription).Append("\n");
            sb.Append("  UvIndex: ").Append(UvIndex).Append("\n");
            sb.Append("  VisibilityInKm: ").Append(VisibilityInKm).Append("\n");
            sb.Append("  WindDirectionInDegrees: ").Append(WindDirectionInDegrees).Append("\n");
            sb.Append("  WindDirectionCardinal: ").Append(WindDirectionCardinal).Append("\n");
            sb.Append("  WindGustInKmph: ").Append(WindGustInKmph).Append("\n");
            sb.Append("  WindSpeedInKmph: ").Append(WindSpeedInKmph).Append("\n");
            sb.Append("  ConditionPhraseLong: ").Append(ConditionPhraseLong).Append("\n");
            sb.Append("  ConditionState: ").Append(ConditionState).Append("\n");
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
            return this.Equals(input as WeatherData);
        }

        /// <summary>
        /// Returns true if WeatherData instances are equal
        /// </summary>
        /// <param name="input">Instance of WeatherData to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(WeatherData input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.CloudCeilingInM == input.CloudCeilingInM ||
                    this.CloudCeilingInM.Equals(input.CloudCeilingInM)
                ) && 
                (
                    this.CloudCoverPhrase == input.CloudCoverPhrase ||
                    (this.CloudCoverPhrase != null &&
                    this.CloudCoverPhrase.Equals(input.CloudCoverPhrase))
                ) && 
                (
                    this.RelativeHumidityInPercent == input.RelativeHumidityInPercent ||
                    this.RelativeHumidityInPercent.Equals(input.RelativeHumidityInPercent)
                ) && 
                (
                    this.SunriseDate == input.SunriseDate ||
                    (this.SunriseDate != null &&
                    this.SunriseDate.Equals(input.SunriseDate))
                ) && 
                (
                    this.SunsetDate == input.SunsetDate ||
                    (this.SunsetDate != null &&
                    this.SunsetDate.Equals(input.SunsetDate))
                ) && 
                (
                    this.TemperatureInC == input.TemperatureInC ||
                    this.TemperatureInC.Equals(input.TemperatureInC)
                ) && 
                (
                    this.TemperatureFeelsLikeInC == input.TemperatureFeelsLikeInC ||
                    this.TemperatureFeelsLikeInC.Equals(input.TemperatureFeelsLikeInC)
                ) && 
                (
                    this.UvDescription == input.UvDescription ||
                    (this.UvDescription != null &&
                    this.UvDescription.Equals(input.UvDescription))
                ) && 
                (
                    this.UvIndex == input.UvIndex ||
                    this.UvIndex.Equals(input.UvIndex)
                ) && 
                (
                    this.VisibilityInKm == input.VisibilityInKm ||
                    this.VisibilityInKm.Equals(input.VisibilityInKm)
                ) && 
                (
                    this.WindDirectionInDegrees == input.WindDirectionInDegrees ||
                    this.WindDirectionInDegrees.Equals(input.WindDirectionInDegrees)
                ) && 
                (
                    this.WindDirectionCardinal == input.WindDirectionCardinal ||
                    (this.WindDirectionCardinal != null &&
                    this.WindDirectionCardinal.Equals(input.WindDirectionCardinal))
                ) && 
                (
                    this.WindGustInKmph == input.WindGustInKmph ||
                    this.WindGustInKmph.Equals(input.WindGustInKmph)
                ) && 
                (
                    this.WindSpeedInKmph == input.WindSpeedInKmph ||
                    this.WindSpeedInKmph.Equals(input.WindSpeedInKmph)
                ) && 
                (
                    this.ConditionPhraseLong == input.ConditionPhraseLong ||
                    (this.ConditionPhraseLong != null &&
                    this.ConditionPhraseLong.Equals(input.ConditionPhraseLong))
                ) && 
                (
                    this.ConditionState == input.ConditionState ||
                    this.ConditionState.Equals(input.ConditionState)
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
                hashCode = (hashCode * 59) + this.CloudCeilingInM.GetHashCode();
                if (this.CloudCoverPhrase != null)
                {
                    hashCode = (hashCode * 59) + this.CloudCoverPhrase.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.RelativeHumidityInPercent.GetHashCode();
                if (this.SunriseDate != null)
                {
                    hashCode = (hashCode * 59) + this.SunriseDate.GetHashCode();
                }
                if (this.SunsetDate != null)
                {
                    hashCode = (hashCode * 59) + this.SunsetDate.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.TemperatureInC.GetHashCode();
                hashCode = (hashCode * 59) + this.TemperatureFeelsLikeInC.GetHashCode();
                if (this.UvDescription != null)
                {
                    hashCode = (hashCode * 59) + this.UvDescription.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.UvIndex.GetHashCode();
                hashCode = (hashCode * 59) + this.VisibilityInKm.GetHashCode();
                hashCode = (hashCode * 59) + this.WindDirectionInDegrees.GetHashCode();
                if (this.WindDirectionCardinal != null)
                {
                    hashCode = (hashCode * 59) + this.WindDirectionCardinal.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.WindGustInKmph.GetHashCode();
                hashCode = (hashCode * 59) + this.WindSpeedInKmph.GetHashCode();
                if (this.ConditionPhraseLong != null)
                {
                    hashCode = (hashCode * 59) + this.ConditionPhraseLong.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.ConditionState.GetHashCode();
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
