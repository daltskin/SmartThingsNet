# Org.OpenAPITools.Api.InstalledappsApi

All URIs are relative to *https://api.smartthings.com/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateInstalledAppEvents**](InstalledappsApi.md#createinstalledappevents) | **POST** /installedapps/{installedAppId}/events | Create Installed App events.
[**DeleteInstallation**](InstalledappsApi.md#deleteinstallation) | **DELETE** /installedapps/{installedAppId} | Delete an installed app.
[**GetInstallation**](InstalledappsApi.md#getinstallation) | **GET** /installedapps/{installedAppId} | Get an installed app.
[**GetInstallationConfig**](InstalledappsApi.md#getinstallationconfig) | **GET** /installedapps/{installedAppId}/configs/{configurationId} | Get an installed app configuration.
[**ListInstallationConfig**](InstalledappsApi.md#listinstallationconfig) | **GET** /installedapps/{installedAppId}/configs | List an installed app&#39;s configurations.
[**ListInstallations**](InstalledappsApi.md#listinstallations) | **GET** /installedapps | List installed apps.


<a name="createinstalledappevents"></a>
# **CreateInstalledAppEvents**
> Object CreateInstalledAppEvents (string authorization, string installedAppId, CreateInstalledAppEventsRequest createInstalledAppEventsRequest)

Create Installed App events.

Create events for an installed app.  This API allows Apps to create events to trigger custom behavior in installed apps. Requires a SmartApp token. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateInstalledAppEventsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new InstalledappsApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var installedAppId = installedAppId_example;  // string | The ID of the installed application.
            var createInstalledAppEventsRequest = new CreateInstalledAppEventsRequest(); // CreateInstalledAppEventsRequest | 

            try
            {
                // Create Installed App events.
                Object result = apiInstance.CreateInstalledAppEvents(authorization, installedAppId, createInstalledAppEventsRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstalledappsApi.CreateInstalledAppEvents: " + e.Message );
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
 **authorization** | **string**| OAuth token | 
 **installedAppId** | **string**| The ID of the installed application. | 
 **createInstalledAppEventsRequest** | [**CreateInstalledAppEventsRequest**](CreateInstalledAppEventsRequest.md)|  | 

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
| **200** | Created events. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **422** | Unprocessable Entity |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deleteinstallation"></a>
# **DeleteInstallation**
> DeleteInstalledAppResponse DeleteInstallation (string authorization, string installedAppId)

Delete an installed app.

Delete an Installed App.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteInstallationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new InstalledappsApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var installedAppId = installedAppId_example;  // string | The ID of the installed application.

            try
            {
                // Delete an installed app.
                DeleteInstalledAppResponse result = apiInstance.DeleteInstallation(authorization, installedAppId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstalledappsApi.DeleteInstallation: " + e.Message );
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
 **authorization** | **string**| OAuth token | 
 **installedAppId** | **string**| The ID of the installed application. | 

### Return type

[**DeleteInstalledAppResponse**](DeleteInstalledAppResponse.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The number of installed apps deleted. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getinstallation"></a>
# **GetInstallation**
> InstalledApp GetInstallation (string authorization, string installedAppId)

Get an installed app.

Fetch a single installed application.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetInstallationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new InstalledappsApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var installedAppId = installedAppId_example;  // string | The ID of the installed application.

            try
            {
                // Get an installed app.
                InstalledApp result = apiInstance.GetInstallation(authorization, installedAppId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstalledappsApi.GetInstallation: " + e.Message );
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
 **authorization** | **string**| OAuth token | 
 **installedAppId** | **string**| The ID of the installed application. | 

### Return type

[**InstalledApp**](InstalledApp.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | An installed app. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getinstallationconfig"></a>
# **GetInstallationConfig**
> InstallConfigurationDetail GetInstallationConfig (string authorization, string installedAppId, Guid configurationId)

Get an installed app configuration.

Fetch a detailed install configuration model containing actual config entries / values.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetInstallationConfigExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new InstalledappsApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var installedAppId = installedAppId_example;  // string | The ID of the installed application.
            var configurationId = new Guid(); // Guid | The ID of the install configuration.

            try
            {
                // Get an installed app configuration.
                InstallConfigurationDetail result = apiInstance.GetInstallationConfig(authorization, installedAppId, configurationId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstalledappsApi.GetInstallationConfig: " + e.Message );
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
 **authorization** | **string**| OAuth token | 
 **installedAppId** | **string**| The ID of the installed application. | 
 **configurationId** | [**Guid**](Guid.md)| The ID of the install configuration. | 

### Return type

[**InstallConfigurationDetail**](InstallConfigurationDetail.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | An install configuration detail model. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="listinstallationconfig"></a>
# **ListInstallationConfig**
> PagedInstallConfigurations ListInstallationConfig (string authorization, string installedAppId, string configurationStatus = null)

List an installed app's configurations.

List all configurations potentially filtered by status for an installed app.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListInstallationConfigExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new InstalledappsApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var installedAppId = installedAppId_example;  // string | The ID of the installed application.
            var configurationStatus = configurationStatus_example;  // string | Filter for configuration status. (optional) 

            try
            {
                // List an installed app's configurations.
                PagedInstallConfigurations result = apiInstance.ListInstallationConfig(authorization, installedAppId, configurationStatus);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstalledappsApi.ListInstallationConfig: " + e.Message );
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
 **authorization** | **string**| OAuth token | 
 **installedAppId** | **string**| The ID of the installed application. | 
 **configurationStatus** | **string**| Filter for configuration status. | [optional] 

### Return type

[**PagedInstallConfigurations**](PagedInstallConfigurations.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | An paginated list of install configuration. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="listinstallations"></a>
# **ListInstallations**
> PagedInstalledApps ListInstallations (string authorization, string locationId = null, string installedAppStatus = null, string installedAppType = null, string tag = null, string appId = null, string modeId = null, string deviceId = null)

List installed apps.

List all installed applications within the specified locations. If no locations are provided, then list all installed apps accessible by the principle. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListInstallationsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new InstalledappsApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var locationId = locationId_example;  // string | The ID of the location that both the installed smart app and source are associated with. (optional) 
            var installedAppStatus = installedAppStatus_example;  // string | State of the Installed App. (optional) 
            var installedAppType = installedAppType_example;  // string | Denotes the type of installed app. (optional) 
            var tag = tag_example;  // string | May be used to filter a resource by it's assigned user-tags.  Multiple tag query params are automatically joined with OR.  Example usage in query string: ``` ?tag:key_name=value1&tag:key_name=value2 ```  (optional) 
            var appId = appId_example;  // string | The ID of an App (optional) 
            var modeId = modeId_example;  // string | The ID of the mode (optional) 
            var deviceId = deviceId_example;  // string | The ID of the device (optional) 

            try
            {
                // List installed apps.
                PagedInstalledApps result = apiInstance.ListInstallations(authorization, locationId, installedAppStatus, installedAppType, tag, appId, modeId, deviceId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstalledappsApi.ListInstallations: " + e.Message );
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
 **authorization** | **string**| OAuth token | 
 **locationId** | **string**| The ID of the location that both the installed smart app and source are associated with. | [optional] 
 **installedAppStatus** | **string**| State of the Installed App. | [optional] 
 **installedAppType** | **string**| Denotes the type of installed app. | [optional] 
 **tag** | **string**| May be used to filter a resource by it&#39;s assigned user-tags.  Multiple tag query params are automatically joined with OR.  Example usage in query string: &#x60;&#x60;&#x60; ?tag:key_name&#x3D;value1&amp;tag:key_name&#x3D;value2 &#x60;&#x60;&#x60;  | [optional] 
 **appId** | **string**| The ID of an App | [optional] 
 **modeId** | **string**| The ID of the mode | [optional] 
 **deviceId** | **string**| The ID of the device | [optional] 

### Return type

[**PagedInstalledApps**](PagedInstalledApps.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A paginated list of installed apps. |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

