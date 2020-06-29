# SmartThingsNet.Api.SubscriptionsApi

All URIs are relative to *https://api.smartthings.com/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**DeleteAllSubscriptions**](SubscriptionsApi.md#deleteallsubscriptions) | **DELETE** /installedapps/{installedAppId}/subscriptions | Delete all of an installed app&#39;s subscriptions.
[**DeleteSubscription**](SubscriptionsApi.md#deletesubscription) | **DELETE** /installedapps/{installedAppId}/subscriptions/{subscriptionId} | Delete an installed app&#39;s subscription.
[**GetSubscription**](SubscriptionsApi.md#getsubscription) | **GET** /installedapps/{installedAppId}/subscriptions/{subscriptionId} | Get an installed app&#39;s subscription.
[**ListSubscriptions**](SubscriptionsApi.md#listsubscriptions) | **GET** /installedapps/{installedAppId}/subscriptions | List an installed app&#39;s subscriptions.
[**SaveSubscription**](SubscriptionsApi.md#savesubscription) | **POST** /installedapps/{installedAppId}/subscriptions | Create a subscription for an installed app.


<a name="deleteallsubscriptions"></a>
# **DeleteAllSubscriptions**
> SubscriptionDelete DeleteAllSubscriptions (string installedAppId, string authorization, string deviceId = null, string modeId = null)

Delete all of an installed app's subscriptions.

Delete all subscriptions for the installed app. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class DeleteAllSubscriptionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            var apiInstance = new SubscriptionsApi(config);
            var installedAppId = installedAppId_example;  // string | The ID of the installed application.

            var deviceId = deviceId_example;  // string | Limit deletion to subscriptions for a particular device. (optional) 
            var modeId = modeId_example;  // string | Limit deletion to subscriptions for a particular mode or deletes parent if last mode (optional) 

            try
            {
                // Delete all of an installed app's subscriptions.
                SubscriptionDelete result = apiInstance.DeleteAllSubscriptions(installedAppId, authorization, deviceId, modeId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SubscriptionsApi.DeleteAllSubscriptions: " + e.Message );
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
 **deviceId** | **string**| Limit deletion to subscriptions for a particular device. | [optional] 
 **modeId** | **string**| Limit deletion to subscriptions for a particular mode or deletes parent if last mode | [optional] 

### Return type

[**SubscriptionDelete**](SubscriptionDelete.md)

### Authorization

No authorization required

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

<a name="deletesubscription"></a>
# **DeleteSubscription**
> SubscriptionDelete DeleteSubscription (string installedAppId, string subscriptionId, string authorization)

Delete an installed app's subscription.

Delete a specific subscription for the installed app. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class DeleteSubscriptionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            var apiInstance = new SubscriptionsApi(config);
            var installedAppId = installedAppId_example;  // string | The ID of the installed application.
            var subscriptionId = subscriptionId_example;  // string | The ID of the subscription


            try
            {
                // Delete an installed app's subscription.
                SubscriptionDelete result = apiInstance.DeleteSubscription(installedAppId, subscriptionId, authorization);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SubscriptionsApi.DeleteSubscription: " + e.Message );
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
 **subscriptionId** | **string**| The ID of the subscription | 
 **authorization** | **string**| OAuth token | 

### Return type

[**SubscriptionDelete**](SubscriptionDelete.md)

### Authorization

No authorization required

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

<a name="getsubscription"></a>
# **GetSubscription**
> Subscription GetSubscription (string installedAppId, string subscriptionId, string authorization)

Get an installed app's subscription.

Get a specific subscription for the installed app. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class GetSubscriptionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            var apiInstance = new SubscriptionsApi(config);
            var installedAppId = installedAppId_example;  // string | The ID of the installed application.
            var subscriptionId = subscriptionId_example;  // string | The ID of the subscription


            try
            {
                // Get an installed app's subscription.
                Subscription result = apiInstance.GetSubscription(installedAppId, subscriptionId, authorization);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SubscriptionsApi.GetSubscription: " + e.Message );
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
 **subscriptionId** | **string**| The ID of the subscription | 
 **authorization** | **string**| OAuth token | 

### Return type

[**Subscription**](Subscription.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The subscription |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="listsubscriptions"></a>
# **ListSubscriptions**
> PagedSubscriptions ListSubscriptions (string installedAppId, string authorization)

List an installed app's subscriptions.

List the subscriptions for the installed app. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class ListSubscriptionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            var apiInstance = new SubscriptionsApi(config);
            var installedAppId = installedAppId_example;  // string | The ID of the installed application.


            try
            {
                // List an installed app's subscriptions.
                PagedSubscriptions result = apiInstance.ListSubscriptions(installedAppId, authorization);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SubscriptionsApi.ListSubscriptions: " + e.Message );
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

[**PagedSubscriptions**](PagedSubscriptions.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | An array of subscriptions |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="savesubscription"></a>
# **SaveSubscription**
> Subscription SaveSubscription (string installedAppId, string authorization, SubscriptionRequest request = null)

Create a subscription for an installed app.

Create a subscription to a type of event from the specified source. Both the source and the installed app must be in the location specified and the installed app must have read access to the event being subscribed to. An installed app is only permitted to created 20 subscriptions.  ### Authorization scopes For installed app principal: * installed app id matches the incoming request installed app id location must match the installed app location  | Subscription Type  | Scope required                                                                         | | - -- -- -- -- -- -- -- -- - | - -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --| | DEVICE             | `r:devices:$deviceId`                                                                  | | CAPABILITY         | `r:devices:*:*:$capability` or `r:devices:*`,                                          | | MODE               | `r:locations:$locationId` or `r:locations:*`                                           | | DEVICE_LIFECYCLE   | `r:devices:$deviceId` or `r:devices:*`                                                 | | DEVICE_HEALTH      | `r:devices:$deviceId` or `r:devices:*`                                                 | | SECURITY_ARM_STATE | `r:security:locations:$locationId:armstate` or `r:security:locations:*:armstate`       | | HUB_HEALTH         | `r:hubs`                                                                               | | SCENE_LIFECYCLE    | `r:scenes:*`                                                                           | 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using SmartThingsNet.Api;
using SmartThingsNet.Client;
using SmartThingsNet.Model;

namespace Example
{
    public class SaveSubscriptionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new SubscriptionsApi(config);
            var installedAppId = installedAppId_example;  // string | The ID of the installed application.

            var request = new SubscriptionRequest(); // SubscriptionRequest | The Subscription to be created. (optional) 

            try
            {
                // Create a subscription for an installed app.
                Subscription result = apiInstance.SaveSubscription(installedAppId, authorization, request);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SubscriptionsApi.SaveSubscription: " + e.Message );
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
 **request** | [**SubscriptionRequest**](SubscriptionRequest.md)| The Subscription to be created. | [optional] 

### Return type

[**Subscription**](Subscription.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | The subscription |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **409** | Conflict |  -  |
| **422** | Unprocessable Entity |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

