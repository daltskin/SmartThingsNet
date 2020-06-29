# SmartThingsNet.Api.AppsApi

All URIs are relative to *https://api.smartthings.com/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateApp**](AppsApi.md#createapp) | **POST** /apps | Create an app.
[**DeleteApp**](AppsApi.md#deleteapp) | **DELETE** /apps/{appNameOrId} | Delete an app.
[**GenerateAppOauth**](AppsApi.md#generateappoauth) | **POST** /apps/{appNameOrId}/oauth/generate | Generate an app&#39;s oauth client/secret.
[**GetApp**](AppsApi.md#getapp) | **GET** /apps/{appNameOrId} | Get an app.
[**GetAppOauth**](AppsApi.md#getappoauth) | **GET** /apps/{appNameOrId}/oauth | Get an app&#39;s oauth settings.
[**GetAppSettings**](AppsApi.md#getappsettings) | **GET** /apps/{appNameOrId}/settings | Get settings.
[**ListApps**](AppsApi.md#listapps) | **GET** /apps | List apps.
[**Register**](AppsApi.md#register) | **PUT** /apps/{appNameOrId}/register | Sends a confirmation request to App.
[**UpdateApp**](AppsApi.md#updateapp) | **PUT** /apps/{appNameOrId} | Update an app.
[**UpdateAppOauth**](AppsApi.md#updateappoauth) | **PUT** /apps/{appNameOrId}/oauth | Update an app&#39;s oauth settings.
[**UpdateAppSettings**](AppsApi.md#updateappsettings) | **PUT** /apps/{appNameOrId}/settings | Update settings.
[**UpdateSignatureType**](AppsApi.md#updatesignaturetype) | **PUT** /apps/{appNameOrId}/signature-type | Update an app&#39;s signature type.


<a name="createapp"></a>
# **CreateApp**
> CreateAppResponse CreateApp (CreateAppRequest createOrUpdateAppRequest, string signatureType = null, bool? requireConfirmation = null)

Create an app.

Create an app integration.  A single developer account is allowed to contain a maximum of 100 apps.  Upon hitting that limit a 422 error response is returned with an error code of LimitError. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class CreateAppExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AppsApi(config);

            var createOrUpdateAppRequest = new CreateAppRequest(); // CreateAppRequest | 
            var signatureType = signatureType_example;  // string | The Signature Type of the application. For WEBHOOK_SMART_APP only.  (optional) 
            var requireConfirmation = true;  // bool? | Override default configuration to use either PING or CONFIRMATION lifecycle. For WEBHOOK_SMART_APP only.  (optional) 

            try
            {
                // Create an app.
                CreateAppResponse result = apiInstance.CreateApp(createOrUpdateAppRequest, signatureType, requireConfirmation);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AppsApi.CreateApp: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **createOrUpdateAppRequest** | [**CreateAppRequest**](CreateAppRequest.md)|  | 
 **signatureType** | **string**| The Signature Type of the application. For WEBHOOK_SMART_APP only.  | [optional] 
 **requireConfirmation** | **bool?**| Override default configuration to use either PING or CONFIRMATION lifecycle. For WEBHOOK_SMART_APP only.  | [optional] 

### Return type

[**CreateAppResponse**](CreateAppResponse.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | An app model. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **422** | Unprocessable Entity |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deleteapp"></a>
# **DeleteApp**
> Object DeleteApp (string appNameOrId)

Delete an app.

Delete an app.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class DeleteAppExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AppsApi(config);

            var appNameOrId = appNameOrId_example;  // string | The appName or appId field of an app.

            try
            {
                // Delete an app.
                Object result = apiInstance.DeleteApp(appNameOrId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AppsApi.DeleteApp: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **appNameOrId** | **string**| The appName or appId field of an app. | 

### Return type

**Object**

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The number of deleted apps. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="generateappoauth"></a>
# **GenerateAppOauth**
> GenerateAppOAuthResponse GenerateAppOauth (string appNameOrId, GenerateAppOAuthRequest generateAppOAuthRequest)

Generate an app's oauth client/secret.

When an app is first created an OAuth client/secret are automatically generated for the integration.  However, there are times when it maybe useful to re-generate a client/secret.  Such as in cases where a secret becomes compromised. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class GenerateAppOauthExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AppsApi(config);

            var appNameOrId = appNameOrId_example;  // string | The appName or appId field of an app.
            var generateAppOAuthRequest = new GenerateAppOAuthRequest(); // GenerateAppOAuthRequest | 

            try
            {
                // Generate an app's oauth client/secret.
                GenerateAppOAuthResponse result = apiInstance.GenerateAppOauth(appNameOrId, generateAppOAuthRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AppsApi.GenerateAppOauth: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **appNameOrId** | **string**| The appName or appId field of an app. | 
 **generateAppOAuthRequest** | [**GenerateAppOAuthRequest**](GenerateAppOAuthRequest.md)|  | 

### Return type

[**GenerateAppOAuthResponse**](GenerateAppOAuthResponse.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | An a response object containing the newly create OAuth Client ID / Secret and relevant details pertaining to the OAuth client.  |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getapp"></a>
# **GetApp**
> App GetApp (string appNameOrId)

Get an app.

Get a single app.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class GetAppExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AppsApi(config);

            var appNameOrId = appNameOrId_example;  // string | The appName or appId field of an app.

            try
            {
                // Get an app.
                App result = apiInstance.GetApp(appNameOrId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AppsApi.GetApp: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **appNameOrId** | **string**| The appName or appId field of an app. | 

### Return type

[**App**](App.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | An app. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getappoauth"></a>
# **GetAppOauth**
> AppOAuth GetAppOauth (string appNameOrId)

Get an app's oauth settings.

Get an app's oauth settings.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class GetAppOauthExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AppsApi(config);

            var appNameOrId = appNameOrId_example;  // string | The appName or appId field of an app.

            try
            {
                // Get an app's oauth settings.
                AppOAuth result = apiInstance.GetAppOauth(appNameOrId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AppsApi.GetAppOauth: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **appNameOrId** | **string**| The appName or appId field of an app. | 

### Return type

[**AppOAuth**](AppOAuth.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | An app. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getappsettings"></a>
# **GetAppSettings**
> GetAppSettingsResponse GetAppSettings (string appNameOrId)

Get settings.

Get settings for an app.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class GetAppSettingsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AppsApi(config);

            var appNameOrId = appNameOrId_example;  // string | The appName or appId field of an app.

            try
            {
                // Get settings.
                GetAppSettingsResponse result = apiInstance.GetAppSettings(appNameOrId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AppsApi.GetAppSettings: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **appNameOrId** | **string**| The appName or appId field of an app. | 

### Return type

[**GetAppSettingsResponse**](GetAppSettingsResponse.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | An app settings model. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="listapps"></a>
# **ListApps**
> PagedApps ListApps (string appType = null, string classification = null, string tag = null)

List apps.

List all apps configured in an account.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class ListAppsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AppsApi(config);

            var appType = appType_example;  // string | The App Type of the application. (optional) 
            var classification = classification_example;  // string | An App maybe associated to many classifications.  A classification drives how the integration is presented to the user in the SmartThings mobile clients.  These classifications include: * AUTOMATION - Denotes an integration that should display under the \"Automation\" tab in mobile clients. * SERVICE - Denotes an integration that is classified as a \"Service\". * DEVICE - Denotes an integration that should display under the \"Device\" tab in mobile clients. * CONNECTED_SERVICE - Denotes an integration that should display under the \"Connected Services\" menu in mobile clients.  (optional) 
            var tag = tag_example;  // string | May be used to filter a resource by it's assigned user-tags.  Multiple tag query params are automatically joined with OR.  Example usage in query string: ``` ?tag:key_name=value1&tag:key_name=value2 ```  (optional) 

            try
            {
                // List apps.
                PagedApps result = apiInstance.ListApps(appType, classification, tag);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AppsApi.ListApps: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **appType** | **string**| The App Type of the application. | [optional] 
 **classification** | **string**| An App maybe associated to many classifications.  A classification drives how the integration is presented to the user in the SmartThings mobile clients.  These classifications include: * AUTOMATION - Denotes an integration that should display under the \&quot;Automation\&quot; tab in mobile clients. * SERVICE - Denotes an integration that is classified as a \&quot;Service\&quot;. * DEVICE - Denotes an integration that should display under the \&quot;Device\&quot; tab in mobile clients. * CONNECTED_SERVICE - Denotes an integration that should display under the \&quot;Connected Services\&quot; menu in mobile clients.  | [optional] 
 **tag** | **string**| May be used to filter a resource by it&#39;s assigned user-tags.  Multiple tag query params are automatically joined with OR.  Example usage in query string: &#x60;&#x60;&#x60; ?tag:key_name&#x3D;value1&amp;tag:key_name&#x3D;value2 &#x60;&#x60;&#x60;  | [optional] 

### Return type

[**PagedApps**](PagedApps.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A paginated list of apps. |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="register"></a>
# **Register**
> Object Register (string appNameOrId, Object appRegisterRequest)

Sends a confirmation request to App.

Prepares to register an App by sending the endpoint a confirmation message.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class RegisterExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AppsApi(config);

            var appNameOrId = appNameOrId_example;  // string | The appName or appId field of an app.
            var appRegisterRequest = ;  // Object | 

            try
            {
                // Sends a confirmation request to App.
                Object result = apiInstance.Register(appNameOrId, appRegisterRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AppsApi.Register: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **appNameOrId** | **string**| The appName or appId field of an app. | 
 **appRegisterRequest** | **Object**|  | 

### Return type

**Object**

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **202** | A request to send a confirm registration request has been accepted. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updateapp"></a>
# **UpdateApp**
> App UpdateApp (string appNameOrId, UpdateAppRequest updateAppRequest, string signatureType = null, bool? requireConfirmation = null)

Update an app.

Update an app.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class UpdateAppExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AppsApi(config);

            var appNameOrId = appNameOrId_example;  // string | The appName or appId field of an app.
            var updateAppRequest = new UpdateAppRequest(); // UpdateAppRequest | 
            var signatureType = signatureType_example;  // string | The Signature Type of the application. For WEBHOOK_SMART_APP only.  (optional) 
            var requireConfirmation = true;  // bool? | Override default configuration to use either PING or CONFIRMATION lifecycle. For WEBHOOK_SMART_APP only.  (optional) 

            try
            {
                // Update an app.
                App result = apiInstance.UpdateApp(appNameOrId, updateAppRequest, signatureType, requireConfirmation);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AppsApi.UpdateApp: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **appNameOrId** | **string**| The appName or appId field of an app. | 
 **updateAppRequest** | [**UpdateAppRequest**](UpdateAppRequest.md)|  | 
 **signatureType** | **string**| The Signature Type of the application. For WEBHOOK_SMART_APP only.  | [optional] 
 **requireConfirmation** | **bool?**| Override default configuration to use either PING or CONFIRMATION lifecycle. For WEBHOOK_SMART_APP only.  | [optional] 

### Return type

[**App**](App.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | An app model. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **422** | Unprocessable Entity |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updateappoauth"></a>
# **UpdateAppOauth**
> AppOAuth UpdateAppOauth (string appNameOrId, UpdateAppOAuthRequest updateAppOAuthRequest)

Update an app's oauth settings.

Update an app's oauth settings.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class UpdateAppOauthExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AppsApi(config);

            var appNameOrId = appNameOrId_example;  // string | The appName or appId field of an app.
            var updateAppOAuthRequest = new UpdateAppOAuthRequest(); // UpdateAppOAuthRequest | 

            try
            {
                // Update an app's oauth settings.
                AppOAuth result = apiInstance.UpdateAppOauth(appNameOrId, updateAppOAuthRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AppsApi.UpdateAppOauth: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **appNameOrId** | **string**| The appName or appId field of an app. | 
 **updateAppOAuthRequest** | [**UpdateAppOAuthRequest**](UpdateAppOAuthRequest.md)|  | 

### Return type

[**AppOAuth**](AppOAuth.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | An app&#39;s oauth settings model. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **422** | Unprocessable Entity |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updateappsettings"></a>
# **UpdateAppSettings**
> UpdateAppSettingsResponse UpdateAppSettings (string appNameOrId, UpdateAppSettingsRequest updateAppSettingsRequest)

Update settings.

Update settings for an app.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class UpdateAppSettingsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AppsApi(config);

            var appNameOrId = appNameOrId_example;  // string | The appName or appId  field of an app.
            var updateAppSettingsRequest = new UpdateAppSettingsRequest(); // UpdateAppSettingsRequest | 

            try
            {
                // Update settings.
                UpdateAppSettingsResponse result = apiInstance.UpdateAppSettings(appNameOrId, updateAppSettingsRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AppsApi.UpdateAppSettings: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **appNameOrId** | **string**| The appName or appId  field of an app. | 
 **updateAppSettingsRequest** | [**UpdateAppSettingsRequest**](UpdateAppSettingsRequest.md)|  | 

### Return type

[**UpdateAppSettingsResponse**](UpdateAppSettingsResponse.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | An app settings model. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **422** | Unprocessable Entity |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updatesignaturetype"></a>
# **UpdateSignatureType**
> Object UpdateSignatureType (string appNameOrId, UpdateSignatureTypeRequest updateSignatureTypeRequest)

Update an app's signature type.

Updates the signature type of an App.  Signature options:   * APP_RSA - Legacy signing mechanism comprised of a public / private key generated for an App during registration.  This mechanism requires an App to download the public key and deploy along side their integration to verify the signature in the authorization header.   * ST_PADLOCK - App callbacks are signed with a SmartThings certificate which is publicly available at https://key.smartthings.com.  App's authorize callbacks by fetching the certificate over HTTPS and using it to validate the signature in the authorization header.  Note that when upgrading an App from APP_RSA to ST_PADLOCK it is recommended to implement both verification methods. This will provide the ability to seamlessly switch between mechanisms in case a rollback is needed. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class UpdateSignatureTypeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new AppsApi(config);

            var appNameOrId = appNameOrId_example;  // string | The appName or appId field of an app.
            var updateSignatureTypeRequest = new UpdateSignatureTypeRequest(); // UpdateSignatureTypeRequest | 

            try
            {
                // Update an app's signature type.
                Object result = apiInstance.UpdateSignatureType(appNameOrId, updateSignatureTypeRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AppsApi.UpdateSignatureType: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **appNameOrId** | **string**| The appName or appId field of an app. | 
 **updateSignatureTypeRequest** | [**UpdateSignatureTypeRequest**](UpdateSignatureTypeRequest.md)|  | 

### Return type

**Object**

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **202** | App signature type will be updated asynchronously.  Developer can expect change to take effect within a few minutes.  |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **422** | Unprocessable Entity |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

