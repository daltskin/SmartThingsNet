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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mime;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace SmartThingsNet.Api
{

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface ISubscriptionsApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Delete all of an installed app&#39;s subscriptions.
        /// </summary>
        /// <remarks>
        /// Delete all subscriptions for the installed app. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>

        /// <param name="deviceId">Limit deletion to subscriptions for a particular device. (optional)</param>
        /// <param name="modeId">Limit deletion to subscriptions for a particular mode or deletes parent if last mode (optional)</param>
        /// <returns>SubscriptionDelete</returns>
        SubscriptionDelete DeleteAllSubscriptions (string installedAppId, string deviceId = default(string), string modeId = default(string));

        /// <summary>
        /// Delete all of an installed app&#39;s subscriptions.
        /// </summary>
        /// <remarks>
        /// Delete all subscriptions for the installed app. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>

        /// <param name="deviceId">Limit deletion to subscriptions for a particular device. (optional)</param>
        /// <param name="modeId">Limit deletion to subscriptions for a particular mode or deletes parent if last mode (optional)</param>
        /// <returns>ApiResponse of SubscriptionDelete</returns>
        ApiResponse<SubscriptionDelete> DeleteAllSubscriptionsWithHttpInfo (string installedAppId, string deviceId = default(string), string modeId = default(string));
        /// <summary>
        /// Delete an installed app&#39;s subscription.
        /// </summary>
        /// <remarks>
        /// Delete a specific subscription for the installed app. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>

        /// <returns>SubscriptionDelete</returns>
        SubscriptionDelete DeleteSubscription (string installedAppId, string subscriptionId);

        /// <summary>
        /// Delete an installed app&#39;s subscription.
        /// </summary>
        /// <remarks>
        /// Delete a specific subscription for the installed app. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>

        /// <returns>ApiResponse of SubscriptionDelete</returns>
        ApiResponse<SubscriptionDelete> DeleteSubscriptionWithHttpInfo (string installedAppId, string subscriptionId);
        /// <summary>
        /// Get an installed app&#39;s subscription.
        /// </summary>
        /// <remarks>
        /// Get a specific subscription for the installed app. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>

        /// <returns>Subscription</returns>
        Subscription GetSubscription (string installedAppId, string subscriptionId);

        /// <summary>
        /// Get an installed app&#39;s subscription.
        /// </summary>
        /// <remarks>
        /// Get a specific subscription for the installed app. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>

        /// <returns>ApiResponse of Subscription</returns>
        ApiResponse<Subscription> GetSubscriptionWithHttpInfo (string installedAppId, string subscriptionId);
        /// <summary>
        /// List an installed app&#39;s subscriptions.
        /// </summary>
        /// <remarks>
        /// List the subscriptions for the installed app. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>

        /// <returns>PagedSubscriptions</returns>
        PagedSubscriptions ListSubscriptions (string installedAppId);

        /// <summary>
        /// List an installed app&#39;s subscriptions.
        /// </summary>
        /// <remarks>
        /// List the subscriptions for the installed app. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>

        /// <returns>ApiResponse of PagedSubscriptions</returns>
        ApiResponse<PagedSubscriptions> ListSubscriptionsWithHttpInfo (string installedAppId);
        /// <summary>
        /// Create a subscription for an installed app.
        /// </summary>
        /// <remarks>
        /// Create a subscription to a type of event from the specified source. Both the source and the installed app must be in the location specified and the installed app must have read access to the event being subscribed to. An installed app is only permitted to created 20 subscriptions.  ### Authorization scopes For installed app principal: * installed app id matches the incoming request installed app id location must match the installed app location  | Subscription Type  | Scope required                                                                         | | - -- -- -- -- -- -- -- -- - | - -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --| | DEVICE             | &#x60;r:devices:$deviceId&#x60;                                                                  | | CAPABILITY         | &#x60;r:devices:*:*:$capability&#x60; or &#x60;r:devices:*&#x60;,                                          | | MODE               | &#x60;r:locations:$locationId&#x60; or &#x60;r:locations:*&#x60;                                           | | DEVICE_LIFECYCLE   | &#x60;r:devices:$deviceId&#x60; or &#x60;r:devices:*&#x60;                                                 | | DEVICE_HEALTH      | &#x60;r:devices:$deviceId&#x60; or &#x60;r:devices:*&#x60;                                                 | | SECURITY_ARM_STATE | &#x60;r:security:locations:$locationId:armstate&#x60; or &#x60;r:security:locations:*:armstate&#x60;       | | HUB_HEALTH         | &#x60;r:hubs&#x60;                                                                               | | SCENE_LIFECYCLE    | &#x60;r:scenes:*&#x60;                                                                           | 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>

        /// <param name="request">The Subscription to be created. (optional)</param>
        /// <returns>Subscription</returns>
        Subscription SaveSubscription (string installedAppId, SubscriptionRequest request = default(SubscriptionRequest));

        /// <summary>
        /// Create a subscription for an installed app.
        /// </summary>
        /// <remarks>
        /// Create a subscription to a type of event from the specified source. Both the source and the installed app must be in the location specified and the installed app must have read access to the event being subscribed to. An installed app is only permitted to created 20 subscriptions.  ### Authorization scopes For installed app principal: * installed app id matches the incoming request installed app id location must match the installed app location  | Subscription Type  | Scope required                                                                         | | - -- -- -- -- -- -- -- -- - | - -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --| | DEVICE             | &#x60;r:devices:$deviceId&#x60;                                                                  | | CAPABILITY         | &#x60;r:devices:*:*:$capability&#x60; or &#x60;r:devices:*&#x60;,                                          | | MODE               | &#x60;r:locations:$locationId&#x60; or &#x60;r:locations:*&#x60;                                           | | DEVICE_LIFECYCLE   | &#x60;r:devices:$deviceId&#x60; or &#x60;r:devices:*&#x60;                                                 | | DEVICE_HEALTH      | &#x60;r:devices:$deviceId&#x60; or &#x60;r:devices:*&#x60;                                                 | | SECURITY_ARM_STATE | &#x60;r:security:locations:$locationId:armstate&#x60; or &#x60;r:security:locations:*:armstate&#x60;       | | HUB_HEALTH         | &#x60;r:hubs&#x60;                                                                               | | SCENE_LIFECYCLE    | &#x60;r:scenes:*&#x60;                                                                           | 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>

        /// <param name="request">The Subscription to be created. (optional)</param>
        /// <returns>ApiResponse of Subscription</returns>
        ApiResponse<Subscription> SaveSubscriptionWithHttpInfo (string installedAppId, SubscriptionRequest request = default(SubscriptionRequest));
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface ISubscriptionsApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Delete all of an installed app&#39;s subscriptions.
        /// </summary>
        /// <remarks>
        /// Delete all subscriptions for the installed app. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>

        /// <param name="deviceId">Limit deletion to subscriptions for a particular device. (optional)</param>
        /// <param name="modeId">Limit deletion to subscriptions for a particular mode or deletes parent if last mode (optional)</param>
        /// <returns>Task of SubscriptionDelete</returns>
        System.Threading.Tasks.Task<SubscriptionDelete> DeleteAllSubscriptionsAsync (string installedAppId, string deviceId = default(string), string modeId = default(string));

        /// <summary>
        /// Delete all of an installed app&#39;s subscriptions.
        /// </summary>
        /// <remarks>
        /// Delete all subscriptions for the installed app. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>

        /// <param name="deviceId">Limit deletion to subscriptions for a particular device. (optional)</param>
        /// <param name="modeId">Limit deletion to subscriptions for a particular mode or deletes parent if last mode (optional)</param>
        /// <returns>Task of ApiResponse (SubscriptionDelete)</returns>
        System.Threading.Tasks.Task<ApiResponse<SubscriptionDelete>> DeleteAllSubscriptionsAsyncWithHttpInfo (string installedAppId, string deviceId = default(string), string modeId = default(string));
        /// <summary>
        /// Delete an installed app&#39;s subscription.
        /// </summary>
        /// <remarks>
        /// Delete a specific subscription for the installed app. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>

        /// <returns>Task of SubscriptionDelete</returns>
        System.Threading.Tasks.Task<SubscriptionDelete> DeleteSubscriptionAsync (string installedAppId, string subscriptionId);

        /// <summary>
        /// Delete an installed app&#39;s subscription.
        /// </summary>
        /// <remarks>
        /// Delete a specific subscription for the installed app. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>

        /// <returns>Task of ApiResponse (SubscriptionDelete)</returns>
        System.Threading.Tasks.Task<ApiResponse<SubscriptionDelete>> DeleteSubscriptionAsyncWithHttpInfo (string installedAppId, string subscriptionId);
        /// <summary>
        /// Get an installed app&#39;s subscription.
        /// </summary>
        /// <remarks>
        /// Get a specific subscription for the installed app. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>

        /// <returns>Task of Subscription</returns>
        System.Threading.Tasks.Task<Subscription> GetSubscriptionAsync (string installedAppId, string subscriptionId);

        /// <summary>
        /// Get an installed app&#39;s subscription.
        /// </summary>
        /// <remarks>
        /// Get a specific subscription for the installed app. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>

        /// <returns>Task of ApiResponse (Subscription)</returns>
        System.Threading.Tasks.Task<ApiResponse<Subscription>> GetSubscriptionAsyncWithHttpInfo (string installedAppId, string subscriptionId);
        /// <summary>
        /// List an installed app&#39;s subscriptions.
        /// </summary>
        /// <remarks>
        /// List the subscriptions for the installed app. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>

        /// <returns>Task of PagedSubscriptions</returns>
        System.Threading.Tasks.Task<PagedSubscriptions> ListSubscriptionsAsync (string installedAppId);

        /// <summary>
        /// List an installed app&#39;s subscriptions.
        /// </summary>
        /// <remarks>
        /// List the subscriptions for the installed app. 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>

        /// <returns>Task of ApiResponse (PagedSubscriptions)</returns>
        System.Threading.Tasks.Task<ApiResponse<PagedSubscriptions>> ListSubscriptionsAsyncWithHttpInfo (string installedAppId);
        /// <summary>
        /// Create a subscription for an installed app.
        /// </summary>
        /// <remarks>
        /// Create a subscription to a type of event from the specified source. Both the source and the installed app must be in the location specified and the installed app must have read access to the event being subscribed to. An installed app is only permitted to created 20 subscriptions.  ### Authorization scopes For installed app principal: * installed app id matches the incoming request installed app id location must match the installed app location  | Subscription Type  | Scope required                                                                         | | - -- -- -- -- -- -- -- -- - | - -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --| | DEVICE             | &#x60;r:devices:$deviceId&#x60;                                                                  | | CAPABILITY         | &#x60;r:devices:*:*:$capability&#x60; or &#x60;r:devices:*&#x60;,                                          | | MODE               | &#x60;r:locations:$locationId&#x60; or &#x60;r:locations:*&#x60;                                           | | DEVICE_LIFECYCLE   | &#x60;r:devices:$deviceId&#x60; or &#x60;r:devices:*&#x60;                                                 | | DEVICE_HEALTH      | &#x60;r:devices:$deviceId&#x60; or &#x60;r:devices:*&#x60;                                                 | | SECURITY_ARM_STATE | &#x60;r:security:locations:$locationId:armstate&#x60; or &#x60;r:security:locations:*:armstate&#x60;       | | HUB_HEALTH         | &#x60;r:hubs&#x60;                                                                               | | SCENE_LIFECYCLE    | &#x60;r:scenes:*&#x60;                                                                           | 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>

        /// <param name="request">The Subscription to be created. (optional)</param>
        /// <returns>Task of Subscription</returns>
        System.Threading.Tasks.Task<Subscription> SaveSubscriptionAsync (string installedAppId, SubscriptionRequest request = default(SubscriptionRequest));

        /// <summary>
        /// Create a subscription for an installed app.
        /// </summary>
        /// <remarks>
        /// Create a subscription to a type of event from the specified source. Both the source and the installed app must be in the location specified and the installed app must have read access to the event being subscribed to. An installed app is only permitted to created 20 subscriptions.  ### Authorization scopes For installed app principal: * installed app id matches the incoming request installed app id location must match the installed app location  | Subscription Type  | Scope required                                                                         | | - -- -- -- -- -- -- -- -- - | - -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --| | DEVICE             | &#x60;r:devices:$deviceId&#x60;                                                                  | | CAPABILITY         | &#x60;r:devices:*:*:$capability&#x60; or &#x60;r:devices:*&#x60;,                                          | | MODE               | &#x60;r:locations:$locationId&#x60; or &#x60;r:locations:*&#x60;                                           | | DEVICE_LIFECYCLE   | &#x60;r:devices:$deviceId&#x60; or &#x60;r:devices:*&#x60;                                                 | | DEVICE_HEALTH      | &#x60;r:devices:$deviceId&#x60; or &#x60;r:devices:*&#x60;                                                 | | SECURITY_ARM_STATE | &#x60;r:security:locations:$locationId:armstate&#x60; or &#x60;r:security:locations:*:armstate&#x60;       | | HUB_HEALTH         | &#x60;r:hubs&#x60;                                                                               | | SCENE_LIFECYCLE    | &#x60;r:scenes:*&#x60;                                                                           | 
        /// </remarks>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>

        /// <param name="request">The Subscription to be created. (optional)</param>
        /// <returns>Task of ApiResponse (Subscription)</returns>
        System.Threading.Tasks.Task<ApiResponse<Subscription>> SaveSubscriptionAsyncWithHttpInfo (string installedAppId, SubscriptionRequest request = default(SubscriptionRequest));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface ISubscriptionsApi : ISubscriptionsApiSync, ISubscriptionsApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class SubscriptionsApi : ISubscriptionsApi
    {
        private SmartThingsNet.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public SubscriptionsApi() : this((string) null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public SubscriptionsApi(String basePath)
        {
            this.Configuration = SmartThingsNet.Client.Configuration.MergeConfigurations(
                SmartThingsNet.Client.GlobalConfiguration.Instance,
                new SmartThingsNet.Client.Configuration { BasePath = basePath }
            );
            this.Client = new SmartThingsNet.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new SmartThingsNet.Client.ApiClient(this.Configuration.BasePath);
            this.ExceptionFactory = SmartThingsNet.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionsApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public SubscriptionsApi(SmartThingsNet.Client.Configuration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Configuration = SmartThingsNet.Client.Configuration.MergeConfigurations(
                SmartThingsNet.Client.GlobalConfiguration.Instance,
                configuration
            );
            this.Client = new SmartThingsNet.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new SmartThingsNet.Client.ApiClient(this.Configuration.BasePath);
            ExceptionFactory = SmartThingsNet.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionsApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public SubscriptionsApi(SmartThingsNet.Client.ISynchronousClient client,SmartThingsNet.Client.IAsynchronousClient asyncClient, SmartThingsNet.Client.IReadableConfiguration configuration)
        {
            if(client == null) throw new ArgumentNullException("client");
            if(asyncClient == null) throw new ArgumentNullException("asyncClient");
            if(configuration == null) throw new ArgumentNullException("configuration");

            this.Client = client;
            this.AsynchronousClient = asyncClient;
            this.Configuration = configuration;
            this.ExceptionFactory = SmartThingsNet.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// The client for accessing this underlying API asynchronously.
        /// </summary>
        public SmartThingsNet.Client.IAsynchronousClient AsynchronousClient { get; set; }

        /// <summary>
        /// The client for accessing this underlying API synchronously.
        /// </summary>
        public SmartThingsNet.Client.ISynchronousClient Client { get; set; }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public String GetBasePath()
        {
            return this.Configuration.BasePath;
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public SmartThingsNet.Client.IReadableConfiguration Configuration {get; set;}

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public SmartThingsNet.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        /// Delete all of an installed app&#39;s subscriptions. Delete all subscriptions for the installed app. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>

        /// <param name="deviceId">Limit deletion to subscriptions for a particular device. (optional)</param>
        /// <param name="modeId">Limit deletion to subscriptions for a particular mode or deletes parent if last mode (optional)</param>
        /// <returns>SubscriptionDelete</returns>
        public SubscriptionDelete DeleteAllSubscriptions (string installedAppId, string deviceId = default(string), string modeId = default(string))
        {
             SmartThingsNet.Client.ApiResponse<SubscriptionDelete> localVarResponse = DeleteAllSubscriptionsWithHttpInfo(installedAppId, deviceId, modeId);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Delete all of an installed app&#39;s subscriptions. Delete all subscriptions for the installed app. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>

        /// <param name="deviceId">Limit deletion to subscriptions for a particular device. (optional)</param>
        /// <param name="modeId">Limit deletion to subscriptions for a particular mode or deletes parent if last mode (optional)</param>
        /// <returns>ApiResponse of SubscriptionDelete</returns>
        public SmartThingsNet.Client.ApiResponse< SubscriptionDelete > DeleteAllSubscriptionsWithHttpInfo (string installedAppId, string deviceId = default(string), string modeId = default(string))
        {
            // verify the required parameter 'installedAppId' is set
            if (installedAppId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installedAppId' when calling SubscriptionsApi->DeleteAllSubscriptions");

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("installedAppId", SmartThingsNet.Client.ClientUtils.ParameterToString(installedAppId)); // path parameter
            if (deviceId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "deviceId", deviceId));
            }
            if (modeId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "modeId", modeId));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete< SubscriptionDelete >("/installedapps/{installedAppId}/subscriptions", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteAllSubscriptions", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete all of an installed app&#39;s subscriptions. Delete all subscriptions for the installed app. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="deviceId">Limit deletion to subscriptions for a particular device. (optional)</param>
        /// <param name="modeId">Limit deletion to subscriptions for a particular mode or deletes parent if last mode (optional)</param>
        /// <returns>Task of SubscriptionDelete</returns>
        public async System.Threading.Tasks.Task<SubscriptionDelete> DeleteAllSubscriptionsAsync (string installedAppId, string deviceId = default(string), string modeId = default(string))
        {
             SmartThingsNet.Client.ApiResponse<SubscriptionDelete> localVarResponse = await DeleteAllSubscriptionsAsyncWithHttpInfo(installedAppId, deviceId, modeId);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Delete all of an installed app&#39;s subscriptions. Delete all subscriptions for the installed app. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>

        /// <param name="deviceId">Limit deletion to subscriptions for a particular device. (optional)</param>
        /// <param name="modeId">Limit deletion to subscriptions for a particular mode or deletes parent if last mode (optional)</param>
        /// <returns>Task of ApiResponse (SubscriptionDelete)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<SubscriptionDelete>> DeleteAllSubscriptionsAsyncWithHttpInfo (string installedAppId, string deviceId = default(string), string modeId = default(string))
        {
            // verify the required parameter 'installedAppId' is set
            if (installedAppId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installedAppId' when calling SubscriptionsApi->DeleteAllSubscriptions");

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };
            
            foreach (var _contentType in _contentTypes)
                localVarRequestOptions.HeaderParameters.Add("Content-Type", _contentType);
            
            foreach (var _accept in _accepts)
                localVarRequestOptions.HeaderParameters.Add("Accept", _accept);
            
            localVarRequestOptions.PathParameters.Add("installedAppId", SmartThingsNet.Client.ClientUtils.ParameterToString(installedAppId)); // path parameter
            if (deviceId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "deviceId", deviceId));
            }
            if (modeId != null)
            {
                localVarRequestOptions.QueryParameters.Add(SmartThingsNet.Client.ClientUtils.ParameterToMultiMap("", "modeId", modeId));
            }

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.DeleteAsync<SubscriptionDelete>("/installedapps/{installedAppId}/subscriptions", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteAllSubscriptions", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete an installed app&#39;s subscription. Delete a specific subscription for the installed app. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>
        /// <returns>SubscriptionDelete</returns>
        public SubscriptionDelete DeleteSubscription (string installedAppId, string subscriptionId)
        {
             SmartThingsNet.Client.ApiResponse<SubscriptionDelete> localVarResponse = DeleteSubscriptionWithHttpInfo(installedAppId, subscriptionId);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Delete an installed app&#39;s subscription. Delete a specific subscription for the installed app. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>
        /// <returns>ApiResponse of SubscriptionDelete</returns>
        public SmartThingsNet.Client.ApiResponse< SubscriptionDelete > DeleteSubscriptionWithHttpInfo (string installedAppId, string subscriptionId)
        {
            // verify the required parameter 'installedAppId' is set
            if (installedAppId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installedAppId' when calling SubscriptionsApi->DeleteSubscription");

            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'subscriptionId' when calling SubscriptionsApi->DeleteSubscription");

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("installedAppId", SmartThingsNet.Client.ClientUtils.ParameterToString(installedAppId)); // path parameter
            localVarRequestOptions.PathParameters.Add("subscriptionId", SmartThingsNet.Client.ClientUtils.ParameterToString(subscriptionId)); // path parameter

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete< SubscriptionDelete >("/installedapps/{installedAppId}/subscriptions/{subscriptionId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteSubscription", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete an installed app&#39;s subscription. Delete a specific subscription for the installed app. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>
        /// <returns>Task of SubscriptionDelete</returns>
        public async System.Threading.Tasks.Task<SubscriptionDelete> DeleteSubscriptionAsync (string installedAppId, string subscriptionId)
        {
             SmartThingsNet.Client.ApiResponse<SubscriptionDelete> localVarResponse = await DeleteSubscriptionAsyncWithHttpInfo(installedAppId, subscriptionId);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Delete an installed app&#39;s subscription. Delete a specific subscription for the installed app. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>

        /// <returns>Task of ApiResponse (SubscriptionDelete)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<SubscriptionDelete>> DeleteSubscriptionAsyncWithHttpInfo (string installedAppId, string subscriptionId)
        {
            // verify the required parameter 'installedAppId' is set
            if (installedAppId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installedAppId' when calling SubscriptionsApi->DeleteSubscription");

            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'subscriptionId' when calling SubscriptionsApi->DeleteSubscription");

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };
            
            foreach (var _contentType in _contentTypes)
                localVarRequestOptions.HeaderParameters.Add("Content-Type", _contentType);
            
            foreach (var _accept in _accepts)
                localVarRequestOptions.HeaderParameters.Add("Accept", _accept);
            
            localVarRequestOptions.PathParameters.Add("installedAppId", SmartThingsNet.Client.ClientUtils.ParameterToString(installedAppId)); // path parameter
            localVarRequestOptions.PathParameters.Add("subscriptionId", SmartThingsNet.Client.ClientUtils.ParameterToString(subscriptionId)); // path parameter

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.DeleteAsync<SubscriptionDelete>("/installedapps/{installedAppId}/subscriptions/{subscriptionId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteSubscription", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get an installed app&#39;s subscription. Get a specific subscription for the installed app. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>

        /// <returns>Subscription</returns>
        public Subscription GetSubscription (string installedAppId, string subscriptionId)
        {
             SmartThingsNet.Client.ApiResponse<Subscription> localVarResponse = GetSubscriptionWithHttpInfo(installedAppId, subscriptionId);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Get an installed app&#39;s subscription. Get a specific subscription for the installed app. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>
        /// <returns>ApiResponse of Subscription</returns>
        public SmartThingsNet.Client.ApiResponse< Subscription > GetSubscriptionWithHttpInfo (string installedAppId, string subscriptionId)
        {
            // verify the required parameter 'installedAppId' is set
            if (installedAppId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installedAppId' when calling SubscriptionsApi->GetSubscription");

            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'subscriptionId' when calling SubscriptionsApi->GetSubscription");

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("installedAppId", SmartThingsNet.Client.ClientUtils.ParameterToString(installedAppId)); // path parameter
            localVarRequestOptions.PathParameters.Add("subscriptionId", SmartThingsNet.Client.ClientUtils.ParameterToString(subscriptionId)); // path parameter

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get< Subscription >("/installedapps/{installedAppId}/subscriptions/{subscriptionId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetSubscription", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get an installed app&#39;s subscription. Get a specific subscription for the installed app. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>
        /// <returns>Task of Subscription</returns>
        public async System.Threading.Tasks.Task<Subscription> GetSubscriptionAsync (string installedAppId, string subscriptionId)
        {
             SmartThingsNet.Client.ApiResponse<Subscription> localVarResponse = await GetSubscriptionAsyncWithHttpInfo(installedAppId, subscriptionId);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Get an installed app&#39;s subscription. Get a specific subscription for the installed app. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="subscriptionId">The ID of the subscription</param>
        /// <returns>Task of ApiResponse (Subscription)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Subscription>> GetSubscriptionAsyncWithHttpInfo (string installedAppId, string subscriptionId)
        {
            // verify the required parameter 'installedAppId' is set
            if (installedAppId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installedAppId' when calling SubscriptionsApi->GetSubscription");

            // verify the required parameter 'subscriptionId' is set
            if (subscriptionId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'subscriptionId' when calling SubscriptionsApi->GetSubscription");

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };
            
            foreach (var _contentType in _contentTypes)
                localVarRequestOptions.HeaderParameters.Add("Content-Type", _contentType);
            
            foreach (var _accept in _accepts)
                localVarRequestOptions.HeaderParameters.Add("Accept", _accept);
            
            localVarRequestOptions.PathParameters.Add("installedAppId", SmartThingsNet.Client.ClientUtils.ParameterToString(installedAppId)); // path parameter
            localVarRequestOptions.PathParameters.Add("subscriptionId", SmartThingsNet.Client.ClientUtils.ParameterToString(subscriptionId)); // path parameter

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<Subscription>("/installedapps/{installedAppId}/subscriptions/{subscriptionId}", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetSubscription", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List an installed app&#39;s subscriptions. List the subscriptions for the installed app. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <returns>PagedSubscriptions</returns>
        public PagedSubscriptions ListSubscriptions (string installedAppId)
        {
             SmartThingsNet.Client.ApiResponse<PagedSubscriptions> localVarResponse = ListSubscriptionsWithHttpInfo(installedAppId);
             return localVarResponse.Data;
        }

        /// <summary>
        /// List an installed app&#39;s subscriptions. List the subscriptions for the installed app. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>

        /// <returns>ApiResponse of PagedSubscriptions</returns>
        public SmartThingsNet.Client.ApiResponse< PagedSubscriptions > ListSubscriptionsWithHttpInfo (string installedAppId)
        {
            // verify the required parameter 'installedAppId' is set
            if (installedAppId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installedAppId' when calling SubscriptionsApi->ListSubscriptions");

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("installedAppId", SmartThingsNet.Client.ClientUtils.ParameterToString(installedAppId)); // path parameter

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get< PagedSubscriptions >($"/installedapps/{installedAppId}/subscriptions", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListSubscriptions", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// List an installed app&#39;s subscriptions. List the subscriptions for the installed app. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <returns>Task of PagedSubscriptions</returns>
        public async System.Threading.Tasks.Task<PagedSubscriptions> ListSubscriptionsAsync (string installedAppId)
        {
             SmartThingsNet.Client.ApiResponse<PagedSubscriptions> localVarResponse = await ListSubscriptionsAsyncWithHttpInfo(installedAppId);
             return localVarResponse.Data;
        }

        /// <summary>
        /// List an installed app&#39;s subscriptions. List the subscriptions for the installed app. 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <returns>Task of ApiResponse (PagedSubscriptions)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<PagedSubscriptions>> ListSubscriptionsAsyncWithHttpInfo (string installedAppId)
        {
            // verify the required parameter 'installedAppId' is set
            if (installedAppId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installedAppId' when calling SubscriptionsApi->ListSubscriptions");

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };
            
            foreach (var _contentType in _contentTypes)
                localVarRequestOptions.HeaderParameters.Add("Content-Type", _contentType);
            
            foreach (var _accept in _accepts)
                localVarRequestOptions.HeaderParameters.Add("Accept", _accept);
            
            localVarRequestOptions.PathParameters.Add("installedAppId", SmartThingsNet.Client.ClientUtils.ParameterToString(installedAppId)); // path parameter

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<PagedSubscriptions>("/installedapps/{installedAppId}/subscriptions", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListSubscriptions", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create a subscription for an installed app. Create a subscription to a type of event from the specified source. Both the source and the installed app must be in the location specified and the installed app must have read access to the event being subscribed to. An installed app is only permitted to created 20 subscriptions.  ### Authorization scopes For installed app principal: * installed app id matches the incoming request installed app id location must match the installed app location  | Subscription Type  | Scope required                                                                         | | - -- -- -- -- -- -- -- -- - | - -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --| | DEVICE             | &#x60;r:devices:$deviceId&#x60;                                                                  | | CAPABILITY         | &#x60;r:devices:*:*:$capability&#x60; or &#x60;r:devices:*&#x60;,                                          | | MODE               | &#x60;r:locations:$locationId&#x60; or &#x60;r:locations:*&#x60;                                           | | DEVICE_LIFECYCLE   | &#x60;r:devices:$deviceId&#x60; or &#x60;r:devices:*&#x60;                                                 | | DEVICE_HEALTH      | &#x60;r:devices:$deviceId&#x60; or &#x60;r:devices:*&#x60;                                                 | | SECURITY_ARM_STATE | &#x60;r:security:locations:$locationId:armstate&#x60; or &#x60;r:security:locations:*:armstate&#x60;       | | HUB_HEALTH         | &#x60;r:hubs&#x60;                                                                               | | SCENE_LIFECYCLE    | &#x60;r:scenes:*&#x60;                                                                           | 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="request">The Subscription to be created. (optional)</param>
        /// <returns>Subscription</returns>
        public Subscription SaveSubscription (string installedAppId, SubscriptionRequest request = default(SubscriptionRequest))
        {
             SmartThingsNet.Client.ApiResponse<Subscription> localVarResponse = SaveSubscriptionWithHttpInfo(installedAppId, request);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Create a subscription for an installed app. Create a subscription to a type of event from the specified source. Both the source and the installed app must be in the location specified and the installed app must have read access to the event being subscribed to. An installed app is only permitted to created 20 subscriptions.  ### Authorization scopes For installed app principal: * installed app id matches the incoming request installed app id location must match the installed app location  | Subscription Type  | Scope required                                                                         | | - -- -- -- -- -- -- -- -- - | - -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --| | DEVICE             | &#x60;r:devices:$deviceId&#x60;                                                                  | | CAPABILITY         | &#x60;r:devices:*:*:$capability&#x60; or &#x60;r:devices:*&#x60;,                                          | | MODE               | &#x60;r:locations:$locationId&#x60; or &#x60;r:locations:*&#x60;                                           | | DEVICE_LIFECYCLE   | &#x60;r:devices:$deviceId&#x60; or &#x60;r:devices:*&#x60;                                                 | | DEVICE_HEALTH      | &#x60;r:devices:$deviceId&#x60; or &#x60;r:devices:*&#x60;                                                 | | SECURITY_ARM_STATE | &#x60;r:security:locations:$locationId:armstate&#x60; or &#x60;r:security:locations:*:armstate&#x60;       | | HUB_HEALTH         | &#x60;r:hubs&#x60;                                                                               | | SCENE_LIFECYCLE    | &#x60;r:scenes:*&#x60;                                                                           | 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="request">The Subscription to be created. (optional)</param>
        /// <returns>ApiResponse of Subscription</returns>
        public SmartThingsNet.Client.ApiResponse< Subscription > SaveSubscriptionWithHttpInfo (string installedAppId, SubscriptionRequest request = default(SubscriptionRequest))
        {
            // verify the required parameter 'installedAppId' is set
            if (installedAppId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installedAppId' when calling SubscriptionsApi->SaveSubscription");

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };

            var localVarContentType = SmartThingsNet.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = SmartThingsNet.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("installedAppId", SmartThingsNet.Client.ClientUtils.ParameterToString(installedAppId)); // path parameter
            
            localVarRequestOptions.Data = request;

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post< Subscription >("/installedapps/{installedAppId}/subscriptions", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("SaveSubscription", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create a subscription for an installed app. Create a subscription to a type of event from the specified source. Both the source and the installed app must be in the location specified and the installed app must have read access to the event being subscribed to. An installed app is only permitted to created 20 subscriptions.  ### Authorization scopes For installed app principal: * installed app id matches the incoming request installed app id location must match the installed app location  | Subscription Type  | Scope required                                                                         | | - -- -- -- -- -- -- -- -- - | - -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --| | DEVICE             | &#x60;r:devices:$deviceId&#x60;                                                                  | | CAPABILITY         | &#x60;r:devices:*:*:$capability&#x60; or &#x60;r:devices:*&#x60;,                                          | | MODE               | &#x60;r:locations:$locationId&#x60; or &#x60;r:locations:*&#x60;                                           | | DEVICE_LIFECYCLE   | &#x60;r:devices:$deviceId&#x60; or &#x60;r:devices:*&#x60;                                                 | | DEVICE_HEALTH      | &#x60;r:devices:$deviceId&#x60; or &#x60;r:devices:*&#x60;                                                 | | SECURITY_ARM_STATE | &#x60;r:security:locations:$locationId:armstate&#x60; or &#x60;r:security:locations:*:armstate&#x60;       | | HUB_HEALTH         | &#x60;r:hubs&#x60;                                                                               | | SCENE_LIFECYCLE    | &#x60;r:scenes:*&#x60;                                                                           | 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="request">The Subscription to be created. (optional)</param>
        /// <returns>Task of Subscription</returns>
        public async System.Threading.Tasks.Task<Subscription> SaveSubscriptionAsync (string installedAppId, SubscriptionRequest request = default(SubscriptionRequest))
        {
             SmartThingsNet.Client.ApiResponse<Subscription> localVarResponse = await SaveSubscriptionAsyncWithHttpInfo(installedAppId, request);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Create a subscription for an installed app. Create a subscription to a type of event from the specified source. Both the source and the installed app must be in the location specified and the installed app must have read access to the event being subscribed to. An installed app is only permitted to created 20 subscriptions.  ### Authorization scopes For installed app principal: * installed app id matches the incoming request installed app id location must match the installed app location  | Subscription Type  | Scope required                                                                         | | - -- -- -- -- -- -- -- -- - | - -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --| | DEVICE             | &#x60;r:devices:$deviceId&#x60;                                                                  | | CAPABILITY         | &#x60;r:devices:*:*:$capability&#x60; or &#x60;r:devices:*&#x60;,                                          | | MODE               | &#x60;r:locations:$locationId&#x60; or &#x60;r:locations:*&#x60;                                           | | DEVICE_LIFECYCLE   | &#x60;r:devices:$deviceId&#x60; or &#x60;r:devices:*&#x60;                                                 | | DEVICE_HEALTH      | &#x60;r:devices:$deviceId&#x60; or &#x60;r:devices:*&#x60;                                                 | | SECURITY_ARM_STATE | &#x60;r:security:locations:$locationId:armstate&#x60; or &#x60;r:security:locations:*:armstate&#x60;       | | HUB_HEALTH         | &#x60;r:hubs&#x60;                                                                               | | SCENE_LIFECYCLE    | &#x60;r:scenes:*&#x60;                                                                           | 
        /// </summary>
        /// <exception cref="SmartThingsNet.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="installedAppId">The ID of the installed application.</param>
        /// <param name="request">The Subscription to be created. (optional)</param>
        /// <returns>Task of ApiResponse (Subscription)</returns>
        public async System.Threading.Tasks.Task<SmartThingsNet.Client.ApiResponse<Subscription>> SaveSubscriptionAsyncWithHttpInfo (string installedAppId, SubscriptionRequest request = default(SubscriptionRequest))
        {
            // verify the required parameter 'installedAppId' is set
            if (installedAppId == null)
                throw new SmartThingsNet.Client.ApiException(400, "Missing required parameter 'installedAppId' when calling SubscriptionsApi->SaveSubscription");

            SmartThingsNet.Client.RequestOptions localVarRequestOptions = new SmartThingsNet.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };
            
            foreach (var _contentType in _contentTypes)
                localVarRequestOptions.HeaderParameters.Add("Content-Type", _contentType);
            
            foreach (var _accept in _accepts)
                localVarRequestOptions.HeaderParameters.Add("Accept", _accept);
            
            localVarRequestOptions.PathParameters.Add("installedAppId", SmartThingsNet.Client.ClientUtils.ParameterToString(installedAppId)); // path parameter
            
            localVarRequestOptions.Data = request;

            // authentication (Bearer) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<Subscription>("/installedapps/{installedAppId}/subscriptions", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("SaveSubscription", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }
    }
}
