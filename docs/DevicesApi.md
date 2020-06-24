# Org.OpenAPITools.Api.DevicesApi

All URIs are relative to *https://api.smartthings.com/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateDeviceEvents**](DevicesApi.md#createdeviceevents) | **POST** /devices/{deviceId}/events | Create Device Events.
[**DeleteDevice**](DevicesApi.md#deletedevice) | **DELETE** /devices/{deviceId} | Delete a Device.
[**ExecuteDeviceCommands**](DevicesApi.md#executedevicecommands) | **POST** /devices/{deviceId}/commands | Execute commands on device.
[**GetDevice**](DevicesApi.md#getdevice) | **GET** /devices/{deviceId} | Get a device&#39;s description.
[**GetDeviceComponentStatus**](DevicesApi.md#getdevicecomponentstatus) | **GET** /devices/{deviceId}/components/{componentId}/status | Get a device component&#39;s status.
[**GetDeviceStatus**](DevicesApi.md#getdevicestatus) | **GET** /devices/{deviceId}/status | Get the full status of a device.
[**GetDeviceStatusByCapability**](DevicesApi.md#getdevicestatusbycapability) | **GET** /devices/{deviceId}/components/{componentId}/capabilities/{capabilityId}/status | Get a capability&#39;s status.
[**GetDevices**](DevicesApi.md#getdevices) | **GET** /devices | List devices.
[**InstallDevice**](DevicesApi.md#installdevice) | **POST** /devices | Install a Device.
[**UpdateDevice**](DevicesApi.md#updatedevice) | **PUT** /devices/{deviceId} | Update a device.


<a name="createdeviceevents"></a>
# **CreateDeviceEvents**
> Object CreateDeviceEvents (string authorization, string deviceId, DeviceEventsRequest deviceEventRequest)

Create Device Events.

Create events for a device. When a device is managed by a SmartApp then it is responsible for creating events to update the attributes of the device in the SmartThings platform. The token must be for a SmartApp and it must be the SmartApp that created the Device. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateDeviceEventsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new DevicesApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var deviceId = deviceId_example;  // string | the device ID
            var deviceEventRequest = new DeviceEventsRequest(); // DeviceEventsRequest | 

            try
            {
                // Create Device Events.
                Object result = apiInstance.CreateDeviceEvents(authorization, deviceId, deviceEventRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DevicesApi.CreateDeviceEvents: " + e.Message );
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
 **deviceId** | **string**| the device ID | 
 **deviceEventRequest** | [**DeviceEventsRequest**](DeviceEventsRequest.md)|  | 

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

<a name="deletedevice"></a>
# **DeleteDevice**
> Object DeleteDevice (string authorization, string deviceId)

Delete a Device.

Delete a device by device id. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteDeviceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new DevicesApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var deviceId = deviceId_example;  // string | the device ID

            try
            {
                // Delete a Device.
                Object result = apiInstance.DeleteDevice(authorization, deviceId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DevicesApi.DeleteDevice: " + e.Message );
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
 **deviceId** | **string**| the device ID | 

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
| **200** | Device deleted. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="executedevicecommands"></a>
# **ExecuteDeviceCommands**
> Object ExecuteDeviceCommands (string authorization, string deviceId, DeviceCommandsRequest executeCapabilityCommand)

Execute commands on device.

Execute commands on a device.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ExecuteDeviceCommandsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new DevicesApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var deviceId = deviceId_example;  // string | the device ID
            var executeCapabilityCommand = new DeviceCommandsRequest(); // DeviceCommandsRequest | 

            try
            {
                // Execute commands on device.
                Object result = apiInstance.ExecuteDeviceCommands(authorization, deviceId, executeCapabilityCommand);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DevicesApi.ExecuteDeviceCommands: " + e.Message );
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
 **deviceId** | **string**| the device ID | 
 **executeCapabilityCommand** | [**DeviceCommandsRequest**](DeviceCommandsRequest.md)|  | 

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
| **200** | Created commands. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **422** | Unprocessable Entity |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getdevice"></a>
# **GetDevice**
> Device GetDevice (string authorization, string deviceId)

Get a device's description.

Get a device's description.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetDeviceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new DevicesApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var deviceId = deviceId_example;  // string | the device ID

            try
            {
                // Get a device's description.
                Device result = apiInstance.GetDevice(authorization, deviceId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DevicesApi.GetDevice: " + e.Message );
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
 **deviceId** | **string**| the device ID | 

### Return type

[**Device**](Device.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A Device |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getdevicecomponentstatus"></a>
# **GetDeviceComponentStatus**
> Dictionary&lt;string, Dictionary&gt; GetDeviceComponentStatus (string authorization, string deviceId, string componentId)

Get a device component's status.

Get the status of all attributes of a the component. The results may be filtered if the requester only has permission to view a subset of the component's capabilities. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetDeviceComponentStatusExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new DevicesApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var deviceId = deviceId_example;  // string | the device ID
            var componentId = componentId_example;  // string | The name of the component.

            try
            {
                // Get a device component's status.
                Dictionary<string, Dictionary> result = apiInstance.GetDeviceComponentStatus(authorization, deviceId, componentId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DevicesApi.GetDeviceComponentStatus: " + e.Message );
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
 **deviceId** | **string**| the device ID | 
 **componentId** | **string**| The name of the component. | 

### Return type

**Dictionary<string, Dictionary>**

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successful return  current status of device component&#39;s attributes. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getdevicestatus"></a>
# **GetDeviceStatus**
> DeviceStatus GetDeviceStatus (string authorization, string deviceId)

Get the full status of a device.

Get the current status of all of a device's component's attributes. The results may be filtered if the requester only has permission to view a subset of the device's components or capabilities. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetDeviceStatusExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new DevicesApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var deviceId = deviceId_example;  // string | the device ID

            try
            {
                // Get the full status of a device.
                DeviceStatus result = apiInstance.GetDeviceStatus(authorization, deviceId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DevicesApi.GetDeviceStatus: " + e.Message );
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
 **deviceId** | **string**| the device ID | 

### Return type

[**DeviceStatus**](DeviceStatus.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful return of current status of device attributes |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getdevicestatusbycapability"></a>
# **GetDeviceStatusByCapability**
> Dictionary&lt;string, AttributeState&gt; GetDeviceStatusByCapability (string authorization, string deviceId, string componentId, string capabilityId)

Get a capability's status.

Get the current status of a device component's capability. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetDeviceStatusByCapabilityExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new DevicesApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var deviceId = deviceId_example;  // string | the device ID
            var componentId = componentId_example;  // string | The name of the component.
            var capabilityId = capabilityId_example;  // string | The ID of the capability

            try
            {
                // Get a capability's status.
                Dictionary<string, AttributeState> result = apiInstance.GetDeviceStatusByCapability(authorization, deviceId, componentId, capabilityId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DevicesApi.GetDeviceStatusByCapability: " + e.Message );
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
 **deviceId** | **string**| the device ID | 
 **componentId** | **string**| The name of the component. | 
 **capabilityId** | **string**| The ID of the capability | 

### Return type

[**Dictionary&lt;string, AttributeState&gt;**](AttributeState.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successful return of current status of the attributes of a device component&#39;s capability |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getdevices"></a>
# **GetDevices**
> PagedDevices GetDevices (string authorization, List<string> capability = null, List<string> locationId = null, List<string> deviceId = null, string capabilitiesMode = null)

List devices.

Get a list of devices.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetDevicesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new DevicesApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var capability = new List<string>(); // List<string> | The device capabilities to filter the results by. The capabilities are treated as an \"and\" so all capabilities must be present.  (optional) 
            var locationId = new List<string>(); // List<string> | The device locations to filter the results by.  (optional) 
            var deviceId = new List<string>(); // List<string> | The device ids to filter the results by.  (optional) 
            var capabilitiesMode = capabilitiesMode_example;  // string | Treat all capability filter query params as a logical \"or\" or \"and\" with a default of \"and\".  (optional)  (default to and)

            try
            {
                // List devices.
                PagedDevices result = apiInstance.GetDevices(authorization, capability, locationId, deviceId, capabilitiesMode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DevicesApi.GetDevices: " + e.Message );
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
 **capability** | [**List&lt;string&gt;**](string.md)| The device capabilities to filter the results by. The capabilities are treated as an \&quot;and\&quot; so all capabilities must be present.  | [optional] 
 **locationId** | [**List&lt;string&gt;**](string.md)| The device locations to filter the results by.  | [optional] 
 **deviceId** | [**List&lt;string&gt;**](string.md)| The device ids to filter the results by.  | [optional] 
 **capabilitiesMode** | **string**| Treat all capability filter query params as a logical \&quot;or\&quot; or \&quot;and\&quot; with a default of \&quot;and\&quot;.  | [optional] [default to and]

### Return type

[**PagedDevices**](PagedDevices.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | A list of devices. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="installdevice"></a>
# **InstallDevice**
> Device InstallDevice (string authorization, DeviceInstallRequest installationRequest)

Install a Device.

Install a device. This is only available for SmartApp managed devices. The SmartApp that creates the device is responsible for handling commands for the device and updating the status of the device by creating events. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class InstallDeviceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new DevicesApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var installationRequest = new DeviceInstallRequest(); // DeviceInstallRequest | Installation Request

            try
            {
                // Install a Device.
                Device result = apiInstance.InstallDevice(authorization, installationRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DevicesApi.InstallDevice: " + e.Message );
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
 **installationRequest** | [**DeviceInstallRequest**](DeviceInstallRequest.md)| Installation Request | 

### Return type

[**Device**](Device.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Device Installed. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **422** | Unprocessable Entity |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updatedevice"></a>
# **UpdateDevice**
> Device UpdateDevice (string authorization, string deviceId, UpdateDeviceRequest updateDeviceRequest)

Update a device.

Update the properties of a device. If the token is for a SmartApp that created the device then it implicitly has permission for this api. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateDeviceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.smartthings.com/v1";
            // Configure OAuth2 access token for authorization: Bearer
            config.AccessToken = "YOUR_ACCESS_TOKEN";

            var apiInstance = new DevicesApi(config);
            var authorization = authorization_example;  // string | OAuth token
            var deviceId = deviceId_example;  // string | the device ID
            var updateDeviceRequest = new UpdateDeviceRequest(); // UpdateDeviceRequest | 

            try
            {
                // Update a device.
                Device result = apiInstance.UpdateDevice(authorization, deviceId, updateDeviceRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DevicesApi.UpdateDevice: " + e.Message );
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
 **deviceId** | **string**| the device ID | 
 **updateDeviceRequest** | [**UpdateDeviceRequest**](UpdateDeviceRequest.md)|  | 

### Return type

[**Device**](Device.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Updated Device. |  -  |
| **400** | Bad request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **422** | Unprocessable Entity |  -  |
| **429** | Too many requests |  -  |
| **0** | Unexpected error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

