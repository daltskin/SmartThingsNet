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
    /// PlaytextBody
    /// </summary>
    [DataContract(Name = "PlaytextBody")]
    public partial class PlaytextBody : IEquatable<PlaytextBody>, IValidatableObject
    {
        /// <summary>
        /// Audio format of the audio file, default to mp3 when ttsProvider is Amazon Polly, default to pcm when ttsProvider is Bixby. Amazon Polly supports mp3 and pcm. Bixby supports only pcm.
        /// </summary>
        /// <value>Audio format of the audio file, default to mp3 when ttsProvider is Amazon Polly, default to pcm when ttsProvider is Bixby. Amazon Polly supports mp3 and pcm. Bixby supports only pcm.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum AudioFormatEnum
        {
            /// <summary>
            /// Enum Mp3 for value: mp3
            /// </summary>
            [EnumMember(Value = "mp3")]
            Mp3 = 1,

            /// <summary>
            /// Enum Pcm for value: pcm
            /// </summary>
            [EnumMember(Value = "pcm")]
            Pcm = 2

        }


        /// <summary>
        /// Audio format of the audio file, default to mp3 when ttsProvider is Amazon Polly, default to pcm when ttsProvider is Bixby. Amazon Polly supports mp3 and pcm. Bixby supports only pcm.
        /// </summary>
        /// <value>Audio format of the audio file, default to mp3 when ttsProvider is Amazon Polly, default to pcm when ttsProvider is Bixby. Amazon Polly supports mp3 and pcm. Bixby supports only pcm.</value>
        [DataMember(Name = "audioFormat", EmitDefaultValue = false)]
        public AudioFormatEnum? AudioFormat { get; set; }
        /// <summary>
        /// TTS provider to use for converting text to speech, default to polly
        /// </summary>
        /// <value>TTS provider to use for converting text to speech, default to polly</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum TtsProviderEnum
        {
            /// <summary>
            /// Enum Polly for value: polly
            /// </summary>
            [EnumMember(Value = "polly")]
            Polly = 1,

            /// <summary>
            /// Enum Bixby for value: bixby
            /// </summary>
            [EnumMember(Value = "bixby")]
            Bixby = 2

        }


        /// <summary>
        /// TTS provider to use for converting text to speech, default to polly
        /// </summary>
        /// <value>TTS provider to use for converting text to speech, default to polly</value>
        [DataMember(Name = "ttsProvider", EmitDefaultValue = false)]
        public TtsProviderEnum? TtsProvider { get; set; }
        /// <summary>
        /// TTS engine to be used, default to standard, standard - robotic voice, neural - human like and natural voice. This option is available only when ttsProvider is Amazon Polly. Please refer tts/info API to get the options available
        /// </summary>
        /// <value>TTS engine to be used, default to standard, standard - robotic voice, neural - human like and natural voice. This option is available only when ttsProvider is Amazon Polly. Please refer tts/info API to get the options available</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum EngineEnum
        {
            /// <summary>
            /// Enum Standard for value: standard
            /// </summary>
            [EnumMember(Value = "standard")]
            Standard = 1,

            /// <summary>
            /// Enum Neural for value: neural
            /// </summary>
            [EnumMember(Value = "neural")]
            Neural = 2

        }


        /// <summary>
        /// TTS engine to be used, default to standard, standard - robotic voice, neural - human like and natural voice. This option is available only when ttsProvider is Amazon Polly. Please refer tts/info API to get the options available
        /// </summary>
        /// <value>TTS engine to be used, default to standard, standard - robotic voice, neural - human like and natural voice. This option is available only when ttsProvider is Amazon Polly. Please refer tts/info API to get the options available</value>
        [DataMember(Name = "engine", EmitDefaultValue = false)]
        public EngineEnum? Engine { get; set; }
        /// <summary>
        /// Speaking style to be used. This option is available only when ttsProvider is Amazon Polly, engine is neural, and voiceIds are one of the following - Matthew, Joanna, Lupe. Please refer tts/info API to get the options available
        /// </summary>
        /// <value>Speaking style to be used. This option is available only when ttsProvider is Amazon Polly, engine is neural, and voiceIds are one of the following - Matthew, Joanna, Lupe. Please refer tts/info API to get the options available</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum SpeakingStyleEnum
        {
            /// <summary>
            /// Enum News for value: news
            /// </summary>
            [EnumMember(Value = "news")]
            News = 1,

            /// <summary>
            /// Enum Conversational for value: conversational
            /// </summary>
            [EnumMember(Value = "conversational")]
            Conversational = 2

        }


        /// <summary>
        /// Speaking style to be used. This option is available only when ttsProvider is Amazon Polly, engine is neural, and voiceIds are one of the following - Matthew, Joanna, Lupe. Please refer tts/info API to get the options available
        /// </summary>
        /// <value>Speaking style to be used. This option is available only when ttsProvider is Amazon Polly, engine is neural, and voiceIds are one of the following - Matthew, Joanna, Lupe. Please refer tts/info API to get the options available</value>
        [DataMember(Name = "speakingStyle", EmitDefaultValue = false)]
        public SpeakingStyleEnum? SpeakingStyle { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaytextBody" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected PlaytextBody() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaytextBody" /> class.
        /// </summary>
        /// <param name="deviceId">Device ID of device on which the audio should be played. Device must support audioNotification capability. (required).</param>
        /// <param name="text">Text to be converted to audio. Cannot exceed 2000 characters for Polly and 425 characters for Bixby (required).</param>
        /// <param name="languageCode">Language used for the text to be converted to speech. Please refer tts/info API to get the options available (required).</param>
        /// <param name="voiceId">VoiceId to be used for the text. Please refer tts/info API to get the options available (required).</param>
        /// <param name="audioFormat">Audio format of the audio file, default to mp3 when ttsProvider is Amazon Polly, default to pcm when ttsProvider is Bixby. Amazon Polly supports mp3 and pcm. Bixby supports only pcm..</param>
        /// <param name="ttsProvider">TTS provider to use for converting text to speech, default to polly.</param>
        /// <param name="engine">TTS engine to be used, default to standard, standard - robotic voice, neural - human like and natural voice. This option is available only when ttsProvider is Amazon Polly. Please refer tts/info API to get the options available.</param>
        /// <param name="speakingStyle">Speaking style to be used. This option is available only when ttsProvider is Amazon Polly, engine is neural, and voiceIds are one of the following - Matthew, Joanna, Lupe. Please refer tts/info API to get the options available.</param>
        /// <param name="volume">Volume of the device at which the audio notification will be played.</param>
        public PlaytextBody(string deviceId = default(string), string text = default(string), string languageCode = default(string), string voiceId = default(string), AudioFormatEnum? audioFormat = default(AudioFormatEnum?), TtsProviderEnum? ttsProvider = default(TtsProviderEnum?), EngineEnum? engine = default(EngineEnum?), SpeakingStyleEnum? speakingStyle = default(SpeakingStyleEnum?), int volume = default(int))
        {
            // to ensure "deviceId" is required (not null)
            if (deviceId == null) {
                throw new ArgumentNullException("deviceId is a required property for PlaytextBody and cannot be null");
            }
            this.DeviceId = deviceId;
            // to ensure "text" is required (not null)
            if (text == null) {
                throw new ArgumentNullException("text is a required property for PlaytextBody and cannot be null");
            }
            this.Text = text;
            // to ensure "languageCode" is required (not null)
            if (languageCode == null) {
                throw new ArgumentNullException("languageCode is a required property for PlaytextBody and cannot be null");
            }
            this.LanguageCode = languageCode;
            // to ensure "voiceId" is required (not null)
            if (voiceId == null) {
                throw new ArgumentNullException("voiceId is a required property for PlaytextBody and cannot be null");
            }
            this.VoiceId = voiceId;
            this.AudioFormat = audioFormat;
            this.TtsProvider = ttsProvider;
            this.Engine = engine;
            this.SpeakingStyle = speakingStyle;
            this.Volume = volume;
        }

        /// <summary>
        /// Device ID of device on which the audio should be played. Device must support audioNotification capability.
        /// </summary>
        /// <value>Device ID of device on which the audio should be played. Device must support audioNotification capability.</value>
        [DataMember(Name = "deviceId", IsRequired = true, EmitDefaultValue = false)]
        public string DeviceId { get; set; }

        /// <summary>
        /// Text to be converted to audio. Cannot exceed 2000 characters for Polly and 425 characters for Bixby
        /// </summary>
        /// <value>Text to be converted to audio. Cannot exceed 2000 characters for Polly and 425 characters for Bixby</value>
        [DataMember(Name = "text", IsRequired = true, EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// Language used for the text to be converted to speech. Please refer tts/info API to get the options available
        /// </summary>
        /// <value>Language used for the text to be converted to speech. Please refer tts/info API to get the options available</value>
        [DataMember(Name = "languageCode", IsRequired = true, EmitDefaultValue = false)]
        public string LanguageCode { get; set; }

        /// <summary>
        /// VoiceId to be used for the text. Please refer tts/info API to get the options available
        /// </summary>
        /// <value>VoiceId to be used for the text. Please refer tts/info API to get the options available</value>
        [DataMember(Name = "voiceId", IsRequired = true, EmitDefaultValue = false)]
        public string VoiceId { get; set; }

        /// <summary>
        /// Volume of the device at which the audio notification will be played
        /// </summary>
        /// <value>Volume of the device at which the audio notification will be played</value>
        [DataMember(Name = "volume", EmitDefaultValue = false)]
        public int Volume { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class PlaytextBody {\n");
            sb.Append("  DeviceId: ").Append(DeviceId).Append("\n");
            sb.Append("  Text: ").Append(Text).Append("\n");
            sb.Append("  LanguageCode: ").Append(LanguageCode).Append("\n");
            sb.Append("  VoiceId: ").Append(VoiceId).Append("\n");
            sb.Append("  AudioFormat: ").Append(AudioFormat).Append("\n");
            sb.Append("  TtsProvider: ").Append(TtsProvider).Append("\n");
            sb.Append("  Engine: ").Append(Engine).Append("\n");
            sb.Append("  SpeakingStyle: ").Append(SpeakingStyle).Append("\n");
            sb.Append("  Volume: ").Append(Volume).Append("\n");
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
            return this.Equals(input as PlaytextBody);
        }

        /// <summary>
        /// Returns true if PlaytextBody instances are equal
        /// </summary>
        /// <param name="input">Instance of PlaytextBody to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PlaytextBody input)
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
                    this.Text == input.Text ||
                    (this.Text != null &&
                    this.Text.Equals(input.Text))
                ) && 
                (
                    this.LanguageCode == input.LanguageCode ||
                    (this.LanguageCode != null &&
                    this.LanguageCode.Equals(input.LanguageCode))
                ) && 
                (
                    this.VoiceId == input.VoiceId ||
                    (this.VoiceId != null &&
                    this.VoiceId.Equals(input.VoiceId))
                ) && 
                (
                    this.AudioFormat == input.AudioFormat ||
                    this.AudioFormat.Equals(input.AudioFormat)
                ) && 
                (
                    this.TtsProvider == input.TtsProvider ||
                    this.TtsProvider.Equals(input.TtsProvider)
                ) && 
                (
                    this.Engine == input.Engine ||
                    this.Engine.Equals(input.Engine)
                ) && 
                (
                    this.SpeakingStyle == input.SpeakingStyle ||
                    this.SpeakingStyle.Equals(input.SpeakingStyle)
                ) && 
                (
                    this.Volume == input.Volume ||
                    this.Volume.Equals(input.Volume)
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
                if (this.Text != null)
                {
                    hashCode = (hashCode * 59) + this.Text.GetHashCode();
                }
                if (this.LanguageCode != null)
                {
                    hashCode = (hashCode * 59) + this.LanguageCode.GetHashCode();
                }
                if (this.VoiceId != null)
                {
                    hashCode = (hashCode * 59) + this.VoiceId.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.AudioFormat.GetHashCode();
                hashCode = (hashCode * 59) + this.TtsProvider.GetHashCode();
                hashCode = (hashCode * 59) + this.Engine.GetHashCode();
                hashCode = (hashCode * 59) + this.SpeakingStyle.GetHashCode();
                hashCode = (hashCode * 59) + this.Volume.GetHashCode();
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
            // Volume (int) maximum
            if (this.Volume > (int)100)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Volume, must be a value less than or equal to 100.", new [] { "Volume" });
            }

            // Volume (int) minimum
            if (this.Volume < (int)0)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Volume, must be a value greater than or equal to 0.", new [] { "Volume" });
            }

            yield break;
        }
    }

}
