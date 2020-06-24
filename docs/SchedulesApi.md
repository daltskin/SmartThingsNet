# Org.OpenAPITools.Api.SchedulesApi

All URIs are relative to *https://api.smartthings.com/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateSchedule**](SchedulesApi.md#createschedule) | **POST** /installedapps/{installedAppId}/schedules | Save an installed app schedule.
[**DeleteSchedule**](SchedulesApi.md#deleteschedule) | **DELETE** /installedapps/{installedAppId}/schedules/{scheduleName} | Delete a schedule.
[**DeleteSchedules**](SchedulesApi.md#deleteschedules) | **DELETE** /installedapps/{installedAppId}/schedules | Delete all of an installed app&#39;s schedules.
[**GetSchedule**](SchedulesApi.md#getschedule) | **GET** /installedapps/{installedAppId}/schedules/{scheduleName} | Get an installed app&#39;s schedule.
[**GetSchedules**](SchedulesApi.md#getschedules) | **GET** /installedapps/{installedAppId}/schedules | List installed app schedules.


<a name="createschedule"></a>
# **CreateSchedule**
> Schedule CreateSchedule (string installedAppId, string authorization, ScheduleRequest request = null)

Save an installed app schedule.

Create a schedule for an installed app. The installed app must be in the location specified and the installed app must have permission to create schedules. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateScheduleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new SchedulesApi(config);
            var installedAppId = installedAppId_example;  // string | The ID of the installed application.
            var authorization = authorization_example;  // string | OAuth token
            var request = new ScheduleRequest(); // ScheduleRequest | The schedule to be created. (optional) 

            try
            {
                // Save an installed app schedule.
                Schedule result = apiInstance.CreateSchedule(installedAppId, authorization, request);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SchedulesApi.CreateSchedule: " + e.Message );
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
 **installedAppId** | **string**| The ID of the installed application. | 
 **authorization** | **string**| OAuth token | 
 **request** | [**ScheduleRequest**](ScheduleRequest.md)| The schedule to be created. | [optional] 

### Return type

[**Schedule**](Schedule.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The created schedule. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **422** | Unprocessable Entity |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deleteschedule"></a>
# **DeleteSchedule**
> Object DeleteSchedule (string installedAppId, string scheduleName, string authorization)

Delete a schedule.

Delete a specific schedule for the installed app. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteScheduleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new SchedulesApi(config);
            var installedAppId = installedAppId_example;  // string | The ID of the installed application.
            var scheduleName = scheduleName_example;  // string | The name of the schedule
            var authorization = authorization_example;  // string | OAuth token

            try
            {
                // Delete a schedule.
                Object result = apiInstance.DeleteSchedule(installedAppId, scheduleName, authorization);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SchedulesApi.DeleteSchedule: " + e.Message );
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
 **installedAppId** | **string**| The ID of the installed application. | 
 **scheduleName** | **string**| The name of the schedule | 
 **authorization** | **string**| OAuth token | 

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
| **200** | Successfully deleted |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deleteschedules"></a>
# **DeleteSchedules**
> Object DeleteSchedules (string installedAppId, string authorization)

Delete all of an installed app's schedules.

Delete all schedules for the installed app. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteSchedulesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new SchedulesApi(config);
            var installedAppId = installedAppId_example;  // string | The ID of the installed application.
            var authorization = authorization_example;  // string | OAuth token

            try
            {
                // Delete all of an installed app's schedules.
                Object result = apiInstance.DeleteSchedules(installedAppId, authorization);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SchedulesApi.DeleteSchedules: " + e.Message );
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
 **installedAppId** | **string**| The ID of the installed application. | 
 **authorization** | **string**| OAuth token | 

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
| **200** | Successfully deleted |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getschedule"></a>
# **GetSchedule**
> Schedule GetSchedule (string installedAppId, string scheduleName, string authorization)

Get an installed app's schedule.

Get a specific schedule for the installed app. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetScheduleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new SchedulesApi(config);
            var installedAppId = installedAppId_example;  // string | The ID of the installed application.
            var scheduleName = scheduleName_example;  // string | The name of the schedule
            var authorization = authorization_example;  // string | OAuth token

            try
            {
                // Get an installed app's schedule.
                Schedule result = apiInstance.GetSchedule(installedAppId, scheduleName, authorization);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SchedulesApi.GetSchedule: " + e.Message );
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
 **installedAppId** | **string**| The ID of the installed application. | 
 **scheduleName** | **string**| The name of the schedule | 
 **authorization** | **string**| OAuth token | 

### Return type

[**Schedule**](Schedule.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The schedule |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getschedules"></a>
# **GetSchedules**
> PagedSchedules GetSchedules (string installedAppId, string authorization)

List installed app schedules.

List the schedules for the installed app. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetSchedulesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new SchedulesApi(config);
            var installedAppId = installedAppId_example;  // string | The ID of the installed application.
            var authorization = authorization_example;  // string | OAuth token

            try
            {
                // List installed app schedules.
                PagedSchedules result = apiInstance.GetSchedules(installedAppId, authorization);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SchedulesApi.GetSchedules: " + e.Message );
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
 **installedAppId** | **string**| The ID of the installed application. | 
 **authorization** | **string**| OAuth token | 

### Return type

[**PagedSchedules**](PagedSchedules.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A paged schedules list |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

